using System.Collections.Generic;

namespace DBI202_Creator.Entities.Candidate
{
    internal class CandidateNode
    {
        public CandidateNode()
        {
            Children = new List<CandidateNode>();
            paths = new List<List<CandidateNode>>();
        }

        public List<CandidateNode> Children { get; set; }
        public Candidate Candi { get; set; }
        public List<List<CandidateNode>> paths { get; set; }

        public void AddPath(CandidateNode node, List<CandidateNode> candidatesPath)
        {
            if (node.Children == null || node.Children.Count == 0)
            {
                paths.Add(candidatesPath);
                return;
            }
            foreach (var child in node.Children)
            {
                var tmp = new List<CandidateNode>();
                foreach (var candidateNode in candidatesPath)
                    tmp.Add(candidateNode);
                tmp.Add(child);
                AddPath(child, tmp);
            }
        }
    }
}