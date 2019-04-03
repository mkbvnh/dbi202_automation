using System.IO;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DBI202_Creator.Utils
{
    internal static class SqlUtils
    {
        public static string FormatSqlCode(string query)
        {
            var parser = new TSql110Parser(false);
            var parsedQuery = parser.Parse(new StringReader(query), out _);

            var generator = new Sql110ScriptGenerator(new SqlScriptGeneratorOptions
            {
                KeywordCasing = KeywordCasing.Uppercase,
                IncludeSemicolons = true,
                NewLineBeforeFromClause = true,
                NewLineBeforeOrderByClause = true,
                NewLineBeforeWhereClause = true,
                AlignClauseBodies = false
            });
            generator.GenerateScript(parsedQuery, out var formattedQuery);
            return formattedQuery;
        }
    }
}