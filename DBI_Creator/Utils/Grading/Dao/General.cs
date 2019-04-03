﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Resources;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DBI202_Creator.Utils.Grading.Dao
{
    public partial class General
    {
        /// <summary>
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static int GetNumberOfTablesInDatabase(string databaseName, SqlConnectionStringBuilder builder)
        {
            var query = string.Concat("USE [", databaseName,
                "]\nSELECT COUNT(*) from information_schema.tables\r\nWHERE table_type = \'base table\'");
            //Prepare connection
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    return (int) command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        ///     Get DataSet
        /// </summary>
        /// <param name="query"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static DataSet GetDataSetFromReader(string query, SqlConnectionStringBuilder builder)
        {
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
        ///     Get DataSet
        /// </summary>
        /// <param name="query"></param>
        /// <param name="builder"></param>
        /// <returns>
        ///     datatable[1] as Schema of Table
        ///     datatable[0] as Data of Table
        /// </returns>
        public static DataTable GetDataTableFromReader(string query, SqlConnectionStringBuilder builder)
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (var sqlCommandAnswer = new SqlCommand(query, connection))
                {
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
        public static bool PrepareSpCompareDatabase(SqlConnectionStringBuilder builder)
        {
            var rm = new ResourceManager("DBI202_Creator.Properties.Resources", Assembly.GetExecutingAssembly());
            try
            {
                ExecuteSingleQuery("ALTER " + rm.GetString("ImportMaterialStartCompareDb"), "master", builder);
            }
            catch
            {
                // ProcCompareDbsCreate has been created
                ExecuteSingleQuery("CREATE " + rm.GetString("ImportMaterialStartCompareDb"), "master", builder);
            }
            return true;
        }

        public static void GenerateDatabase(string dbSolutionName, string dbAnswerName, string dbScript,
            SqlConnectionStringBuilder builder)
        {
            try
            {
                if (string.IsNullOrEmpty(dbScript.Trim()))
                    throw new Exception("DbScript for grading is empty!!!\n");

                var queryGenerateAnswerDb = "CREATE DATABASE [" + dbAnswerName + "]\n" +
                                            "GO\n" +
                                            "USE " + "[" + dbAnswerName + "]\n" + dbScript;
                ExecuteSingleQuery(queryGenerateAnswerDb, "master", builder);

                var queryGenerateSolutionDb = "CREATE DATABASE [" + dbSolutionName + "]\n" +
                                              "GO\n" +
                                              "USE " + "[" + dbSolutionName + "]\n" + dbScript;
                ExecuteSingleQuery(queryGenerateSolutionDb, "master", builder);
            }
            catch (Exception e)
            {
                throw new Exception("Generate databases error: " + e.Message + "\n");
            }
        }

        /// <summary>
        ///     Drop a Database
        /// </summary>
        /// <param name="dbName">Database need to drop</param>
        /// <param name="builder"></param>
        /// <returns>
        ///     "message error" if error
        ///     "" if done
        /// </returns>
        public static void DropDatabase(string dbName, SqlConnectionStringBuilder builder)
        {
            try
            {
                var dropQuery = "DROP DATABASE [" + dbName + "]";
                ExecuteSingleQuery(dropQuery, "master", builder);
            }
            catch (Exception)
            {
                // If DB is not exist or some exception here, we let them out.
            }
        }

        public static SqlConnectionStringBuilder CheckConnection(string dataSource, string userId, string password,
            string initialCatalog)
        {
            // Save to constant
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                UserID = userId,
                Password = password,
                InitialCatalog = initialCatalog
            };
            try
            {
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    // Save to app config when login successfully
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["serverName"].Value = dataSource;
                    config.AppSettings.Settings["username"].Value = userId;
                    config.AppSettings.Settings["password"].Value = password;
                    config.AppSettings.Settings["initialCatalog"].Value = initialCatalog;
                    config.Save(ConfigurationSaveMode.Full, true);
                    ConfigurationManager.RefreshSection("appSettings");
                    return builder;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        ///     Execute Single Query
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <param name="catalog"></param>
        /// <param name="builder"></param>
        public static bool ExecuteSingleQuery(string query, string catalog, SqlConnectionStringBuilder builder)
        {
            query = "Use " + "[" + catalog + "];\nGO\n" + query + "";
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                var server = new Server(new ServerConnection(connection));
                server.ConnectionContext.Connect();
                try
                {
                    server.ConnectionContext.ExecuteNonQuery(query);
                    return true;
                }
                finally
                {
                    server.ConnectionContext.ExecuteNonQuery("Use master");
                    server.ConnectionContext.Disconnect();
                }
            }
        }

        /// <summary>
        ///     To kill all session connected to sql server from the tool
        /// </summary>
        public static void KillAllSessionSql()
        {
            try
            {
                var queryKill = "use master ";
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = ConfigurationManager.AppSettings["serverName"],
                    IntegratedSecurity = true,
                    InitialCatalog = ConfigurationManager.AppSettings["initialCatalog"]
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
                                          "AND login_name = '" + ConfigurationManager.AppSettings["username"] + "'";
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
        }
    }
}