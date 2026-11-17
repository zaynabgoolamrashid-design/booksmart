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
    public partial class ManageAdmin : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString().ToLower() != "admin")
                Response.Redirect("Login.aspx");

            if (!IsPostBack) BindGrid();
        }

        void BindGrid()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users WHERE Role='Admin'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvAdmins.DataSource = dt;
                gvAdmins.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "INSERT INTO Users (First_name, Last_name, Email, Password, Role) VALUES (@fn, @ln, @em, @pw, 'Admin')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fn", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@ln", txtLastName.Text);
                cmd.Parameters.AddWithValue("@em", txtEmail.Text);
                cmd.Parameters.AddWithValue("@pw", txtPassword.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            BindGrid();
        }

        protected void gvAdmins_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvAdmins.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvAdmins_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvAdmins.EditIndex = -1;
            BindGrid();
        }

        protected void gvAdmins_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvAdmins.DataKeys[e.RowIndex].Value);
            string first = ((System.Web.UI.WebControls.TextBox)gvAdmins.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string last = ((System.Web.UI.WebControls.TextBox)gvAdmins.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string email = ((System.Web.UI.WebControls.TextBox)gvAdmins.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string pass = ((System.Web.UI.WebControls.TextBox)gvAdmins.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "UPDATE Users SET First_name=@fn, Last_name=@ln, Email=@em, Password=@pw WHERE User_id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fn", first);
                cmd.Parameters.AddWithValue("@ln", last);
                cmd.Parameters.AddWithValue("@em", email);
                cmd.Parameters.AddWithValue("@pw", pass);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            gvAdmins.EditIndex = -1;
            BindGrid();
        }

        protected void gvAdmins_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvAdmins.DataKeys[e.RowIndex].Value);
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE User_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            BindGrid();
        }
    }
}