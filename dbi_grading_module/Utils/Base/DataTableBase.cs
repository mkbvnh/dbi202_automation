using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using dbi_grading_module.Configuration;

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
        /// <param name="table1"></param>
        /// <param name="table2"></param>
        /// <returns></returns>
        internal static bool CompareTwoDataTablesByExceptOneDirection(DataTable table1,
            DataTable table2)
        {
            return !table1.AsEnumerable().Except(table2.AsEnumerable(), DataRowComparer.Default)
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

        internal static List<string> GetPkInDb(string dbName)
        {
            var query =
                $"USE [{dbName}]; \r\nSELECT OBJECT_NAME(ic.OBJECT_ID) AS TableName, \r\n       COL_NAME(ic.OBJECT_ID,ic.column_id) AS ColumnName\r\nFROM sys.indexes AS i\r\nINNER JOIN sys.index_columns AS ic\r\nON i.OBJECT_ID = ic.OBJECT_ID\r\nAND i.index_id = ic.index_id\r\nWHERE i.is_primary_key = 1";
            var dt = DatabaseConfig.ExecuteQueryReader(query);
            var pkList = new List<string>();
            foreach (DataRow row in dt.Rows) pkList.Add($"{FirstCharToUpper(row["ColumnName"].ToString())} ({FirstCharToUpper(row["TableName"].ToString())})");
            return pkList;
        }

        internal static string FirstCharToUpper(string s)
        {
            // Check for empty string.  
            if (String.IsNullOrEmpty(s))
            {
                return String.Empty;
            }
            // Return char and concat substring.  
            return Char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}