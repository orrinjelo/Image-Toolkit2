using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToolkit.Operations
{
    public partial class FormRotate : Form
    {
        readonly Bitmap bmp;

        private int cX, cY; // Center of drawing area (relative to top left)
        private float lastTheta = 0; // Last angle of mouse rotation relative to start point

        private double[,] source; // will contain polygon to be rotated
        private double rotateSum; // total of rotation angle
        private readonly Pen myPen = new Pen(Color.HotPink, 2);
        private double[,] transform; // rotation transformation matrix
        private bool mouseAngle = false; // true if using the mouse to rotate
        private readonly bool Spawn;
        private readonly FormStandard myParent;


        private double delta = 0.0d;
        private double Delta = 0.0d;
        private double theta = 0.0d;

        public FormRotate(IOperand x, bool spawn = true)
        {
            InitializeComponent();
            bmp = ((FormStandard)x).Image;
            Spawn = spawn;
            myParent = (FormStandard)x;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            if (rbCW.Checked) OperationsTransformations.Rotate(90, myParent, Spawn);
            else if (rbCCW.Checked) OperationsTransformations.Rotate(-90, myParent, Spawn);
            else if (rbFlip.Checked) OperationsTransformations.Rotate(180, myParent, Spawn);
            else OperationsTransformations.Rotate(Convert.ToInt32(udAngle.Value), myParent, Spawn);
            Close();
        }

        private void CheckIfBoundary(object sender, EventArgs e)
        {
            if (!mouseAngle) // It messes up when you are moving it with the mouse...
            {
                if (udAngle.Value > 180) udAngle.Value -= 360;
                else if (udAngle.Value < -179) udAngle.Value += 360;
            }
        }

        private void EnableAngleBox(object sender, EventArgs e)
        {
            udAngle.Enabled = true;
            PlotPic(sender, e);
            PicBox_MouseUp(sender, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0));
            BtnReset_Click(sender, e);
        }

        private void DisableAngleBox(object sender, EventArgs e)
        {
            udAngle.Enabled = false;
            PicBox_MouseUp(sender, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0));
        }

        private void PlotPic(object sender, EventArgs e)
        {
            picBox.Image = bmp;

            if (udAngle.Enabled)
            {
                double width = Math.Min(picBox.Width, picBox.Height) * 0.6f;
                double centerOffset = width / 2.0d;
                cX = (picBox.Width / 2) - 1;
                cY = (picBox.Height / 2) - 1;

                source = new double[,]
                {{-centerOffset, centerOffset},
                {centerOffset, centerOffset},
                {centerOffset, -centerOffset},
                {-centerOffset, -centerOffset},
                {-centerOffset, centerOffset},
                {0, 0}}; // [0,0] is just a shape sequence terminator
                // Initialize transform to the identity array.
                transform = new double[,]
                {{1.0D, 0.0D},
                {0.0D, 1.0D}};
                rotateSum = 0;
            }
        }

        private void UpdateUDAngle()
        {
            udAngle.Value += (decimal)Delta;
            if (udAngle.Value > 180) udAngle.Value -= 360;
            else if (udAngle.Value < -179) udAngle.Value += 360;
        }

        private void PicBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (udAngle.Enabled)
            {
                int x, y;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    mouseAngle = true;
                    x = (e.X - cX);
                    y = (cY - e.Y);
                    lastTheta = (float)Math.Atan2(y, x);
                }
            }
        }

        private void Rotate()
        {
            if (udAngle.Enabled)
            {
                double radians;
                // the following matches a signed or unsigned real number (with or without decimal)

                rotateSum += delta; //= angle;
                rotateSum %= 360; // ensure rotation is always between 0 and 360 degrees

                radians = Math.PI * (rotateSum / 180.0D); // convert degrees to radians (trig library uses radians)
                // get the transformation matrix for rotation (note this is a left hand rule rotation)
                transform = new double[,]
                {
                    {Math.Cos(radians), Math.Sin(radians)},
                    {-Math.Sin(radians), Math.Cos(radians)}
                };
                UpdateUDAngle();
            }
        }

        private double[,] ApplyTransform(double[,] transform, double[,] poly)
        {
            int p, i, j;
            double sum;
            double[,] result;
            result = new double[poly.GetLength(0), poly.GetLength(1)]; // create a result matrix
            // perform matrix multiplication
            for (p = 0; p < poly.GetLength(0); p++) // for each result row
            {
                for (i = 0; i < transform.GetLength(0); i++) // for each transform row
                {
                    sum = 0;
                    for (j = 0; j < transform.GetLength(1); j++) // for each transform column
                    {
                        sum += Math.Round(transform[i, j] * poly[p, j]);
                    }
                    result[p, i] = sum;
                }
            }
            return result;
        }

        private void PicBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (udAngle.Enabled)
            {
                int x, y;
                //double theta;//, delta;
                if (mouseAngle && e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    x = (e.X - cX);
                    y = (cY - e.Y);
                    theta = Math.Atan2(y, x);
                    delta = (lastTheta - theta) * (180.0 / Math.PI);


                    if (Math.Abs(delta) > 1)
                    {
                        lastTheta = (float)theta;
                        Delta = delta;
                        Rotate();
                    }
                }
            }
            picBox.Refresh();
        }

        private void PicBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (udAngle.Enabled)
            {
                mouseAngle = false;
                picBox.Image = OperationsTransformations.Rotate(bmp, Convert.ToInt32(udAngle.Value));
            }
            else if (rbCW.Checked) picBox.Image = OperationsTransformations.Rotate(bmp, Convert.ToInt32(90));
            else if (rbCCW.Checked) picBox.Image = OperationsTransformations.Rotate(bmp, Convert.ToInt32(-90));
            else if (rbFlip.Checked) picBox.Image = OperationsTransformations.Rotate(bmp, Convert.ToInt32(180));

        }

        private void PicBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g;
            int p;
            double[,] polygon;
            g = e.Graphics;
            if (transform != null)
            {
                // draw the polygon
                polygon = ApplyTransform(transform, source);
                for (p = 0; p <= polygon.GetUpperBound(0) - 1; p++) // for each point in the polygon
                {
                    g.DrawLine(myPen, cX + (int)(polygon[p, 0]), cY - (int)polygon[p, 1], cX + (int)polygon[p + 1, 0],
                    cY - (int)polygon[p + 1, 1]);
                }
                // draw a circle at one of it's corners
                g.DrawEllipse(myPen, cX + (int)polygon[0, 0] - 10, cY - (int)polygon[0, 1] - 10, 21, 21);
                // draw a filled circle in it's center
                //g.FillEllipse(Brushes.Blue, cX - 12, cY - 12, 25, 25);
                //g.FillEllipse(Brushes.Black, cX - 2, cY - 2, 5, 5);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            PlotPic(sender, e);
            udAngle.Value = 0;
            picBox.Refresh();
            picBox.Update();
        }
    }
}
