using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

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
        public static T DeserializeObject<T>(string localPath) where T : new()
        {
            T rez = new T();

            try
            {
                using (Stream stream = File.Open(localPath, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    rez = (T)bin.Deserialize(stream);
                }
            }
            catch (IOException e)
            {
                throw e;
            }

            return rez;
        }
    }
}