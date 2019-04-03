using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using DBI202_Creator.UI;
using DBI202_Creator.Utils.Grading;
using DBI_Grading.Model.Candidate;
using DBI_Grading.Model.Question;

namespace DBI202_Creator.Model
{
    public class Result
    {
        private readonly SqlConnectionStringBuilder _builder;
        private readonly VerifyForm _parentForm;
        private readonly QuestionSet _questionSet;

        public Result(QuestionSet questionSet, SqlConnectionStringBuilder builder, VerifyForm parentForm)
        {
            _questionSet = questionSet;
            _builder = builder;
            _parentForm = parentForm;
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
                    return PaperUtils.SchemaType(candidate, "Test", answer, questionOrder, _builder);
                case Candidate.QuestionTypes.Select:
                    //Select Question
                    return PaperUtils.SelectType(candidate, "Test", answer, questionOrder, dbScript, _builder);
                case Candidate.QuestionTypes.DML:
                    // DML: Insert/Delete/Update Question
                    return PaperUtils.OthersType(candidate, "Test", answer, questionOrder, dbScript, _builder);
                case Candidate.QuestionTypes.Procedure:
                    // Procedure Question
                    return PaperUtils.OthersType(candidate, "Test", answer, questionOrder, dbScript, _builder);
                case Candidate.QuestionTypes.Trigger:
                    // Trigger Question
                    return PaperUtils.OthersType(candidate, "Test", answer, questionOrder, dbScript, _builder);
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
                foreach (var question in _questionSet.QuestionList)
                {
                    AppendVerifyText(@"Question " + ++countQs + ":\n");
                    foreach (var candidate in question.Candidates)
                    {
                        AppendVerifyText("Candi " + ++countCandi + ":\n");

                        var result = GradeAnswer(candidate, candidate.Solution, 0,
                            _questionSet.DBScriptList[1]);
                        AppendVerifyText(result["Comment"]);
                    }
                    countCandi = 0;
                    AppendVerifyText("--------------------\n");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(_parentForm, e.Message, @"Error");
                AppendVerifyText(@"Error: " + e.Message);
            }
        }

        private void AppendVerifyText(string txt)
        {
            _parentForm.verifyText.Invoke(new Action(() => _parentForm.verifyText.Text += txt));
        }
    }
}