using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTest.UnitTestBase
{
    internal class SerializeUtils
    {
        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <param name="localPath"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string localPath) where T : new()
        {
            var rez = new T();
            using (Stream stream = File.Open(localPath, FileMode.Open))
            {
                var bin = new BinaryFormatter();
                rez = (T) bin.Deserialize(stream);
            }

            return rez;
        }
    }
}