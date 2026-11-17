<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="BookSmart.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
  <h2>Reports</h2>

  <div class="row g-2 mb-3">
    <div class="col-md-3"><asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" placeholder="From (yyyy-mm-dd)" /></div>
    <div class="col-md-3"><asp:TextBox ID="txtTo" runat="server" CssClass="form-control" placeholder="To (yyyy-mm-dd)" /></div>
    <div class="col-md-2"><asp:Button ID="btnFilter" runat="server" CssClass="btn btn-secondary w-100" Text="Filter" OnClick="btnFilter_Click" /></div>
    <div class="col-md-2"><asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary w-100" Text="Export PDF" OnClick="btnExport_Click" /></div>
  </div>
 <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-2"></asp:Label>

  <asp:GridView ID="gvReport" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="False">
     
    <Columns>
      <asp:BoundField DataField="OrderID" HeaderText="Order#" />
      <asp:BoundField DataField="OrderDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
      <asp:BoundField DataField="Customer" HeaderText="Customer" />
      <asp:BoundField DataField="Items" HeaderText="Items" />
      <asp:BoundField DataField="Total" HeaderText="Total (Rs)" DataFormatString="{0:N2}" />
      <asp:BoundField DataField="PaymentStatus" HeaderText="Payment" />
    </Columns>
  </asp:GridView>

  <div class="mt-3">
    <strong>Total Orders:</strong> <asp:Label ID="lblCount" runat="server" /> &nbsp; | &nbsp;
    <strong>Total Revenue (Rs):</strong> <asp:Label ID="lblRevenue" runat="server" />
  </div>
</div>
</asp:Content>
