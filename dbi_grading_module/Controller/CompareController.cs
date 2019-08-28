using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using dbi_grading_module.Configuration;
using dbi_grading_module.Entity.Candidate;
using dbi_grading_module.Model;
using dbi_grading_module.Utils;
using dbi_grading_module.Utils.Base;

namespace dbi_grading_module.Controller
{
    public class CompareController
    {
        /// <summary>
        ///     Compare 2 databases
        /// </summary>
        /// <param name="dbAnswerName">Student Database Name</param>
        /// <param name="dbSolutionName">Solution Database Name</param>
        /// <param name="dbEmptyName"></param>
        /// <param name="candidate"></param>
        /// <param name="errorMessage"></param>
        /// <returns>
        ///     "true" if correct
        ///     "false" if wrong
        ///     message error from sql server
        /// </returns>
        internal static Dictionary<string, string> CompareSchemaType(string dbAnswerName, string dbSolutionName,
            string dbEmptyName, Candidate candidate, string errorMessage)
        {
            //Prepare query
            var compareQuery = $"exec sp_CompareDb [{dbSolutionName}], [{dbAnswerName}]";
            var countComparisonQuery = $"exec sp_CompareDb [{dbSolutionName}], [{dbEmptyName}]";
            //Comment result
            if (errorMessage == null) throw new Exception("Compare error! Please check connection then run again!");
            var comment = errorMessage;

            //Count tables
            var countAnswerTables = DatabaseConfig.GetNumberOfTablesInDatabase(dbAnswerName);
            var countSolutionTables = DatabaseConfig.GetNumberOfTablesInDatabase(dbSolutionName);

            if (countAnswerTables <= 0)
                return new Dictionary<string, string>
                {
                    {"Point", "0"},
                    {"Comment", comment}
                };
            //List all primary key in 2 databases
            var pkListAnswer = DataTableBase.GetPkInDb(dbAnswerName);
            var pkListSolution = DataTableBase.GetPkInDb(dbSolutionName);

            //Find difference in PK
            var missingPkList = pkListSolution.Except(pkListAnswer).ToList(); //missing
            var redundantPkList = pkListAnswer.Except(pkListSolution).ToList(); //redundant


            //Decrease max point by rate
            //Max point
            var maxPoint = candidate.Point;
            comment += "Count Tables in database: ";
            if (countAnswerTables > countSolutionTables)
            {
                var ratePoint = (double)countSolutionTables / countAnswerTables;
                maxPoint = Math.Round(candidate.Point * ratePoint, 4);
                comment += string.Concat("Answer has more tables than Solution's database (", countAnswerTables, ">",
                    countSolutionTables, ") => Decrease Max Point by ", Math.Round(ratePoint * 100, 4),
                    "% => Max Point = ", maxPoint,
                    "\n");
            }
            else if (countSolutionTables > countAnswerTables)
            {
                comment += string.Concat("Answer has less tables than Solution's database (", countAnswerTables, "<",
                    countSolutionTables, ")\n");
            }
            else
            {
                comment += "Same\n";
            }



            //Count Comparison
            int comparisonOfStructure;
            int comparisonOfConstraints;
            using (DataSet dtsNumOfComparison = DatabaseConfig.GetDataSetFromReader(countComparisonQuery))
            {
                comparisonOfStructure = dtsNumOfComparison.Tables[0].Rows.Count * 2; //A column need both name and type
                comparisonOfConstraints = DatabaseConfig.GetNumberOfConstraintsInDatabase(dbSolutionName) + pkListSolution.Count;
            }

            //Get DataSet compare result
            using (var dtsCompare = DatabaseConfig.GetDataSetFromReader(compareQuery))
            {
                if (dtsCompare == null)
                    throw new Exception("Compare error");
                //Get all errors
                var errorsStructureRows = dtsCompare.Tables[0].AsEnumerable()
                    .Where(myRow => myRow.Field<string>("DATABASENAME").Contains("Solution")); //Structure

                //Constraints
                var constraintsOfSolutionDt = DatabaseConfig.GetConstraintsAsDatatable(dbSolutionName);
                var constraintsOfAnswerDt = DatabaseConfig.GetConstraintsAsDatatable(dbAnswerName);

                var errorsConstraintRows = FindWrongConstraints(GetConstrainModel(constraintsOfSolutionDt), GetConstrainModel(constraintsOfAnswerDt));

                if (!errorsStructureRows.Any() && !errorsConstraintRows.Any())
                {
                    comment = string.Concat("Total Point: ", candidate.Point, "/", candidate.Point, " - Passed\n",
                        comment);

                    return new Dictionary<string, string>
                    {
                        {"Point", Math.Round(maxPoint, 2).ToString()},
                        {"Comment", comment}
                    };
                }

                //Separate Structure errors :
                //Definition not matching
                var defErrors = errorsStructureRows.Where(myRow =>
                    myRow.Field<string>("REASON").Equals("Definition not matching"));
                var defColumnErrors = defErrors.Count(); //minus 1 comparision each error

                //Missing column
                var errorsMissingColumnRows = errorsStructureRows.Where(myRow =>
                    myRow.Field<string>("REASON").Equals("Missing column or Wrong column name") &&
                    myRow.Field<string>("DATABASENAME").Contains("Solution"));
                var missingColumnErrors = errorsMissingColumnRows.Count(); //minus 2 comparison each error
                //
                var trueComparisonStructure = comparisonOfStructure - defColumnErrors - missingColumnErrors * 2;
                if (trueComparisonStructure < 0) trueComparisonStructure = 0;
                var errorsConstraint = errorsConstraintRows.Count() + missingPkList.Count() + redundantPkList.Count();

                var trueComparisonConstraints = comparisonOfConstraints - errorsConstraint;
                if (trueComparisonConstraints < 0) trueComparisonConstraints = 0;

                //Calculate point
                //Point for structure
                var pointForStructure = Math.Round(Grading.RateStructure * maxPoint *
                                                   trueComparisonStructure / comparisonOfStructure, 2);
                //Point for Constraint
                var pointForConstraints =
                    Math.Round(
                        (1 - Grading.RateStructure) * maxPoint * trueComparisonConstraints / comparisonOfConstraints,
                        2);


                var gradePoint = pointForStructure + pointForConstraints;

                comment = $"Total Point: {gradePoint}/{candidate.Point},  - Errors details:\n" + comment;

                comment +=
                    $"\n- Structure - [Point: {pointForStructure}/{maxPoint * Grading.RateStructure}] - [Comparison: {trueComparisonStructure}/{comparisonOfStructure}]:\n";
                //Details
                //About structure
                if (errorsStructureRows.Any())
                {
                    if (defErrors.Any())
                    {
                        var countDefError = 0;
                        comment += $"+ Definition has {defColumnErrors} error(s) (-1 comparison each error):\n";
                        foreach (var rowSolution in defErrors)
                        {
                            comment +=
                                $"{++countDefError}. Required: {rowSolution["TABLENAME"]} ({rowSolution["COLUMNNAME"]}) => {rowSolution["DATATYPE"]}, {rowSolution["NULLABLE"]}\n";

                            var rowAnswer = dtsCompare.Tables[0].AsEnumerable().Where(myRow =>
                                myRow.Field<string>("REASON").Equals("Definition not matching") &&
                                myRow.Field<string>("COLUMNNAME").ToLower()
                                    .Equals(rowSolution["COLUMNNAME"].ToString().ToLower())
                                && myRow.Field<string>("TABLENAME").ToLower()
                                    .Equals(rowSolution["TABLENAME"].ToString().ToLower()) &&
                                myRow.Field<string>("DATABASENAME").Contains("Answer")).ElementAt(0);

                            comment +=
                                $"    Answer  : {rowAnswer["TABLENAME"]} ({rowAnswer["COLUMNNAME"]} => {rowAnswer["DATATYPE"]}, {rowAnswer["NULLABLE"]}\n";
                        }
                    }

                    if (errorsMissingColumnRows.Any())
                    {
                        comment +=
                            $"+ Column(s) missing has {missingColumnErrors} errors(s) (-2 comparison each error):\n";
                        var countMissingError = 0;
                        foreach (var rowSolution in errorsMissingColumnRows)
                            comment +=
                                $"{++countMissingError}. {rowSolution["COLUMNNAME"]} ({rowSolution["TABLENAME"]})\n";
                    }
                }


                comment +=
                    $"\n- Constraint - [Point: {pointForConstraints}/{maxPoint * (1 - Grading.RateStructure)}] - [Comparison: {trueComparisonConstraints}/{comparisonOfConstraints}]:\n";

                //About Constraints
                if (errorsConstraintRows.Any())
                {
                    var countReferences = 0;
                    comment += "+ References check  (-1 comparison each error):\n";
                    foreach (var rowSolution in errorsConstraintRows)
                        comment +=
                            $"{++countReferences}. Missing {rowSolution.PrimaryColumnName} ({rowSolution.PrimaryTableName}) - {rowSolution.ForeignColumnName} ({rowSolution.ForeignTableName})\n";
                }

                if (missingPkList.Any())
                {
                    var countMissingPk = 0;
                    comment += "+ Missing Primary Key  (-1 comparison each error): \n";
                    foreach (var element in missingPkList) comment += $"{++countMissingPk}. {element} \n";
                }

                if (redundantPkList.Any())
                {
                    var countRedundantPk = 0;
                    comment += "+ Redundant Primary Key (-1 comparison each error): \n";
                    foreach (var element in redundantPkList) comment += $"{++countRedundantPk}. {element} \n";
                }

                if (gradePoint > maxPoint) gradePoint = maxPoint;
                return new Dictionary<string, string>
                {
                    {"Point", Math.Round(gradePoint, 2).ToString()},
                    {"Comment", comment}
                };
            }
        }

        private static List<ConstraintModel> GetConstrainModel(DataTable constraintDt)
        {
            List<ConstraintModel> constraints = new List<ConstraintModel>();
            foreach (DataRow row in constraintDt.Rows)
            {
                constraints.Add(new ConstraintModel(row["FK_Table"].ToString().ToLower(), row["FK_Column"].ToString().ToLower(),
                    row["PK_Table"].ToString().ToLower(), row["PK_Column"].ToString().ToLower()));
            }
            return constraints;
        }

        private static List<ConstraintModel> FindWrongConstraints(List<ConstraintModel> solutionConstraints, List<ConstraintModel> answerConstraints)
        {
            List<ConstraintModel> wrongs = new List<ConstraintModel>();
            var nameConstraints = new List<string>();

            foreach (var item in answerConstraints)
            {
                nameConstraints.Add(item.Name);
            }
            return solutionConstraints.AsEnumerable().Where(s => !nameConstraints.Contains(s.Name)).ToList();
        }

        /// <summary>
        ///     Compare tables with sort
        /// </summary>
        /// <param name="dbAnswerName"></param>
        /// <param name="dbSolutionName"></param>
        /// <param name="answer"></param>
        /// <param name="candidate"></param>
        /// <returns>
        ///     "true" if correct
        ///     "false" if wrong
        ///     message error from sql server if error
        /// </returns>
        internal static Dictionary<string, string> CompareSelectType(string dbAnswerName, string dbSolutionName,
            string answer, Candidate candidate)
        {
            //Running answer query
            var dataTableAnswer = DatabaseConfig.GetDataTableFromReader("USE [" + dbAnswerName + "];\n" + answer + "");

            //Running Tq 
            var dataTableTq =
                DatabaseConfig.GetDataTableFromReader("USE [" + dbSolutionName + "];\n" + candidate.TestQuery + "");

            //Running Tq 
            var dataTableSolution =
                DatabaseConfig.GetDataTableFromReader("USE [" + dbSolutionName + "];\n" + candidate.Solution + "");

            //Number of testcases
            var numOfTc = 0;
            if (candidate.RequireSort) numOfTc++;
            if (candidate.CheckColumnName) numOfTc++;
            if (candidate.CheckDistinct) numOfTc++;

            //Prepare point for testcases and data
            var dataPoint = numOfTc != 0 ? Math.Round(candidate.Point / 2, 4) : candidate.Point;

            double gradePoint = 0; //Grading Point
            var comment = ""; //Logs

            //Point for each testcase passed
            var tcPoint = numOfTc > 0
                ? Math.Round(candidate.Point / 2 / numOfTc, 4)
                : Math.Round(candidate.Point / 2, 4);

            //Count testcases true
            var tcCount = 0;

            //Format column name
            DataTableBase.LowerCaseColumnName(dataTableAnswer);
            DataTableBase.LowerCaseColumnName(dataTableSolution);

            //STARTING FOR GRADING
            comment += "- Check Data: ";
            try
            {
                if (CompareDataTableUtils.CompareData(dataTableAnswer.Copy(), dataTableTq.Copy()))
                {
                    gradePoint += dataPoint;
                    comment += string.Concat("Passed => +", dataPoint, "\n");

                    //1. Check if distinct is required
                    if (candidate.CheckDistinct)
                    {
                        comment += "- Check distinct: ";
                        //Compare number of rows
                        if (dataTableSolution.Rows.Count == dataTableAnswer.Rows.Count)
                        {
                            tcCount++;
                            comment += string.Concat("Passed => +", tcPoint, "\n");
                        }
                        else
                        {
                            comment += "Not pass\n";
                        }
                    }

                    //2. Check if sort is required
                    if (candidate.RequireSort)
                    {
                        comment += "- Check sort: ";
                        //Compare row by row
                        try
                        {
                            if (CompareDataTableUtils.CompareTwoDataTablesByRow(dataTableAnswer.Copy(),
                               dataTableTq.Copy())
                           )
                            {
                                tcCount++;
                                comment += string.Concat("Passed => +", tcPoint, "\n");
                            }
                            else
                            {
                                comment += "Not pass\n";
                            }
                        }
                        catch (Exception e)
                        {
                            comment += $"Not pass {e.Message}\n";
                        }

                    }

                    //3. Check if checkColumnName is required
                    if (candidate.CheckColumnName)
                    {
                        comment += "- Check Columns Name: ";
                        var resCompareColumnName =
                            CompareDataTableUtils.CompareColumnsName(dataTableAnswer, dataTableTq);
                        if (resCompareColumnName.Equals(""))
                        {
                            tcCount++;
                            comment += string.Concat("Passed => +", tcPoint, "\n");
                        }
                        else
                        {
                            comment += string.Concat(resCompareColumnName, "\n");
                        }
                    }
                }
                else
                {
                    comment += "Not pass => Point = 0\nStop checking\n";
                }
            }
            catch (Exception e)
            {
                comment += e.Message + " => Point = 0";
                return new Dictionary<string, string>
                {
                    {"Point", 0.ToString()},
                    {"Comment", comment}
                };
            }


            //Calculate Point
            if (numOfTc > 0 && numOfTc == tcCount)
                gradePoint = candidate.Point;
            else
                gradePoint = Math.Round(tcCount * tcPoint + gradePoint, 4);
            if (gradePoint > candidate.Point) gradePoint = Math.Round(candidate.Point, 4);
            comment = string.Concat("Total Point: ", gradePoint, "/", candidate.Point, "\n", comment);
            return new Dictionary<string, string>
            {
                {"Point", Math.Round(gradePoint, 2).ToString()},
                {"Comment", comment}
            };
        }

        /// <summary>
        ///     SP/Trigger/Dml Type
        /// </summary>
        /// <param name="dbAnswerName">DB Name to check student query</param>
        /// <param name="dbSolutionName">DB Name to check teacher query</param>
        /// <param name="candidate">Candidate</param>
        /// <param name="errorMessage"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>
        ///     "true" if correct
        ///     "false" if wrong
        ///     message error from sqlserver if error
        /// </returns>
        internal static Dictionary<string, string> CompareOthersType(string dbAnswerName, string dbSolutionName,
            Candidate candidate, string errorMessage)
        {
            //Get testcases from comment in test query
            var testCases = StringUtils.GetTestCases(candidate.TestQuery, candidate);

            //Init comment
            var comment = errorMessage;
            var countTesting = 0;

            double gradePoint = 0;
            var maxPoint = candidate.Point;
            try
            {
                foreach (var testCase in testCases)
                {
                    comment += string.Concat("TC ", ++countTesting, ": ",
                        testCase.Description.Trim(), " - ");
                    // Prepare query
                    var queryAnswer = "USE [" + dbAnswerName + "] \n" + testCase.TestQuery;
                    var querySolution = "USE [" + dbSolutionName + "] \n" + testCase.TestQuery;

                    // Prepare DataSet  
                    var dataSetAnswer = DatabaseConfig.GetDataSetFromReader(queryAnswer);
                    var dataSetSolution = DatabaseConfig.GetDataSetFromReader(querySolution);

                    if (CompareDataTableUtils.CompareTwoDataSets(dataSetAnswer.Copy(), dataSetSolution.Copy()))
                    {
                        var decreaseRate = (double)dataSetSolution.Tables.Count / dataSetAnswer.Tables.Count;
                        var maxTcPoint = Math.Round(testCase.RatePoint * decreaseRate * candidate.Point, 4);
                        if (dataSetAnswer.Tables.Count > dataSetSolution.Tables.Count)
                        {
                            comment += string.Concat("Answer has more tables than Solution's database (",
                                dataSetAnswer.Tables.Count, ">",
                                dataSetSolution.Tables.Count, ") => Decrease Max Point by ",
                                Math.Round(decreaseRate * 100, 4),
                                "% => Max TC Point = ", maxTcPoint,
                                "\n");
                            countTesting--;
                        }

                        gradePoint += maxTcPoint;
                        comment += string.Concat("Passed => +", maxTcPoint, "\n");
                    }
                    else
                    {
                        comment += "Not pass\n";
                    }
                }
            }
            catch (Exception e)
            {
                comment += e.Message + "\n";
            }

            comment = string.Concat("Total Point: ", gradePoint, "/", candidate.Point, "\n", comment);
            if (gradePoint > maxPoint) gradePoint = maxPoint;
            return new Dictionary<string, string>
            {
                {"Point", Math.Round(gradePoint, 2).ToString()},
                {"Comment", comment}
            };
        }
    }
}