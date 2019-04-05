using System;
using System.Collections.Generic;

namespace dbi_grading_module.Entity.Paper
{
    [Serializable]
    public class Paper
    {
        public Paper(string paperNo, List<Candidate.Candidate> candidateSet)
        {
            PaperNo = paperNo;
            CandidateSet = candidateSet;
        }

        public string PaperNo { get; set; }
        public List<Candidate.Candidate> CandidateSet { get; set; }
    }
}