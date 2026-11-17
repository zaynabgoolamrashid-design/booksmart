<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="BookSmart.AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <h2 class="mb-4">📋 Admin Dashboard</h2>

        <div class="alert alert-info">
            Welcome <strong><%= Session["UserName"] %></strong> — you are logged in as <strong>Admin</strong>.
        </div>
          

       <div class="row g-4">
    <div class="col-md-4">
        <a href="ManageAdmin.aspx" class="btn btn-outline-dark w-100 p-3">🛠 Manage Admins</a>
    </div>
    <div class="col-md-4">
        <a href="Users.aspx" class="btn btn-outline-secondary w-100 p-3">👤 Manage Users</a>
    </div>
    <div class="col-md-4">
        <a href="ManageBooks.aspx" class="btn btn-outline-primary w-100 p-3">📚 Manage Books</a>
    </div>
    <div class="col-md-4">
        <a href="Payments.aspx" class="btn btn-outline-success w-100 p-3">💳 View Payments</a>
    </div>
    <div class="col-md-4">
        <a href="Orders.aspx" class="btn btn-outline-warning w-100 p-3">📦 Customer Orders</a>
    </div>
    <div class="col-md-4">
        <a href="Reports.aspx" class="btn btn-outline-info w-100 p-3">📊 Reports</a>
    </div>
</div>
        </div>
   

</asp:Content>
