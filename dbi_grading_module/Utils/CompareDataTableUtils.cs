using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using dbi_grading_module.Utils.Base;

namespace dbi_grading_module.Utils
{
    internal class CompareDataTableUtils
    {
        /// <summary>
        ///     Check data
        /// </summary>
        /// <param name="dataTableAnswer"></param>
        /// <param name="dataTableTq"></param>
        /// <returns>
        ///     true = same
        /// </returns>
        internal static bool CompareData(DataTable dataTableAnswer, DataTable dataTableTq)
        {
            //Get First Column from TQ
            var firstColumnName = dataTableTq.Columns[0].ColumnName;

            //Sort Column Name
            DataTableBase.SortColumnNameTable(dataTableAnswer);
            DataTableBase.SortColumnNameTable(dataTableTq);
            DataTable sortedTableAnswer, sortedTableTq;

            try
            {
                //Sort Data by firstColumn
                sortedTableTq = DataTableBase.SortDataTable(dataTableTq, firstColumnName);
                sortedTableAnswer = DataTableBase.SortDataTable(dataTableAnswer, firstColumnName);
            }
            catch
            {
                throw new Exception("Answer error at column name " + firstColumnName);
            }

            //Distinct
            var distinctTableTq = DataTableBase.DistinctTable(sortedTableTq, DataTableBase.GetColumnsName(dataTableTq));
            var distinctTableAnswer =
                DataTableBase.DistinctTable(sortedTableAnswer, DataTableBase.GetColumnsName(dataTableAnswer));

            //Rotate table
            var rotateTableTq = DataTableBase.RotateTable(distinctTableTq);
            var rotateTableAnswer = DataTableBase.RotateTable(distinctTableAnswer);
            return DataTableBase.CompareTwoDataTablesByExceptOneDirection(rotateTableTq, rotateTableAnswer);
        }

        /// <summary>
        ///     Compare data line by row
        /// </summary>
        /// <param name="dataTableAnswer"></param>
        /// <param name="dataTableSolution"></param>
        /// <returns>
        ///     true = same
        /// </returns>
        internal static bool CompareTwoDataTablesByRow(DataTable dataTableAnswer, DataTable dataTableTq)
        {
            //Sort by column name
            DataTableBase.SortColumnNameTable(dataTableAnswer);
            DataTableBase.SortColumnNameTable(dataTableTq);

            //Distinct
            var distinctTableTq = DataTableBase.DistinctTable(dataTableTq, DataTableBase.GetColumnsName(dataTableTq));
            var distinctTableAnswer =
                DataTableBase.DistinctTable(dataTableAnswer, DataTableBase.GetColumnsName(dataTableAnswer));

            //Rotate table
            var rotateTableTq = DataTableBase.RotateTable(distinctTableTq);
            var rotateTableAnswer = DataTableBase.RotateTable(distinctTableAnswer);
            return DataTableBase.CompareTwoDataTablesByExceptOneDirection(rotateTableTq, rotateTableAnswer);
        }

        /// <summary>
        ///     Compare 2 DataSet
        /// </summary>
        /// <param name="dataSetAnswer"></param>
        /// <param name="dataSetSolution"></param>
        /// <returns></returns>
        internal static bool CompareTwoDataSets(DataSet dataSetAnswer, DataSet dataSetSolution)
        {
            var countComparison = 0;
            foreach (DataTable dataTableSolution in dataSetSolution.Tables)
            {
                DataTableBase.LowerCaseColumnName(dataTableSolution);
                foreach (DataTable dataTableAnswer in dataSetAnswer.Tables)
                {
                    DataTableBase.LowerCaseColumnName(dataTableAnswer);
                    if (CompareData(dataTableAnswer, dataTableSolution))
                        break;
                    countComparison++;
                }

                if (countComparison == dataSetAnswer.Tables.Count)
                    return false;
            }

            return true;
        }

        /// <summary>
        ///     Compare Columns Name of tables
        /// </summary>
        /// <param name="dataTableAnswer"></param>
        /// <param name="dataTableSolution"></param>
        /// <returns>"(Empty)" if true, "(comment)" if false</returns>
        internal static string CompareColumnsName(DataTable dataTableAnswer, DataTable dataTableSolution)
        {
            List<string> namesAnswer = DataTableBase.GetColumnsName(dataTableAnswer);
            List<string> namesSolution = new List<string>();
            var tmp = DataTableBase.GetColumnsName(dataTableSolution);
            foreach (string name in tmp)
            {
                namesSolution.Add(name.ToLower());
            }
            string[] listExcept = namesSolution.Except(namesAnswer).ToArray();
            if (listExcept.Any())
            {
                return $"Column Name Wrong - {listExcept[0]}";
            }
            return "";
        }
    }
}