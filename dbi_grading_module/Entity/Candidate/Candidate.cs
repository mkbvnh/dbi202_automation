using System;
using System.Collections.Generic;

namespace dbi_grading_module.Entity.Candidate
{
    [Serializable]
    public class Candidate
    {
        public enum QuestionTypes
        {
            Select = 1,
            Procedure = 2,
            Trigger = 3,
            Schema = 4,
            DML = 5
        }

        public Candidate()
        {
            QuestionType = QuestionTypes.Select;
            Illustration = new List<string>();
        }

        public string CandidateId { get; set; }
        public string QuestionId { get; set; }
        public string QuestionRequirement { get; set; }
        public QuestionTypes QuestionType { get; set; }

        public string Solution { get; set; }
        public string TestQuery { get; set; }

        public bool RequireSort { get; set; }
        public bool CheckColumnName { get; set; }
        public bool CheckDistinct { get; set; }

        public List<string> Illustration { get; set; }

        public double Point { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Candidate candidate &&
                   CandidateId == candidate.CandidateId;
        }
    }
}