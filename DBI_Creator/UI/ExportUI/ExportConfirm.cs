using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DBI202_Creator.Commons;
using DBI202_Creator.Model;
using DBI202_Creator.Utils;
using DBI_Grading.Model.Question;

namespace DBI202_Creator.UI.ExportUI
{
    public partial class ExportConfirm : Form
    {
        private readonly QuestionSet QuestionSet;
        private string FirstPagePath;
        private string OutPutPath;
        private ShufflePaperModel Spm;

        public ExportConfirm(QuestionSet questionSet)
        {
            InitializeComponent();
            QuestionSet = questionSet;

            exportBtn.Visible = true;
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OutPutPath = FileUtils.SaveFileLocation();
                locationTxt.Text = OutPutPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(OutPutPath))
                {
                    MessageBox.Show(@"You need to Browse first!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (var question in QuestionSet.QuestionList)
                foreach (var candidate in question.Candidates)
                    candidate.Point = decimal.ToDouble(question.Point);

                Spm = new ShufflePaperModel(QuestionSet, Convert.ToInt32(papersNumberInput.Value));

                if (!string.IsNullOrEmpty(FirstPagePath))
                    File.Copy(FirstPagePath, @".\firstpage.docx", true);

                //Create Test
                var paperModel = new PaperModel
                {
                    Path = OutPutPath,
                    Spm = Spm,
                    FirstPagePath = @".\firstpage.docx"
                };
                Process.Start(OutPutPath);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Dispose();
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            Constants.PaperSet.ListPaperMatrixId = null;
            papersNumberInput.Enabled = true;
            papersNumberInput.Maximum = PaperModel.MaxNumberOfTests(QuestionSet.QuestionList);
            papersNumberInput.Value = papersNumberInput.Maximum;
            papersNumberInput.Enabled = true;
            newBtn.Enabled = false;
        }

        private void importFirstPageBtn_Click(object sender, EventArgs e)
        {
            FirstPagePath = FileUtils.GetFileLocation(@"Document File|*.docx", @"Select a Document File");
            if (!string.IsNullOrEmpty(FirstPagePath))
            {
                firstPagePathTextBox.Text = FirstPagePath;
            }
        }
    }
}