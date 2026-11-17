<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BookSmart.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container mt-5" style="max-width:550px;">
        <h3 class="mb-4">📝 Create your BookSmart account</h3>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control mb-3" placeholder="First Name" />
        <asp:TextBox ID="txtLastName"  runat="server" CssClass="form-control mb-3" placeholder="Last Name" />
        <asp:TextBox ID="txtEmail"     runat="server" CssClass="form-control mb-3" placeholder="Email" />
        <asp:TextBox ID="txtPassword"  runat="server" TextMode="Password" CssClass="form-control mb-3" placeholder="Password" />
        <asp:TextBox ID="txtConfirm"   runat="server" TextMode="Password" CssClass="form-control mb-4" placeholder="Confirm Password" />

        <asp:Button  ID="btnRegister"  runat="server" Text="Sign Up"
                     CssClass="btn btn-primary w-100" OnClick="btnRegister_Click" />

        <p class="mt-3">
            Already have an account?
            <a href="Login.aspx">Login here</a>
        </p>
    </div>

</asp:Content>
