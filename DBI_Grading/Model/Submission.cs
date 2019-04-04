﻿using System;
using System.Collections.Generic;
using DBI_Grading.Common;

namespace DBI_Grading.Model
{
    [Serializable]
    public class Submission
    {
        public Submission()
        {
            ListAnswer = new List<string>();
            AnswerPaths = new List<string>();

            for (var i = 0; i < Constant.PaperSet.QuestionSet.QuestionList.Count; i++)
            {
                ListAnswer.Add("");
                AnswerPaths.Add("Cannot found");
            }
        }

        public Submission(string studentId, string paperNo)
        {
            StudentId = studentId;
            PaperNo = paperNo;
            ListAnswer = new List<string>();
            AnswerPaths = new List<string>();
        }

        public string StudentId { get; set; }
        public string PaperNo { get; set; }
        public List<string> ListAnswer { get; set; }
        public List<string> AnswerPaths { get; set; }

        public void AddAnswer(string answer)
        {
            ListAnswer.Add(answer);
        }

        public void ClearAnswer()
        {
            ListAnswer.Clear();
        }
    }
}