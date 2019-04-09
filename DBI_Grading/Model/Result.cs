using System;
using System.Collections.Generic;
using System.Linq;
using dbi_grading_module;
using dbi_grading_module.Entity.Candidate;
using dbi_grading_module.Entity.Paper;

namespace DBI_Grading.Model
{
    public class Result
    {
        private double _maxPoint;


        public Result(PaperSet paperSet, Submission submission)
        {
            PaperSet = paperSet;
            Points = new double[PaperSet.QuestionSet.QuestionList.Count];
            ListAnswers = new List<string>();
            Logs = new string[PaperSet.QuestionSet.QuestionList.Count];
            ListRequirement = new List<string>();
            Submission = submission;
            ExamCode = submission.ExamCode;
        }

        public PaperSet PaperSet { get; set; }
        public Submission Submission { get; set; }
        public string StudentId { get; set; }
        public string PaperNo { get; set; }
        public string ExamCode { get; set; }
        public List<string> ListAnswers { get; set; }
        public double[] Points { get; set; }
        public string[] Logs { get; set; }
        public List<string> ListRequirement { get; set; }
        public Paper Paper { get; set; }

        /// <summary>
        ///     Get Sum of point
        /// </summary>
        /// <returns> Sum of point(double)</returns>
        public double SumOfPoint()
        {
            var sum = Points.Sum();
            sum = Math.Round(sum, 2);
            if (sum >= _maxPoint) sum = _maxPoint;
            return sum;
        }

        /// <summary>
        ///     Count Student's point by question and answer
        /// </summary>
        /// <param name="candidate">Question</param>
        /// <param name="answer">Student's answer</param>
        /// <param name="questionOrder"></param>
        /// <returns>
        ///     True if correct
        ///     False if incorrect.
        /// </returns>
        /// <exception>
        ///     if exception was found, throw it for GetPoint function to handle
        ///     <cref>SQLException</cref>
        /// </exception>
        private Dictionary<string, string> GradeAnswer(Candidate candidate, string answer, int questionOrder)
        {
            // await TaskEx.Delay(100);
            if (string.IsNullOrEmpty(answer.Trim()))
                throw new Exception("Empty.");
            // Process by Question Type
            switch (candidate.QuestionType)
            {
                case Candidate.QuestionTypes.Schema:
                    // Schema Question
                    return Grading.SchemaType(candidate, StudentId, answer, questionOrder);
                case Candidate.QuestionTypes.Select:
                    //Select Question
                    return Grading.SelectType(candidate, StudentId, answer, questionOrder, PaperSet.DBScriptList[1]);
                case Candidate.QuestionTypes.DML:
                    // DML: Insert/Delete/Update Question
                    return Grading.DmlSpTriggerType(candidate, StudentId, answer, questionOrder,
                        PaperSet.DBScriptList[1]);
                case Candidate.QuestionTypes.Procedure:
                    // Procedure Question
                    return Grading.DmlSpTriggerType(candidate, StudentId, answer, questionOrder,
                        PaperSet.DBScriptList[1]);
                case Candidate.QuestionTypes.Trigger:
                    // Trigger Question
                    return Grading.DmlSpTriggerType(candidate, StudentId, answer, questionOrder,
                        PaperSet.DBScriptList[1]);
                default:
                    // Not supported yet
                    throw new Exception("This question type has not been supported yet.");
            }
        }

        /// <summary>
        ///     Get GradeAnswer function
        /// </summary>
        public void GetPoint()
        {
            // Find paper of student
            var papersFound = PaperSet.Papers.Where(myPaper => myPaper.PaperNo.Equals(PaperNo));
            var papers = papersFound as Paper[] ?? papersFound.ToArray();
            //Wrong paperNo
            if (papers.Count() == 0)
            {
                Logs[0] = "Wrong Paper No\n";
                return;
            }

            //PaperSet file is not in format
            if (papers.Count() != 1)
                throw new Exception(
                    $"PaperSet has {papers.Count()} paper which have the same PaperNo ({PaperNo})\nWrong format!");

            Paper = papers[0];

            //Set List requirements and List answers
            ListAnswers = Submission.ListAnswer;
            foreach (var candidate in Paper.CandidateSet) ListRequirement.Add(candidate.QuestionRequirement);

            //Calculate max point of the paper
            foreach (var candidate in Paper.CandidateSet) _maxPoint += candidate.Point;
            _maxPoint = _maxPoint > 10 ? Math.Floor(_maxPoint) : Math.Ceiling(_maxPoint);
            // Count number of candidate
            var numberOfQuestion = Paper.CandidateSet.Count;


            // Get mark one by one
            for (var questionOrder = 0; questionOrder < numberOfQuestion; questionOrder++)
                try
                {
                    if (numberOfQuestion > questionOrder)
                    {
                        var res = GradeAnswer(Paper.CandidateSet.ElementAt(questionOrder),
                            ListAnswers.ElementAt(questionOrder), questionOrder);
                        //Exactly -> Log true and return 0 point
                        if (res != null)
                        {
                            Points[questionOrder] = Math.Round(double.Parse(res["Point"]), 4);
                            Logs[questionOrder] = res["Comment"];
                        }
                        else
                        {
                            Points[questionOrder] = 0;
                            Logs[questionOrder] = "False\n";
                        }
                    }
                    else
                    {
                        // Not enough candidate 
                        // It rarely happens, it's this project's demos and faults.
                        throw new Exception("No questions found at question " + questionOrder + " paperNo = " +
                                            PaperNo + "\n");
                    }
                }
                catch (Exception e)
                {
                    // When something's wrong:
                    // Log error and return 0 point for student.
                    Points[questionOrder] = 0;
                    Logs[questionOrder] = e.Message + "\n";
                }
        }
    }
}