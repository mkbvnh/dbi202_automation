using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DBI202_Creator.Model;
using DBI202_Creator.Utils.Grading.Dao;
using DBI_Grading.Model.Question;

namespace DBI202_Creator.UI
{
    public partial class VerifyForm : Form
    {
        private readonly QuestionSet _questionSet;
        private SqlConnectionStringBuilder _builder;

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
            _builder = General.CheckConnection(serverNameTextBox.Text, usernameTextBox.Text, passwordTextBox.Text,
                "master");
            if (_builder != null && General.PrepareSpCompareDatabase(_builder))
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

        private void StartBtn_Click(object sender, EventArgs e)
        {
            verifyText.Text = "";
            if (_questionSet.DBScriptList.Count < 2 || string.IsNullOrEmpty(_questionSet.DBScriptList[1]))
            {
                MessageBox.Show(this, @"Please add Database Script for Grading", @"Error");
                return;
            }

            if (Regex.Replace(_questionSet.DBScriptList[1], @"\s+", "").ToLower().Contains("createdatabase"))
            {
                MessageBox.Show(this,
                    @"Database Script for Grading contains CREATE DATABASE\nDatabase for Grading should not contain CREATE DATABASE and USE!!!",
                    @"Error");
                return;
            }

            var result = new Result(_questionSet, _builder, this);
            var getPointThread = new Thread(result.GetPoint);
            getPointThread.Start();
        }
    }
}