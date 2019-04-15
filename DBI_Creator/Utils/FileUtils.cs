using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;

namespace DBI202_Creator.Utils
{
    [ExcludeFromCodeCoverage]
    internal class FileUtils
    {
        public static string CreateNewDirectory(string path, string nameOfFolder)
        {
            path = path + @"\" + nameOfFolder;
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                    return path;
                // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            catch
            {
                // ignored
            }

            return path;
        }

        /// <summary>
        ///     Choosing a location to save file
        /// </summary>
        /// <returns></returns>
        public static string SaveFileLocation()
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();
            if (result == DialogResult.OK) return fbd.SelectedPath;
            return null;
        }

        public static string GetFileLocation(string filter, string title)
        {
            // Displays an OpenFileDialog so the user can select a File.  
            var ofd = new OpenFileDialog
            {
                Filter = filter,
                Title = title,
                Multiselect = false
            };
            // Show the Dialog.  
            // If the user clicked OK in the dialog and  
            // a .DAT file was selected, take the local path of it.  
            if (ofd.ShowDialog() == DialogResult.OK)
                return ofd.FileName;
            return null;
        }
    }
}