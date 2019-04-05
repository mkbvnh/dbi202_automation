﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

// ReSharper disable SpecifyACultureInStringConversionExplicitly

namespace dbi_grading_module.Utils.Base
{
    internal class CompareDataTable
    {
        /// <summary>
        ///     Check data
        /// </summary>
        /// <param name="dataTableAnswer"></param>
        /// <param name="dataTableTq"></param>
        /// <param name="isRotate"></param>
        /// <returns>
        ///     true = same
        /// </returns>
        internal static bool CompareData(DataTable dataTableAnswer, DataTable dataTableTq)
        {
            //Get First Column from TQ
            var firstColumnName = dataTableTq.Columns[0].ColumnName;

            //Sort Column Name
            SortColumnNameTable(dataTableAnswer);
            SortColumnNameTable(dataTableTq);
            DataTable sortedTableAnswer, sortedTableTq;

            try
            {
                //Sort Data by firstColumn
                sortedTableTq = SortDataTable(dataTableTq, firstColumnName);
                sortedTableAnswer = SortDataTable(dataTableAnswer, firstColumnName);
            }
            catch
            {
                throw new Exception("Answer missing column name " + firstColumnName);
            }

            //Distinct
            var distinctTableTq = DistinctTable(sortedTableTq, GetColumnsName(dataTableTq));
            var distinctTableAnswer = DistinctTable(sortedTableAnswer, GetColumnsName(dataTableAnswer));

            //Rotate table
            var rotateTableTq = RotateTable(distinctTableTq);
            var rotateTableAnswer = RotateTable(distinctTableAnswer);
            return CompareTwoDataTablesByExceptOneDirection(rotateTableAnswer, rotateTableTq);
        }

        /// <summary>
        ///     Compare data line by row
        /// </summary>
        /// <param name="dataTableAnswer"></param>
        /// <param name="dataTableSolution"></param>
        /// <returns>
        ///     true = same
        /// </returns>
        internal static bool CompareTwoDataTablesByRow(DataTable dataTableAnswer, DataTable dataTableSolution)
        {
            //Distinct
            DistinctTable(dataTableAnswer, GetColumnsName(dataTableAnswer));
            DistinctTable(dataTableSolution, GetColumnsName(dataTableSolution));

            //Sort by column name
            SortColumnNameTable(dataTableAnswer);
            SortColumnNameTable(dataTableSolution);

            if (dataTableSolution.Rows.Count != dataTableAnswer.Rows.Count ||
                dataTableSolution.Columns.Count != dataTableAnswer.Columns.Count) return false;
            for (var i = 0; i < dataTableSolution.Rows.Count; i++)
            {
                var rowArraySolution = dataTableSolution.Rows[i].ItemArray;
                var rowArrayAnswer = dataTableAnswer.Rows[i].ItemArray;
                if (!rowArraySolution.SequenceEqual(rowArrayAnswer))
                    return false;
            }

            return true;
        }

        /// <summary>
        ///     Compare 2 DataSet
        /// </summary>
        /// <param name="dataSetAnswer"></param>
        /// <param name="dataSetSolution"></param>
        /// <param name="isRotate"></param>
        /// <returns></returns>
        internal static bool CompareTwoDataSets(DataSet dataSetAnswer, DataSet dataSetSolution)
        {
            var countComparison = 0;
            foreach (DataTable dataTableSolution in dataSetSolution.Tables)
            {
                LowerCaseColumnName(dataTableSolution);
                foreach (DataTable dataTableAnswer in dataSetAnswer.Tables)
                {
                    LowerCaseColumnName(dataTableAnswer);
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
            for (var i = 0; i < dataTableSolution.Columns.Count; i++)
                if (!dataTableSolution.Columns[i].ColumnName.ToLower()
                    .Equals(dataTableAnswer.Columns[i].ColumnName.ToLower()))
                    return "Column Name wrong - " + dataTableSolution.Columns[i].ColumnName;
            return "";
        }

        /// <summary>
        ///     Sort column by column name
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal static DataTable SortColumnNameTable(DataTable dt)
        {
            var columnArray = new DataColumn[dt.Columns.Count];
            dt.Columns.CopyTo(columnArray, 0);
            var ordinal = -1;
            foreach (var orderedColumn in columnArray.OrderBy(c => c.ColumnName))
                orderedColumn.SetOrdinal(++ordinal);
            return dt;
        }

        /// <summary>
        ///     Sort by a column
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        internal static DataTable SortDataTable(DataTable dt, string columnName)
        {
            dt.DefaultView.Sort = columnName;
            return dt.DefaultView.ToTable();
        }

        /// <summary>
        ///     Compare columns name of 2 table data
        /// </summary>
        /// <param name="dtSchemaAnswer"></param>
        /// <param name="dtSchemaTq"></param>
        /// <returns></returns>
        internal static bool CompareColumnName(DataTable dtSchemaAnswer, DataTable dtSchemaTq)
        {
            var columnNameListAnswer = GetColumnsName(dtSchemaAnswer);
            var columnNameListTq = GetColumnsName(dtSchemaTq);
            return !columnNameListTq.Except(columnNameListAnswer).Any();
        }

        /// <summary>
        ///     Get all column name in a list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal static List<string> GetColumnsName(DataTable dt)
        {
            var columnNameList = new List<string>();
            for (var i = 0; i < dt.Columns.Count; i++)
                columnNameList.Add(dt.Columns[i].ColumnName);
            return columnNameList;
        }

        /// <summary>
        ///     Distinct table data
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        internal static DataTable DistinctTable(DataTable dt, List<string> columns)
        {
            var dtUniqRecords = dt.DefaultView.ToTable(true, columns.ToArray());
            return dtUniqRecords;
        }

        /// <summary>
        /// </summary>
        /// <param name="dataTableAnswer"></param>
        /// <param name="dataTableSolution"></param>
        /// <returns></returns>
        internal static bool CompareTwoDataTablesByExceptOneDirection(DataTable dataTableAnswer,
            DataTable dataTableSolution)
        {
            return !dataTableAnswer.AsEnumerable().Except(dataTableSolution.AsEnumerable(), DataRowComparer.Default)
                .Any();
        }

        /// <summary>
        ///     Rotate column and row of a table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal static DataTable RotateTable(DataTable dt)
        {
            var dt2 = new DataTable();
            for (var i = 0; i < dt.Rows.Count; i++)
                dt2.Columns.Add();
            for (var i = 0; i < dt.Columns.Count; i++)
                dt2.Rows.Add();
            for (var i = 0; i < dt.Columns.Count; i++)
                for (var j = 0; j < dt.Rows.Count; j++)
                    dt2.Rows[i][j] = dt.Rows[j][i];
            return dt2;
        }

        /// <summary>
        /// </summary>
        /// <param name="dataTableAnswer"></param>
        /// <param name="dataTableTq"></param>
        /// <returns></returns>
        internal static bool CheckDataTableSort(DataTable dataTableAnswer, DataTable dataTableTq)
        {
            var firstColumnName = dataTableTq.Columns[0].ColumnName;
            return CompareTwoDataTablesByRow(
                SortDataTable(DistinctTable(dataTableAnswer, GetColumnsName(dataTableAnswer)), firstColumnName),
                SortDataTable(DistinctTable(dataTableTq, GetColumnsName(dataTableTq)), firstColumnName));
        }

        /// <summary>
        ///     Lower case all column name
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal static DataTable LowerCaseColumnName(DataTable dt)
        {
            foreach (DataColumn dataColumn in dt.Columns)
                dataColumn.ColumnName = dataColumn.ColumnName.ToLower();
            return dt;
        }
    }
}