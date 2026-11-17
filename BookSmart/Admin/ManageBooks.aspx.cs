using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookSmart
{
    public partial class ManageBooks : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["Role"] ?? "").ToString().ToLower() != "admin")
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                try
                {
                    BindLookups();
                }
                catch (Exception ex)
                {
                    lblInfo.CssClass = "text-danger";
                    lblInfo.Text = "Lookup load error: " + ex.Message;
                }
                BindGrid();
            }
        }

        void BindLookups()
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                new SqlDataAdapter("SELECT Author_id, Author_name FROM Author ORDER BY Author_name", conn)
                    .FillAndBind(ddlAuthor, "Author_name", "Author_id");

                new SqlDataAdapter("SELECT Publisher_id, publisher_name FROM Publisher ORDER BY publisher_name", conn)
                    .FillAndBind(ddlPublisher, "publisher_name", "Publisher_id");

                new SqlDataAdapter("SELECT Category_id, category_name FROM Category ORDER BY category_name", conn)
                    .FillAndBind(ddlCategory, "category_name", "Category_id");
            }
        }

        void BindGrid(string term = "")
        {
            try
            {
                using (var conn = new SqlConnection(connStr))
                using (var da = new SqlDataAdapter(@"
                    SELECT Book_id, Title, price, stock, published_year, cover_image_path
                    FROM Book
                    WHERE (@t = '' OR Title LIKE '%' + @t + '%')
                    ORDER BY Book_id DESC", conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@t", term ?? "");
                    var dt = new DataTable();
                    da.Fill(dt);
                    gvBooks.DataSource = dt;
                    gvBooks.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblInfo.CssClass = "text-danger";
                lblInfo.Text = "Grid load error: " + ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e) => BindGrid(txtSearch.Text.Trim());

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            BindGrid();
        }

        protected void gvBooks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBooks.PageIndex = e.NewPageIndex;
            BindGrid(txtSearch.Text.Trim());
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblInfo.CssClass = "text-success";
            lblInfo.Text = "";

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal priceVal))
            {
                lblInfo.CssClass = "text-danger";
                lblInfo.Text = "Invalid price.";
                return;
            }
            if (!int.TryParse(txtStock.Text.Trim(), out int stockVal))
            {
                lblInfo.CssClass = "text-danger";
                lblInfo.Text = "Invalid stock.";
                return;
            }
            if (!int.TryParse(txtYear.Text.Trim(), out int yearVal))
            {
                lblInfo.CssClass = "text-danger";
                lblInfo.Text = "Invalid published year.";
                return;
            }

            string imagePath = "";
            try
            {
                if (fuCover.HasFile)
                {
                    string folder = Server.MapPath("~/Images/Books/");
                    Directory.CreateDirectory(folder);
                    string fileName = DateTime.Now.Ticks + Path.GetExtension(fuCover.FileName);
                    fuCover.SaveAs(Path.Combine(folder, fileName));
                    imagePath = "~/Images/Books/" + fileName;
                }

                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(@"
                    INSERT INTO Book
                        ([Title], [Author_id], [publisher_id], [Category_id],
                         [price], [stock], [published_year], [description], [cover_image_path], [added_at])
                    VALUES
                        (@t, @a, @p, @c, @pr, @st, @yr, @d, @img, GETDATE())", conn))
                {
                    cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@a", ddlAuthor.SelectedValue);
                    cmd.Parameters.AddWithValue("@p", ddlPublisher.SelectedValue);
                    cmd.Parameters.AddWithValue("@c", ddlCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@pr", priceVal);
                    cmd.Parameters.AddWithValue("@st", stockVal);
                    cmd.Parameters.AddWithValue("@yr", yearVal);
                    cmd.Parameters.AddWithValue("@d", txtDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@img", imagePath);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                txtTitle.Text = txtPrice.Text = txtStock.Text = txtYear.Text = txtDesc.Text = "";
                lblInfo.CssClass = "text-success";
                lblInfo.Text = "Book added.";
                BindGrid();
            }
            catch (Exception ex)
            {
                lblInfo.CssClass = "text-danger";
                lblInfo.Text = "Add failed: " + ex.Message;
            }
        }

        protected void gvBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBooks.EditIndex = e.NewEditIndex;
            BindGrid(txtSearch.Text.Trim());
        }

        protected void gvBooks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBooks.EditIndex = -1;
            BindGrid(txtSearch.Text.Trim());
        }

        protected void gvBooks_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblInfo.Text = "";

            int id = (int)gvBooks.DataKeys[e.RowIndex].Value;
            string title = ((TextBox)gvBooks.Rows[e.RowIndex].Cells[1].Controls[0]).Text.Trim();
            string priceStr = ((TextBox)gvBooks.Rows[e.RowIndex].Cells[2].Controls[0]).Text.Trim();
            string stockStr = ((TextBox)gvBooks.Rows[e.RowIndex].Cells[3].Controls[0]).Text.Trim();
            string yearStr = ((TextBox)gvBooks.Rows[e.RowIndex].Cells[4].Controls[0]).Text.Trim();
            string cover = ((TextBox)gvBooks.Rows[e.RowIndex].Cells[5].Controls[0]).Text.Trim();

            if (!decimal.TryParse(priceStr, out decimal priceVal) ||
                !int.TryParse(stockStr, out int stockVal) ||
                !int.TryParse(yearStr, out int yearVal))
            {
                lblInfo.CssClass = "text-danger";
                lblInfo.Text = "Invalid values (price/stock/year).";
                return;
            }

            try
            {
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(@"
                    UPDATE Book
                       SET Title=@t, price=@pr, stock=@st, published_year=@yr, cover_image_path=@img
                     WHERE Book_id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@t", title);
                    cmd.Parameters.AddWithValue("@pr", priceVal);
                    cmd.Parameters.AddWithValue("@st", stockVal);
                    cmd.Parameters.AddWithValue("@yr", yearVal);
                    cmd.Parameters.AddWithValue("@img", cover);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gvBooks.EditIndex = -1;
                BindGrid(txtSearch.Text.Trim());
                lblInfo.CssClass = "text-success";
                lblInfo.Text = "Book updated.";
            }
            catch (Exception ex)
            {
                lblInfo.CssClass = "text-danger";
                lblInfo.Text = "Update failed: " + ex.Message;
            }
        }

        protected void gvBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = (int)gvBooks.DataKeys[e.RowIndex].Value;
            try
            {
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand("DELETE FROM Book WHERE Book_id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                BindGrid(txtSearch.Text.Trim());
                lblInfo.CssClass = "text-success";
                lblInfo.Text = "Book deleted.";
            }
            catch (Exception ex)
            {
                lblInfo.CssClass = "text-danger";
                lblInfo.Text = "Delete failed: " + ex.Message;
            }
        }
    }

    // Extension method to bind dropdowns
    static class SqlDataAdapterExt
    {
        public static void FillAndBind(this SqlDataAdapter da, ListControl ddl, string textField, string valueField)
        {
            var dt = new DataTable();
            da.Fill(dt);
            ddl.DataSource = dt;
            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
            ddl.DataBind();
        }
    }
}
