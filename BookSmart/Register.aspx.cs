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
    public partial class Register : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Read & validate
            string f = txtFirstName.Text.Trim();
            string l = txtLastName.Text.Trim();
            string em = txtEmail.Text.Trim().ToLower();
            string pw = txtPassword.Text;
            string c = txtConfirm.Text;

            if (f == "" || l == "" || em == "" || pw == "" || c == "")
            { lblMessage.Text = "All fields are required."; return; }

            if (pw != c)
            { lblMessage.Text = "Passwords do not match."; return; }

            // 2. Insert
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"INSERT INTO Users (First_name, Last_name, Email, Password, Role)
                               VALUES (@fn,@ln,@em,@pw,'customer')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fn", f);
                cmd.Parameters.AddWithValue("@ln", l);
                cmd.Parameters.AddWithValue("@em", em);
                cmd.Parameters.AddWithValue("@pw", pw);   // (plain here ‑ add hashing later)

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Response.Redirect("Login.aspx");

                    // Show success message and redirect after 2 seconds
                    lblMessage.Text = "<span style='color:green;'>Account created successfully! Redirecting to login...</span>";
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    // Delay redirect using JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "redirectLogin",
                        "setTimeout(function(){ window.location='Login.aspx'; }, 2000);", true);
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("UNIQUE") || ex.Message.Contains("duplicate"))
                        lblMessage.Text = "Email is already registered.";
                    else
                        lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}
