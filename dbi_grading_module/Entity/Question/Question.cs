using System;
using System.Collections.Generic;

namespace dbi_grading_module.Entity.Question
{
    [Serializable]
    public class Question
    {
        public Question(string questionId, decimal point, List<Candidate.Candidate> candidates)
        {
            QuestionId = questionId;
            Point = point;
            foreach (var candidate in candidates)
                candidate.Point = decimal.ToDouble(point);
            Candidates = candidates;
        }

        public Question()
        {
            Candidates = new List<Candidate.Candidate>();
        }

        public string QuestionId { get; set; }
        public decimal Point { get; set; }
        public List<Candidate.Candidate> Candidates { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Question question &&
                   QuestionId == question.QuestionId;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = QuestionId != null ? QuestionId.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ Point.GetHashCode();
                hashCode = (hashCode * 397) ^ (Candidates != null ? Candidates.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(Question other)
        {
            return string.Equals(QuestionId, other.QuestionId) && Point == other.Point &&
                   Equals(Candidates, other.Candidates);
        }
    }
}