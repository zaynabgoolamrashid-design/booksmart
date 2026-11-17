<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="BookSmart.BookDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container mt-4">
        <div class="row">
            <div class="col-md-4">
                <asp:Image ID="imgCover" runat="server" CssClass="img-fluid" />
            </div>
            <div class="col-md-8">
                <h2><asp:Label ID="lblTitle" runat="server" /></h2>
                <p class="text-success fw-bold">Rs <asp:Label ID="lblPrice" runat="server" /></p>
                <p><asp:Label ID="lblDescription" runat="server" /></p>
                

                Quantity:
                <asp:TextBox ID="txtQuantity" runat="server" Text="1" CssClass="form-control w-25" />

                <br />
                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-primary" OnClick="btnAddToCart_Click" />
                <asp:Label ID="lblMessage" runat="server" CssClass="text-success" />
            </div>
        </div>
    </div>

</asp:Content>
