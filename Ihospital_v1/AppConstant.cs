using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ihospital_v1
{
    public class AppConstant
    {

        public class DBServerConnection 

        {
            public const string developmentConnectionString = "Data Source=SQL5111.site4now.net;Initial Catalog=db_9ab8b7_224dda10173;User Id=db_9ab8b7_224dda10173_admin;Password=d69GBPZq;";
            public const string testConnectionString = "Data Source=SQL5111.site4now.net;Initial Catalog=db_9ab8b7_224dda10173;User Id=db_9ab8b7_224dda10173_admin;Password=d69GBPZq;";
            public const string productionConnectionString = "Data Source=SQL5111.site4now.net;Initial Catalog=db_9ab8b7_224dda10173;User Id=db_9ab8b7_224dda10173_admin;Password=d69GBPZq;";
        }

        public enum ParticipantsAnswers 
        {
            ParticipantID,
            AnswerID,
            FreeTextAnswer
        }

        public class QuestionType
        {
            public const string textBoxStringType = "txtAnswer";
            public const string radioButtonType = "rbAnswer";
            public const string checkBoxType = "cbAnswer";
            public const string textBoxDateType = "txtDate";
        }
       
    }
}