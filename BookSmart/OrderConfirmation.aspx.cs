using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BookSmart
{
    public partial class OrderConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadOrderDetails();
        }

        private void LoadOrderDetails()
        {
            if (Session["LastOrderID"] == null)
            {
                pnlOrderDetails.Visible = false;
                pnlNoOrder.Visible = true;
                return;
            }

            int orderId = Convert.ToInt32(Session["LastOrderID"]);
            string strcon = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();

                // Get order info
                SqlCommand cmdOrder = new SqlCommand(@"SELECT Order_ID, Order_Date, Payment_Method, Status, total_amount 
                                                       FROM Orders WHERE Order_ID = @OrderID", conn);
                cmdOrder.Parameters.AddWithValue("@OrderID", orderId);

                SqlDataReader rdr = cmdOrder.ExecuteReader();
                if (rdr.Read())
                {
                    lblOrderID.Text = rdr["Order_ID"].ToString();
                    lblOrderDate.Text = Convert.ToDateTime(rdr["Order_Date"]).ToString("dd MMM yyyy hh:mm tt");
                    lblPaymentMethod.Text = rdr["Payment_Method"].ToString();
                    lblStatus.Text = rdr["Status"].ToString();
                    lblTotal.Text = Convert.ToDecimal(rdr["total_amount"]).ToString("C");
                }
                rdr.Close();

                // Get order items
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT b.Title, oi.Price, oi.Quantity
                                                         FROM Order_Item oi
                                                         JOIN Book b ON oi.Book_ID = b.Book_ID
                                                         WHERE oi.Order_ID = @OrderID", conn);
                da.SelectCommand.Parameters.AddWithValue("@OrderID", orderId);
                DataTable dtItems = new DataTable();
                da.Fill(dtItems);

                gvItems.DataSource = dtItems;
                gvItems.DataBind();

                pnlOrderDetails.Visible = true;
                pnlNoOrder.Visible = false;
            }
        }
    }
}

