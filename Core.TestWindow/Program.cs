using System;
using System.Windows.Forms;
using Core.TestWindow.Forms;

namespace Core.TestWindow
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TimeSeries());
        }
    }
}
