using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookSmart
{
    public partial class category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string genre = Request.QueryString["genre"];
                if (!string.IsNullOrEmpty(genre))
                {
                    LoadCategoryByName(genre);
                }
            }
        }

        private void LoadCategoryByName(string genre)
        {
            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT category_id, category_name FROM Category WHERE category_name = @genre";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@genre", genre);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int catId = Convert.ToInt32(reader["category_id"]);
                    lblCategoryName.Text = reader["category_name"].ToString();
                    LoadBooks(catId);
                }
                else
                {
                    lblCategoryName.Text = "Category Not Found";
                }
            }
        }

        private void LoadBooks(int Category_id)
        {
            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT Book_id, Title, Price, Cover_Image_path FROM Book WHERE Category_id = @Category_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Category_id", Category_id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                rptBooks.DataSource = reader;
                rptBooks.DataBind();
            }
        }
        protected void btnAddToCart_Command(object sender, CommandEventArgs e)
        {
            int bookId = Convert.ToInt32(e.CommandArgument);

            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT Title, Price FROM Book WHERE Book_id = @Book_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Book_id", bookId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string title = reader["Title"].ToString();
                    decimal price = Convert.ToDecimal(reader["Price"]);

                    AddToCart(bookId, title, price);
                }
            }

            // Redirect to Cart page after adding
            Response.Redirect("Cart.aspx");
        }

        private void AddToCart(int bookId, string title, decimal price)
        {
            DataTable cart = Session["Cart"] as DataTable;

            if (cart == null)
            {
                cart = new DataTable();
                cart.Columns.Add("Book_id", typeof(int));
                cart.Columns.Add("Title", typeof(string));
                cart.Columns.Add("Price", typeof(decimal));
                cart.Columns.Add("Quantity", typeof(int));
            }

            DataRow existingRow = cart.AsEnumerable()
                .FirstOrDefault(row => (int)row["Book_id"] == bookId);

            if (existingRow != null)
            {
                existingRow["Quantity"] = (int)existingRow["Quantity"] + 1;
            }
            else
            {
                DataRow newRow = cart.NewRow();
                newRow["Book_id"] = bookId;
                newRow["Title"] = title;
                newRow["Price"] = price;
                newRow["Quantity"] = 1;
                cart.Rows.Add(newRow);
            }

            Session["Cart"] = cart;
        }
    }
}