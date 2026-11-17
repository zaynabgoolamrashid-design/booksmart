using System;
using System.Data;

namespace BookSmart
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadCart();
        }

        private void LoadCart()
        {
            if (Session["Cart"] != null)
            {
                DataTable cart = (DataTable)Session["Cart"];
                if (cart.Rows.Count > 0)
                {
                    gvCart.DataSource = cart;
                    gvCart.DataBind();

                    decimal total = 0;
                    foreach (DataRow row in cart.Rows)
                        total += Convert.ToDecimal(row["Price"]) * Convert.ToInt32(row["Quantity"]);

                    lblTotal.Text = total.ToString("C");

                    pnlCart.Visible = true;
                    pnlEmpty.Visible = false;
                }
                else
                {
                    pnlCart.Visible = false;
                    pnlEmpty.Visible = true;
                }
            }
            else
            {
                pnlCart.Visible = false;
                pnlEmpty.Visible = true;
            }
        }

        protected void btnProceedPayment_Click(object sender, EventArgs e)
        {
            if (Session["Cart"] == null || ((DataTable)Session["Cart"]).Rows.Count == 0)
            {
                pnlCart.Visible = false;
                pnlEmpty.Visible = true;
                return;
            }

            Response.Redirect("Payment.aspx");
        }
    }
}
