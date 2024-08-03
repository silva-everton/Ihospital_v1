<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Ihospital_v1.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register as Member</title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="form-container">
            <h1>Register as Member</h1>
            <asp:Label ID="lblGivenName" runat="server" Text="Given Name:" AssociatedControlID="txtGivenName" CssClass="form-label"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="txtGivenName" runat="server" CssClass="text-input"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" AssociatedControlID="txtLastName" CssClass="form-label"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="txtLastName" runat="server" CssClass="text-input"></asp:TextBox>
            <br />
            <br />          
            <asp:Label ID="lblDOB" runat="server" Text="Date of Birth:" AssociatedControlID="txtDOB" CssClass="form-label"></asp:Label>
            <br />
            <br /> 
            <asp:TextBox ID="txtDOB" runat="server" CssClass="text-input" TextMode="Date"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblContactPhone" runat="server" Text="Contact Phone Number:" AssociatedControlID="txtContactPhone" CssClass="form-label"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="txtContactPhone" runat="server" CssClass="text-input"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="button-submit" />
            <br />
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message"></asp:Label>
        </div>
    </form>
</body>
</html>
