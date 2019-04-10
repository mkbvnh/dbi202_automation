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
            if (questionSet.QuestionList.Count == 0) throw new Exception("Please add question!!!");

            var countQuestion = 0;
            foreach (var question in questionSet.QuestionList)
            {
                countQuestion++;
                if (question.Candidates.Count == 0)
                    throw new Exception($"Please add candidate into question {countQuestion} !!!");
                totalPoint += question.Point;
            }

            if (totalPoint < 10 || totalPoint > 10) throw new Exception($"Total Point must equal 10 (Current Point: {totalPoint})");

            //Validate DBScript
            if (questionSet.DBScriptList.Count < 2 || string.IsNullOrEmpty(questionSet.DBScriptList[0].Trim()) ||
                string.IsNullOrEmpty(questionSet.DBScriptList[1].Trim())) throw new Exception("Please add 2 DBScripts");

            return true;
        }
    }
}