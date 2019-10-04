using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/* * * * *
 * Fun instructions will go here.
 * Have a nice day.
 * */

namespace ImageToolkit
{
    public partial class FormMain : Form
    {
        //public ExecutionStack ExecutionStack { get; set; }
        public FormMain()
        {
            InitializeComponent();
            //ExecutionStack = new ExecutionStack(this);
            PopulateOperations();
        }

        public void Remove(FormStandard item)
        {
            //MessageBox.Show("Removing " + item.ToString(), "Stuff", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            lstImages.Items.Remove(item);
            ExecutionStack.Remove(item);
        }

        private void PopulateOperations()
        {
            OperationsMathSimple.RegisterOperations();
            Operations.OperationsTransformations.RegisterOperations();
            Operations.OperationsSpaceDomainFilters.RegisterOperations();
            Operations.OperationsFFT.RegisterOperations();
            Operations.OperationsStatistical.RegisterOperations();
            Operations.OperationHSI.RegisterOperations();
            Operations.OperationsMorph.RegisterOperations();
            HyperOperationsMathSimple.RegisterOperations();

            foreach (Operation op in OperationRegistry.operations)
            {
                TreeNode tn = new TreeNode();
                if (tvwOperations.Nodes.ContainsKey(op.Catagory))
                    tvwOperations.Nodes[op.Catagory].Nodes.Add(op.Name).Tag = op.Description;

                else
                {
                    tvwOperations.Nodes.Add(op.Catagory, op.Catagory);
                    tvwOperations.Nodes[op.Catagory].Nodes.Add(op.Name).Tag = op.Description;
                }
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            FormStandard frm;
            string filename;

            try
            {
                openFileDialog.Filter = MyConstants.FILE_FILTER_IN;
                openFileDialog.FilterIndex = MyConstants.FILTER_INDEX;

                result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    filename = openFileDialog.FileName;
                    frm = CreateFrmStandard(filename);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Display(ex, "frmImageColorRGB(filename)");
            }

        }

        public FormStandard CreateFrmStandard(string filename)
        {
            FormStandard frm;
            Bitmap image = new Bitmap(filename);
            string imageName = Path.GetFileName(filename);
            frm = new FormStandard(imageName, image, this);
            frm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MainFormClosing);
            frm.Show();
            int index = lstImages.Items.Add(frm);
            lstImages.SelectedIndex = index;
            return frm;
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            // Do stuff
        }

        private void OpenImageForm(object sender, EventArgs e)
        {
            foreach (FormStandard f in lstImages.SelectedItems)
            {
                f.Show();
                if (f.WindowState == FormWindowState.Minimized)
                    f.WindowState = FormWindowState.Normal;
                else if (f.WindowState == FormWindowState.Normal)
                    PushOnStackToolStripMenuItem_Click(sender, e);
            }
        }

        private void ToolStripMenuSaveAs_Click(object sender, EventArgs e)
        {
            foreach (FormStandard f in lstImages.SelectedItems)
            {
                f.SaveImage();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (FormStandard f in lstImages.SelectedItems)
            {
                f.SaveToolStripMenuItem_Click(sender, e);
            }

        }

        public void UpdatelstImages()
        {
            /*int insertPt = lstImages.Items.IndexOf(oldName);
            lstImages.Items.Remove(oldName);
            lstImages.Items.Add(newName);*/
            lstImages.Update();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<FormStandard> flist = new List<FormStandard>();
            foreach (FormStandard f in lstImages.SelectedItems)
            {
                ExecutionStack.Remove(f);
                flist.Add(f);
            }
            for (int i = 0; i < flist.Count; i++)
                flist[i].Close();
        }

        private void CloseAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (IOperand f in lstImages.Items)
                ExecutionStack.Remove(f);
            lstImages.Items.Clear();
        }

        private void TvwOperations_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstImages.Items.Count; i++)
                lstImages.SetSelected(i, true);
        }

        private void UnselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstImages.Items.Count; i++)
                lstImages.SetSelected(i, false);
        }

        public ListBox GetStack()
        {
            return lstStack;
        }

        private void PushOnStackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (FormStandard f in lstImages.SelectedItems)
            {
                ExecutionStack.Push(f);
            }

        }

        private void PopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ExecutionStack.Pop();
        }

        private void SwapXAndYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecutionStack.SwapXY();
        }

        private void ClearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecutionStack.RemoveAll();
        }

        private void MakeToolTip(object sender, MouseEventArgs e)
        {
            // Get the node at the current mouse pointer location.
            TreeNode theNode = tvwOperations.GetNodeAt(e.X, e.Y);

            // Set a ToolTip only if the mouse pointer is actually paused on a node.
            if ((theNode != null))
            {
                // Verify that the tag property is not "null".
                if (theNode.Tag != null)
                {
                    // Change the ToolTip only if the pointer moved to a new node.
                    if (theNode.Tag.ToString() != toolTip1.GetToolTip(tvwOperations))
                    {
                        toolTip1.Show(theNode.Tag.ToString(), tvwOperations, e.X + 50, e.Y, 4000);
                        //this.toolTip1.SetToolTip(this.tvwOperations, theNode.Tag.ToString());
                    }
                }
                else
                {
                    toolTip1.SetToolTip(tvwOperations, "");
                }
            }
            else     // Pointer is not over a node so clear the ToolTip.
            {
                toolTip1.SetToolTip(tvwOperations, "");
            }
        }

        private void ClickNode(object sender, MouseEventArgs e)
        {
            // Get the node at the current mouse pointer location.
            TreeNode theNode = tvwOperations.GetNodeAt(e.X, e.Y);

            // Set a ToolTip only if the mouse pointer is actually paused on a node.
            if ((theNode != null))
            {
                // Verify that the tag property is not "null".
                if (theNode.Tag != null)
                    lblImageExpression.Text = theNode.Tag.ToString();

            }
        }

        private void ExecuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(tvwOperations.SelectedNode.Tag.ToString());
            try
            {
                var watch = Stopwatch.StartNew();
                OperationRegistry.Retrieve("", tvwOperations.SelectedNode.Tag.ToString()).Execute();
                watch.Stop();
                string[] row = { watch.Elapsed.ToString(), tvwOperations.SelectedNode.Tag.ToString() };
                listView1.Items.Add(new ListViewItem(row));
            }
            catch
            {
                // Let's only get the operations, not the categories...
            }

        }

        public int AddToImages(FormStandard frm)
        {
            int index = lstImages.Items.Add(frm);
            lstImages.SelectedIndex = index;
            return index;
        }

        public int AddToImages(ColorRoutines.FormHSI frm)
        {
            int index = lstImages.Items.Add(frm);
            lstImages.SelectedIndex = index;
            return index;
        }

        private void HyperDriveTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormHyperDriveTest frmHyper = new FormHyperDriveTest())
            {
                frmHyper.Show();
            }
        }

        private void ScaleTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FFTTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FFTTool frmFFT = new FFTTool())
            {
                frmFFT.Show();
            }
        }
    }

    public static class MyConstants
    {
        public const String FILE_FILTER_IN = "Image Files(*.BMP; *.JPG; *.GIF; *.TIF; *.TIFF)|*.BMP;*.JPG;*.GIF;*.TIF;*.TIFF|All Files|*.*";
        public const String FILE_FILTER_OUT = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|TIF (*.tif)|*.tif|TIFF (*.tiff)|*.tiff";
        public const int FILTER_INDEX = 1;
    }

    public static class ErrorMessage
    {
        public static void Display(Exception ex, String source)
        {
            if (ex is ApplicationException) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error at " + source, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}