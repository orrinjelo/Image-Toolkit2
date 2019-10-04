using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToolkit.ColorRoutines
{
    public partial class FormHSI : Form, IOperand
    {
        public string Description { get; set; }
        public FormMain myParent;

        public int ImageWidth { get; }
        public int ImageHeight { get; }

        float[][][] Image
        {
            get
            {
                float[][][] img = new float[3][][];
                float[][][] hue = Normalize.ToFloat((Bitmap)picHue.Image);
                float[][][] sat = Normalize.ToFloat((Bitmap)picSaturation.Image);
                float[][][] ity = Normalize.ToFloat((Bitmap)picIntensity.Image);

                for (int c = 0; c < 3; c++)
                {
                    img[c] = new float[ImageHeight][];
                    for (int h = 0; h < ImageHeight; h++)
                    {
                        img[c][h] = new float[ImageWidth];
                    }
                }

                for (int h = 0; h < ImageHeight; h++)
                {
                    for (int w = 0; w < ImageWidth; w++)
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

                for (int c = 0; c < 3; c++)
                {
                    hue[c] = new float[ImageHeight][];
                    sat[c] = new float[ImageHeight][];
                    ity[c] = new float[ImageHeight][];
                    for (int h = 0; h < ImageHeight; h++)
                    {
                        hue[c][h] = new float[ImageWidth];
                        sat[c][h] = new float[ImageWidth];
                        ity[c][h] = new float[ImageWidth];
                    }
                }

                for (int h = 0; h < ImageHeight; h++)
                {
                    for (int w = 0; w < ImageWidth; w++)
                    {
                        ColorRoutines.ImageLibrary.Pixel_RGBtoHSI(img[0][h][w], img[1][h][w], img[2][h][w], out hue[0][h][w], out sat[0][h][w], out ity[0][h][w]);
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

        public FormHSI(Bitmap bmp, FormMain main)
        {
            InitializeComponent();
            ImageHeight = bmp.Height;
            ImageWidth = bmp.Width;
            Image = Normalize.ToFloat(bmp);
            myParent = main;
        }

        public IOperand CreateSibling(float[][][] sourceImage, String description)    // like a clone with new content
        {
            FormHSI frm;
            Bitmap image = Normalize.FromFloat(sourceImage);

            frm = new FormHSI(image, myParent);

            frm.Show();

            myParent.AddToImages(frm);
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
