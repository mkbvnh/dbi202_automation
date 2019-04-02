using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DBI_Exam_Creator_Tool.Entities;

namespace DBI_Exam_Creator_Tool.UI
{
    class QuestionTabPage : TabPage
    {
        public Question Question { get; set; }

        public QuestionTabPage(Question question)
        {
            Question = question;
        }
    }
}
