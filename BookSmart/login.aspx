<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BookSmart.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5" style="max-width: 500px;">
        <h3 class="mb-4">🔐 Login to BookSmart</h3>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

        <asp:TextBox ID="txtEmail" runat="server"
                     CssClass="form-control mb-3" placeholder="Email" />

        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"
                     CssClass="form-control mb-3" placeholder="Password" />

        <asp:Button ID="btnLogin" runat="server" Text="Login"
                    CssClass="btn btn-success w-100" OnClick="btnLogin_Click" />

                       <a href="Register.aspx" class="d-block mt-3">Create an account</a>
                       <a href="ForgotPassword.aspx" class="d-block">Forgot password?</a>
    </div>
   </asp:Content>
