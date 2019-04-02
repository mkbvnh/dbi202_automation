using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DBI202_Creator.Entities.Question;

namespace DBI202_Creator.Utils
{
    internal class SerializeUtils
    {
        /// <summary>
        ///     Write to File
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SerializeObject(object obj, string path)
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    formatter.Serialize(stream, obj);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <param name="localPath"></param>
        /// <returns></returns>
        public static QuestionSet DeserializeObject(string localPath)
        {
            // read file into a string and deserialize JSON to a type
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(localPath, FileMode.Open, FileAccess.Read))
            {
                return formatter.Deserialize(stream) as QuestionSet;
            }
        }
    }
}