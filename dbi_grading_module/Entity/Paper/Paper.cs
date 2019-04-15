using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace dbi_grading_module.Entity.Paper
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class Paper
    {
        public string PaperNo { get; set; }
        public List<Candidate.Candidate> CandidateSet { get; set; }
    }
}