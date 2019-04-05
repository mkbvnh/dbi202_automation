﻿using System;
using System.Windows.Forms;
using dbi_grading_module.Entity;

namespace DBI202_Creator.UI.CandidateUI
{
    public partial class TestCaseDialog : Form
    {
        public delegate bool HandleInsert(TestCase tc);

        private readonly HandleInsert _handleInsert;

        private readonly TestCase _testCase;

        public TestCaseDialog(TestCase testCase, HandleInsert handleInsert)
        {
            InitializeComponent();
            _testCase = testCase;
            _handleInsert = handleInsert;

            testCaseTxt.DataBindings.Add("Text", _testCase, "TestQuery", true, DataSourceUpdateMode.OnPropertyChanged);
            descriptionTxt.DataBindings.Add("Text", _testCase, "Description", true,
                DataSourceUpdateMode.OnPropertyChanged);
            percentageNumericUpDown.DataBindings.Add("Value", _testCase, "RatePoint", true,
                DataSourceUpdateMode.OnPropertyChanged);

            ActiveControl = descriptionTxt;
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_testCase.TestQuery.Trim()))
            {
                MessageBox.Show(@"You need to input Test Query!", @"Error");
                return;
            }

            _handleInsert(_testCase);
            Dispose();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}