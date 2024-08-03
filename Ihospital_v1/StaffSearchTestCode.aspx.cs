using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ihospital_v1
{
    public partial class StaffSearchTestCode : System.Web.UI.Page
    {
        private string connectionString = "Data Source=SQL5111.site4now.net;Initial Catalog=db_9ab8b7_224dda10173;User Id=db_9ab8b7_224dda10173_admin;Password=d69GBPZq;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetupGridView();
                BindData();
            }
        }

        private void SetupGridView()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT QuestionText FROM Questions ORDER BY QuestionID", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    string questionText = row["QuestionText"].ToString();
                    BoundField field = new BoundField();
                    field.DataField = questionText;
                    field.HeaderText = questionText;
                    GridView1.Columns.Add(field);
                    GridView1.Columns[GridView1.Columns.Count - 1].HeaderStyle.CssClass = "column-header-style";
                }
            }
        }

        private void BindData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Create columns in the DataTable for each question
                SqlCommand getQuestions = new SqlCommand("SELECT QuestionText FROM Questions ORDER BY QuestionID", con);
                SqlDataAdapter daQuestions = new SqlDataAdapter(getQuestions);
                DataTable questions = new DataTable();
                daQuestions.Fill(questions);

                foreach (DataRow question in questions.Rows)
                {
                    dt.Columns.Add(question["QuestionText"].ToString());
                }

                // Populate rows with AnswerText
                SqlCommand cmd = new SqlCommand("SELECT P.ParticipantID, Q.QuestionText, A.AnswerText FROM ParticipantsAnswers PA INNER JOIN Answers A ON PA.AnswerID = A.AnswerID INNER JOIN Questions Q ON A.QuestionID = Q.QuestionID INNER JOIN Participants P ON PA.ParticipantID = P.ParticipantID ORDER BY P.ParticipantID, Q.QuestionID", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable answers = new DataTable();
                da.Fill(answers);

                // Group by ParticipantID to create one row per participant
                var grouped = answers.AsEnumerable().GroupBy(r => r.Field<int>("ParticipantID"));
                foreach (var group in grouped)
                {
                    DataRow newRow = dt.NewRow();
                    foreach (DataRow ansRow in group)
                    {
                        newRow[ansRow["QuestionText"].ToString()] = ansRow["AnswerText"].ToString();
                    }
                    dt.Rows.Add(newRow);
                }
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void SearchData(object sender, EventArgs e)
        {
            // Implement your search logic here
            // You can access the search fields using their ID and the `FindControl` method or directly if they are public/protected
            string name = ((TextBox)form1.FindControl("searchByName")).Text;
            string date = ((TextBox)form1.FindControl("searchByDate")).Text;
            string service = ((DropDownList)form1.FindControl("searchByService")).SelectedValue;
            string state = ((DropDownList)form1.FindControl("searchByState")).SelectedValue;

            // Example SQL query to demonstrate filtering (you will need to adjust this according to your actual database schema and setup)
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM YourDataTable WHERE Name LIKE @Name AND Date = @Date AND Service = @Service AND State = @State", con);
                cmd.Parameters.AddWithValue("@Name", '%' + name + '%');
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Service", service);
                cmd.Parameters.AddWithValue("@State", state);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }


    }
}