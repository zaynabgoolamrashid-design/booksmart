<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="BookSmart.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     
    <div class="container py-5" style="max-width: 800px;">
        <h2 class="mb-4 text-center">📬 Get in Touch with BookSmart</h2>
        
            <!-- Contact Form -->
           <div class="container py-5" style="max-width: 800px;">
                <asp:Label ID="lblMsg" runat="server" CssClass="text-success" />

                <div class="mb-3">
                    <label class="form-label">Your Name</label>
                    <div class="input-group">
                        <span class="input-group-text">👤</span>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter your name" />
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Your Email</label>
                    <div class="input-group">
                        <span class="input-group-text">📧</span>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email" />
                    </div>
                </div>

                <div class="mb-3">
                    <label class="form-label">Message</label>
                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Type your message..." />
                </div>

                <asp:Button ID="btnSubmit" runat="server" Text="Send Message" CssClass="btn btn-primary w-100" OnClick="btnSubmit_Click" />
            </div>

            <div class="col-12 mt-4 text-center">
                <h5>📞 Call Us</h5>
                <p>+230 58190394</p>

                <h5>📩 Email Us</h5>
                <p>support@booksmart.mu</p>

            </div>
       
    </div>

</asp:Content>
