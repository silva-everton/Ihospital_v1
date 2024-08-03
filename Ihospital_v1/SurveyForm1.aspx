<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyForm1.aspx.cs" Inherits="Ihospital_v1.SurveyForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey Form</title>
    <link href="styles.css?v=1.1" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Survey Form</h1>
            <asp:DropDownList ID="ddlQuestions" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlQuestions_SelectedIndexChanged" CssClass="dropdown"></asp:DropDownList>
            <asp:Label ID="lblQuestion" runat="server" Text="" CssClass="question-label"></asp:Label>
            <asp:PlaceHolder ID="phAnswers" runat="server"></asp:PlaceHolder>
            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" CssClass="button-next" />
            <asp:Button ID="btnSaveAll" runat="server" Text="Submit All Answers" OnClick="btnSaveAll_Click" CssClass="button-save-all" />
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label-error"></asp:Label>
        </div>
    </form>
</body>
</html>

