using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BookSmart
{
    public partial class OrderHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            if (!IsPostBack)
                LoadOrderHistory();
        }

        private void LoadOrderHistory()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            string strcon = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT Order_ID, Order_Date, Payment_Method, Status, total_amount
                                                         FROM Orders
                                                         WHERE User_ID = @UserID
                                                         ORDER BY Order_Date DESC", conn);
                da.SelectCommand.Parameters.AddWithValue("@UserID", userId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    gvOrders.DataSource = dt;
                    gvOrders.DataBind();
                    pnlNoOrders.Visible = false;
                }
                else
                {
                    pnlNoOrders.Visible = true;
                }
            }
        }

        protected void gvOrders_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewOrder")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                Session["LastOrderID"] = orderId;
                Response.Redirect("OrderConfirmation.aspx");
            }
        }
    }
}
