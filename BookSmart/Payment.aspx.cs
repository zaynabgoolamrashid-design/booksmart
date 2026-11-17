using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace BookSmart
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if cart exists
                if (Session["Cart"] == null || ((DataTable)Session["Cart"]).Rows.Count == 0)
                {
                    Session["ErrorMessage"] = "⚠️ Your shopping session has expired. Please add items to your cart before proceeding to payment.";
                    Response.Redirect("Cart.aspx");
                    return;
                }

                // Show total
                DataTable cart = (DataTable)Session["Cart"];
                decimal total = 0;
                foreach (DataRow row in cart.Rows)
                    total += Convert.ToDecimal(row["Price"]) * Convert.ToInt32(row["Quantity"]);

                lblTotalAmount.Text = "Total Amount: " + total.ToString("C");
            }
        }

        protected void rblPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            cardDetails.Visible = (rblPaymentMethod.SelectedValue == "CC");
        }

        protected void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            // Validate cart again
            if (Session["Cart"] == null || ((DataTable)Session["Cart"]).Rows.Count == 0)
            {
                lblMessage.Text = "⚠️ Your shopping session has expired. Please add items to your cart before proceeding to payment.";
                lblMessage.Visible = true;
                return;
            }

            // Validate credit card fields if chosen
            if (rblPaymentMethod.SelectedValue == "CC")
            {
                if (string.IsNullOrWhiteSpace(txtCardNumber.Text) ||
                    string.IsNullOrWhiteSpace(txtExpiry.Text) ||
                    string.IsNullOrWhiteSpace(txtCVV.Text))
                {
                    lblMessage.Text = "⚠️ Please fill in all credit card fields.";
                    lblMessage.Visible = true;
                    return;
                }
            }

            try
            {
                int orderId = SaveOrder(rblPaymentMethod.SelectedValue);
                Session["LastOrderID"] = orderId;
                Session["Cart"] = null; // clear after saving
                Response.Redirect("OrderConfirmation.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error placing order: " + ex.Message;
                lblMessage.Visible = true;
            }
        }

        private int SaveOrder(string paymentMethod)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            DataTable cart = (DataTable)Session["Cart"];

            // Calculate total
            decimal totalAmount = 0;
            foreach (DataRow row in cart.Rows)
                totalAmount += Convert.ToDecimal(row["Price"]) * Convert.ToInt32(row["Quantity"]);

            string strcon = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert order
                    string orderSql = @"INSERT INTO Orders (User_ID, Order_Date, Payment_Method, Status, total_amount)
                                        OUTPUT INSERTED.Order_ID
                                        VALUES (@UserID, GETDATE(), @PaymentMethod, 'Pending', @TotalAmount)";
                    SqlCommand orderCmd = new SqlCommand(orderSql, conn, transaction);
                    orderCmd.Parameters.AddWithValue("@UserID", userId);
                    orderCmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    orderCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    int orderId = (int)orderCmd.ExecuteScalar();

                    // Insert order items
                    foreach (DataRow row in cart.Rows)
                    {
                        string itemSql = @"INSERT INTO Order_Item (Order_ID, Book_ID, Quantity, Price)
                                           VALUES (@OrderID, @BookID, @Qty, @Price)";
                        SqlCommand itemCmd = new SqlCommand(itemSql, conn, transaction);
                        itemCmd.Parameters.AddWithValue("@OrderID", orderId);
                        itemCmd.Parameters.AddWithValue("@BookID", row["Book_ID"]);
                        itemCmd.Parameters.AddWithValue("@Qty", row["Quantity"]);
                        itemCmd.Parameters.AddWithValue("@Price", row["Price"]);
                        itemCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return orderId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
