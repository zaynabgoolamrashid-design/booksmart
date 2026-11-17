using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BookSmart
{
    public partial class login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT User_ID, First_name, Last_name, Role FROM Users WHERE Email=@Email AND Password=@Password", conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    Session["UserId"] = rdr["User_ID"];
                    Session["UserName"] = rdr["First_name"].ToString() + " " + rdr["Last_name"].ToString();
                    Session["role"] = rdr["Role"].ToString();

                   
                    // Redirect logic
                    string returnUrl = Request.QueryString["returnUrl"];
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        Response.Redirect(returnUrl);
                    }
                    else
                    {
                        if (Session["role"] != null && Session["role"].ToString().Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            Response.Redirect("~/Admin/AdminHome.aspx"); // Go straight to Admin Dashboard
                        }
                        else
                        {
                            Response.Redirect("Default.aspx"); // Regular users
                        }
                    }
                }


            }
        }
    }
}

