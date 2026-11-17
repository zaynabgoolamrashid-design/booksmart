<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="BookSmart.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">👤 Manage Users</h2>
        <asp:GridView ID="gvUsers" runat="server" CssClass="table table-striped table-bordered"
        AutoGenerateColumns="False" DataKeyNames="User_id"
        AllowPaging="True" PageSize="10" OnPageIndexChanging="gvUsers_PageIndexChanging"
        OnRowEditing="gvUsers_RowEditing" OnRowCancelingEdit="gvUsers_RowCancelingEdit"
        OnRowUpdating="gvUsers_RowUpdating" OnRowDeleting="gvUsers_RowDeleting">
        <Columns>
            <asp:BoundField DataField="User_id" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="First_name" HeaderText="First Name" />
            <asp:BoundField DataField="Last_name" HeaderText="Last Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Password" HeaderText="Password" />
            <asp:BoundField DataField="Role" HeaderText="Role" ReadOnly="True" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
        <PagerStyle CssClass="pagination-outer" />
    </asp:GridView>

    <h5 class="mt-4">Add New Customer</h5>
    <div class="row g-2">
        <div class="col-md-3"><asp:TextBox ID="txtFN" runat="server" CssClass="form-control" Placeholder="First name" /></div>
        <div class="col-md-3"><asp:TextBox ID="txtLN" runat="server" CssClass="form-control" Placeholder="Last name" /></div>
        <div class="col-md-3"><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" /></div>
        <div class="col-md-2"><asp:TextBox ID="txtPwd" runat="server" CssClass="form-control" Placeholder="Password" /></div>
        <div class="col-md-1"><asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary w-100" OnClick="btnAdd_Click" /></div>
    </div>

    <asp:Label ID="lblMsg" runat="server" CssClass="text-success mt-3 d-block"></asp:Label>
</div>
</asp:Content>
