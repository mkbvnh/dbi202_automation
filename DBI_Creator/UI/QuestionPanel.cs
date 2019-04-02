using System;
using System.Drawing;
using System.Windows.Forms;
using DBI202_Creator.Entities.Candidate;
using DBI202_Creator.Entities.Question;

namespace DBI202_Creator.UI
{
    public partial class QuestionPanel : UserControl
    {
        private readonly HandleRemove handleRemove;

        public QuestionPanel()
        {
            InitializeComponent();
        }

        public QuestionPanel(Question question, Func<Question, TabPage, bool> _handleRemove)
        {
            InitializeComponent();
            this.question = question;
            handleRemove = new HandleRemove(_handleRemove);

            OnCreate();
        }

        public Question question { get; set; }

        private void OnCreate()
        {
            questionIdTxt.Text = question.QuestionId;
            pointNumeric.DataBindings.Add("Value", question, "Point", true, DataSourceUpdateMode.OnPropertyChanged);

            for (var i = 0; i < question.Candidates.Count; i++)
                AddCandidateTab(question.Candidates[i], "Candidate " + (i + 1));
        }

        private void removeQuestionBtn_Click(object sender, EventArgs e)
        {
            handleRemove(question, (TabPage) Parent);
        }

        private void addCandidateBtn_Click(object sender, EventArgs e)
        {
            var c = new Candidate();
            c.Point = decimal.ToDouble(question.Point);
            c.QuestionId = question.QuestionId;
            c.CandidateId = Guid.NewGuid().ToString();
            c.QuestionType = Candidate.QuestionTypes.Select;

            question.Candidates.Add(c);

            var tabTitle = "Candidate " + question.Candidates.Count;

            AddCandidateTab(c, tabTitle);

            // Focus new candidate tab
            candidateTabControl.SelectedIndex = candidateTabControl.TabCount - 1;
        }

        private void AddCandidateTab(Candidate c, string tabTitle)
        {
            var tp = new TabPage(tabTitle);
            tp.BackColor = SystemColors.Control;

            var candidatePanel = new CandidatePanel(c, handleDeleteCandidate);
            candidatePanel.BackColor = SystemColors.Control;
            tp.Controls.Add(candidatePanel);
            candidateTabControl.TabPages.Add(tp);
        }

        private bool handleDeleteCandidate(Candidate c, TabPage tab)
        {
            question.Candidates.Remove(c);
            candidateTabControl.TabPages.Remove(tab);
            return false;
        }

        private delegate bool HandleRemove(Question q, TabPage tab);
    }
}