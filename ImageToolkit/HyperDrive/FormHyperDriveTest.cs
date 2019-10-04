using ImageToolkit.HyperDrive;
using System;
using System.Windows.Forms;

namespace ImageToolkit
{
    public partial class FormHyperDriveTest : Form
    {
        public FormHyperDriveTest()
        {
            InitializeComponent();
            lblproc.Text = CHyperDrive.Processors.ToString();
        }

        private void CloseHyperDriveTest(object sender, EventArgs e)
        {
            Close();
        }

        private void RunTest_Click(object sender, EventArgs e)
        {
            TimeSpan elapseTime;

            Cursor.Current = Cursors.WaitCursor;

            lbProgress.Items.Clear();
            lbProgress.Items.Add("Queuing Operations...");

            int numberOfOperations = Convert.ToInt32(udops.Value);
            int numberOfIterations = Convert.ToInt32(udits.Value);

            int startTime = Environment.TickCount;
            for (int i = 0; i < numberOfIterations; i++)
            {
                HyperDrive.CHyperDrive.AddTask(new TestOperation(numberOfIterations));
            }
            lbProgress.Items.Add("Start threads running...");
            CHyperDrive.Run();

            int deltaTime = (Environment.TickCount - startTime) * 10000;
            elapseTime = new TimeSpan(deltaTime);
            lbProgress.Items.Add("Threads completed.");

            string completionMessage = string.Format(
                "Time to execute {0:N0} operations with {1:N0} iterations = {2} hh:mm:ss",
                numberOfOperations, numberOfIterations, elapseTime.ToString());
            lbProgress.Items.Add(completionMessage);
            Cursor.Current = Cursors.Default;
        }
    }
}