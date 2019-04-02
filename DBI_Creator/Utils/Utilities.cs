using System;
using System.Drawing;
using System.IO;

namespace DBI202_Creator.Utils
{
    public class Utilities
    {
        public static Image Base64StringToImage(string inputString)
        {
            Image img = null;

            var imageBytes = Convert.FromBase64String(inputString);

            using (var ms = new MemoryStream())
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                img = Image.FromStream(ms, true);
                ms.Close();
                imageBytes = null;
            }
            return img;
        }

        public static string ImageToBase64(string filePath)
        {
            using (var image = Image.FromFile(filePath))
            {
                using (var m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    var imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    var base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}