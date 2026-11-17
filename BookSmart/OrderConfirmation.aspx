<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="BookSmart.OrderConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div class="container mt-5">
        <h2>Order Confirmation</h2>
        <hr />

        <asp:Panel ID="pnlOrderDetails" runat="server" Visible="false">
            <div class="alert alert-success">
                <strong>Thank you!</strong> Your order has been placed successfully.
            </div>

            <p><strong>Order ID:</strong> <asp:Label ID="lblOrderID" runat="server" /></p>
            <p><strong>Order Date:</strong> <asp:Label ID="lblOrderDate" runat="server" /></p>
            <p><strong>Payment Method:</strong> <asp:Label ID="lblPaymentMethod" runat="server" /></p>
            <p><strong>Status:</strong> <asp:Label ID="lblStatus" runat="server" /></p>

            <h4>Items:</h4>
            <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Book" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <%# (Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("C") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <h4 class="mt-3">Grand Total: <asp:Label ID="lblTotal" runat="server" /></h4>
        </asp:Panel>

        <asp:Panel ID="pnlNoOrder" runat="server" Visible="false" CssClass="alert alert-warning">
            No order found. <a href="Default.aspx">Continue shopping</a>.
        </asp:Panel>
    </div>
</asp:Content>
