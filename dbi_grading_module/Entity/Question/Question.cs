using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace dbi_grading_module.Entity.Question
{
    [ExcludeFromCodeCoverage]
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
    }
}