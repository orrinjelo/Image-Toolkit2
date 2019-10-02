using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageToolkit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static frmMain main;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            main = new frmMain();
            OperationsMathSimple.main = main;
            ExecutionStack.mainForm = main;
            ExecutionStack.ls = main.getStack();
            Application.Run(main);
        }
    }
}
