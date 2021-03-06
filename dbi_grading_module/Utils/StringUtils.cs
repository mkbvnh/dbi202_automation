﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using dbi_grading_module.Entity;
using dbi_grading_module.Entity.Candidate;

namespace dbi_grading_module.Utils
{
    public class StringUtils
    {
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

            output = output.Trim();
            if (output.ToLower().EndsWith(oldValue)) output = output.Remove(output.Length - 2);
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
                queryList.Add(matchQuery.Value.Substring(2).Remove(matchQuery.Value.Length - 4, 2).Trim());
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
                    tcp.RatePoint = double.Parse(matchFormatted.Replace(',', '.'), CultureInfo.InvariantCulture);
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

        internal static Exception GetInnerException(Exception e)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }
            return e;
        }
    }
}