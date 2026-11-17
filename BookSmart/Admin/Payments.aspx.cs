using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BookSmart
{
    public partial class Payments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == null || !Session["role"].ToString().Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
                LoadPayments();
        }

        private void LoadPayments()
        {
            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT o.Order_ID, 
                           (u.First_name + ' ' + u.Last_name) AS CustomerName, 
                           o.Payment_Method, 
                           o.total_amount, 
                           o.Order_Date
                    FROM Orders o
                    JOIN Users u ON o.User_ID = u.User_ID
                    ORDER BY o.Order_Date DESC", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPayments.DataSource = dt;
                gvPayments.DataBind();
            }
        }
    }
}
