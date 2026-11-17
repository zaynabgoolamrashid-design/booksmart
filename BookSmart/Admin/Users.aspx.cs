using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BookSmart
{
    public partial class Users : Page
    {

        string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["Role"] ?? "").ToString().ToLower() != "admin") Response.Redirect("Login.aspx");
            if (!IsPostBack) BindGrid();
        }

        void BindGrid()
        {
            using (var conn = new SqlConnection(connStr))
            using (var da = new SqlDataAdapter("SELECT * FROM Users WHERE Role='customer' ORDER BY User_id DESC", conn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(@"INSERT INTO Users(First_name,Last_name,Email,Password,Role)
                                              VALUES(@fn,@ln,@em,@pw,'customer')", conn))
            {
                cmd.Parameters.AddWithValue("@fn", txtFN.Text.Trim());
                cmd.Parameters.AddWithValue("@ln", txtLN.Text.Trim());
                cmd.Parameters.AddWithValue("@em", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@pw", txtPwd.Text.Trim());
                conn.Open(); cmd.ExecuteNonQuery();
            }
            txtFN.Text = txtLN.Text = txtEmail.Text = txtPwd.Text = "";
            lblMsg.Text = "Customer added.";
            BindGrid();
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex; BindGrid();
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1; BindGrid();
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = (int)gvUsers.DataKeys[e.RowIndex].Value;
            string first = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[1].Controls[0]).Text.Trim();
            string last = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[2].Controls[0]).Text.Trim();
            string email = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[3].Controls[0]).Text.Trim();
            string pass = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[4].Controls[0]).Text.Trim();

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(@"UPDATE Users SET First_name=@fn, Last_name=@ln, Email=@em, Password=@pw
                                              WHERE User_id=@id AND Role='customer'", conn))
            {
                cmd.Parameters.AddWithValue("@fn", first);
                cmd.Parameters.AddWithValue("@ln", last);
                cmd.Parameters.AddWithValue("@em", email);
                cmd.Parameters.AddWithValue("@pw", pass);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open(); cmd.ExecuteNonQuery();
            }
            gvUsers.EditIndex = -1; BindGrid();
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)gvUsers.DataKeys[e.RowIndex].Value;
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand("DELETE FROM Users WHERE User_id=@id AND Role='customer'", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open(); cmd.ExecuteNonQuery();
            }
            BindGrid();
        }
    }
}