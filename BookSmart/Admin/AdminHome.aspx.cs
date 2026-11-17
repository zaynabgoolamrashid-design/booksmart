using System;
using System.Web;
using System.Web.UI;

namespace BookSmart
{
    public partial class AdminHome : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoServerCaching();

            if (!IsPostBack)
            {
                if (Session["role"] == null || !Session["role"].ToString().Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
}
