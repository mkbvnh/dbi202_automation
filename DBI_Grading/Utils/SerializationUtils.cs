using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DBI_Grading.Utils
{
    internal class SerializationUtils
    {
        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <param name="localPath"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string localPath) where T : new()
        {
            T rez;
            using (Stream stream = File.Open(localPath, FileMode.Open))
            {
                var bin = new BinaryFormatter();
                rez = (T) bin.Deserialize(stream);
            }

            return rez;
        }
    }
}