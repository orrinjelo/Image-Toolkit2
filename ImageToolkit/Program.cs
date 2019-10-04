using System;
using System.Windows.Forms;

namespace ImageToolkit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static FormMain main;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            main = new FormMain();
            OperationsMathSimple.main = main;
            ExecutionStack.mainForm = main;
            ExecutionStack.ls = main.GetStack();
            Application.Run(main);
        }
    }
}
