using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ihospital_v1
{
    /// <summary>
    /// Data Example:
    ///    1, 10
    ///    2, 15
    ///    3, 31|32
    ///    10, Ultimo
    ///    11, 2007
    /// </summary>
    public class AnswerSessionModel
    {
        #region Fields
        private int _question_id;
        private String _question_answer;
        #endregion

        #region Properties
        public int Question_id
        {
            get { return _question_id; }
            set { _question_id = value; }
        }
        public String Question_answer
        {
            get { return _question_answer; }
            set { _question_answer = value; }
        }
        #endregion

        public AnswerSessionModel()
        {
            _question_id = 0;
            _question_answer = "";
        }

        public AnswerSessionModel(int question_id, String question_answer)
        {
            _question_id = question_id;
            _question_answer = question_answer;
        }

    }
}