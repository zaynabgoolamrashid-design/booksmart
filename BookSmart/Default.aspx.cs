using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookSmart
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = Request.QueryString["search"];


                if (!string.IsNullOrEmpty(keyword))
                {
                    SearchBooks(keyword);
                }
                else
                {
                    BindNewArrivals();
                }
            }
        }

        private void SearchBooks(string keyword)
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = @"
            SELECT b.Book_id, b.title, b.author_id, b.price, b.cover_image_path
            FROM Book b
            LEFT JOIN Category c ON b.category_id = c.category_id
            WHERE 
                b.title LIKE @keyword 
                OR b.author_id LIKE @keyword 
                OR c.category_name LIKE @keyword";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptNewArrivals.DataSource = dt;
                    rptNewArrivals.DataBind();
                }
            }
        }

        private void BindNewArrivals()
        {
            string strcon = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT TOP 8 * FROM Book ORDER BY added_at DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptNewArrivals.DataSource = dt;
                    rptNewArrivals.DataBind();
                }
            }
        }
        protected void btnAddToCart_Command(object sender, CommandEventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            int bookId = Convert.ToInt32(e.CommandArgument);

            // You can fetch book details from DB here if you want full info
            string title = "";
            decimal price = 0;

            string strcon = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("SELECT Title, Price FROM Book WHERE Book_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", bookId);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    title = rdr["Title"].ToString();
                    price = Convert.ToDecimal(rdr["Price"]);
                }
            }

            // Use shared method
            CartHelper.AddToCart(bookId, title, price);

            Response.Redirect("Cart.aspx");
        }


    }
}
