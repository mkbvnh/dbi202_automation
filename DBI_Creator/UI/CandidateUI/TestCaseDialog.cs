using System;
using System.Windows.Forms;
using DBI202_Creator.Model;

namespace DBI202_Creator.UI.CandidateUI
{
    public partial class TestCaseDialog : Form
    {
        public delegate bool HandleInsert(TestCase tc);

        private readonly HandleInsert handleInsert;

        private readonly TestCase testCase;

        public TestCaseDialog(TestCase _testCase, HandleInsert _handleInsert)
        {
            InitializeComponent();
            testCase = _testCase;
            handleInsert = _handleInsert;

            testCaseTxt.DataBindings.Add("Text", testCase, "TestQuery", true, DataSourceUpdateMode.OnPropertyChanged);
            descriptionTxt.DataBindings.Add("Text", testCase, "Description", true,
                DataSourceUpdateMode.OnPropertyChanged);
            percentageNumericUpDown.DataBindings.Add("Value", testCase, "RatePoint", true,
                DataSourceUpdateMode.OnPropertyChanged);

            ActiveControl = descriptionTxt;
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(testCase.TestQuery.Trim()))
            {
                MessageBox.Show(@"You need to input Test Query!", @"Error");
                return;
            }
            handleInsert(testCase);
            Dispose();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}