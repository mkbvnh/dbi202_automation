using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DBI202_Creator.Entities.Question;

namespace DBI202_Creator.Entities.Paper
{
    [Serializable]
    public class PaperSet
    {
        public PaperSet()
        {
        }

        public PaperSet(List<Paper> papers, List<string> dBScriptList, List<int> listPaperMatrixId,
            QuestionSet questionSet)
        {
            Papers = papers;
            DBScriptList = dBScriptList;
            ListPaperMatrixId = listPaperMatrixId;
            QuestionSet = questionSet;
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