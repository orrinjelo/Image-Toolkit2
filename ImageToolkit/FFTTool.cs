using Exocortex.DSP;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToolkit
{
    public partial class FFTTool : Form
    {
        private readonly Bitmap bmp;
        private float[][][] img;
        private Fourier.CImage[] rimg = new Fourier.CImage[3];
        private readonly int NCOLORS = 3;

        public FFTTool()
        {
            InitializeComponent();
            bmp = new Bitmap(Properties.Resources.domocolor);
            pictureBox1.Image = bmp;
            ResetImg();
        }

        private void ResetImg()
        {
            img = Normalize.ToFloat(bmp);
            for (int i = 0; i < NCOLORS; i++)
                rimg[i] = new Fourier.CImage(img[i], bmp.Height, bmp.Width);

        }

        private Fourier.CImage[] FFT2D()
        {
            ResetImg();
            float scale = 1f / (float)Math.Sqrt(rimg[0].Width * rimg[0].Height);
            ComplexF[][] data = new ComplexF[NCOLORS][];
            for (int i = 0; i < NCOLORS; i++) data[i] = rimg[i].Data;

            for (int c = 0; c < NCOLORS; c++)
            {
                int offset = 0;
                for (int y = 0; y < rimg[c].Height; y++)
                {
                    for (int x = 0; x < rimg[c].Width; x++)
                    {
                        if (((x + y) & 0x1) != 0)
                        {
                            data[c][offset] *= -1;
                        }
                        offset++;
                    }
                }

                Exocortex.DSP.Fourier.FFT2(data[c], rimg[c].Width, rimg[c].Height, FourierDirection.Forward);

                rimg[c].FrequencySpace = true;

                for (int i = 0; i < data[c].Length; i++)
                {
                    data[c][i] *= scale;
                }
            }

            return rimg;
        }


        private Fourier.CImage[] IFFT2D()
        {
            float scale = 1f / (float)Math.Sqrt(rimg[0].Width * rimg[0].Height);
            ComplexF[][] data = new ComplexF[NCOLORS][];
            for (int c = 0; c < NCOLORS; c++)
            {
                data[c] = rimg[c].Data;


                Exocortex.DSP.Fourier.FFT2(data[c], rimg[c].Width, rimg[c].Height, FourierDirection.Backward);

                rimg[c].FrequencySpace = false;

                for (int i = 0; i < data[c].Length; i++)
                {
                    data[c][i] *= scale;
                }
            }
            return rimg;
        }

        private void RbOriginal_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = bmp;
        }

        private void BtnReal_CheckedChanged(object sender, EventArgs e)
        {

            if (!rimg[0].FrequencySpace)
                rimg = FFT2D();
            for (int c = 0; c < 3; c++)
            {
                img[c] = rimg[c].FromFloatReal();
            }
            pictureBox1.Image = Normalize.FromFloat(img);
        }

        private void RbImag_CheckedChanged(object sender, EventArgs e)
        {
            for (int c = 0; c < 3; c++)
            {
                img[c] = rimg[c].FromFloatImag();
            }
            pictureBox1.Image = Normalize.FromFloat(img);
        }

        private void RbMagnitude_CheckedChanged(object sender, EventArgs e)
        {
            if (!rimg[0].FrequencySpace)
                rimg = FFT2D();
            for (int c = 0; c < 3; c++)
            {
                img[c] = rimg[c].FromFloatModulus();
            }
            pictureBox1.Image = Normalize.FromFloat(img);
        }

        private void RbPhase_CheckedChanged(object sender, EventArgs e)
        {
            if (!rimg[0].FrequencySpace)
                rimg = FFT2D();
            for (int c = 0; c < 3; c++)
            {
                img[c] = rimg[c].FromFloatModulusSquared();
            }
            pictureBox1.Image = Normalize.FromFloat(img);
        }

        private void RbIFFT_CheckedChanged(object sender, EventArgs e)
        {
            if (rimg[0].FrequencySpace)
                rimg = IFFT2D();
            for (int c = 0; c < 3; c++)
            {
                img[c] = rimg[c].FromFloatModulus();
            }
            pictureBox1.Image = Normalize.FromFloat(img);
            //pictureBox1.Image = rimg[0].Magnitude();
        }

        private void RbLogReal_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RbLogImag_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RbLogMagnitude_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
