<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="BookSmart.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div class="container mt-4">
        <h2>Payment Details</h2>

        <!-- Error message -->
        <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-warning d-block" Visible="false"></asp:Label>

        <asp:Label ID="lblTotalAmount" runat="server" CssClass="fw-bold"></asp:Label>
        <hr />

        <h4>Select Payment Method</h4>
        <asp:RadioButtonList ID="rblPaymentMethod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblPaymentMethod_SelectedIndexChanged">
            <asp:ListItem Text="Cash on Delivery" Value="COD" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Credit Card" Value="CC"></asp:ListItem>
        </asp:RadioButtonList>

        <!-- Credit Card details -->
        <div id="cardDetails" runat="server" visible="false" class="mt-3">
            <div class="mb-2">
                <label>Card Number</label>
                <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control" MaxLength="16"></asp:TextBox>
            </div>
            <div class="mb-2">
                <label>Expiry Date</label>
                <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control" placeholder="MM/YY"></asp:TextBox>
            </div>
            <div class="mb-2">
                <label>CVV</label>
                <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" MaxLength="3" TextMode="Password"></asp:TextBox>
            </div>
        </div>

        <asp:Button ID="btnConfirmPayment" runat="server" Text="Confirm Payment" CssClass="btn btn-success mt-3" OnClick="btnConfirmPayment_Click" />
    </div>
</asp:Content>
