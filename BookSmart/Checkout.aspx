<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="BookSmart.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div class="container mt-5">
        <h2>Checkout</h2>

        <!-- Empty cart panel -->
        <asp:Panel ID="pnlEmpty" runat="server" Visible="false" CssClass="alert alert-info">
            Your cart is empty. <a href="Default.aspx">Continue Shopping</a>.
        </asp:Panel>

        <!-- Cart details panel -->
        <asp:Panel ID="pnlCart" runat="server" Visible="false">
            <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
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

            <!-- ONLY Proceed to Payment button -->
            <div class="mt-4">
                <asp:Button ID="btnProceedPayment" runat="server" 
                    Text="Proceed to Payment" CssClass="btn btn-primary" 
                    OnClick="btnProceedPayment_Click" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>
