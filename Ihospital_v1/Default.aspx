<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ihospital_v1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">        
        <div>             
           <asp:Label ID="Label1" runat="server" Text="Welcome to the iHospital System" Font-Size="XX-Large" ForeColor="#000099"></asp:Label>
           <br />
           <asp:GridView ID="ParticipantsAnswersGridView" runat="server" OnRowDataBound="ParticipantsAnswersGridView_RowDataBound"></asp:GridView>  

        </div>
    </form>
</body>
</html>
