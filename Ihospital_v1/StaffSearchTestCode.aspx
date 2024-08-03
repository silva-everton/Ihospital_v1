<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffSearchTestCode.aspx.cs" Inherits="Ihospital_v1.StaffSearchTestCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Search Screen</title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
      <div class="container">
            <h1>Survey Responses</h1>
            <div class="search-container">
                <div class="search-fields">
                    <div class="search-field">
                        <label for="searchByName" class="form-label">Search by Name:</label>
                        <input type="text" id="searchByName" runat="server" class="text-input" placeholder="Enter name"/>
                    </div>
                    <div class="search-field">
                        <label for="searchByDate" class="form-label">Search by Date:</label>
                        <input type="date" id="searchByDate" runat="server" class="text-input"/>
                    </div>
                    <div class="search-field">
                        <label for="searchByService" class="form-label">Search by Service:</label>
                        <select id="searchByService" runat="server" class="dropdown">
                            <option value="">Select Service</option>
                            <option value="Day Surgery">Day Surgery</option>
                            <option value="Pathology">Pathology</option>
                            <option value="Rehabilitation">Rehabilitation</option>
                            <option value="Operation/Surgery">Operation/Surgery</option>
                        </select>
                    </div>
                    <div class="search-field">
                        <label for="searchByState" class="form-label">Search by State:</label>
                        <select id="searchByState" runat="server" class="dropdown">
                            <option value="">Select State</option>
                            <option value="NSW">NSW</option>
                            <option value="VIC">VIC</option>
                            <option value="QLD">QLD</option>
                            <option value="SA">SA</option>
                            <option value="WA">WA</option>
                            <option value="TAS">TAS</option>
                            <option value="NT">NT</option>
                            <option value="ACT">ACT</option>
                        </select>
                    </div>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button-submit" OnClick="SearchData" />
                </div>
            </div>
            <asp:GridView ID="GridView1" runat="server" CssClass="survey-grid" AutoGenerateColumns="False">
                <Columns>
                   
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
