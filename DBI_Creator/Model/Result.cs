using System;
using System.Collections.Generic;
using dbi_grading_module;
using dbi_grading_module.Entity.Candidate;
using dbi_grading_module.Entity.Question;
using DBI202_Creator.UI;

namespace DBI202_Creator.Model
{
    public class Result
    {
        private readonly VerifyForm _parentForm;
        private readonly QuestionSet _questionSet;

        public Result(QuestionSet questionSet, VerifyForm parentForm)
        {
            _questionSet = questionSet;
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
        private Dictionary<string, string> GradeAnswer(Candidate candidate, string answer, int questionOrder)
        {
            if (string.IsNullOrEmpty(answer.Trim()))
                throw new Exception("Empty.");
            // Process by Question Type
            switch (candidate.QuestionType)
            {
                case Candidate.QuestionTypes.Schema:
                    // Schema Question
                    return Grading.SchemaType(candidate, "Test", answer, questionOrder);
                case Candidate.QuestionTypes.Select:
                    //Select Question
                    return Grading.SelectType(candidate, "Test", answer, questionOrder, _questionSet.DBScriptList[1]);
                case Candidate.QuestionTypes.DML:
                    // DML: Insert/Delete/Update Question
                    return Grading.DmlSpTriggerType(candidate, "Test", answer, questionOrder, _questionSet.DBScriptList[1]);
                case Candidate.QuestionTypes.Procedure:
                    // Procedure Question
                    return Grading.DmlSpTriggerType(candidate, "Test", answer, questionOrder, _questionSet.DBScriptList[1]);
                case Candidate.QuestionTypes.Trigger:
                    // Trigger Question
                    return Grading.DmlSpTriggerType(candidate, "Test", answer, questionOrder, _questionSet.DBScriptList[1]);
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

                        var result = GradeAnswer(candidate, candidate.Solution, 0);
                        AppendVerifyText(result["Comment"]);
                    }

                    countCandi = 0;
                    AppendVerifyText("--------------------\n");
                }
            }
            catch (Exception e)
            {
                AppendVerifyText(@"Error: " + e.Message);
            }
        }

        private void AppendVerifyText(string txt)
        {
            _parentForm.verifyText.Invoke(new Action(() => _parentForm.verifyText.Text += txt));
        }
    }
}