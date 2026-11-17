<%@ Page Title="All orders" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="BookSmart.Orders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>📦 All Customer Orders</h2>
        <hr />

        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnRowCommand="gvOrders_RowCommand">
            <Columns>
                <asp:BoundField DataField="Order_ID" HeaderText="Order ID" />
                <asp:BoundField DataField="UserName" HeaderText="Customer Name" />
                <asp:BoundField DataField="Order_Date" HeaderText="Order Date" DataFormatString="{0:dd MMM yyyy hh:mm tt}" />
                <asp:BoundField DataField="Payment_Method" HeaderText="Payment Method" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="total_amount" HeaderText="Total" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="View Details" CommandName="ViewOrder" CommandArgument='<%# Eval("Order_ID") %>' CssClass="btn btn-sm btn-primary" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
