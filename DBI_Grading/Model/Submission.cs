using System;
using System.Collections.Generic;
using dbi_grading_module.Entity.Paper;

namespace DBI_Grading.Model
{
    [Serializable]
    public class Submission
    {
        public Submission(PaperSet paperSet, string examCode)
        {
            ListAnswer = new List<string>();
            AnswerPaths = new List<string>();
            ExamCode = examCode;

            for (var i = 0; i < paperSet.QuestionSet.QuestionList.Count; i++)
            {
                ListAnswer.Add("");
                AnswerPaths.Add("Cannot found");
            }
        }

        public string StudentId { get; set; }
        public string PaperNo { get; set; }
        public List<string> ListAnswer { get; set; }
        public List<string> AnswerPaths { get; set; }
        public string ExamCode { get; set; }
    }
}