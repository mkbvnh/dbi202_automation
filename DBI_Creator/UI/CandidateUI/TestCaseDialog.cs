using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBI202_Creator.UI.CandidateUI
{
    public partial class TestCaseDialog : Form
    {
        public delegate bool HandleInsert(TestCase tc);

        private TestCase testCase;
        HandleInsert handleInsert;

        public TestCaseDialog(TestCase _testCase, HandleInsert _handleInsert)
        {
            InitializeComponent();
            testCase = _testCase;
            handleInsert = _handleInsert;

            testCaseTxt.DataBindings.Add("Text", testCase, "TestQuery", true, DataSourceUpdateMode.OnPropertyChanged);
            descriptionTxt.DataBindings.Add("Text", testCase, "Description", true, DataSourceUpdateMode.OnPropertyChanged);
            percentageNumericUpDown.DataBindings.Add("Value", testCase, "RatePoint", true, DataSourceUpdateMode.OnPropertyChanged);

            this.ActiveControl = descriptionTxt;
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(testCase.TestQuery))
            {
                MessageBox.Show("You need to input test case!", "Error");
                return;
            }
            handleInsert(testCase);
            this.Dispose();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
