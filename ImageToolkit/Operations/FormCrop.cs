using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToolkit.Operations
{
    public partial class FormCrop : Form
    {

        int cropX, cropY, cropW, cropH;//, ocropX, ocropY;
        public Pen cropPen;
        readonly bool Spawn;
        readonly IOperand myParent;

        public FormCrop(IOperand parent = null, bool spawn = true)
        {
            Bitmap bmp = ((FormStandard)parent).Image; // Normalize.FromFloat(img);
            myParent = parent;
            Spawn = spawn;
            InitializeComponent();
            pictureBox1.Image = bmp;

            udx2.Maximum = bmp.Width;
            udy2.Maximum = bmp.Height;

            udx2.Value = bmp.Width;
            udy2.Value = bmp.Height;

            pictureBox1.Width = Convert.ToInt32(Convert.ToDouble(pictureBox1.Height) / Convert.ToDouble(pictureBox1.Image.Height) * pictureBox1.Image.Width);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            OperationsTransformations.Crop(Convert.ToInt32(udx1.Value), Convert.ToInt32(udy1.Value),
                Convert.ToInt32(udx2.Value), Convert.ToInt32(udy2.Value), myParent, Spawn);
            Close();
        }

        private void ControlChange(object sender, EventArgs e)
        {
            /*
            this.udx1.Maximum = this.udx2.Value - 1;
            this.udx2.Minimum = this.udx1.Value + 1;
            this.udy1.Maximum = this.udy2.Value - 1;
            this.udy2.Minimum = this.udy1.Value + 1;
            this.pictureBox1.Update();
             */
            if (udx1.Value > udx2.Value)
            {
                decimal temp = udx2.Value;
                udx2.Value = udx1.Value;
                udx1.Value = temp;
            }
        }

        private void StartSelection(object sender, EventArgs e)
        {

        }

        private void SelectCursor(object sender, MouseEventArgs e)
        {
            int W = pictureBox1.Image.Width;
            int H = pictureBox1.Image.Height;
            int h, w;
            if (W > H)
            {
                w = pictureBox1.Width;
                h = (int)(H * (w / (float)W));
            }
            else
            {
                h = pictureBox1.Height;
                w = (int)(W * (h / (float)W));
            }

            pictureBox1.Width = w;

            udx1.Minimum = 0;
            udx1.Maximum = W;
            udx2.Minimum = 0;
            udx2.Maximum = W;
            udy1.Minimum = 0;
            udy1.Maximum = H;
            udy2.Minimum = 0;
            udy2.Maximum = H;

            cropPen = new Pen(Color.Black, 1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot
            };
            if (e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                cropX = e.X;
                cropY = e.Y;
                udx1.Value = (cropX > udx1.Maximum ? W : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width;
                udy1.Value = (cropY > udy1.Maximum ? H : cropY < 0 ? 0 : cropY) * H / pictureBox1.Height;
            }
            pictureBox1.Refresh();
        }

        private void SelectMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null) return;

            int W = pictureBox1.Image.Width;
            int H = pictureBox1.Image.Height;

            int w = pictureBox1.Width;
            int h = pictureBox1.Height;

            udx1.Minimum = 0;
            udx1.Maximum = W;
            udx2.Minimum = 0;
            udx2.Maximum = W;
            udy1.Minimum = 0;
            udy1.Maximum = H;
            udy2.Minimum = 0;
            udy2.Maximum = H;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox1.Refresh();
                cropW = e.X - cropX;
                cropH = e.Y - cropY;
                if (e.X > cropX)
                {
                    udx1.Value = Math.Abs((cropX > w ? w : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width) > udx1.Maximum
                        ? udx1.Maximum : Math.Abs((cropX > w ? w : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width);
                    udx2.Value = Math.Abs((e.X > w ? w : e.X < 0 ? 0 : e.X) * W / pictureBox1.Width) > udx2.Maximum
                        ? udx2.Maximum : Math.Abs((e.X > w ? w : e.X < 0 ? 0 : e.X) * W / pictureBox1.Width);
                }
                else
                {
                    udx1.Value = Math.Abs((e.X > w ? w : e.X < 0 ? 0 : e.X) * W / pictureBox1.Width) > udx1.Maximum
                        ? udx1.Value : Math.Abs((e.X > w ? w : e.X < 0 ? 0 : e.X) * W / pictureBox1.Width);
                    udx2.Value = Math.Abs((cropX > W ? W : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width) > udx2.Maximum
                        ? udx2.Maximum : Math.Abs((cropX > W ? W : cropX < 0 ? 0 : cropX) * W / pictureBox1.Width);
                }
                if (e.Y > cropY)
                {
                    udy1.Value = Math.Abs((cropY > H ? H : cropY < 0 ? 0 : cropY) * H / pictureBox1.Height) > udy1.Maximum
                        ? udy1.Maximum : Math.Abs((cropY > H ? H : cropY < 0 ? 0 : cropY) * H / pictureBox1.Height);
                    udy2.Value = Math.Abs((e.Y > w ? w : e.Y < 0 ? 0 : e.Y) * H / pictureBox1.Height) > udy2.Maximum
                        ? udy2.Maximum : Math.Abs((e.Y > w ? w : e.Y < 0 ? 0 : e.Y) * H / pictureBox1.Height);
                }
                else
                {
                    udy1.Value = Math.Abs((e.Y > w ? w : e.Y < 0 ? 0 : e.Y) * H / pictureBox1.Height) > udy1.Maximum
                        ? udy1.Maximum : Math.Abs((e.Y > w ? w : e.Y < 0 ? 0 : e.Y) * H / pictureBox1.Height);
                    udy2.Value = Math.Abs((cropY > H ? H : cropY < 0 ? 0 : cropY) * H / pictureBox1.Height) > udy2.Maximum
                        ? udy2.Maximum : Math.Abs((cropY > H ? H : cropY < 0 ? 0 : cropY) * H / pictureBox1.Height);
                }
                pictureBox1.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropW, cropH);
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}