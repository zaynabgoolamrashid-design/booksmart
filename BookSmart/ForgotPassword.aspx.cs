using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookSmart
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim().ToLower();
            if (email == "")
            {
                lblMsg.Text = "Please enter your email.";
                return;
            }

            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = "SELECT Password FROM Users WHERE LOWER(Email)=@em";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@em", email);
                conn.Open();
                object pwdObj = cmd.ExecuteScalar();

                if (pwdObj == null)
                {
                    lblMsg.Text = "Email not found.";
                    return;
                }

                // ==== DEMO ONLY ====
                // In production you'd email a reset link. For coursework we simply show a notice.
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "A reset link has been sent to your email (demo).";
            }
        }
    }
}