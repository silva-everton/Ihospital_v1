using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Runtime.InteropServices;

namespace Ihospital_v1
{
    public partial class SurveyForm1 : System.Web.UI.Page
    {

        //connection string to the database 

        private string connString = "Data Source=SQL5111.site4now.net;Initial Catalog=db_9ab8b7_224dda10173;User Id=db_9ab8b7_224dda10173_admin;Password=d69GBPZq;";

        ///importing the SendARP function from the Iphlpapi.dll
        //  this function is used to get the MAC address of the client
        //  based on the IP address
        //  the MAC address is used to identify the client
        //  since the IP address can change
        ///the MAC address is used to identify the client

        [DllImport("Iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 pMacAddr, ref Int32 PhyAddrLen);

        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        protected void Page_Load(object sender, EventArgs e) //This method is called when the page is loaded
        {
            if (!IsPostBack)
            {
                int participantID = CreateParticipantAndGetID();
                Session["ParticipantID"] = participantID;
                Session["dataTypeName"] = "";
                Session["Answers"] = new List<AnswerSessionModel>();
                LoadQuestionDropdown();
            }

            DisplayQuestion();
        }

        private string GetMACAddress(string ipAddress) //This method is used to get the MAC address of the client
        {
            Int64 macinfo = new Int64();
            Int32 remote = inet_addr(ipAddress);
            Int32 len = 6;
            SendARP(remote, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");

            if (mac_src == "0")
            {
                return "MAC Address not found for IP Address: " + ipAddress;
            }

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            string macAddress = "";
            for (int i = 0; i < 11; i += 2)
            {
                macAddress = macAddress.Insert(macAddress.Length, mac_src.Substring(i, 2) + ":");
            }

            macAddress = macAddress.TrimEnd(':');

            return macAddress;
        }

        private int CreateParticipantAndGetID() //This method is used to create a new participant and get the ID
        {
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            string macAddress = GetMACAddress(clientIP);

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Participants (DateTime, MACAddress) OUTPUT INSERTED.ParticipantID VALUES (GETDATE(), @MACAddress)", conn);
                cmd.Parameters.AddWithValue("@MACAddress", macAddress);
                return (int)cmd.ExecuteScalar();
            }
        }

        private void LoadQuestionDropdown() //This method is used to load the questions into the dropdown
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                // Adjusted SQL to exclude certain QuestionIDs
                var cmd = new SqlCommand("SELECT QuestionID, QuestionText FROM Questions WHERE QuestionID NOT IN (13, 14, 15, 16, 17) ORDER BY QuestionID", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddlQuestions.Items.Clear();
                while (reader.Read())
                {
                    ddlQuestions.Items.Add(new ListItem(reader["QuestionText"].ToString(), reader["QuestionID"].ToString()));
                }
                reader.Close();
            }
        }

        protected void ddlQuestions_SelectedIndexChanged(object sender, EventArgs e) //This method is called when the selected index of the dropdown changes
        {
            DisplayQuestion();
        }

        private int GetMaxVisibleQuestionId() // This method is now aware of which questions are visible
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                // Fetch the maximum ID of the questions that are not excluded
                var cmd = new SqlCommand("SELECT MAX(QuestionID) FROM Questions WHERE QuestionID NOT IN (13, 14, 15, 16, 17)", conn);
                int maxVisibleQuestionId = Convert.ToInt32(cmd.ExecuteScalar());
                return maxVisibleQuestionId;
            }

        }

        protected void DisplayQuestion() // Adjusted to use the updated method for determining the last question
        {
            int questionID = int.Parse(ddlQuestions.SelectedValue);
            Session["CurrentQuestionID"] = questionID;

            int maxVisibleQuestionId = GetMaxVisibleQuestionId(); // Use the updated method

            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT QuestionText, DataTypeID FROM Questions WHERE QuestionID = @QuestionID", conn);
                cmd.Parameters.AddWithValue("@QuestionID", questionID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblQuestion.Text = reader["QuestionText"].ToString();
                    int dataTypeID = (int)reader["DataTypeID"];
                    reader.Close();
                    LoadAnswers(questionID, dataTypeID);
                }
            }

            // Update visibility based on whether it's the last visible question
            btnSaveAll.Visible = (questionID == maxVisibleQuestionId);
            btnNext.Visible = (questionID != maxVisibleQuestionId);
        }




        private void LoadAnswers(int questionID, int dataTypeID) //This method is used to load the answers
        {
            phAnswers.Controls.Clear(); 
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT DataTypeName FROM DataTypes WHERE DataTypeID = @DataTypeID", conn);
                cmd.Parameters.AddWithValue("@DataTypeID", dataTypeID);
                string dataTypeName = (string)cmd.ExecuteScalar();
                Session["dataTypeName"] = dataTypeName;

                switch (dataTypeName)
                {
                    case "txtAnswer":
                        CreateTextInput(questionID);
                        break;
                    case "rbAnswer":
                        CreateRadioButtonControls(questionID);
                        break;
                    case "cbAnswer":
                        CreateCheckBoxControls(questionID);
                        break;
                    case "txtDate":
                        CreateDateInput(questionID);
                        break;
                }
            }
        }

        private void CreateTextInput(int questionID) //This method is used to create a text input
        {
            TextBox txtInput = new TextBox { ID = "txtInput" + questionID, TextMode = TextBoxMode.SingleLine };
            LoadExistingAnswer(txtInput, questionID);
            phAnswers.Controls.Add(txtInput);
        }

        private void CreateDateInput(int questionID)
        {
            TextBox txtDate = new TextBox { ID = "txtDate" + questionID, TextMode = TextBoxMode.Date };
            LoadExistingAnswer(txtDate, questionID);
            phAnswers.Controls.Add(txtDate);
        }

        private void CreateRadioButtonControls(int questionID) //This method is used to create radio button controls
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT AnswerID, AnswerText FROM Answers WHERE QuestionID = @QuestionID", conn);
                cmd.Parameters.AddWithValue("@QuestionID", questionID);
                SqlDataReader reader = cmd.ExecuteReader();
                RadioButtonList rbl = new RadioButtonList { ID = "rblAnswers" + questionID };
                while (reader.Read())
                {
                    rbl.Items.Add(new ListItem(reader["AnswerText"].ToString(), reader["AnswerID"].ToString()));
                }
                reader.Close();
                phAnswers.Controls.Add(rbl);
                LoadExistingAnswer(rbl, questionID);
            }
        }

        private void CreateCheckBoxControls(int questionID) //This method is used to create checkbox controls
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT AnswerID, AnswerText FROM Answers WHERE QuestionID = @QuestionID", conn);
                cmd.Parameters.AddWithValue("@QuestionID", questionID);
                SqlDataReader reader = cmd.ExecuteReader();
                CheckBoxList cbl = new CheckBoxList { ID = "cblAnswers" + questionID };
                while (reader.Read())
                {
                    cbl.Items.Add(new ListItem(reader["AnswerText"].ToString(), reader["AnswerID"].ToString()));
                }
                reader.Close();
                phAnswers.Controls.Add(cbl);
                LoadExistingAnswer(cbl, questionID);
            }
        }

        private void LoadExistingAnswer(WebControl control, int questionID) //This method is used to load the existing answer
        {
            var answers = Session["Answers"] as List<AnswerSessionModel>;
            var existingAnswer = answers?.FirstOrDefault(a => a.Question_id == questionID)?.Question_answer;
            if (existingAnswer != null)
            {
                if (control is TextBox txt)
                {
                    txt.Text = existingAnswer;
                }
                else if (control is RadioButtonList rbl)
                {
                    rbl.SelectedValue = existingAnswer;
                }
                else if (control is CheckBoxList cbl)
                {
                    string[] selectedValues = existingAnswer.Split('|');
                    foreach (var item in cbl.Items.Cast<ListItem>())
                    {
                        item.Selected = selectedValues.Contains(item.Value);
                    }
                }
            }
        }

        protected void btnSaveAll_Click(object sender, EventArgs e) //This method is called when the Save All button is clicked
        {
            SaveAllAnswers();
            Session.Remove("Answers");
            Response.Redirect("Default.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e) //This method is called when the Next button is clicked
        {
            SaveCurrentAnswer();
            int currentIndex = ddlQuestions.SelectedIndex;
            if (currentIndex < ddlQuestions.Items.Count - 1)
            {
                ddlQuestions.SelectedIndex = currentIndex + 1;
                DisplayQuestion();
            }
        }

        private void SaveCurrentAnswer() //This method is used to save the current answer
        {
            int questionID = (int)Session["CurrentQuestionID"];
            var answers = Session["Answers"] as List<AnswerSessionModel>;
            string questionType = (string)Session["dataTypeName"];
            string answerValue = null;

            if (questionType == AppConstant.QuestionType.radioButtonType)
            {
                var rbl = phAnswers.FindControl("rblAnswers" + questionID) as RadioButtonList;
                answerValue = rbl.SelectedValue;
                AnswerSessionModel currentAnswer = new AnswerSessionModel(questionID, answerValue);
                answers.Add(currentAnswer);
                Session["Answers"] = answers;
            }
            else if (questionType == AppConstant.QuestionType.checkBoxType)
            {
                var cbl = phAnswers.FindControl("cblAnswers" + questionID) as CheckBoxList;
                answerValue = string.Join("|", cbl.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value));
                AnswerSessionModel currentAnswer = new AnswerSessionModel(questionID, answerValue);
                answers.Add(currentAnswer);
                Session["Answers"] = answers;
            }
            else if (questionType == AppConstant.QuestionType.textBoxStringType)
            {
                var txtBox = phAnswers.FindControl("txtInput" + questionID) as TextBox;
                answerValue = txtBox.Text;
                AnswerSessionModel currentAnswer = new AnswerSessionModel(questionID, answerValue);
                answers.Add(currentAnswer);
                Session["Answers"] = answers;
            }
            else if (questionType == AppConstant.QuestionType.textBoxDateType)
            {
                var txtDate = phAnswers.FindControl("txtDate" + questionID) as TextBox;
                answerValue = txtDate.Text;
                AnswerSessionModel currentAnswer = new AnswerSessionModel(questionID, answerValue);
                answers.Add(currentAnswer);
                Session["Answers"] = answers;
            }
            else
            {
                throw new Exception("Invalid question type");
               
            }

        }

        private void SaveAllAnswers() //This method is used to save all the answers
        {
            int participantID = (int)Session["ParticipantID"];
            var answers = Session["Answers"] as List<AnswerSessionModel>;
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                foreach (var answer in answers)
                {
                    var cmd = new SqlCommand("INSERT INTO ParticipantsAnswers (ParticipantID, AnswerID, FreeTextAnswer) VALUES (@ParticipantID, @AnswerID, @FreeTextAnswer)", conn);
                    cmd.Parameters.AddWithValue("@ParticipantID", participantID);
                    cmd.Parameters.AddWithValue("@AnswerID", answer.Question_id);
                    cmd.Parameters.AddWithValue("@FreeTextAnswer", answer.Question_answer);
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
