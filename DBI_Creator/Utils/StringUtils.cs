using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DBI202_Creator.Model;
using DBI_Grading.Model.Candidate;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DBI202_Creator.Utils
{
    public static class StringUtils
    {
        public static string FormatSqlCode(string query)
        {
            var parser = new TSql110Parser(false);
            IList<ParseError> errors;
            var parsedQuery = parser.Parse(new StringReader(query), out errors);

            var generator = new Sql110ScriptGenerator(new SqlScriptGeneratorOptions
            {
                KeywordCasing = KeywordCasing.Uppercase,
                IncludeSemicolons = true,
                NewLineBeforeFromClause = true,
                NewLineBeforeOrderByClause = true,
                NewLineBeforeWhereClause = true,
                AlignClauseBodies = false
            });
            string formattedQuery;
            generator.GenerateScript(parsedQuery, out formattedQuery);
            return formattedQuery;
        }

        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetNumbers(this string input)
        {
            while (input.Length > 0 && !char.IsDigit(input[input.Length - 1]))
                input = input.RemoveAt(input.Length - 1);
            var position = input.Length - 1;
            if (position == -1)
                return input;
            while (position != -1)
            {
                position--;
                if (position == -1) break;
                if (!char.IsNumber(input[position]))
                    break;
            }

            return position == -1 ? input : input.Remove(0, position + 1);
        }

        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string RemoveAt(this string s, int index)
        {
            return s.Remove(index, 1);
        }

        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="oldValue">in lowercase</param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceByLine(string input, string oldValue, string newValue)
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

        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        internal static int GetHammingDistance(string s, string t)
        {
            if (s.Length != t.Length)
                throw new Exception("Strings must be equal length");

            var distance =
                s.ToCharArray()
                    .Zip(t.ToCharArray(), (c1, c2) => new {c1, c2})
                    .Count(m => m.c1 != m.c2);

            return distance;
        }

        /// <summary>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="candidate"></param>
        /// <returns></returns>
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