using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BookSmart
{
    public partial class BookDetails : System.Web.UI.Page
    {
        protected int bookId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.QueryString["id"], out bookId))
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadBookDetails();
            }
        }

        private void LoadBookDetails()
        {
            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Book WHERE Book_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", bookId);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    lblTitle.Text = rdr["Title"].ToString();
                    lblPrice.Text = rdr["Price"].ToString();
                    lblDescription.Text = rdr["Description"].ToString();
                    imgCover.ImageUrl = rdr["Cover_Image_path"].ToString();
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("login.aspx?returnUrl=" + Server.UrlEncode(Request.RawUrl));
                return;
            }

            lblMessage.Text = "Add to Cart clicked!";
            int quantity = 1;
            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                quantity = 1; // default quantity
            }

            decimal price = 0m;
            decimal.TryParse(lblPrice.Text, out price);

            AddToCart(bookId, lblTitle.Text, price);

            // Update the quantity properly
            DataTable cart = GetCartFromSession();

            foreach (DataRow row in cart.Rows)
            {
                if ((int)row["Book_id"] == bookId)
                {
                    row["Quantity"] = (int)row["Quantity"] + quantity - 1; // because AddToCart adds 1 already
                    break;
                }
            }
            Session["Cart"] = cart;

            lblMessage.Text = "Added to cart!";
        }



        private void AddToCart(int bookId, string title, decimal price)
        {
            DataTable cart = GetCartFromSession();

            DataRow existingRow = null;
            foreach (DataRow row in cart.Rows)
            {
                if ((int)row["Book_id"] == bookId)
                {
                    existingRow = row;
                    break;
                }
            }

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

        private DataTable GetCartFromSession()
        {
            if (Session["Cart"] == null)
            {
                DataTable cart = new DataTable();
                cart.Columns.Add("Book_id", typeof(int));
                cart.Columns.Add("Title", typeof(string));
                cart.Columns.Add("Price", typeof(decimal));
                cart.Columns.Add("Quantity", typeof(int));
                Session["Cart"] = cart;
            }
            return Session["Cart"] as DataTable;
        }
    }
}
