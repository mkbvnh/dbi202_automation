using System;
using System.Collections.Generic;

namespace DBI_Grading.Model.Candidate
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

        public Candidate(string candidateId, string questionId, string questionRequirement, QuestionTypes questionType,
            string solution, string testQuery, bool requireSort, bool checkColumnName, bool checkDistinct,
            bool relatedSchema, List<string> illustration, double point)
        {
            CandidateId = candidateId;
            QuestionId = questionId;
            QuestionRequirement = questionRequirement;
            QuestionType = questionType;
            Solution = solution;
            TestQuery = testQuery;
            RequireSort = requireSort;
            CheckColumnName = checkColumnName;
            CheckDistinct = checkDistinct;
            RelatedSchema = relatedSchema;
            Illustration = illustration;
            Point = point;
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
        public bool RelatedSchema { get; set; }

        public List<string> Illustration { get; set; }

        public double Point { get; set; }

        public override bool Equals(object obj)
        {
            var candidate = obj as Candidate;
            return candidate != null &&
                   CandidateId == candidate.CandidateId;
        }

        protected bool Equals(Candidate other)
        {
            return string.Equals(CandidateId, other.CandidateId) && string.Equals(QuestionId, other.QuestionId) &&
                   string.Equals(QuestionRequirement, other.QuestionRequirement) &&
                   QuestionType == other.QuestionType && string.Equals(Solution, other.Solution) &&
                   string.Equals(TestQuery, other.TestQuery) && RequireSort == other.RequireSort &&
                   CheckColumnName == other.CheckColumnName && CheckDistinct == other.CheckDistinct &&
                   RelatedSchema == other.RelatedSchema && Equals(Illustration, other.Illustration) &&
                   Point.Equals(other.Point);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CandidateId != null ? CandidateId.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (QuestionId != null ? QuestionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (QuestionRequirement != null ? QuestionRequirement.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) QuestionType;
                hashCode = (hashCode * 397) ^ (Solution != null ? Solution.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TestQuery != null ? TestQuery.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ RequireSort.GetHashCode();
                hashCode = (hashCode * 397) ^ CheckColumnName.GetHashCode();
                hashCode = (hashCode * 397) ^ CheckDistinct.GetHashCode();
                hashCode = (hashCode * 397) ^ RelatedSchema.GetHashCode();
                hashCode = (hashCode * 397) ^ (Illustration != null ? Illustration.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Point.GetHashCode();
                return hashCode;
            }
        }
    }
}