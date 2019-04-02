using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using DBI202_Creator.Commons;
using DBI202_Creator.Entities.Paper;
using DBI202_Creator.Entities.Question;
using DBI202_Creator.Model;
using DBI202_Creator.UI.CandidateUI;
using DBI202_Creator.UI.ExportUI;
using DBI202_Creator.Utils;
using DBI202_Creator.Utils.OfficeUtils;

namespace DBI202_Creator.UI
{
    public partial class CreatorForm : Form
    {
        private List<Question> questions;
        private QuestionSet questionSet = new QuestionSet();

        public CreatorForm()
        {
            InitializeComponent();

            questions = questionSet.QuestionList;
        }

        // Add Question - New Tab.
        private void addQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var q = new Question();
            q.QuestionId = Guid.NewGuid().ToString();
            q.Point = 1;
            questions.Add(q);
            addQuestionTab(q);
        }

        private void addQuestionBtn_Click(object sender, EventArgs e)
        {
            var q = new Question();
            q.QuestionId = Guid.NewGuid().ToString();
            q.Point = 1;
            questions.Add(q);
            addQuestionTab(q);

            // Focus newest tab
            questionTabControl.SelectedIndex = questionTabControl.TabPages.Count - 1;
        }

        private void questionTabControl_Selected(object sender, TabControlEventArgs e)
        {
            //TabPage currentTab = e.TabPage;
        }

        private bool handleRemoveQuestion(Question q, TabPage tab)
        {
            questions.Remove(q);
            questionTabControl.TabPages.Remove(tab);
            printQuestionNo();
            return false;
        }

        // Preview entire the Questions List.
        private void previewBtn_Click(object sender, EventArgs e)
        {
            PreviewDocUtils.PreviewCandidatePackage(questionSet);
        }

        private void removeQuestionBtn_Click(object sender, EventArgs e)
        {
            var tab = questionTabControl.TabPages[questionTabControl.SelectedIndex];
            var qp = (QuestionPanel)tab.Controls["questionPanel"];

            questions.Remove(qp.question);
            questionTabControl.TabPages.Remove(tab);

            printQuestionNo();
        }

        // Add question.
        private void addQuestionTab(Question q)
        {
            var questionPanel = new QuestionPanel(q, handleRemoveQuestion);
            questionPanel.Dock = DockStyle.Fill;
            questionPanel.Name = "questionPanel";

            var tab = new TabPage(q.QuestionId);
            tab.Controls.Add(questionPanel);

            questionTabControl.TabPages.Add(tab);

            printQuestionNo();
        }

        private void open()
        {
            openFileDialog.Filter = "Data (*.dat)|*.dat";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Clear.
                    questions.Clear();
                    questionTabControl.TabPages.Clear();

                    // Load data.
                    var localPath = openFileDialog.FileName;
                    var set = SerializeUtils.DeserializeObject(localPath);
                    questionSet = set;
                    questions = questionSet.QuestionList;

                    // Visualization.
                    foreach (var q in questions)
                        addQuestionTab(q);
                    MessageBox.Show("Open question set successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Open question set faield.\n" + ex.Message);
                }
            }
        }

        // Export to .jon file.
        private void save()
        {
            saveQuestionSetDialog.Filter = "Data (*.dat)|*.dat";
            saveQuestionSetDialog.FilterIndex = 2;
            saveQuestionSetDialog.RestoreDirectory = true;
            if (saveQuestionSetDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var saveFolder = Path.GetDirectoryName(saveQuestionSetDialog.FileName);
                    var savePath = Path.Combine(saveFolder, saveQuestionSetDialog.FileName);
                    SerializeUtils.SerializeObject(questionSet, savePath);
                    MessageBox.Show("Export data successfully to " + savePath, "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save failed.\n" + ex.Message);
                }
            }
        }

        private void printQuestionNo()
        {
            for (var i = 0; i < questionTabControl.TabPages.Count; i++)
                questionTabControl.TabPages[i].Text = "Question " + (i + 1);
        }

        // DrawItem for Vertical TabControl - Question Tabs.
        private void questionTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            Brush _textBrush = new SolidBrush(Color.Black);
            Font _tabFont;

            // Get the item from the collection.
            var _tabPage = questionTabControl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            var _tabBounds = questionTabControl.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                // Draw a different background color.
                _tabFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                g.FillRectangle(Brushes.LightBlue, e.Bounds);
            }
            else
            {
                _tabFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                e.DrawBackground();
            }

            // Draw string. Center the text.
            var _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void scriptBtn_Click(object sender, EventArgs e)
        {
            var scriptForm = new InputScriptForm(handleCloseScriptForm, questionSet.DBScriptList);

            scriptForm.Visible = true;
            scriptForm.Show();
        }

        private bool handleCloseScriptForm(List<string> scripts)
        {
            questionSet.DBScriptList = scripts;
            return false;
        }

        private void exportPaperSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var exportConfirm = new ExportConfirm(questionSet);
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
                exportConfirm.papersNumberInput.Maximum = PaperModel.MaxNumberOfTests(questionSet.QuestionList);
                exportConfirm.papersNumberInput.Value = exportConfirm.papersNumberInput.Maximum;
                exportConfirm.papersNumberInput.Enabled = true;
                exportConfirm.newBtn.Enabled = false;
            }
            exportConfirm.Show(this);
        }

        private void importPaperSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Data (*.dat)|*.dat";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Clear.
                questions.Clear();
                questionTabControl.TabPages.Clear();

                // Load data.
                var localPath = openFileDialog.FileName;

                try
                {
                    using (var stream = new FileStream(localPath, FileMode.Open, FileAccess.Read))
                    {
                        var formatter = new BinaryFormatter();
                        Constants.PaperSet = (PaperSet)formatter.Deserialize(stream);
                        questionSet = Constants.PaperSet.QuestionSet;
                        questions = questionSet.QuestionList;
                        questionSet.DBScriptList = Constants.PaperSet.DBScriptList;

                        // Visualization.
                        foreach (var q in questions)
                            addQuestionTab(q);
                        MessageBox.Show("Import data successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import data failed.\n" + ex.Message);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }
    }
}