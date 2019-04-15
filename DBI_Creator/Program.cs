using System;
using System.Diagnostics.CodeAnalysis;
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
            Application.Run(new CreatorForm());
        }
    }
}