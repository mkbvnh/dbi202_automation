using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Resources;
using dbi_grading_module.Utils;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace dbi_grading_module.Configuration
{
    public class DatabaseConfig
    {
        internal static DataTable GetConstraintsAsDatatable(string databaseName)
        {
            var query =
                $"USE [{databaseName}]\r\nSELECT RC.CONSTRAINT_NAME FK_Name\r\n, KF.TABLE_SCHEMA FK_Schema\r\n, KF.TABLE_NAME FK_Table\r\n, KF.COLUMN_NAME FK_Column\r\n, RC.UNIQUE_CONSTRAINT_NAME PK_Name\r\n, KP.TABLE_SCHEMA PK_Schema\r\n, KP.TABLE_NAME PK_Table\r\n, KP.COLUMN_NAME PK_Column\r\n, RC.MATCH_OPTION MatchOption\r\n, RC.UPDATE_RULE UpdateRule\r\n, RC.DELETE_RULE DeleteRule\r\nFROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC\r\nJOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KF ON RC.CONSTRAINT_NAME = KF.CONSTRAINT_NAME\r\nJOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KP ON RC.UNIQUE_CONSTRAINT_NAME = KP.CONSTRAINT_NAME";

            var dataTable = new DataTable();
            var builder = Grading.SqlConnectionStringBuilder;
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (var sqlCommandAnswer = new SqlCommand(query, connection))
                {
                    sqlCommandAnswer.CommandTimeout = Grading.TimeOutInSecond;
                    SqlDataReader sqlReaderAnswer;
                    sqlReaderAnswer = sqlCommandAnswer.ExecuteReader();
                    dataTable.Load(sqlReaderAnswer);
                }
                return dataTable;
            }
        }

        public static int GetNumberOfConstraintsInDatabase(string databaseName)
        {
            var query =
                $"USE [{databaseName}]\r\nselect COUNT(*) from\r\n(\r\nSELECT RC.CONSTRAINT_NAME FK_Name\r\n, KF.TABLE_SCHEMA FK_Schema\r\n, KF.TABLE_NAME FK_Table\r\n, KF.COLUMN_NAME FK_Column\r\n, RC.UNIQUE_CONSTRAINT_NAME PK_Name\r\n, KP.TABLE_SCHEMA PK_Schema\r\n, KP.TABLE_NAME PK_Table\r\n, KP.COLUMN_NAME PK_Column\r\n, RC.MATCH_OPTION MatchOption\r\n, RC.UPDATE_RULE UpdateRule\r\n, RC.DELETE_RULE DeleteRule\r\nFROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC\r\nJOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KF ON RC.CONSTRAINT_NAME = KF.CONSTRAINT_NAME\r\nJOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KP ON RC.UNIQUE_CONSTRAINT_NAME = KP.CONSTRAINT_NAME)  x";
            //Prepare connection
            var builder = Grading.SqlConnectionStringBuilder;
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        internal static int GetNumberOfTablesInDatabase(string databaseName)
        {
            var query = string.Concat("USE [", databaseName,
                "]\nSELECT COUNT(*) from information_schema.tables\r\nWHERE table_type = \'base table\'");
            //Prepare connection
            var builder = Grading.SqlConnectionStringBuilder;
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        ///     Get DataSet
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static DataSet GetDataSetFromReader(string query)
        {
            var builder = Grading.SqlConnectionStringBuilder;
            builder.MultipleActiveResultSets = true;
            var dts = new DataSet();
            // Connect to SQL
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                //1. Check number of tables
                try
                {
                    using (var sqlDataAdapter = new SqlDataAdapter(query, connection))
                    {
                        sqlDataAdapter.Fill(dts);
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.Contains(@"Incorrect syntax near 'GO'."))
                    {
                        using (var sqlDataAdapter =
                            new SqlDataAdapter(StringUtils.ReplaceByLine(query, "go", "\n"), connection))
                        {
                            sqlDataAdapter.Fill(dts);
                        }

                        return dts;
                    }

                    throw;
                }
            }

            return dts;
        }

        /// <summary>
        /// Get DataTable
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        ///     datatable[1] as Schema of Table
        ///     datatable[0] as Data of Table
        /// </returns>
        internal static DataTable GetDataTableFromReader(string query)
        {
            var dataTable = new DataTable();
            var builder = Grading.SqlConnectionStringBuilder;
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (var sqlCommandAnswer = new SqlCommand(query, connection))
                {
                    sqlCommandAnswer.CommandTimeout = Grading.TimeOutInSecond;
                    SqlDataReader sqlReaderAnswer;
                    try
                    {
                        sqlReaderAnswer = sqlCommandAnswer.ExecuteReader();
                    }
                    catch (Exception e)
                    {
                        if (e.Message.Contains(@"Incorrect syntax near 'GO'."))
                            using (var sqlCommandAnswerBackup =
                                new SqlCommand(StringUtils.ReplaceByLine(query, "go", "\n"), connection))
                            {
                                dataTable.Load(sqlCommandAnswerBackup.ExecuteReader());
                                return dataTable;
                            }

                        throw;
                    }

                    dataTable.Load(sqlReaderAnswer);
                }

                return dataTable;
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static bool PrepareSpCompareDatabase()
        {
            var rm = new ResourceManager("dbi_grading_module.Properties.Resources", Assembly.GetExecutingAssembly());
            try
            {
                ExecuteSingleQuery("ALTER " + rm.GetString("ProcCompareDb"), "master");
            }
            catch
            {
                // ProcCompareDbsCreate has been created
                ExecuteSingleQuery("CREATE " + rm.GetString("ProcCompareDb"), "master");
            }

            return true;
        }

        public static bool GenerateDatabase(string dbSolutionName, string dbAnswerName, string dbScript)
        {
            try
            {
                if (string.IsNullOrEmpty(dbScript.Trim()))
                    throw new Exception("DbScript for grading is empty!!!\n");

                var queryGenerateAnswerDb = "CREATE DATABASE [" + dbAnswerName + "]\n" +
                                            "GO\n" +
                                            "USE " + "[" + dbAnswerName + "]\n" + dbScript;
                ExecuteSingleQuery(queryGenerateAnswerDb, "master");

                var queryGenerateSolutionDb = "CREATE DATABASE [" + dbSolutionName + "]\n" +
                                              "GO\n" +
                                              "USE " + "[" + dbSolutionName + "]\n" + dbScript;
                ExecuteSingleQuery(queryGenerateSolutionDb, "master");
            }
            catch (Exception e)
            {
                throw new Exception("Generate databases error: " + e.Message + "\n");
            }

            return true;
        }

        /// <summary>
        ///     Drop a Database
        /// </summary>
        /// <param name="dbName">Database need to drop</param>
        /// <returns>
        ///     "message error" if error
        ///     "" if done
        /// </returns>
        internal static bool DropDatabase(string dbName)
        {
            try
            {
                var dropQuery = "DROP DATABASE [" + dbName + "]";
                ExecuteSingleQuery(dropQuery, "master");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static SqlConnectionStringBuilder CheckConnection(string dataSource, string userId, string password,
            string initialCatalog)
        {
            // Save to Grading
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                UserID = userId,
                Password = password,
                InitialCatalog = initialCatalog,
                MaxPoolSize = Grading.MaxConnectionPoolSize,
                ConnectTimeout = Grading.TimeOutInSecond
            };
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                Grading.SqlConnectionStringBuilder = builder;
                return builder;
            }
        }

        /// <summary>
        ///     Execute Single Query
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <param name="catalog"></param>
        public static bool ExecuteSingleQuery(string query, string catalog)
        {
            query = "Use " + "[" + catalog + "];\nGO\n" + query + "";
            using (var connection = new SqlConnection(Grading.SqlConnectionStringBuilder.ConnectionString))
            {
                var server = new Server(new ServerConnection(connection));
                server.ConnectionContext.StatementTimeout = Grading.TimeOutInSecond;
                server.ConnectionContext.Connect();
                try
                {
                    server.ConnectionContext.ExecuteNonQuery(query);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    server.ConnectionContext.ExecuteNonQuery("Use master");
                    server.ConnectionContext.Disconnect();
                }
            }
        }

        /// <summary>
        ///     Drop all database has been created after {afterTime}
        /// </summary>
        /// <param name="afterTime">time start</param>
        /// <returns></returns>
        public static bool DropAllDatabaseCreated(string afterTime)
        {
            var databases = new List<string>();
            var query = $"select name from sys.databases where create_date >= '{afterTime}'";
            using (var connection = new SqlConnection(Grading.SqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand(query, connection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read()) databases.Add(reader.GetString(0));
                    }
                }
            }

            KillAllSessionSql();
            foreach (var dbName in databases) DropDatabase(dbName);
            return true;
        }

        /// <summary>
        ///     Execute scalar query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static object ExecuteScalarQuery(string query)
        {
            using (var connection = new SqlConnection(Grading.SqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand(query, connection))
                {
                    return sqlCommand.ExecuteScalar();
                }
            }
        }

        internal static DataTable ExecuteQueryReader(string query)
        {
            using (var connection = new SqlConnection(Grading.SqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand(query, connection))
                {
                    var dt = new DataTable();
                    dt.Load(sqlCommand.ExecuteReader());
                    return dt;
                }
            }
        }

        /// <summary>
        ///     To kill all session connected to sql server from the tool
        /// </summary>
        public static bool KillAllSessionSql()
        {
            try
            {
                var queryKill = "use master ";
                var builder = new SqlConnectionStringBuilder
                {
                    ConnectTimeout = Grading.TimeOutInSecond,
                    DataSource = Grading.SqlConnectionStringBuilder.DataSource,
                    IntegratedSecurity = true,
                    InitialCatalog = Grading.SqlConnectionStringBuilder.InitialCatalog
                };
                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    var queryGetSession = "SELECT conn.session_id, host_name, program_name,\n" +
                                          "    nt_domain, login_name, connect_time, last_request_end_time \n" +
                                          "FROM sys.dm_exec_sessions AS sess\n" +
                                          "JOIN sys.dm_exec_connections AS conn\n" +
                                          "   ON sess.session_id = conn.session_id\n" +
                                          "   WHERE program_name = '.Net SqlClient Data Provider' " +
                                          "AND login_name = '" + Grading.SqlConnectionStringBuilder.UserID + "'";
                    using (var command = new SqlCommand(queryGetSession, conn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader.GetInt32(0);
                                queryKill += " KILL " + id + " ";
                            }
                        }
                    }

                    using (var command = new SqlCommand(queryKill, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;
        }
    }
}