<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageAdmin.aspx.cs" Inherits="BookSmart.ManageAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container mt-4">
    <h2>Manage Admins</h2>
    <asp:GridView ID="gvAdmins" runat="server" CssClass="table table-bordered"
        AutoGenerateColumns="False" DataKeyNames="User_id"
        OnRowEditing="gvAdmins_RowEditing" OnRowCancelingEdit="gvAdmins_RowCancelingEdit"
        OnRowUpdating="gvAdmins_RowUpdating" OnRowDeleting="gvAdmins_RowDeleting">
        <Columns>
            <asp:BoundField DataField="User_id" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="First_name" HeaderText="First Name" />
            <asp:BoundField DataField="Last_name" HeaderText="Last Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Password" HeaderText="Password" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <h4>Add New Admin</h4>
    <div class="row g-2">
        <div class="col"><asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Placeholder="First Name" /></div>
        <div class="col"><asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Placeholder="Last Name" /></div>
        <div class="col"><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" /></div>
        <div class="col"><asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Placeholder="Password" /></div>
        <div class="col"><asp:Button ID="btnAdd" runat="server" Text="Add Admin" CssClass="btn btn-primary" OnClick="btnAdd_Click" /></div>
    </div>
</div>
</asp:Content>
