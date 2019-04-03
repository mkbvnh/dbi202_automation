using System.Collections.Generic;
using DBI_Grading.Model.Candidate;
using DBI_Grading.Model.Paper;

namespace DBI202_Creator.Commons
{
    public static class Constants
    {
        public static PaperSet PaperSet;

        //Configure SQL
        public static int TimeOutInSecond;

        public static int MaxThreadPoolSize = 1;
        public static int MaxConnectionPoolSize = 100;


        public static Dictionary<string, Candidate.QuestionTypes> QuestionTypes()
        {
            return new Dictionary<string, Candidate.QuestionTypes>
            {
                {QuestionType.SELECT, Candidate.QuestionTypes.Select},
                {QuestionType.PROCUDURE, Candidate.QuestionTypes.Procedure},
                {QuestionType.TRIGGER, Candidate.QuestionTypes.Trigger},
                {QuestionType.SCHEMA, Candidate.QuestionTypes.Schema},
                {QuestionType.DML, Candidate.QuestionTypes.DML}
            };
        }

        public class QuestionType
        {
            public const string SELECT = "Select Query";
            public const string PROCUDURE = "Procedure";
            public const string TRIGGER = "Trigger";
            public const string SCHEMA = "Schema";
            public const string DML = "Insert Delete Update";
        }

        public class RequirementType
        {
            public const string RESULT_SET = "Result Set";
            public const string EFFECT = "Effect";
            public const string PARAMETER = "Parameter";
        }

        public class Size
        {
            public const int IMAGE_WIDTH = 669;
        }
    }
}