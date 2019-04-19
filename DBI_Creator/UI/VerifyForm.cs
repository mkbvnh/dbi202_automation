using System;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;
using dbi_grading_module.Configuration;
using dbi_grading_module.Entity.Question;
using DBI202_Creator.Model;
using DBI202_Creator.Properties;

namespace DBI202_Creator.UI
{
    public partial class VerifyForm : Form
    {
        private readonly QuestionSet _questionSet;

        public VerifyForm(QuestionSet questionSet)
        {
            InitializeComponent();
            _questionSet = questionSet;
            serverNameTextBox.Text = ConfigurationManager.AppSettings["serverName"];
            usernameTextBox.Text = ConfigurationManager.AppSettings["username"];
            passwordTextBox.Text = ConfigurationManager.AppSettings["password"];
        }

        private void CheckConnectionButton_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseConfig.CheckConnection(serverNameTextBox.Text, usernameTextBox.Text, passwordTextBox.Text,
                    "master");
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["serverName"].Value = serverNameTextBox.Text;
                config.AppSettings.Settings["username"].Value = usernameTextBox.Text;
                config.AppSettings.Settings["password"].Value = passwordTextBox.Text;
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DatabaseConfig.PrepareSpCompareDatabase())
            {
                startBtn.Enabled = true;
                checkConnectionButton.Enabled = false;
                serverNameTextBox.Enabled = false;
                usernameTextBox.Enabled = false;
                passwordTextBox.Enabled = false;
            }
            else
            {
                MessageBox.Show(Resources.VerifyForm_CheckConnectionButton_Click_Error_Connect_Sql, @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void StartBtn_Click(object sender, EventArgs e)
        {
            verifyText.Text = "";
            try
            {
                QuestionModel.VerifyQuestionSet(_questionSet);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string serverDateTime = null;
            try
            {
                //Get server date and time
                serverDateTime = DatabaseConfig.ExecuteScalarQuery(@"SELECT SYSDATETIME()").ToString();
                DatabaseConfig.ExecuteSingleQuery(_questionSet.DBScriptList[0], "master");
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    string.Format(Resources.VerifyForm_StartBtn_Click_Error_DBScript_Student, exception.Message),
                    @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (!string.IsNullOrEmpty(serverDateTime))
                    try
                    {
                        DatabaseConfig.DropAllDatabaseCreated(serverDateTime);
                    }
                    catch
                    {
                        //ignored
                    }
            }

            var dbName = $"CheckDB{new Random().Next()}";
            try
            {
                //Get server date and time
                var query = "CREATE DATABASE [" + dbName + "]\n" +
                            "GO\n" +
                            "USE " + "[" + dbName + "]\n" + _questionSet.DBScriptList[1] + "";
                serverDateTime = DatabaseConfig.ExecuteScalarQuery(@"SELECT SYSDATETIME()").ToString();
                DatabaseConfig.ExecuteSingleQuery(query, "master");
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    string.Format(Resources.VerifyForm_StartBtn_Click_Error_DBScript_Teacher, exception.Message),
                    @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (!string.IsNullOrEmpty(serverDateTime))
                    try
                    {
                        DatabaseConfig.DropAllDatabaseCreated(serverDateTime);
                    }
                    catch
                    {
                        //ignored
                    }
            }

            var result = new Result(_questionSet, this);
            var getPointThread = new Thread(result.GetPoint);
            getPointThread.Start();
        }

        private void VerifyText_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            verifyText.SelectionStart = verifyText.Text.Length;
            // scroll it automatically
            verifyText.ScrollToCaret();
        }
    }
}