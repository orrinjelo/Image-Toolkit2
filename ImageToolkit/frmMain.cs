using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


/* * * * *
 * Fun instructions will go here.
 * Have a nice day.
 * */


namespace ImageToolkit
{
    
    public partial class frmMain : Form
    {
        //public ExecutionStack ExecutionStack { get; set; }
        public frmMain()
        {
            InitializeComponent();
            //ExecutionStack = new ExecutionStack(this);
            populateOperations();
        }

        public void remove(frmStandard item)
        {
            //MessageBox.Show("Removing " + item.ToString(), "Stuff", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            lstImages.Items.Remove(item);
            ExecutionStack.Remove(item);
        }

        private void populateOperations()
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            frmStandard frm;
            string filename;

            try
            {
                openFileDialog.Filter = MyConstants.FILE_FILTER_IN;
                openFileDialog.FilterIndex = MyConstants.FILTER_INDEX;

                result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    filename = openFileDialog.FileName;
                    frm = createFrmStandard(filename);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Display(ex, "frmImageColorRGB(filename)");
            }
             
        }

        public frmStandard createFrmStandard(string filename)
        {
            frmStandard frm;
            Bitmap image = new Bitmap(filename);
            string imageName = Path.GetFileName(filename);
            frm = new frmStandard(imageName, image, this);
            frm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainFormClosing);
            frm.Show();
            int index = lstImages.Items.Add(frm);
            lstImages.SelectedIndex = index;
            return frm;
        }

        private void mainFormClosing(object sender, FormClosingEventArgs e)
        {
            // Do stuff
        }

        private void openImageForm(object sender, EventArgs e)
        {
            foreach (frmStandard f in lstImages.SelectedItems)
            {
                f.Show();
                if (f.WindowState == FormWindowState.Minimized)
                    f.WindowState = FormWindowState.Normal;
                else if (f.WindowState == FormWindowState.Normal)
                    pushOnStackToolStripMenuItem_Click(sender, e);
            }
        }

        private void toolStripMenuSaveAs_Click(object sender, EventArgs e)
        {
            foreach (frmStandard f in lstImages.SelectedItems)
            {
                    f.saveImage();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (frmStandard f in lstImages.SelectedItems)
            {
                    f.saveToolStripMenuItem_Click(sender,e);
            }
            
        }

        public void updatelstImages()
        {
            /*int insertPt = lstImages.Items.IndexOf(oldName);
            lstImages.Items.Remove(oldName);
            lstImages.Items.Add(newName);*/
            lstImages.Update();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<frmStandard> flist = new List<frmStandard>();
            foreach (frmStandard f in lstImages.SelectedItems)
            {
                ExecutionStack.Remove((IOperand)f);
                flist.Add(f);
            }
            for (int i = 0; i < flist.Count; i++ )
                flist[i].Close();
        }

        private void closeAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (IOperand f in lstImages.Items)
                ExecutionStack.Remove((IOperand)f);
            lstImages.Items.Clear();         
        }

        private void tvwOperations_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstImages.Items.Count; i++)
                lstImages.SetSelected(i, true);
        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstImages.Items.Count; i++)
                lstImages.SetSelected(i, false);
        }

        public ListBox getStack()
        {
            return lstStack;
        }

        private void pushOnStackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (frmStandard f in lstImages.SelectedItems)
            {
                ExecutionStack.Push((IOperand)f);
            }
             
        }

        private void popToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ExecutionStack.Pop();
        }

        private void swapXAndYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecutionStack.SwapXY();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecutionStack.RemoveAll();
        }

        private void makeToolTip(object sender, MouseEventArgs e)
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
                    if (theNode.Tag.ToString() != this.toolTip1.GetToolTip(this.tvwOperations))
                    {
                        this.toolTip1.Show(theNode.Tag.ToString(), tvwOperations, e.X + 50, e.Y, 4000);
                        //this.toolTip1.SetToolTip(this.tvwOperations, theNode.Tag.ToString());
                    }
                }
                else
                {
                    this.toolTip1.SetToolTip(this.tvwOperations, "");
                }
            }
            else     // Pointer is not over a node so clear the ToolTip.
            {
                this.toolTip1.SetToolTip(this.tvwOperations, "");
            }
        }

        private void clickNode(object sender, MouseEventArgs e)
        {
            // Get the node at the current mouse pointer location.
            TreeNode theNode = tvwOperations.GetNodeAt(e.X, e.Y);

            // Set a ToolTip only if the mouse pointer is actually paused on a node.
            if ((theNode != null))
            {
                // Verify that the tag property is not "null".
                if (theNode.Tag != null)
                    this.lblImageExpression.Text = theNode.Tag.ToString();
                
            }
        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
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

        public int AddToImages(frmStandard frm)
        {
            int index = lstImages.Items.Add(frm);
            lstImages.SelectedIndex = index;
            return index;
        }

        public int AddToImages(ColorRoutines.frmHSI frm)
        {
            int index = lstImages.Items.Add(frm);
            lstImages.SelectedIndex = index;
            return index;
        }

        private void hyperDriveTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHyperDriveTest frmHyper = new frmHyperDriveTest();
            frmHyper.Show();
        }

        private void scaleTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void fFTTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FFTTool frmFFT = new FFTTool();
            frmFFT.Show();
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
            if (ex is ApplicationException) MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            else MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error at " + source, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}
