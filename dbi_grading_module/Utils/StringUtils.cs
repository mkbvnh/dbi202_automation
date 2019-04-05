using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using dbi_grading_module.Entity.Candidate;
using DBI202_Creator.Model;

namespace dbi_grading_module.Utils
{
    internal class StringUtils
    {
        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="oldValue">in lowercase</param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        internal static string ReplaceByLine(string input, string oldValue, string newValue)
        {
            var list = input.Split('\n');
            var output = "";
            for (var i = 0; i < list.Length; i++)
            {
                var tmp = Regex.Replace(list[i], @"\s+", "");
                if (tmp.ToLower().Equals(oldValue))
                    list[i] = newValue;
                output = string.Concat(output, "\n", list[i]);
            }

            return output;
        }

        public static List<TestCase> GetTestCases(string input, Candidate candidate)
        {
            var matchPoint = Regex.Match(input, @"(/\*(.|[\r\n])*?\*/)|(--(.*|[\r\n]))",
                RegexOptions.Singleline);
            var matchQuery = Regex.Match(input + "/*", @"(\*/(.|[\r\n])*?/\*)|(--(.*|[\r\n]))",
                RegexOptions.Multiline);
            var queryList = new List<string>();
            while (matchQuery.Success)
            {
                queryList.Add(matchQuery.Value.Split('/')[1].Trim());
                matchQuery = matchQuery.NextMatch();
            }

            var tcpList = new List<TestCase>();
            var count = 0;
            var tcp = new TestCase();
            while (matchPoint.Success)
            {
                var matchFormatted = matchPoint.Value.Split('*')[1];
                if (count++ % 2 == 0)
                {
                    tcp.RatePoint = double.Parse(matchFormatted, CultureInfo.InvariantCulture);
                }
                else
                {
                    tcp.Description = matchFormatted;
                    tcp.TestQuery = queryList.ElementAt(count - 1);
                    tcpList.Add(tcp);
                    tcp = new TestCase();
                }

                matchPoint = matchPoint.NextMatch();
            }

            if (tcpList.Count == 0)
                tcpList.Add(new TestCase
                {
                    TestQuery = input,
                    Description = "",
                    RatePoint = candidate.Point
                });
            return tcpList;
        }
    }
}