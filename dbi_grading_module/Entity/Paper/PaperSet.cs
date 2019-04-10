using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using dbi_grading_module.Entity.Question;

namespace dbi_grading_module.Entity.Paper
{
    [Serializable]
    public class PaperSet
    {
        public PaperSet(List<Paper> papers, List<string> dBScriptList, List<int> listPaperMatrixId,
            QuestionSet questionSet)
        {
            Papers = papers;
            DBScriptList = dBScriptList;
            ListPaperMatrixId = listPaperMatrixId;
            QuestionSet = questionSet;
        }

        public PaperSet()
        {
        }

        public PaperSet(PaperSet paperSet)
        {
            Papers = paperSet.Papers;
            DBScriptList = paperSet.DBScriptList;
            ListPaperMatrixId = paperSet.ListPaperMatrixId;
            QuestionSet = paperSet.QuestionSet;
        }

        public List<Paper> Papers { get; set; }
        public List<string> DBScriptList { get; set; }
        public List<int> ListPaperMatrixId { get; set; }
        public QuestionSet QuestionSet { get; set; }

        public T CloneObjectSerializable<T>() where T : class
        {
            var ms = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Position = 0;
            var result = bf.Deserialize(ms);
            ms.Close();
            return (T) result;
        }
    }
}