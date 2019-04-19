using System;
using System.Collections.Generic;

namespace dbi_grading_module.Entity.Paper
{
    [Serializable]
    public class Paper
    {
        public string PaperNo { get; set; }
        public List<Candidate.Candidate> CandidateSet { get; set; }
    }
}