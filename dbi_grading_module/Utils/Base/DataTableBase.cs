using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace dbi_grading_module.Utils.Base
{
    internal class DataTableBase
    {
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