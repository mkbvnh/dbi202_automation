using System;
using System.Collections.Generic;
using dbi_grading_module.Entity.Paper;

namespace DBI_Grading.Model
{
    public class Submission
    {
        public string StudentId { get; set; }
        public string PaperNo { get; set; }
        public List<string> ListAnswer { get; set; }
        public List<string> AnswerPaths { get; set; }
        public string ExamCode { get; set; }

        public Submission(string studentId, string paperNo, List<string> listAnswer, List<string> answerPaths, string examCode)
        {
            StudentId = studentId;
            paperNo = PaperNo;
            ListAnswer = listAnswer;
            AnswerPaths = answerPaths;
            ExamCode = examCode;
        }

        public Submission(int numOfQuestion, string examCode)
        {
            ListAnswer = new List<string>();
            AnswerPaths = new List<string>();
            ExamCode = examCode;

            for (var i = 0; i < numOfQuestion; i++)
            {
                ListAnswer.Add("");
                AnswerPaths.Add("Cannot found");
            }
        }
    }
}