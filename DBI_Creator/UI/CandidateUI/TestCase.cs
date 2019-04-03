using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBI202_Creator.UI.CandidateUI
{
    public class TestCase
    {
        public double RatePoint { get; set; }
        public string Description { get; set; }
        public string TestQuery { get; set; }

        public override string ToString()
        {
            return String.Format("/*{0}*/\n/*{1}*/\n{2}\n", RatePoint/100, Description, TestQuery).Replace("\n", Environment.NewLine);
        }
    }
}
