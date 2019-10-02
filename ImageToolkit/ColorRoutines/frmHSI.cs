using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageToolkit.ColorRoutines
{
    public partial class frmHSI : Form, IOperand
    {
        int H, W;

        public string Description { get; set; }
        public frmMain myParent;

        public int ImageWidth {
            get
            {
                return W;
            }
        }
        public int ImageHeight
        {
            get
            {
                return H;
            }
        }

        float[][][] Image
        {
            get
            {
                float[][][] img = new float[3][][];
                float[][][] hue = Normalize.ToFloat((Bitmap)picHue.Image);
                float[][][] sat = Normalize.ToFloat((Bitmap)picSaturation.Image);
                float[][][] ity = Normalize.ToFloat((Bitmap)picIntensity.Image);

                for (int c=0; c<3; c++)
                {
                    img[c] = new float[H][];
                    for (int h=0; h<H; h++)
                    {
                        img[c][h] = new float[W];
                    }
                }

                for (int h=0; h<H; h++)
                {
                    for (int w=0; w<W; w++)
                    {
                        ColorRoutines.ImageLibrary.Pixel_HSItoRGB(hue[0][h][w], sat[0][h][w], ity[0][h][w], out img[0][h][w], out img[1][h][w], out img[2][h][w]);
                    }
                }
                return img;
            }

            set 
            {
                float[][][] img = value;
                float[][][] hue = new float[3][][];
                float[][][] sat = new float[3][][];
                float[][][] ity = new float[3][][];

                for (int c=0; c<3; c++)
                {
                    hue[c] = new float[H][];
                    sat[c] = new float[H][];
                    ity[c] = new float[H][];
                    for (int h=0; h<H; h++)
                    {
                        hue[c][h] = new float[W];
                        sat[c][h] = new float[W];
                        ity[c][h] = new float[W];
                    }
                }

                for (int h=0; h<H; h++)
                {
                    for (int w=0; w<W; w++)
                    {
                        ColorRoutines.ImageLibrary.Pixel_RGBtoHSI( img[0][h][w], img[1][h][w], img[2][h][w], out hue[0][h][w], out sat[0][h][w], out ity[0][h][w]);
                        hue[1][h][w] = hue[2][h][w] = hue[0][h][w];
                        sat[1][h][w] = sat[2][h][w] = sat[0][h][w];
                        ity[1][h][w] = ity[2][h][w] = ity[0][h][w];
                    }
                }

                picHue.Image = Normalize.FromFloat(hue);
                picSaturation.Image = Normalize.FromFloat(sat);
                picIntensity.Image = Normalize.FromFloat(ity);
            }
        }

        public frmHSI(Bitmap bmp, frmMain main)
        {
            InitializeComponent();
            this.H = bmp.Height;
            this.W = bmp.Width;
            this.Image = Normalize.ToFloat(bmp);
            this.myParent = main;
        }

        public IOperand CreateSibling(float[][][] sourceImage, String description)    // like a clone with new content
        {
            frmHSI frm;
            Bitmap image = Normalize.FromFloat(sourceImage);

            frm = new frmHSI(image, this.myParent);

            frm.Show();

            int index = myParent.AddToImages(frm);
            return frm;
        }

        public Bitmap GetBitmap()
        {
            return Normalize.FromFloat(Image);
        }

        public float[][][] GetFloat()
        {
            return Image;
        }
    }
}
