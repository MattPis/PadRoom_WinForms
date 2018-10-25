using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PadRoom
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /// Only one instance of PadRoom is allowed 
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length == 1)
            {
                StartApplication();
                return;
            }
        }

         static void StartApplication()
        {
            Application.Run(new TrayContext());
        }
    }

}
