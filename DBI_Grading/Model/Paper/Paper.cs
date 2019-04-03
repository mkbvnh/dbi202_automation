﻿using System;
using System.Collections.Generic;

namespace DBI_Grading.Model.Paper
{
    [Serializable]
    public class Paper
    {
        public Paper(string paperNo, List<Candidate.Candidate> candidateSet)
        {
            PaperNo = paperNo;
            CandidateSet = candidateSet;
        }

        public Paper()
        {
        }

        public string PaperNo { get; set; }
        public List<Candidate.Candidate> CandidateSet { get; set; }
    }
}