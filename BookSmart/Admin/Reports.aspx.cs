using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;

namespace BookSmart
{
    public partial class Reports : System.Web.UI.Page
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        // SQL adjusted to your table/column names
        private const string ReportSql = @"
            SELECT o.order_id AS OrderID, 
                   o.order_date AS OrderDate,
                   (u.First_name + ' ' + u.Last_name) AS Customer,
                   SUM(oi.quantity) AS Items,
                   SUM(oi.quantity * oi.Price) AS Total,
                   o.Status AS PaymentStatus
            FROM Orders o
              INNER JOIN Users u ON u.User_id = o.User_id
              INNER JOIN Order_Item oi ON oi.order_id = o.order_id
            WHERE o.order_date >= @from AND o.order_date < @to
            GROUP BY o.order_id, o.order_date, u.First_name, u.Last_name, o.Status
            ORDER BY o.order_date DESC;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["Role"] ?? "").ToString().ToLower() != "admin")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Default range: last 30 days
                txtFrom.Text = DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd");
                txtTo.Text = DateTime.Today.ToString("yyyy-MM-dd");
                BindGrid();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e) => BindGrid();

        private void BindGrid()
        {
            lblError.Text = ""; // clear previous errors

            var (from, to) = GetDateRange();

            try
            {
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(ReportSql, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@from", SqlDbType.DateTime).Value = from;
                    cmd.Parameters.Add("@to", SqlDbType.DateTime).Value = to;

                    var dt = new DataTable();
                    da.Fill(dt);

                    gvReport.DataSource = dt;
                    gvReport.DataBind();

                    // Totals
                    decimal revenue = 0m;
                    foreach (DataRow r in dt.Rows)
                        revenue += r["Total"] == DBNull.Value ? 0m : Convert.ToDecimal(r["Total"]);

                    lblCount.Text = dt.Rows.Count.ToString();
                    lblRevenue.Text = revenue.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                gvReport.DataSource = null;
                gvReport.DataBind();
                lblCount.Text = "0";
                lblRevenue.Text = "0.00";
            }
        }

        private (DateTime from, DateTime to) GetDateRange()
        {
            DateTime from, to;

            bool okFrom = DateTime.TryParseExact(
                txtFrom.Text?.Trim(),
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out from);

            bool okTo = DateTime.TryParseExact(
                txtTo.Text?.Trim(),
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out to);

            if (!okFrom) from = DateTime.Today.AddDays(-30);
            if (!okTo) to = DateTime.Today;

            // Make 'to' inclusive by adding one day
            to = to.AddDays(1);

            return (from, to);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            // Re-bind to ensure the latest filter
            BindGrid();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            // Render the GridView HTML
            using (var sw = new StringWriter())
            using (var hw = new HtmlTextWriter(sw))
            {
                gvReport.RenderControl(hw);
                string html = $"<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /></head><body>{sw}</body></html>";
                byte[] bytes = Encoding.UTF8.GetBytes(html);
                Response.OutputStream.Write(bytes, 0, bytes.Length);
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Required for exporting server controls
        }
    }
}
