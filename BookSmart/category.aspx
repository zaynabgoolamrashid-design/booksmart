<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="BookSmart.category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
    <h2 class="text-center mb-4">
        <asp:Label ID="lblCategoryName" runat="server" CssClass="text-primary"></asp:Label>
    </h2>

    <div class="row">
        <asp:Repeater ID="rptBooks" runat="server">
           <ItemTemplate>
    <div class="col-md-3 mb-4">
        <div class="card h-100">
            <img src='<%# Eval("cover_image_path") %>' class="card-img-top" alt="Book Cover" />
            <div class="card-body">
                <h5 class="card-title"><%# Eval("title") %></h5>
                <p class="card-text">Rs<%# Eval("price") %></p>
                <a href="BookDetails.aspx?id=<%# Eval("book_id") %>" class="btn btn-primary">View</a>

                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart"
                    CommandArgument='<%# Eval("Book_id") %>'
                    CssClass="btn btn-outline-success btn-sm ms-2"
                    OnCommand="btnAddToCart_Command" />

            </div>
        </div>
    </div>
</ItemTemplate>
               

        </asp:Repeater>
    </div>
</div>


</asp:Content>
