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
            return $"\n/*{RatePoint / 100}*/\n/*{Description}*/\n{TestQuery}".Replace("\n", Environment.NewLine);
        }
    }
}