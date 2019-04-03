using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DBI202_Creator.Entities.Question;
using DBI202_Creator.Utils.Grading;
using DBI202_Creator.Utils.Grading.Utils.Dao;

namespace DBI202_Creator.UI
{
    public partial class VerifyForm : Form
    {
        private readonly QuestionSet QuestionSet;
        private SqlConnectionStringBuilder Builder;

        public VerifyForm(QuestionSet questionSet)
        {
            InitializeComponent();
            QuestionSet = questionSet;
            serverNameTextBox.Text = ConfigurationManager.AppSettings["serverName"];
            usernameTextBox.Text = ConfigurationManager.AppSettings["username"];
            passwordTextBox.Text = ConfigurationManager.AppSettings["password"];
        }

        private void checkConnectionButton_Click(object sender, EventArgs e)
        {
            Builder = General.CheckConnection(serverNameTextBox.Text, usernameTextBox.Text, passwordTextBox.Text,
                "master");
            if (Builder != null)
            {
                startBtn.Enabled = true;
                checkConnectionButton.Enabled = false;
                serverNameTextBox.Enabled = false;
                usernameTextBox.Enabled = false;
                passwordTextBox.Enabled = false;
            }
            else
            {
                MessageBox.Show(this, @"Cannot connect to Sql Server", @"Error");
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            verifyText.Text = "";
            if (QuestionSet.DBScriptList.Count < 2 || string.IsNullOrEmpty(QuestionSet.DBScriptList[1]))
            {
                MessageBox.Show(this, @"Please add Database Script for Grading", @"Error");
                return;
            }
            if (Regex.Replace(QuestionSet.DBScriptList[1], @"\s+", "").ToLower().Contains("createdatabase"))
            {
                MessageBox.Show(this,
                    @"Database Script for Grading contains CREATE DATABASE\nDatabase for Grading should not contain CREATE DATABASE and USE!!!",
                    @"Error");
                return;
            }
            var result = new Result(QuestionSet, Builder, this);
            var getPointThread = new Thread(result.GetPoint);
            getPointThread.Start();
        }
    }
}