using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageToolkit.HyperDrive;

namespace ImageToolkit
{
    public partial class frmHyperDriveTest : Form
    {
        public frmHyperDriveTest()
        {
            InitializeComponent();
            lblproc.Text = cHyperDrive.Processors.ToString();
        }

        private void closeHyperDriveTest(object sender, EventArgs e)
        {
            this.Close();
        }

        private void runTest_Click(object sender, EventArgs e)
        {
            int startTime = 0, deltaTime;
            TimeSpan elapseTime;

            Cursor.Current = Cursors.WaitCursor;

            lbProgress.Items.Clear();
            lbProgress.Items.Add("Queuing Operations...");

            int numberOfOperations = Convert.ToInt32(udops.Value);
            int numberOfIterations = Convert.ToInt32(udits.Value);

            startTime = Environment.TickCount;
            for (int i = 0; i < numberOfIterations; i++)
            {
                HyperDrive.cHyperDrive.AddTask(new testOperation(numberOfIterations));
            }
            lbProgress.Items.Add("Start threads running...");
            cHyperDrive.Run();

            deltaTime = (Environment.TickCount - startTime) * 10000;
            elapseTime = new TimeSpan((long)deltaTime);
            lbProgress.Items.Add("Threads completed.");

            String completionMessage = String.Format(
                "Time to execute {0:N0} operations with {1:N0} iterations = {2} hh:mm:ss",
                numberOfOperations, numberOfIterations, elapseTime.ToString());
            lbProgress.Items.Add(completionMessage);
            Cursor.Current = Cursors.Default;
        }
    }
}
