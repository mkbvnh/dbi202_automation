using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dbi_grading_module.Model
{
    class ConstraintModel
    {
        public string ForeignTableName { get; set; }
        public string ForeignColumnName { get; set; }
        public string PrimaryTableName { get; set; }
        public string PrimaryColumnName { get; set; }
        public string Name { get; set; }

        public ConstraintModel(string foreignTableName, string foreignColumnName, string primaryTableName, string primaryColumnName)
        {
            ForeignTableName = foreignTableName;
            ForeignColumnName = foreignColumnName;
            PrimaryTableName = primaryTableName;
            PrimaryColumnName = primaryColumnName;
            Name = $"{foreignTableName}-{foreignColumnName}-{primaryTableName}-{primaryColumnName}";
        }
    }
}
