<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BookSmart.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <div class="hero-box d-flex flex-column justify-content-center align-items-start ps-5">
    <div class="hero-text-bg">
        <h1 class="display-4 fw-bold text-white">📍BookSmart</h1>
        <p class="lead text-white">"Your next great read starts here."</p>
    </div>
</div>


    <div class="container mt-4">
        <h3 class="mb-3">📚 New Arrivals</h3>

        <div class="row">
            <asp:Repeater ID="rptNewArrivals" runat="server">
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







