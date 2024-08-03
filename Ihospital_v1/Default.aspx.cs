using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Ihospital_v1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn;
            SqlCommand myCommand;

            string myConnectionString = ConfigurationManager.ConnectionStrings["CurrentConnection"].ConnectionString;
            if (myConnectionString.Equals ("dev"))                           
                myConnectionString = AppConstant.DBServerConnection.developmentConnectionString;             
            else if (myConnectionString.Equals ("test"))            
                myConnectionString = AppConstant.DBServerConnection.testConnectionString;            
            else             
                myConnectionString = AppConstant.DBServerConnection.productionConnectionString;
           
           //string myConnectionString = "Data Source=SQL5111.site4now.net;Initial Catalog=db_9ab8b7_224dda10173;User Id=db_9ab8b7_224dda10173_admin;Password=d69GBPZq;";

            try
            { 
                conn = new SqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                Label1.Text = "My DataBase List";

                //Here goes the code to execute the query

                myCommand = new SqlCommand("SELECT * FROM ParticipantsAnswers", conn);

                SqlDataReader reader = myCommand.ExecuteReader(); // Execute the query/get all the data

                DataTable dt = new DataTable(); // Create a new DataTable
                dt.Columns.Add("ParticipantID", typeof(Int32));
                dt.Columns.Add("AnswerID", typeof(Int32));
                dt.Columns.Add("FreeTextAnswer", typeof(String));

                //time to read the data and fill the DataTable
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();

                    row["ParticipantID"] = reader["ParticipantID"];
                    row["AnswerID"] = reader["AnswerID"];
                    row["FreeTextAnswer"] = reader["FreeTextAnswer"];

                    dt.Rows.Add(row);
                }

                ParticipantsAnswersGridView.DataSource = dt;
                ParticipantsAnswersGridView.DataBind(); //show the data in the GridView

                conn.Close();


            }
            catch (InvalidOperationException ex)
            {
                Label1.Text = "Internal error, contact Admin!!!/n" + ex.Message;
            }
            catch (SqlException ex)
            {
                Label1.Text = "Internal error, contact Admin!!!/n" + ex.Message;
            }
            catch (ConfigurationErrorsException ex)
            {
                Label1.Text = "Internal error, contact Admin!!!/n" + ex.Message;
            }
            catch (Exception ex)
            {
                Label1.Text = "Internal error, contact Admin!!!/n" + ex.Message;
            }

        }

        protected void ParticipantsAnswersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //RowDataBound is an event that is triggered for each row in the GridView
            // This, we can use it to manipulate the data in the GridView    
            // In this case, we are adding an image to the GridView based on the AnswerID
            // We are also adding a CheckBox to the ParticipantID column
            // This is just an example of what can be done with the RowDataBound event
            // This is will be useful when you want to manipulate the data in the GridView
            // based on certain conditions
            // This is will be useful to display the survey for the admin to see the answers
            /*
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
               Image img = new Image();
               CheckBox chb = new CheckBox(); 

                if (e.Row.Cells[(int)AppConstant.ParticipantsAnswers.AnswerID].Text.Equals("1"))
                {
                    img.ImageUrl = "~/images/King.gif"; 
                }
                else if (e.Row.Cells[1].Text.Equals("5"))
                {
                    img.ImageUrl = "~/images/Knight.gif";
                }
                else
                {
                    img.ImageUrl = "~/images/pawn.gif";
                }

                e.Row.Cells[(int)AppConstant.ParticipantsAnswers.ParticipantID].Controls.Add(chb);
                e.Row.Cells[(int)AppConstant.ParticipantsAnswers.AnswerID].Controls.Add(img);
                
                
               


            }*/

        }
    }
}