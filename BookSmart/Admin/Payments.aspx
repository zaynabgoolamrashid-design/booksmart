<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="BookSmart.Payments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>💳 Payment History</h2>
        <hr />
        <asp:GridView ID="gvPayments" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="Order_ID" HeaderText="Order ID" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                <asp:BoundField DataField="Payment_Method" HeaderText="Payment Method" />
                <asp:BoundField DataField="total_amount" HeaderText="Amount" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Order_Date" HeaderText="Date" DataFormatString="{0:dd MMM yyyy hh:mm tt}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
