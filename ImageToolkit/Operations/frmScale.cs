using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ImageToolkit.Operations
{
    public partial class frmScale : Form
    {
        int originalx, originaly;
        IOperand parent;
        bool Spawn;
        public frmScale(int w, int h, IOperand x=null, bool spawn=true)
        {
            InitializeComponent();
            //if (bmp == null)
             //   MessageBox.Show("Error, null image?");
            originalx = w;
            originaly = h;
            this.udx.Value = w;
            this.udy.Value = h;
            parent = x;
            Spawn = spawn;
            //MessageBox.Show("Help!");
            
        }

        private void xvaluechanged(object sender, EventArgs e)
        {
            if (this.cbratio.Checked)
            {
                int x = Convert.ToInt32(this.udx.Value);
                this.udy.Value = Convert.ToInt32(originaly * (float)x / (float)originalx);
            }
        }

        private void yvaluechanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OperationsTransformations.Scale(Convert.ToInt32(this.udx.Value), Convert.ToInt32(this.udy.Value),parent,Spawn);
            this.Close();
        }

        private void AbleDisable(object sender, EventArgs e)
        {
            if (this.cbratio.Checked)
                this.udy.Enabled = false;
            else
                this.udy.Enabled = true;
        }
    }
}
