using System.Collections.Generic;

namespace dbi_grading_module.Entity.Candidate
{
    public class CandidateNode
    {
        public CandidateNode()
        {
            Children = new List<CandidateNode>();
            Paths = new List<List<CandidateNode>>();
        }

        public List<CandidateNode> Children { get; set; }
        public Candidate Candidate { get; set; }
        public List<List<CandidateNode>> Paths { get; set; }

        public void AddPath(CandidateNode node, List<CandidateNode> candidatesPath)
        {
            if (node.Children == null || node.Children.Count == 0)
            {
                Paths.Add(candidatesPath);
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