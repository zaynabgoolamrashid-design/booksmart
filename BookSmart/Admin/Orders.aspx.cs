using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace BookSmart
{
    public partial class Orders : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || !Session["role"].ToString().Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
                LoadOrders();
        }

        private void LoadOrders()
        {
            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT o.Order_ID, 
                           (u.First_name + ' ' + u.Last_name) AS UserName, 
                           o.Order_Date, o.Payment_Method, o.Status, o.total_amount
                    FROM Orders o
                    JOIN Users u ON o.User_ID = u.User_ID
                    ORDER BY o.Order_Date DESC", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        protected void gvOrders_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewOrder")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                Session["LastOrderID"] = orderId;
                Response.Redirect("~/OrderConfirmation.aspx");
            }
        }
    }
}
