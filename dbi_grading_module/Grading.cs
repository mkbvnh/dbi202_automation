using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using dbi_grading_module.Configuration;
using dbi_grading_module.Entity.Candidate;
using dbi_grading_module.Entity.Controller;
using dbi_grading_module.Entity.Paper;
using dbi_grading_module.Utils;

namespace dbi_grading_module
{
    public static class Grading
    {
        public static PaperSet PaperSet;

        //Configure SQL
        public static int TimeOutInSecond = 10;

        public static int MaxConnectionPoolSize = 100;

        // This will be configured in DatabaseConfig.cs when user check connection to sql.
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder;

        //Database Config
        public static StringUtils StringUtils = new StringUtils();

        /// <summary>
        ///     Test Schema of 2 DBs.
        /// </summary>
        /// <param name="candidate">Question and requirement to check</param>
        /// <param name="studentId"></param>
        /// <param name="answer">Answer of student</param>
        /// <param name="questionOrder"></param>
        /// <returns>Result when compare 2 DB</returns>
        /// <exception cref="SqlException">
        ///     When something's wrong, throw exception to log error
        /// </exception>
        public static Dictionary<string, string> SchemaType(Candidate candidate, string studentId, string answer,
            int questionOrder)
        {
            var dbSolutionName = studentId.Replace(" ", "") + questionOrder + "_Solution" + "_" + new Random().Next();
            var dbAnswerName = studentId.Replace(" ", "") + "_" + questionOrder + "_Answer" + "_" + new Random().Next();
            var dbEmptyName = studentId.Replace(" ", "") + questionOrder + "_EmptyDb" + "_" + new Random().Next();
            var querySolution = string.Concat("create database [", dbSolutionName, "]\nGO\nUSE [", dbSolutionName,
                "]\n", candidate.Solution);
            var queryAnswer = string.Concat("create database [", dbAnswerName, "]\nGO\nUSE [", dbAnswerName, "]\n",
                answer);
            var queryEmptyDb = string.Concat("create database [", dbEmptyName, "]");

            try
            {
                var errorMessage = "";
                // Execute query
                try
                {
                    DatabaseConfig.ExecuteSingleQuery(queryAnswer, "master");
                }
                catch (Exception e)
                {
                    //Keep grading instead of errors
                    if (e.InnerException != null)
                        errorMessage = string.Concat("Answer query error: ", e.InnerException.Message, "\n");
                    else
                        errorMessage = string.Concat("Answer query error: ", e.Message, "\n");
                }

                try
                {
                    DatabaseConfig.ExecuteSingleQuery(querySolution, "master");
                    DatabaseConfig.ExecuteSingleQuery(queryEmptyDb, "master");
                }
                catch (Exception e)
                {
                    throw new Exception("Compare error: " + e.Message);
                }

                // Execute query
                return ThreadUtils.WithTimeout(
                    () => CompareController.CompareSchemaType(dbAnswerName, dbSolutionName, dbEmptyName, candidate,
                        errorMessage),
                    TimeOutInSecond);
            }
            finally
            {
                DatabaseConfig.KillAllSessionSql();
                DatabaseConfig.DropDatabase(dbAnswerName);
                DatabaseConfig.DropDatabase(dbSolutionName);
                DatabaseConfig.DropDatabase(dbEmptyName);
            }
        }

        /// <summary>
        ///     Test Simple Query
        /// </summary>
        /// <param name="candidate">Question and requirement to check</param>
        /// <param name="studentId"></param>
        /// <param name="answer">Answer of student</param>
        /// <param name="questionOrder"></param>
        /// <returns>Result when compare 2 table</returns>
        /// <exception cref="Exception">
        ///     When something's wrong, throw exception to log error
        /// </exception>
        public static Dictionary<string, string> SelectType(Candidate candidate, string studentId, string answer,
            int questionOrder)
        {
            var dbSolutionName = studentId.Replace(" ", "") + questionOrder + "_Solution" + "_" + new Random().Next();
            var dbAnswerName = studentId.Replace(" ", "") + questionOrder + "_Answer" + "_" + new Random().Next();

            try
            {
                //Generate 2 new DB for student's answer and solution
                DatabaseConfig.GenerateDatabase(dbSolutionName, dbAnswerName, PaperSet.DBScriptList[1]);
                //Compare
                return ThreadUtils.WithTimeout(
                    () => CompareController.CompareSelectType(dbAnswerName, dbSolutionName, answer, candidate),
                    TimeOutInSecond);
            }
            finally
            {
                DatabaseConfig.KillAllSessionSql();
                DatabaseConfig.DropDatabase(dbSolutionName);
                DatabaseConfig.DropDatabase(dbAnswerName);
            }
        }

        /// <summary>
        ///     Execute Query and compare 2 effected tables
        /// </summary>
        /// <param name="candidate">Question and requirement to check</param>
        /// <param name="studentId"></param>
        /// <param name="answer">Answer of student</param>
        /// <param name="questionOrder"></param>
        /// <returns>Result when compare 2 result tables</returns>
        /// <exception cref="Exception">
        ///     When something's wrong, throw exception to log
        /// </exception>
        public static Dictionary<string, string> OthersType(Candidate candidate, string studentId,
            string answer, int questionOrder)
        {
            var dbSolutionName = studentId.Replace(" ", "") + questionOrder + "_Solution" + "_" + new Random().Next();
            var dbAnswerName = studentId.Replace(" ", "") + questionOrder + "_Answer" + "_" + new Random().Next();

            //Generate 2 new DB for student's answer and solution
            DatabaseConfig.GenerateDatabase(dbSolutionName, dbAnswerName, PaperSet.DBScriptList[1]);
            try
            {
                var errorMessage = "";
                // Execute query
                try
                {
                    DatabaseConfig.ExecuteSingleQuery(answer, dbAnswerName);
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        errorMessage += string.Concat("Answer query error: ", e.InnerException.Message, "\n");
                    else errorMessage += string.Concat("Answer query error: " + e.Message, "\n");
                    //Still grading for student even error
                    //Student still right at some testcase, need to keep grading
                }

                try
                {
                    DatabaseConfig.ExecuteSingleQuery(candidate.Solution, dbSolutionName);
                }
                catch (Exception e)
                {
                    if (e.InnerException != null) throw new Exception("Compare error: " + e.InnerException.Message);
                    throw new Exception("Compare error: " + e.Message);
                }

                return ThreadUtils.WithTimeout(
                    () => CompareController.CompareOthersType(dbAnswerName, dbSolutionName, candidate, errorMessage),
                    TimeOutInSecond);
            }
            finally
            {
                DatabaseConfig.KillAllSessionSql();
                DatabaseConfig.DropDatabase(dbSolutionName);
                DatabaseConfig.DropDatabase(dbAnswerName);
            }
        }
    }
}