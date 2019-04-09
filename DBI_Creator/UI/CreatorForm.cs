using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using dbi_grading_module.Entity.Paper;
using dbi_grading_module.Entity.Question;
using DBI202_Creator.Commons;
using DBI202_Creator.Model;
using DBI202_Creator.Properties;
using DBI202_Creator.UI.CandidateUI;
using DBI202_Creator.UI.ExportUI;
using DBI202_Creator.Utils;
using DBI202_Creator.Utils.OfficeUtils;

namespace DBI202_Creator.UI
{
    public partial class CreatorForm : Form
    {
        private List<Question> _questions;
        private QuestionSet _questionSet = new QuestionSet();

        public CreatorForm()
        {
            InitializeComponent();

            _questions = _questionSet.QuestionList;
        }

        // Add Question - New Tab.
        private void AddQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var q = new Question
            {
                QuestionId = Guid.NewGuid().ToString(),
                Point = 1
            };
            _questions.Add(q);
            AddQuestionTab(q);
        }

        private void AddQuestionBtn_Click(object sender, EventArgs e)
        {
            var q = new Question
            {
                QuestionId = Guid.NewGuid().ToString(),
                Point = 1
            };
            _questions.Add(q);
            AddQuestionTab(q);

            // Focus newest tab
            questionTabControl.SelectedIndex = questionTabControl.TabPages.Count - 1;
        }

        private void QuestionTabControl_Selected(object sender, TabControlEventArgs e)
        {
            //TabPage currentTab = e.TabPage;
        }

        private bool HandleRemoveQuestion(Question q, TabPage tab)
        {
            _questions.Remove(q);
            questionTabControl.TabPages.Remove(tab);
            PrintQuestionNo();
            return false;
        }

        // Preview entire the Questions List.
        private void PreviewBtn_Click(object sender, EventArgs e)
        {
            PreviewDocUtils.PreviewCandidatePackage(_questionSet);
        }

        private void RemoveQuestionBtn_Click(object sender, EventArgs e)
        {
            var tab = questionTabControl.TabPages[questionTabControl.SelectedIndex];
            var qp = (QuestionPanel) tab.Controls["questionPanel"];

            _questions.Remove(qp.Question);
            questionTabControl.TabPages.Remove(tab);

            PrintQuestionNo();
        }

        // Add question.
        private void AddQuestionTab(Question q)
        {
            var questionPanel = new QuestionPanel(q, HandleRemoveQuestion)
            {
                Dock = DockStyle.Fill,
                Name = "questionPanel"
            };

            var tab = new TabPage(q.QuestionId);
            tab.Controls.Add(questionPanel);

            questionTabControl.TabPages.Add(tab);

            PrintQuestionNo();
        }

        private void Open()
        {
            openFileDialog.Filter = @"Data (*.dat)|*.dat";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    // Clear.
                    _questions.Clear();
                    questionTabControl.TabPages.Clear();

                    // Load data.
                    var localPath = openFileDialog.FileName;
                    _questionSet = SerializeUtils.DeserializeObject<QuestionSet>(localPath);
                    _questions = _questionSet.QuestionList;

                    // Visualization.
                    foreach (var q in _questions)
                        AddQuestionTab(q);
                    MessageBox.Show(Resources.CreatorForm_Open_Info_Open_Successful, @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Resources.CreatorForm_Open_Error, ex.Message), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        // Export to .jon file.
        private void Save()
        {
            saveQuestionSetDialog.Filter = @"Data (*.dat)|*.dat";
            saveQuestionSetDialog.FilterIndex = 2;
            saveQuestionSetDialog.RestoreDirectory = true;
            if (saveQuestionSetDialog.ShowDialog() == DialogResult.OK)
                try
                {
                    var saveFolder = Path.GetDirectoryName(saveQuestionSetDialog.FileName);
                    if (saveFolder != null)
                    {
                        var savePath = Path.Combine(saveFolder, saveQuestionSetDialog.FileName);
                        SerializeUtils.SerializeObject(_questionSet, savePath);
                        MessageBox.Show(string.Format(Resources.CreatorForm_Save_Info_Export_Successful, savePath), @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Resources.CreatorForm_Open_Error, ex.Message), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(@"Save failed." + ex.Message);
                }
        }

        private void PrintQuestionNo()
        {
            for (var i = 0; i < questionTabControl.TabPages.Count; i++)
                questionTabControl.TabPages[i].Text = @"Question " + (i + 1);
        }

        // DrawItem for Vertical TabControl - Question Tabs.
        private void QuestionTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            Brush textBrush = new SolidBrush(Color.Black);
            Font tabFont;

            // Get the item from the collection.
            var tabPage = questionTabControl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            var tabBounds = questionTabControl.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                // Draw a different background color.
                tabFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                g.FillRectangle(Brushes.LightBlue, e.Bounds);
            }
            else
            {
                tabFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                e.DrawBackground();
            }

            // Draw string. Center the text.
            var stringFlags = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(tabPage.Text, tabFont, textBrush, tabBounds, new StringFormat(stringFlags));
        }

        private void ScriptBtn_Click(object sender, EventArgs e)
        {
            var scriptForm = new InputScriptForm(HandleCloseScriptForm, _questionSet.DBScriptList) {Visible = true};
            scriptForm.Show();
        }

        private bool HandleCloseScriptForm(List<string> scripts)
        {
            _questionSet.DBScriptList = scripts;
            return false;
        }

        private void ExportPaperSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                QuestionModel.VerifyQuestionSet(_questionSet);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var exportConfirm = new ExportConfirm(_questionSet);
            //exportConfirm.Visible = true;
            if (Constants.PaperSet != null && Constants.PaperSet.ListPaperMatrixId != null &&
                Constants.PaperSet.ListPaperMatrixId.Count > 0)
            {
                exportConfirm.papersNumberInput.Value = Constants.PaperSet.ListPaperMatrixId.Count;
                exportConfirm.papersNumberInput.Enabled = false;
                exportConfirm.newBtn.Enabled = true;
            }
            else
            {
                exportConfirm.papersNumberInput.Maximum = PaperModel.MaxNumberOfTests(_questionSet.QuestionList);
                exportConfirm.papersNumberInput.Value = exportConfirm.papersNumberInput.Maximum;
                exportConfirm.papersNumberInput.Enabled = true;
                exportConfirm.newBtn.Enabled = false;
            }

            exportConfirm.Show(this);
        }

        private void ImportPaperSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = @"Data (*.dat)|*.dat";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Clear.
                _questions.Clear();
                questionTabControl.TabPages.Clear();

                // Load data.
                var localPath = openFileDialog.FileName;

                try
                {
                    Constants.PaperSet = SerializeUtils.DeserializeObject<PaperSet>(localPath);
                    _questionSet = Constants.PaperSet.QuestionSet;
                    _questions = _questionSet.QuestionList;
                    _questionSet.DBScriptList = Constants.PaperSet.DBScriptList;

                    // Visualization.
                    foreach (var q in _questions)
                        AddQuestionTab(q);
                    MessageBox.Show(Resources.CreatorForm_Open_Info_Open_Successful);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Resources.CreatorForm_ImportPaperSetToolStripMenuItem_Click_Import_Failed, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void VerifySolutionBtn_Click(object sender, EventArgs e)
        {
            var verifyForm = new VerifyForm(_questionSet);
            verifyForm.ShowDialog(this);
        }
    }
}