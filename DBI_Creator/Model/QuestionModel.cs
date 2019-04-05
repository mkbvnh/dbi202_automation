using System;
using dbi_grading_module.Entity.Question;

namespace DBI202_Creator.Model
{
    internal class QuestionModel
    {
        internal static bool VerifyQuestionSet(QuestionSet questionSet)
        {
            //Validate Total point
            decimal totalPoint = 0;
            foreach (var question in questionSet.QuestionList) totalPoint += question.Point;
            if (totalPoint < 10) throw new Exception($"Total Point must equal 10 (Current Point: {totalPoint})");

            //Validate DBScript
            if (questionSet.DBScriptList.Count < 2 || string.IsNullOrEmpty(questionSet.DBScriptList[0].Trim()) ||
                string.IsNullOrEmpty(questionSet.DBScriptList[1].Trim())) throw new Exception("Please add 2 DBScripts");

            return true;
        }
    }
}