<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="BookSmart.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container mt-4">
        <h2>My Orders</h2>
        <asp:GridView ID="gvOrders" runat="server" CssClass="table table-bordered" AutoGenerateColumns="True"></asp:GridView>
    </div>

</asp:Content>
