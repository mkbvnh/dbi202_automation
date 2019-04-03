﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DBI202_Creator.UI.CandidateUI;
using DBI_Grading.Model.Candidate;
using DBI_Grading.Model.Question;

namespace DBI202_Creator.UI
{
    public partial class QuestionPanel : UserControl
    {
        private readonly HandleRemove _handleRemove;

        public QuestionPanel()
        {
            InitializeComponent();
        }

        public QuestionPanel(Question question, Func<Question, TabPage, bool> handleRemove)
        {
            InitializeComponent();
            Question = question;
            _handleRemove = new HandleRemove(handleRemove);

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

        private void RemoveQuestionBtn_Click(object sender, EventArgs e)
        {
            _handleRemove(Question, (TabPage) Parent);
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
            return false;
        }

        private delegate bool HandleRemove(Question q, TabPage tab);
    }
}