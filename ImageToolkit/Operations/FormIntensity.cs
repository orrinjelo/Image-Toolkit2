using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToolkit
{
    public partial class FormIntensity : Form
    {
        public bool ThumbnailCallback()
        {
            MessageBox.Show("Callback...");
            return true;
        }

        readonly float[][][] original;
        readonly bool Spawn;
        readonly IOperand Caller;

        public FormIntensity(Bitmap bmp, IOperand caller = null, bool spawn = true)
        {
            Spawn = spawn;
            Caller = caller;
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            InitializeComponent();
            Image img = bmp;
            original = Normalize.ToFloat((Bitmap)img.GetThumbnailImage(thumbnail.Width, thumbnail.Height, callback, new IntPtr()));
            thumbnail.Image = Normalize.FromFloat(original);
        }


        private void ReportIntensity(object sender, EventArgs e)
        {
            Hide();
            MessageBox.Show("Reporting intensity.");
            OperationsMathSimple.ChangeIntensity((float)intensityUD.Value, Caller, Spawn);
            Close();
        }

        private void UpdateThumb(object sender, EventArgs e)
        {

            float[][][] img = Normalize.ToFloat((Bitmap)thumbnail.Image);
            for (int c = 0; c < 3; c++)
            {
                for (int h = 0; h < thumbnail.Image.Height; h++)
                {
                    for (int w = 0; w < thumbnail.Image.Width; w++)
                    {
                        img[c][h][w] = (float)(original[c][h][w] * (1.0 + Convert.ToSingle(intensityUD.Value)));
                        if (img[c][h][w] > 1.0f)
                            img[c][h][w] = 1.0f;
                    }
                }
            }

            thumbnail.Image = Normalize.FromFloat(img);
        }
    }
}