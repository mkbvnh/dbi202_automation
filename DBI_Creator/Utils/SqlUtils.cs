using System.Collections.Generic;
using System.IO;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DBI202_Creator.Utils
{
    internal static class SqlUtils
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
    }
}