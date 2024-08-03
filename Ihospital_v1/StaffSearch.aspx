<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffSearch.aspx.cs" Inherits="Ihospital_v1.StaffSearch" %>

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
                        <input type="text" id="searchByName" class="text-input" placeholder="Enter name"/>
                    </div>
                    <div class="search-field">
                        <label for="searchByDate" class="form-label">Search by Date:</label>
                        <input type="date" id="searchByDate" class="text-input"/>
                    </div>
                    <div class="search-field">
                        <label for="searchByService" class="form-label">Search by Service:</label>
                        <select id="searchByService" class="dropdown">
                            <option value="">Select Service</option>
                            <option value="Day Surgery">Day Surgery</option>
                            <option value="Pathology">Pathology</option>
                            <option value="Rehabilitation">Rehabilitation</option>
                            <option value="Operation/Surgery">Operation/Surgery</option>
                        </select>
                    </div>
                    <div class="search-field">
                        <label for="searchByState" class="form-label">Search by State:</label>
                        <select id="searchByState" class="dropdown">
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
                    <button type="button" class="button-submit">Search</button>
                </div>
                <asp:GridView ID="GridView1" runat="server" CssClass="survey-grid" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Gender" HeaderText="What is your gender?" />
                        <asp:BoundField DataField="AgeRange" HeaderText="What is your age range?" />
                        <asp:BoundField DataField="State" HeaderText="Which state do you live in?" />
                        <asp:BoundField DataField="ServiceType" HeaderText="Which type of service did you use?" />
                        <asp:BoundField DataField="PrivateInsurance" HeaderText="Do you have private health insurance?" />
                        <asp:BoundField DataField="RoomType" HeaderText="What type of room did you stay in?" />
                        <asp:BoundField DataField="InRoomService" HeaderText="What in-room service did you use?" />
                        <asp:BoundField DataField="WiFiService" HeaderText="Which WiFi service did you use?" />
                        <asp:BoundField DataField="InsuranceProvider" HeaderText="Who is your health insurance provider?" />
                        <asp:BoundField DataField="SuburbPostcode" HeaderText="Home Suburb or Postcode" />
                        <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" />
                        <asp:BoundField DataField="DischargePlan" HeaderText="Discharge Plan" />
                        <asp:BoundField DataField="Membership" HeaderText="Would you like to register as a member?" />
                        <asp:BoundField DataField="GivenNames" HeaderText="Given Names" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth" />
                        <asp:BoundField DataField="ContactNumber" HeaderText="Contact Phone Number" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
