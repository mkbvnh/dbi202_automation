﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using dbi_grading_module.Entity.Question;
using DBI202_Creator.Commons;
using DBI202_Creator.Model;
using DBI202_Creator.Properties;
using DBI202_Creator.Utils;

namespace DBI202_Creator.UI.ExportUI
{
    public partial class ExportConfirm : Form
    {
        private readonly QuestionSet _questionSet;
        private string _firstPagePath;
        private string _outPutPath;
        private ShufflePaperModel _spm;

        public ExportConfirm(QuestionSet questionSet)
        {
            InitializeComponent();
            _questionSet = questionSet;

            exportBtn.Visible = true;
        }

        [ExcludeFromCodeCoverage]
        public void browseBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _outPutPath = FileUtils.SaveFileLocation();
                locationTxt.Text = _outPutPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [ExcludeFromCodeCoverage]
        private void exportBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_outPutPath))
                {
                    MessageBox.Show(Resources.ExportConfirm_exportBtn_Click_Error_Location_First, @"Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                foreach (var question in _questionSet.QuestionList)
                foreach (var candidate in question.Candidates)
                    candidate.Point = decimal.ToDouble(question.Point);

                _spm = new ShufflePaperModel(_questionSet, Convert.ToInt32(papersNumberInput.Value));

                if (!string.IsNullOrEmpty(_firstPagePath))
                    File.Copy(_firstPagePath, @".\firstpage.docx", true);

                //Create Test
                var paperModel = new PaperModel
                {
                    Path = _outPutPath,
                    Spm = _spm,
                    FirstPagePath = Environment.CurrentDirectory + @"\firstpage.docx"
                };
                Process.Start(_outPutPath);
                paperModel.CreatePaperDat();

                if (DocCheckBox.Checked)
                    using (var progress = new ProgressBarForm(paperModel.CreateAll))
                    {
                        progress.ShowDialog(this);
                    }

                File.Delete(paperModel.FirstPagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Dispose();
        }
        
        [ExcludeFromCodeCoverage]
        public void newBtn_Click(object sender, EventArgs e)
        {
            Constants.PaperSet.ListPaperMatrixId = null;
            papersNumberInput.Enabled = true;
            papersNumberInput.Maximum = PaperModel.MaxNumberOfTests(_questionSet.QuestionList);
            papersNumberInput.Value = papersNumberInput.Maximum;
            papersNumberInput.Enabled = true;
            newBtn.Enabled = false;
        }

        [ExcludeFromCodeCoverage]
        private void importFirstPageBtn_Click(object sender, EventArgs e)
        {
            _firstPagePath = FileUtils.GetFileLocation(@"Document File|*.docx", @"Select a Document File");
            if (!string.IsNullOrEmpty(_firstPagePath))
                firstPagePathTextBox.Text = _firstPagePath;
        }
    }
}