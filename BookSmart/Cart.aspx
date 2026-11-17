<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="BookSmart.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <h2>Your Cart</h2>
        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gvCart_RowCommand">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Book" />
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:TemplateField HeaderText="Remove">
                    <ItemTemplate>
                        <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="RemoveItem" CommandArgument='<%# Eval("Book_id") %>' CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <h4>Total: Rs <asp:Label ID="lblTotal" runat="server" /></h4>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-success" />
        <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" CssClass="btn btn-success" OnClick="btnCheckout_Click" />
    </div>

</asp:Content>
