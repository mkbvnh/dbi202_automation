using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using DBI202_Creator.Entities.Candidate;
using DBI202_Creator.Entities.Question;
using DBI202_Creator.UI;
using DBI202_Creator.Utils.Grading.Utils;

namespace DBI202_Creator.Utils.Grading
{
    public class Result
    {
        private readonly SqlConnectionStringBuilder Builder;
        private readonly QuestionSet QuestionSet;
        private readonly VerifyForm ParentForm;

        public Result(QuestionSet questionSet, SqlConnectionStringBuilder builder, VerifyForm parentForm)
        {
            QuestionSet = questionSet;
            Builder = builder;
            ParentForm = parentForm;
        }


        /// <summary>
        ///     Count Student's point by question and answer
        /// </summary>
        /// <param name="candidate">Question</param>
        /// <param name="answer">Student's answer</param>
        /// <param name="questionOrder"></param>
        /// <param name="dbScript"></param>
        /// <returns>
        ///     True if correct
        ///     False if incorrect.
        /// </returns>
        /// <exception>
        ///     if exception was found, throw it for GetPoint function to handle
        ///     <cref>SQLException</cref>
        /// </exception>
        private Dictionary<string, string> GradeAnswer(Candidate candidate, string answer, int questionOrder,
            string dbScript)
        {
            if (string.IsNullOrEmpty(answer.Trim()))
                throw new Exception("Empty.");
            // Process by Question Type
            switch (candidate.QuestionType)
            {
                case Candidate.QuestionTypes.Schema:
                    // Schema Question
                    return PaperUtils.SchemaType(candidate, "Test", answer, questionOrder, Builder);
                case Candidate.QuestionTypes.Select:
                    //Select Question
                    return PaperUtils.SelectType(candidate, "Test", answer, questionOrder, dbScript, Builder);
                case Candidate.QuestionTypes.DML:
                    // DML: Insert/Delete/Update Question
                    return PaperUtils.OthersType(candidate, "Test", answer, questionOrder, dbScript, Builder);
                case Candidate.QuestionTypes.Procedure:
                    // Procedure Question
                    return PaperUtils.OthersType(candidate, "Test", answer, questionOrder, dbScript, Builder);
                case Candidate.QuestionTypes.Trigger:
                    // Trigger Question
                    return PaperUtils.OthersType(candidate, "Test", answer, questionOrder, dbScript, Builder);
                default:
                    // Not supported yet
                    throw new Exception("This question type has not been supported yet.");
            }
        }

        /// <summary>
        ///     Get GradeAnswer function
        /// </summary>
        public void GetPoint()
        {
            var countQs = 0;
            var countCandi = 0;
            try
            {
                foreach (var question in QuestionSet.QuestionList)
                {
                    ParentForm.verifyText.Text += @"Question " + ++countQs + ":\n";
                    foreach (var candidate in question.Candidates)
                    {
                        ParentForm.verifyText.Text += "Candi " + ++countCandi + ":\n";

                        var result = GradeAnswer(candidate, candidate.Solution, 0,
                            QuestionSet.DBScriptList[1]);
                        ParentForm.verifyText.Text += result["Comment"];
                    }
                    countCandi = 0;
                    ParentForm.verifyText.Text += @"--------------------";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(ParentForm, e.Message, @"Error");
                ParentForm.verifyText.Text = @"Error: " + e.Message;
            }
            
        }
    }
}