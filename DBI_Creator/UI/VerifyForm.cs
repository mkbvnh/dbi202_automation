using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using DBI202_Creator.Entities.Question;
using DBI202_Creator.Utils.Grading;
using DBI202_Creator.Utils.Grading.Utils.Dao;

namespace DBI202_Creator.UI
{
    public partial class VerifyForm : Form
    {
        private SqlConnectionStringBuilder Builder;
        private readonly QuestionSet QuestionSet;

        public VerifyForm(QuestionSet questionSet)
        {
            InitializeComponent();
            QuestionSet = questionSet;
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
            var result = new Result(QuestionSet, Builder, verifyText);
            result.GetPoint();
        }
    }
}