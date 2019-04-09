using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using dbi_grading_module.Configuration;
using dbi_grading_module.Entity.Paper;
using dbi_grading_module.Properties;
using DBI_Grading.Common;
using DBI_Grading.Model;
using DBI_Grading.Utils;

namespace DBI_Grading.UI
{
    public partial class GradingForm : Form
    {
        private readonly List<Submission> _listSubmissions;
        private readonly PaperSet _paperSet;
        private readonly string _serverDateTime;
        private int _count;
        private bool _scored;


        public GradingForm(List<Submission> submissions, PaperSet paperSet)
        {
            InitializeComponent();
            try
            {
                //Get server date and time
                _serverDateTime = DatabaseConfig.ExecuteScalarQuery(@"SELECT SYSDATETIME()").ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //Cannot connect to the server, then everything will stop here
            }

            //Set PaperSet
            _paperSet = paperSet;

            // Show Scoring Form and generate Score here
            _listSubmissions = submissions;
            ListResults = new List<Result>();
            //Prepare();
            SetupUi();

            // Merge Question and submission to ListResults
            foreach (var submission in submissions)
            {
                var result = new Result(_paperSet, submission)
                {
                    // Add PaperNo
                    PaperNo = submission.PaperNo,
                    // Add StudentID
                    StudentId = submission.StudentId
                };
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
            for (var i = 0; i < _paperSet.QuestionSet.QuestionList.Count; i++)
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
                MessageBox.Show(Resources.GradingForm_StartGrading_Import_Successfully, @"Successfully",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                for (var questionOrder = 0; questionOrder < input.Result.Paper.CandidateSet.Count; questionOrder++)
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
                DatabaseConfig.DropAllDatabaseCreated(_serverDateTime);

                //Enable export button
                exportButton.Invoke((MethodInvoker) (() => { exportButton.Enabled = true; }));

                // Done
                var dialogResult =
                    MessageBox.Show(Resources.GradingForm_Result_Export_Question, @"Result", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes) ExportButton_Click(null, null);
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            try
            {
                double maxPoint = 0;
                foreach (var candidate in _paperSet.Papers[0].CandidateSet) maxPoint += candidate.Point;
                ExcelUtils.ExportResultsExcel(ListResults, _listSubmissions, maxPoint,
                    _paperSet.Papers[0].CandidateSet.Count);
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