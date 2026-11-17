using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookSmart
{
    public partial class MyOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["UserId"] != null)
                LoadOrders();
        }

        private void LoadOrders()
        {
            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT o.order_id, o.order_date, o.total_amount, b.Title, oi.quantity, oi.Price " +
                    "FROM Orders o " +
                    "JOIN Order_Item oi ON o.order_id = oi.order_id " +
                    "JOIN Book b ON oi.Book_id = b.Book_id " +
                    "WHERE o.User_id=@uid", conn);
                cmd.Parameters.AddWithValue("@uid", (int)Session["UserId"]);
                conn.Open();
                gvOrders.DataSource = cmd.ExecuteReader();
                gvOrders.DataBind();
            }
        }
    }
}