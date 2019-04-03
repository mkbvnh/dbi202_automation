using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DBI_Grading.Model.Question
{
    [Serializable]
    public class QuestionSet
    {
        public QuestionSet()
        {
            QuestionList = new List<DBI_Grading.Model.Question.Question>();
            DBScriptList = new List<string>();
        }

        public QuestionSet(List<DBI_Grading.Model.Question.Question> questionList, List<string> dBScriptList)
        {
            QuestionList = questionList;
            DBScriptList = dBScriptList;
        }

        public List<DBI_Grading.Model.Question.Question> QuestionList { get; set; }
        public List<string> DBScriptList { get; set; }

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