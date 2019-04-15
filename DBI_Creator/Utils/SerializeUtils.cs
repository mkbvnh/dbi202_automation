using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DBI202_Creator.Utils
{
    [ExcludeFromCodeCoverage]
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
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, obj);
            }

            return true;
        }

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