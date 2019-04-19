using System;
using System.Drawing;
using System.Windows.Forms;
using dbi_grading_module.Entity.Candidate;
using dbi_grading_module.Entity.Question;
using DBI202_Creator.UI.CandidateUI;

namespace DBI202_Creator.UI
{
    public partial class QuestionPanel : UserControl
    {
        public QuestionPanel(Question question, Func<Question, TabPage, bool> handleRemove)
        {
            InitializeComponent();
            Question = question;
            OnCreate();
        }

        public Question Question { get; set; }

        private void OnCreate()
        {
            questionIdTxt.Text = Question.QuestionId;
            pointNumeric.DataBindings.Add("Value", Question, "Point", true, DataSourceUpdateMode.OnPropertyChanged);

            for (var i = 0; i < Question.Candidates.Count; i++)
                AddCandidateTab(Question.Candidates[i], "Candidate " + (i + 1));
        }

        private void AddCandidateBtn_Click(object sender, EventArgs e)
        {
            var c = new Candidate
            {
                Point = decimal.ToDouble(Question.Point),
                QuestionId = Question.QuestionId,
                CandidateId = Guid.NewGuid().ToString(),
                QuestionType = Candidate.QuestionTypes.Select
            };

            Question.Candidates.Add(c);

            var tabTitle = "Candidate " + Question.Candidates.Count;

            AddCandidateTab(c, tabTitle);

            // Focus new candidate tab
            candidateTabControl.SelectedIndex = candidateTabControl.TabCount - 1;
        }

        private void AddCandidateTab(Candidate c, string tabTitle)
        {
            var tp = new TabPage(tabTitle) {BackColor = SystemColors.Control};

            var candidatePanel = new CandidatePanel(c, HandleDeleteCandidate) {BackColor = SystemColors.Control};
            tp.Controls.Add(candidatePanel);
            candidateTabControl.TabPages.Add(tp);
        }

        private bool HandleDeleteCandidate(Candidate c, TabPage tab)
        {
            Question.Candidates.Remove(c);
            candidateTabControl.TabPages.Remove(tab);
            RenderTabTitles();
            return false;
        }

        private void RenderTabTitles()
        {
            for (var i = 0; i < candidateTabControl.TabCount; i++)
            {
                candidateTabControl.TabPages[i].Text = "Candidate " + (i + 1);
            }
        }

        private delegate bool HandleRemove(Question q, TabPage tab);
    }
}