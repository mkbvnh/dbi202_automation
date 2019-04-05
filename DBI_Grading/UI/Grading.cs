using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DBI_Grading.Common;
using DBI_Grading.Model;
using DBI_Grading.Utils;
using DBI_Grading.Utils.Dao;

namespace DBI_Grading.UI
{
    public partial class Grading : Form
    {
        private readonly List<Submission> _listSubmissions;
        private readonly string _serverDateTime;
        private int _count;
        private bool _scored;

        public Grading(List<Submission> submisstions)
        {
            InitializeComponent();
            try
            {
                //Get server date and time
                _serverDateTime = General.ExecuteScalarQuery(@"SELECT SYSDATETIME()").ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //cannot run only if cannot connect to the server, then everything will stop here
            }


            // Show Scoring Form and generate Score here
            _listSubmissions = submisstions;
            ListResults = new List<Result>();
            //Prepare();
            SetupUi();

            // Merge Question and submission to ListResults
            foreach (var submission in submisstions)
            {
                var result = new Result
                {
                    // Add PaperNo
                    PaperNo = submission.PaperNo,
                    // Add StudentID
                    StudentId = submission.StudentId
                };

                // Add Answers
                foreach (var answer in submission.ListAnswer)
                    result.ListAnswers.Add(answer);

                // Add Candidates
                foreach (var paper in Constant.PaperSet.Papers)
                    if (paper.PaperNo.Equals(result.PaperNo))
                    {
                        foreach (var candidate in paper.CandidateSet)
                            result.ListCandidates.Add(candidate);
                        break;
                    }

                // Add to List to get score
                ListResults.Add(result);
            }

            Show();
            StartGrading();
        }

        private List<Result> ListResults { get; }

        private void SetupUi()
        {
            // Initialize the DataGridView.
            scoreGridView.AutoGenerateColumns = false;
            // Initialize and add a text box column for StudentID
            scoreGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentID",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            // Initialize and add a text box column for paperNo
            scoreGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PaperNo",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            // Initialize and add a text box column for Total Point
            scoreGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total Point",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
            // Initialize and add a text box column for score of each answer
            for (var i = 0; i < Constant.PaperSet.QuestionSet.QuestionList.Count; i++)
                scoreGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Question " + (i + 1),
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
        }

        /// <summary>
        ///     Setup Min Max Threads in ThreadPool
        ///     Show Point procedure
        /// </summary>
        private void StartGrading()
        {
            // Setup Min Max Threads in ThreadPool
            // This should be 1 because cpu is easy to run but HDD disk can not load over 2 threads in 1 time => wrong mark for student
            // So workerThreads = 1, completionPortThreads = 1;
            int workerThreads = Constant.MaxThreadPoolSize, completionPortThreads = Constant.MaxThreadPoolSize;
            ThreadPool.SetMinThreads(workerThreads, completionPortThreads);
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            ThreadPool.SetMaxThreads(workerThreads, completionPortThreads);

            // Get Point
            if (!_scored)
            {
                // Reset to count how many results has been marked.
                _count = 0;
                // Populate the data source.
                for (var row = 0; row < ListResults.Count; row++)
                {
                    var currentResult = ListResults.ElementAt(row);
                    // Prepare 2 first columns
                    scoreGridView.Invoke((MethodInvoker) (() =>
                    {
                        scoreGridView.Rows.Add(1);
                        scoreGridView.Rows[row].Cells[0].Value = currentResult.StudentId;
                        scoreGridView.Rows[row].Cells[1].Value = currentResult.PaperNo;
                    }));
                    var input = new Input(row, currentResult);
                    ThreadPool.QueueUserWorkItem(callBack => Grade(input));
                }

                _scored = true;
            }
            else
            {
                MessageBox.Show(@"Open question set successfully.");
            }
        }

        private void Grade(Input input)
        {
            input.Result.GetPoint();
            // Refresh to show point and scroll view to the last row
            // Show point of each question
            scoreGridView.Invoke((MethodInvoker) (() =>
            {
                scoreGridView.Rows[input.Row].Cells[2].Value = input.Result.SumOfPoint();
                for (var questionOrder = 0; questionOrder < input.Result.ListCandidates.Count; questionOrder++)
                    scoreGridView.Rows[input.Row].Cells[3 + questionOrder].Value =
                        input.Result.Points[questionOrder].ToString();
            }));
            CountDown();
        }

        /// <summary>
        ///     Handle ThreadPool Completion
        /// </summary>
        private void CountDown()
        {
            _count++;
            if (_count == ListResults.Count)
            {
                //drop any database has created after grading
                General.DropAllDatabaseCreated(_serverDateTime);

                //Enable export button
                exportButton.Invoke((MethodInvoker) (() => { exportButton.Enabled = true; }));

                // Done
                var dialogResult =
                    MessageBox.Show(@"Do you want to export result?", @"Result", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) ExportButton_Click(null, null);
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                double maxPoint = 0;
                foreach (var candidate in ListResults.ElementAt(0).ListCandidates)
                    maxPoint += candidate.Point;
                ExcelUtils.ExportResultsExcel(ListResults, _listSubmissions, maxPoint,
                    ListResults.ElementAt(0).ListCandidates.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error");
            }
        }

        private void Scoring_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    internal class Input
    {
        public Input(int row, Result result)
        {
            Row = row;
            Result = result;
        }

        public Result Result { get; set; }
        public int Row { get; set; }
    }
}