using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BookSmart
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (Session["UserName"] != null)
                {
                    lblUsername.Text = Session["UserName"].ToString();
                    phLoggedIn.Visible = true;
                    phNotLoggedIn.Visible = false;

                    if (Session["role"] != null)
                    {
                        string Role = Session["role"].ToString();

                        // Show links depending on role
                        if (Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            phAdminLinks.Visible = true;
                        }
                        else if (Role.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                        {
                            phCustomerLinks.Visible = true;
                        }
                    }
                }
                else
                {
                    // Not logged in
                    phLoggedIn.Visible = false;
                    phNotLoggedIn.Visible = true;
                    phAdminLinks.Visible = false;
                    phCustomerLinks.Visible = false;
                }
            }
        }

        public TextBox SearchTextBox
        {
            get { return txtSearch; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                Response.Redirect("Default.aspx?search=" + Server.UrlEncode(keyword));
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}