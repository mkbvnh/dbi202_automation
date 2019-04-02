using System.Collections.Generic;
using DBI202_Creator.Entities.Candidate;
using DBI202_Creator.Entities.Paper;

namespace DBI202_Creator.Commons
{
    public static class Constants
    {
        public static PaperSet PaperSet;


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

        //public static Dictionary<string, Requirement.RequirementTypes> GetRequirementTypes(Candidate.QuestionTypes questionType)
        //{
        //    Dictionary<string, Requirement.RequirementTypes> dictionary = new Dictionary<string, Requirement.RequirementTypes>();

        //    switch (questionType)
        //    {
        //        case Candidate.QuestionTypes.Select:
        //            dictionary.Add(RequirementType.RESULT_SET, Requirement.RequirementTypes.ResultSet);
        //            break;
        //        case Candidate.QuestionTypes.Procedure:
        //            dictionary.Add(RequirementType.RESULT_SET, Requirement.RequirementTypes.ResultSet);
        //            dictionary.Add(RequirementType.EFFECT, Requirement.RequirementTypes.Effect);
        //            dictionary.Add(RequirementType.PARAMETER, Requirement.RequirementTypes.Parameter);
        //            break;
        //        case Candidate.QuestionTypes.Trigger:
        //            dictionary.Add(RequirementType.RESULT_SET, Requirement.RequirementTypes.ResultSet);
        //            dictionary.Add(RequirementType.EFFECT, Requirement.RequirementTypes.Effect);
        //            break;
        //        case Candidate.QuestionTypes.Schema:
        //            dictionary.Add(RequirementType.RESULT_SET, Requirement.RequirementTypes.ResultSet);
        //            break;
        //        case Candidate.QuestionTypes.DML:
        //            dictionary.Add(RequirementType.EFFECT, Requirement.RequirementTypes.Effect);
        //            break;
        //        default:
        //            break;
        //    }
        //    return dictionary;
        //}

        public class Size
        {
            public const int IMAGE_WIDTH = 669;
        }
    }
}