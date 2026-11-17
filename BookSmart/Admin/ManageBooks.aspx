<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageBooks.aspx.cs" Inherits="BookSmart.ManageBooks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
  <h2 class="mb-3">Manage Books</h2>

  <div class="row g-2 mb-3">
    <div class="col-md-4">
      <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by title..." />
    </div>
    <div class="col-md-2">
      <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-secondary w-100" OnClick="btnSearch_Click" />
    </div>
    <div class="col-md-2">
      <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-secondary w-100" OnClick="btnClear_Click" />
    </div>
  </div>

  <asp:GridView ID="gvBooks" runat="server" CssClass="table table-striped table-bordered"
      AutoGenerateColumns="False" DataKeyNames="Book_id" AllowPaging="True" PageSize="10"
      OnPageIndexChanging="gvBooks_PageIndexChanging"
      OnRowEditing="gvBooks_RowEditing" OnRowCancelingEdit="gvBooks_RowCancelingEdit"
      OnRowUpdating="gvBooks_RowUpdating" OnRowDeleting="gvBooks_RowDeleting">
    <Columns>
      <asp:BoundField DataField="Book_id" HeaderText="ID" ReadOnly="True" />
      <asp:BoundField DataField="Title" HeaderText="Title" />
      <asp:BoundField DataField="price" HeaderText="Price" DataFormatString="{0:N2}" />
      <asp:BoundField DataField="stock" HeaderText="Stock" />
      <asp:BoundField DataField="published_year" HeaderText="Year" />
      <asp:BoundField DataField="cover_image_path" HeaderText="Cover" />
      <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
  </asp:GridView>

  <h5 class="mt-4">Add New Book</h5>
  <div class="row g-2">
    <div class="col-md-4"><asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Title" /></div>
    <div class="col-md-2"><asp:DropDownList ID="ddlAuthor" runat="server" CssClass="form-select"></asp:DropDownList></div>
    <div class="col-md-2"><asp:DropDownList ID="ddlPublisher" runat="server" CssClass="form-select"></asp:DropDownList></div>
    <div class="col-md-2"><asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select"></asp:DropDownList></div>
    <div class="col-md-2"><asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Placeholder="Price" /></div>
  </div>
  <div class="row g-2 mt-2">
    <div class="col-md-2"><asp:TextBox ID="txtStock" runat="server" CssClass="form-control" Placeholder="Stock" /></div>
    <div class="col-md-2"><asp:TextBox ID="txtYear" runat="server" CssClass="form-control" Placeholder="Published year" /></div>
    <div class="col-md-5"><asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" Placeholder="Description" /></div>
    <div class="col-md-2"><asp:FileUpload ID="fuCover" runat="server" CssClass="form-control" /></div>
    <div class="col-md-1"><asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary w-100" OnClick="btnAdd_Click" /></div>
  </div>

  <asp:Label ID="lblInfo" runat="server" CssClass="text-success mt-3 d-block"></asp:Label>
</div>
</asp:Content>
