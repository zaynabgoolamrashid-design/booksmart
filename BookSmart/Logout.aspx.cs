using System;
using System.Web;
using System.Web.UI;

namespace BookSmart
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear session
            Session.Clear();
            Session.Abandon();

            // Prevent browser from caching the last page
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            // Redirect to login page or homepage
            Response.Redirect("~/Login.aspx"); 
        }
    }
}
