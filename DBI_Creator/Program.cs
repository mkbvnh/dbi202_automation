using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DBI202_Creator.UI;

namespace DBI202_Creator
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(new CreatorForm());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string logPath = "./log/";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            File.AppendAllText(String.Concat(logPath, DateTime.Now.ToString("yyyy-MM-dd"), ".txt"), String.Concat(
                "============\n",
                "Message: ", e.Exception.Message, "\n",
                "Trace: ", e.Exception.StackTrace, "\n"));
            MessageBox.Show("Something not right, please send log file in log folder to me\nEmail: nguyenquocbaobm@gmail.com\nThank you!", "Error");
        }
    }
}