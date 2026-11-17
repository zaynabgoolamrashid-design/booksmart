<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="BookSmart.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5" style="max-width:500px;">
        <h3 class="mb-4">🔑 Reset your password</h3>

        <asp:Label ID="lblMsg" runat="server" CssClass="text-danger" />

        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-3"
                     placeholder="Enter your registered email" />

        <asp:Button ID="btnSend" runat="server" Text="Send Reset Link"
                    CssClass="btn btn-success w-100" OnClick="btnSend_Click" />

        <p class="mt-3">
            <a href="Login.aspx">Back to Login</a>
        </p>
    </div>

</asp:Content>
