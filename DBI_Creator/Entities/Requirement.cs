using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBI_Exam_Creator_Tool.Commons;

namespace DBI_Exam_Creator_Tool.Entities
{
    [Serializable]
    public class Requirement
    {
        public enum RequirementTypes
        {
            ResultSet = 1,
            Effect = 2,
            Parameter = 3
        }

        public string RequirementId { get; set; }
        public string CandidateId { get; set; }
        public RequirementTypes Type { get; set; }
        public bool RequireSort { get; set; }
        public string CheckEffectQuery { get; set; }

        public Requirement()
        {
            Type = RequirementTypes.ResultSet;
        }

        public Requirement(string requirementId, string candidateId, RequirementTypes type, bool requireSort, string checkEffectQuery)
        {
            RequirementId = requirementId;
            CandidateId = candidateId;
            Type = type;
            RequireSort = requireSort;
            CheckEffectQuery = checkEffectQuery;
        }

        public override bool Equals(object obj)
        {
            var requirement = obj as Requirement;
            return requirement != null &&
                   RequirementId == requirement.RequirementId;
        }
    }
}
