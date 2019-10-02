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
    public partial class frmCrop : Form
    {

        int cropX, cropY, cropW, cropH;//, ocropX, ocropY;
        public Pen cropPen;

        bool Spawn;
        IOperand myParent;
        
        public frmCrop(IOperand parent=null, bool spawn=true)
        {
            Bitmap bmp = ((frmStandard)parent).Image; // Normalize.FromFloat(img);
            myParent = parent;
            Spawn = spawn;
            InitializeComponent();
            this.pictureBox1.Image = bmp;

            this.udx2.Maximum = bmp.Width;
            this.udy2.Maximum = bmp.Height;

            this.udx2.Value = bmp.Width;
            this.udy2.Value = bmp.Height;

            this.pictureBox1.Width = Convert.ToInt32(Convert.ToDouble(this.pictureBox1.Height) / Convert.ToDouble(this.pictureBox1.Image.Height) * this.pictureBox1.Image.Width);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            OperationsTransformations.Crop(Convert.ToInt32(this.udx1.Value), Convert.ToInt32(this.udy1.Value), 
                Convert.ToInt32(this.udx2.Value), Convert.ToInt32(this.udy2.Value),myParent,Spawn);
            this.Close();
        }

        private void controlChange(object sender, EventArgs e)
        {
            /*
            this.udx1.Maximum = this.udx2.Value - 1;
            this.udx2.Minimum = this.udx1.Value + 1;
            this.udy1.Maximum = this.udy2.Value - 1;
            this.udy2.Minimum = this.udy1.Value + 1;
            this.pictureBox1.Update();
             */
            if (this.udx1.Value > this.udx2.Value)
            {
                decimal temp = this.udx2.Value;
                this.udx2.Value = this.udx1.Value;
                this.udx1.Value = temp;
            }
        }



        private void startSelection(object sender, EventArgs e)
        {

            //Cursor.Current = Cursors.Cross;
        }

        private void selectCursor(object sender, MouseEventArgs e)
        {
            int W = pictureBox1.Image.Width;
            int H = pictureBox1.Image.Height;
            int h, w;
            if (W > H)
            {
                w = pictureBox1.Width;
                h = (int)(H * ((float)w / (float)W));
            }
            else
            {
                h = pictureBox1.Height;
                w = (int)(W * ((float)h / (float)W));
            }

            pictureBox1.Width = w;
            //if (e.X > w || e.Y > h) return;

            //MessageBox.Show(W.ToString() + " " + H.ToString());

            this.udx1.Minimum = 0;
            this.udx1.Maximum = W;
            this.udx2.Minimum = 0;
            this.udx2.Maximum = W;
            this.udy1.Minimum = 0;
            this.udy1.Maximum = H;
            this.udy2.Minimum = 0;
            this.udy2.Maximum = H;

            cropPen = new Pen(Color.Black, 1);
            cropPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                cropX = e.X;
                cropY = e.Y;
                this.udx1.Value = (cropX > this.udx1.Maximum ? W : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width;
                this.udy1.Value = (cropY > this.udy1.Maximum ? H : cropY < 0 ? 0 : cropY)  * H / pictureBox1.Height;
            }
            pictureBox1.Refresh();
            //Cursor.Current = Cursors.Cross;
        }

        private void selectMove(object sender, MouseEventArgs e)
        {

            if (pictureBox1.Image == null) return;

            int W = pictureBox1.Image.Width;
            int H = pictureBox1.Image.Height;

            int w = pictureBox1.Width;
            int h = pictureBox1.Height;

            this.udx1.Minimum = 0;
            this.udx1.Maximum = W;
            this.udx2.Minimum = 0;
            this.udx2.Maximum = W;
            this.udy1.Minimum = 0;
            this.udy1.Maximum = H;
            this.udy2.Minimum = 0;
            this.udy2.Maximum = H;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox1.Refresh();
                cropW = e.X - cropX;
                cropH = e.Y - cropY;
                if (e.X > cropX)
                {
                    this.udx1.Value = Math.Abs((cropX > w ? w : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width) > this.udx1.Maximum
                        ? this.udx1.Maximum : Math.Abs((cropX > w ? w : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width);
                    this.udx2.Value = Math.Abs((e.X > w ? w : e.X < 0 ? 0 : e.X) * W / pictureBox1.Width) > this.udx2.Maximum
                        ? this.udx2.Maximum : Math.Abs((e.X > w ? w : e.X < 0 ? 0 : e.X) * W / pictureBox1.Width);
                }
                else
                {
                    this.udx1.Value = Math.Abs((e.X > w ? w : e.X < 0 ? 0 : e.X) * W / pictureBox1.Width) > this.udx1.Maximum
                        ? this.udx1.Value : Math.Abs((e.X > w ? w : e.X < 0 ? 0 : e.X) * W / pictureBox1.Width);
                    this.udx2.Value = Math.Abs((cropX > W ? W : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width) > this.udx2.Maximum
                        ? this.udx2.Maximum : Math.Abs((cropX > W ? W : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width);
                }
                if (e.Y > cropY)
                {
                    this.udy1.Value = Math.Abs((cropY > H ? H : cropY < 0 ? 0 : cropY) * H / pictureBox1.Height) > this.udy1.Maximum
                        ? this.udy1.Maximum : Math.Abs((cropY > H ? H : cropY < 0 ? 0 : cropY) * H / pictureBox1.Height);
                    this.udy2.Value = Math.Abs((e.Y > w ? w : e.Y < 0 ? 0 : e.Y) * H / pictureBox1.Height) > this.udy2.Maximum
                        ? this.udy2.Maximum : Math.Abs((e.Y > w ? w : e.Y < 0 ? 0 : e.Y) * H / pictureBox1.Height);
                }
                else
                {
                    this.udy1.Value = Math.Abs((e.Y > w ? w : e.Y < 0 ? 0 : e.Y) * H / pictureBox1.Height) > this.udy1.Maximum
                        ? this.udy1.Maximum : Math.Abs((e.Y > w ? w : e.Y < 0 ? 0 : e.Y) * H / pictureBox1.Height);
                    this.udy2.Value = Math.Abs((cropY > H ? H : cropY < 0 ? 0 : cropY) * H / pictureBox1.Height) > this.udy2.Maximum
                        ? this.udy2.Maximum : Math.Abs((cropY > H ? H : cropY < 0 ? 0 : cropY) * H / pictureBox1.Height);
                }
                pictureBox1.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropW, cropH);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}
