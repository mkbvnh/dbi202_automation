using System;

namespace DBI202_Creator.Model
{
    public class TestCase
    {
        public double RatePoint { get; set; }
        public string Description { get; set; }
        public string TestQuery { get; set; }

        public override string ToString()
        {
            return string.Format("\n/*{0}*/\n/*{1}*/\n{2}", RatePoint / 100, Description, TestQuery)
                .Replace("\n", Environment.NewLine);
        }
    }
}