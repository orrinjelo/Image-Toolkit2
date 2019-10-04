using System;
using System.Windows.Forms;

namespace ImageToolkit.Operations
{
    public partial class FormScale : Form
    {
        readonly int originalx, originaly;
        readonly IOperand parent;
        readonly bool Spawn;
        public FormScale(int w, int h, IOperand x = null, bool spawn = true)
        {
            InitializeComponent();
            //if (bmp == null)
            //   MessageBox.Show("Error, null image?");
            originalx = w;
            originaly = h;
            udx.Value = w;
            udy.Value = h;
            parent = x;
            Spawn = spawn;
            //MessageBox.Show("Help!");

        }

        private void Xvaluechanged(object sender, EventArgs e)
        {
            if (cbratio.Checked)
            {
                int x = Convert.ToInt32(udx.Value);
                udy.Value = Convert.ToInt32(originaly * (float)x / originalx);
            }
        }

        private void Yvaluechanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            OperationsTransformations.Scale(Convert.ToInt32(udx.Value), Convert.ToInt32(udy.Value), parent, Spawn);
            Close();
        }

        private void AbleDisable(object sender, EventArgs e)
        {
            if (cbratio.Checked)
                udy.Enabled = false;
            else
                udy.Enabled = true;
        }
    }
}