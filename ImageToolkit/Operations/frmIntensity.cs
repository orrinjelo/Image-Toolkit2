using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageToolkit
{
    public partial class frmIntensity : Form
    {
        public bool ThumbnailCallback()
        {
            MessageBox.Show("Callback...");
            return true;
        }

        float[][][] original;
        bool Spawn;
        IOperand Caller;

        public frmIntensity(Bitmap bmp, IOperand caller=null, bool spawn=true)
        {
            Spawn = spawn;
            Caller = caller;
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            InitializeComponent();
            Image img = bmp;
            this.original = Normalize.ToFloat((Bitmap)img.GetThumbnailImage( this.thumbnail.Width, this.thumbnail.Height, callback, new IntPtr()));
            this.thumbnail.Image = Normalize.FromFloat(this.original);
        }


        private void reportIntensity(object sender, EventArgs e)
        {
            this.Hide();
            MessageBox.Show("Reporting intensity.");
            OperationsMathSimple.ChangeIntensity((float)this.intensityUD.Value,Caller,Spawn);
            this.Close();
        }

        private void updateThumb(object sender, EventArgs e)
        {

            float[][][] img = Normalize.ToFloat((Bitmap)this.thumbnail.Image);
            for (int c = 0; c < 3; c++)
                for (int h = 0; h < thumbnail.Image.Height; h++)
                    for (int w = 0; w < thumbnail.Image.Width; w++)
                    { 
                        img[c][h][w] = (float)(this.original[c][h][w] * (1.0 + Convert.ToSingle(this.intensityUD.Value)));
                        if (img[c][h][w] > 1.0f)
                            img[c][h][w] = 1.0f;
                    }
            this.thumbnail.Image = Normalize.FromFloat(img);
        }
    }
}
