using System;
using System.Collections.Generic;
using System.Linq;
using DBI202_Creator.Commons;
using DBI202_Creator.Entities.Candidate;
using DBI202_Creator.Entities.Paper;
using DBI202_Creator.Entities.Question;

namespace DBI202_Creator.Model
{
    internal class ShufflePaperModel
    {
        public PaperSet PaperSet; //Include PaperSet after create 
        public QuestionSet QuestionSet; //QBank from Creator

        /// <summary>
        ///     Create List of PaperSet
        /// </summary>
        /// <param name="questionSet"></param>
        /// <param name="numOfPage"></param>
        public ShufflePaperModel(QuestionSet questionSet, int numOfPage)
        {
            QuestionSet = questionSet;
            PaperSet = new PaperSet(new List<Paper>(), QuestionSet.DBScriptList, new List<int>(), QuestionSet);

            var allCases = GetAllPaperCases();

            var papersCandidateNode = new List<List<CandidateNode>>();

            if (Constants.PaperSet != null && Constants.PaperSet.ListPaperMatrixId != null &&
                Constants.PaperSet.ListPaperMatrixId.Count > 0 && Constants.PaperSet.ListPaperMatrixId.Count > 0)
                foreach (var paperId in Constants.PaperSet.ListPaperMatrixId)
                    papersCandidateNode.Add(allCases.ElementAt(paperId));
            else
                papersCandidateNode = GetRandomNElementsInList(numOfPage, allCases);

            //Adding Matrix Id
            foreach (var candidateNode in papersCandidateNode)
                PaperSet.ListPaperMatrixId.Add(allCases.IndexOf(candidateNode));

            //codeTestCount: for TestCode
            var codeTestCount = 0;
            //Adding candidate into Tests
            foreach (var c in papersCandidateNode)
            {
                var candidateList = new List<Candidate>();
                //Adding candidate into a Test

                foreach (var candidateNode in c)
                    candidateList.Add(candidateNode.Candi);
                var paper = new Paper
                {
                    PaperNo = (++codeTestCount).ToString(),
                    CandidateSet = candidateList
                };
                PaperSet.Papers.Add(paper);
            }
        }

        /// <summary>
        ///     Get random elements in List
        /// </summary>
        /// <param name="numOfCases"></param>
        /// <param name="allCases"></param>
        /// <returns></returns>
        private List<List<CandidateNode>> GetRandomNElementsInList(int numOfCases, List<List<CandidateNode>> allCases)
        {
            if (numOfCases == 1)
            {
                var oneCase =
                    new List<List<CandidateNode>> {allCases.ElementAt(new Random().Next(allCases.Count))};
                return oneCase;
            }
            var newList = new List<List<CandidateNode>>();
            var jump = (allCases.Count - 1) / (numOfCases - 1);

            for (var i = 0; i < numOfCases; i++)
            {
                var pos = jump * i;
                newList.Add(allCases.ElementAt(pos));
            }

            //Shuffle a list C# (Fisher-Yates shuffle)
            var n = numOfCases;
            while (n > 1)
            {
                n--;
                var k = new Random().Next(n + 1);
                var value = newList[k];
                newList[k] = newList[n];
                newList[n] = value;
            }

            return newList;
        }

        /// <summary>
        ///     Add all cases of the tests
        /// </summary>
        /// <returns></returns>
        private List<List<CandidateNode>> GetAllPaperCases()
        {
            var root = SetCandidateNode(null, 0, BuildingTree());
            root.AddPath(root, new List<CandidateNode>());
            return root.paths;
        }

        /// <summary>
        ///     Building a tree of Candidates for generate all the cases
        /// </summary>
        /// <returns></returns>
        private int[] BuildingTree()
        {
            var quizs = new int[QuestionSet.QuestionList.Count];
            var i = 0;
            foreach (var question in QuestionSet.QuestionList)
                quizs[i++] = question.Candidates.Count;
            return quizs;
        }

        /// <summary>
        ///     Set relation of all Nodes in the Tree
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pos"></param>
        /// <param name="quizs"></param>
        /// <returns></returns>
        public CandidateNode SetCandidateNode(Candidate value, int pos, int[] quizs)
        {
            var child = new CandidateNode
            {
                Candi = value
            };
            if (pos < quizs.Length)
                foreach (var candi in QuestionSet.QuestionList.ElementAt(pos).Candidates)
                    child.Children.Add(SetCandidateNode(candi, pos + 1, quizs));
            else
                child.Children = null;
            return child;
        }
    }
}