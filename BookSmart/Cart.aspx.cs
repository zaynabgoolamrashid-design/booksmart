using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookSmart
{
    public partial class Cart : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadCart();
        }

        private void LoadCart()
        {
            DataTable cart = GetCartFromSession();

            if (cart == null || cart.Rows.Count == 0)
            {
                lblMessage.Text = "Your cart is empty.";
                gvCart.DataSource = null;
                gvCart.DataBind();
                lblTotal.Text = "0.00";
                return;
            }

            lblMessage.Text = "";
            gvCart.DataSource = cart;
            gvCart.DataBind();

            decimal total = cart.AsEnumerable()
                .Sum(row => (decimal)row["Price"] * (int)row["Quantity"]);

            lblTotal.Text = total.ToString("0.00");
        }

        private DataTable GetCartFromSession()
        {
            if (Session["Cart"] == null)
            {
                // Create new DataTable with columns matching GridView
                DataTable cart = new DataTable();
                cart.Columns.Add("Book_id", typeof(int));
                cart.Columns.Add("Title", typeof(string));
                cart.Columns.Add("Price", typeof(decimal));
                cart.Columns.Add("Quantity", typeof(int));

                Session["Cart"] = cart;
            }
            return Session["Cart"] as DataTable;
        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveItem")
            {
                int bookId = Convert.ToInt32(e.CommandArgument);

                DataTable cart = GetCartFromSession();

                DataRow rowToRemove = null;
                foreach (DataRow row in cart.Rows)
                {
                    if ((int)row["Book_id"] == bookId)
                    {
                        rowToRemove = row;
                        break;
                    }
                }

                if (rowToRemove != null)
                {
                    cart.Rows.Remove(rowToRemove);
                    Session["Cart"] = cart;
                    LoadCart();
                }
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                // Redirect to login with return URL back to cart
                Response.Redirect("login.aspx?returnUrl=" + Server.UrlEncode(Request.RawUrl));
                return;
            }

            Response.Redirect("Checkout.aspx");
        }

    }
}

