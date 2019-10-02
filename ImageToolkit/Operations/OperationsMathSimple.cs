using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ImageToolkit
{
    public static class OperationsMathSimple
    {
        public static frmMain main;
        // The following describes and registers all operations contained in this module.
        public static void RegisterOperations()
        {
            OperationRegistry.RegisterOperation(new Operation("Math Operations Simple", "To Gray Scale", "Z = ToGrayScale(X)", ToGrayScale, true));
            OperationRegistry.RegisterOperation(new Operation("Math Operations Simple", "Invert", "Z = -X", Invert, true));
            OperationRegistry.RegisterOperation(new Operation("Math Operations Simple", "Add", "Z = X + Y", Add, true));
            OperationRegistry.RegisterOperation(new Operation("Math Operations Simple", "Subtract", "Z = X - Y", Subtract, true));
            OperationRegistry.RegisterOperation(new Operation("Math Operations Simple", "Average", "Z = (X + Y) / 2", Average, true));
            OperationRegistry.RegisterOperation(new Operation("Math Operations Simple", "ChangeIntensity", "Z = X + c", ChangeIntensity, true));
        }

        public static void ToGrayScale()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            Bitmap grayScale = x.GetBitmap();
            float[][][] img = ToGrayScale(grayScale);
            x.CreateSibling(img, "Grayscale of " + grayScale.ToString());
        }

        public static float[][][] ToGrayScale(Bitmap grayScale)
        {
            float[][][] img = Normalize.ToFloat(grayScale);
            for (int h = 0; h < grayScale.Height; h++)
            {
                for (int w = 0; w < grayScale.Width; w++)
                {
                    float r = img[Normalize.RGBPLANE_RED][h][w];
                    float g = img[Normalize.RGBPLANE_GREEN][h][w];
                    float b = img[Normalize.RGBPLANE_BLUE][h][w];
                    img[Normalize.RGBPLANE_RED][h][w] =
                      img[Normalize.RGBPLANE_GREEN][h][w] =
                      img[Normalize.RGBPLANE_BLUE][h][w] =
                         (r + g + b) / 3.0f;
                }
            }
            return img;
        }

        public static void Invert()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = x.GetBitmap();
            float[][][] img = Invert(bmp);
            x.CreateSibling(img, "Inversion of " + bmp.ToString());
        }

        public static float[][][] Invert(Bitmap bmp)
        {
            float[][][] img = Normalize.ToFloat(bmp);
            for (int h = 0; h < bmp.Height; h++)
            {
                for (int w = 0; w < bmp.Width; w++)
                {
                    float r = img[Normalize.RGBPLANE_RED][h][w];
                    float g = img[Normalize.RGBPLANE_GREEN][h][w];
                    float b = img[Normalize.RGBPLANE_BLUE][h][w];
                    img[Normalize.RGBPLANE_RED][h][w] = 1.0f - r;
                    img[Normalize.RGBPLANE_GREEN][h][w] = 1.0f - g;
                    img[Normalize.RGBPLANE_BLUE][h][w] = 1.0f - b;
                }
            }
            return img;
        }

        public static void Add()
        {
            IOperand x = ExecutionStack.X;
            IOperand y = ExecutionStack.Y;
            if (x == null || y == null) return;
            Bitmap bmp = x.GetBitmap();
            Bitmap bmp2 = y.GetBitmap();

            float[][][] img = Normalize.ToFloat(bmp);
            float[][][] img2 = Normalize.ToFloat(bmp2);
            int maxw = bmp.Width > bmp2.Width ? bmp.Width : bmp2.Width;
            int maxh = bmp.Height > bmp2.Height ? bmp.Height : bmp2.Height;
            int minw = bmp.Width < bmp2.Width ? bmp.Width : bmp2.Width;
            int minh = bmp.Height < bmp2.Height ? bmp.Height : bmp2.Height;

            float[][][] newimg = new float[Normalize.RGBPLANE_LENGTH][][];
            for (int c = 0; c < Normalize.RGBPLANE_LENGTH;c++)
            {
                newimg[c] = new float[maxh][];
                for (int h = 0; h < maxh; h++)
                {
                    newimg[c][h] = new float[maxw];
                    for (int w = 0; w < maxw; w++) newimg[c][h][w] = 0.0f;
                }
            }


            for (int h = 0; h < bmp.Height; h++)
            {
                for (int w = 0; w < bmp.Width; w++)
                {
                    newimg[Normalize.RGBPLANE_RED][h][w] += img[Normalize.RGBPLANE_RED][h][w];
                    newimg[Normalize.RGBPLANE_GREEN][h][w] += img[Normalize.RGBPLANE_GREEN][h][w];
                    newimg[Normalize.RGBPLANE_BLUE][h][w] += img[Normalize.RGBPLANE_BLUE][h][w];
                }
            }

            for (int h = 0; h < bmp2.Height; h++)
            {
                for (int w = 0; w < bmp2.Width; w++)
                {
                    newimg[Normalize.RGBPLANE_RED][h][w] += img2[Normalize.RGBPLANE_RED][h][w];
                    newimg[Normalize.RGBPLANE_GREEN][h][w] += img2[Normalize.RGBPLANE_GREEN][h][w];
                    newimg[Normalize.RGBPLANE_BLUE][h][w] += img2[Normalize.RGBPLANE_BLUE][h][w];
                }
            }

            x.CreateSibling(newimg, "Addition of " + bmp.ToString() + " and " + bmp2.ToString());
        }

        public static void Subtract()
        {
            IOperand x = ExecutionStack.X;
            IOperand y = ExecutionStack.Y;
            if (x == null || y == null) return;
            Bitmap bmp = x.GetBitmap();
            Bitmap bmp2 = y.GetBitmap();

            float[][][] img = Normalize.ToFloat(bmp);
            float[][][] img2 = Normalize.ToFloat(bmp2);


            float[][][] newimg = new float[Normalize.RGBPLANE_LENGTH][][];
            for (int c = 0; c < Normalize.RGBPLANE_LENGTH; c++)
            {
                newimg[c] = new float[bmp.Height][];
                for (int h = 0; h < bmp.Height; h++)
                {
                    newimg[c][h] = new float[bmp.Width];
                    for (int w = 0; w < bmp.Width; w++) newimg[c][h][w] = 0.0f;
                }
            }


            for (int h = 0; h < bmp.Height; h++)
            {
                for (int w = 0; w < bmp.Width; w++)
                {
                    newimg[Normalize.RGBPLANE_RED][h][w] += img[Normalize.RGBPLANE_RED][h][w];
                    newimg[Normalize.RGBPLANE_GREEN][h][w] += img[Normalize.RGBPLANE_GREEN][h][w];
                    newimg[Normalize.RGBPLANE_BLUE][h][w] += img[Normalize.RGBPLANE_BLUE][h][w];
                }
            }

            for (int h = 0; h < bmp2.Height; h++)
            {
                for (int w = 0; w < bmp2.Width; w++)
                {
                    newimg[Normalize.RGBPLANE_RED][h][w] -= img2[Normalize.RGBPLANE_RED][h][w];
                    newimg[Normalize.RGBPLANE_GREEN][h][w] -= img2[Normalize.RGBPLANE_GREEN][h][w];
                    newimg[Normalize.RGBPLANE_BLUE][h][w] -= img2[Normalize.RGBPLANE_BLUE][h][w];
                }
            }

            x.CreateSibling(newimg, "Subtraction of " + bmp.ToString() + " and " + bmp2.ToString());
        }

        public static void Average()
        {
            IOperand x = ExecutionStack.X;
            IOperand y = ExecutionStack.Y;
            if (x == null || y == null) return;
            Bitmap bmp = x.GetBitmap();
            Bitmap bmp2 = y.GetBitmap();

            float[][][] img = Normalize.ToFloat(bmp);
            float[][][] img2 = Normalize.ToFloat(bmp2);
            int maxw = bmp.Width > bmp2.Width ? bmp.Width : bmp2.Width;
            int maxh = bmp.Height > bmp2.Height ? bmp.Height : bmp2.Height;
            int minw = bmp.Width < bmp2.Width ? bmp.Width : bmp2.Width;
            int minh = bmp.Height < bmp2.Height ? bmp.Height : bmp2.Height;

            float[][][] newimg = new float[Normalize.RGBPLANE_LENGTH][][];
            for (int c = 0; c < Normalize.RGBPLANE_LENGTH; c++)
            {
                newimg[c] = new float[maxh][];
                for (int h = 0; h < maxh; h++)
                {
                    newimg[c][h] = new float[maxw];
                    for (int w = 0; w < maxw; w++) newimg[c][h][w] = 0.0f;
                }
            }


            for (int h = 0; h < bmp.Height; h++)
            {
                for (int w = 0; w < bmp.Width; w++)
                {
                    newimg[Normalize.RGBPLANE_RED][h][w] += img[Normalize.RGBPLANE_RED][h][w] / 2.0f;
                    newimg[Normalize.RGBPLANE_GREEN][h][w] += img[Normalize.RGBPLANE_GREEN][h][w] / 2.0f;
                    newimg[Normalize.RGBPLANE_BLUE][h][w] += img[Normalize.RGBPLANE_BLUE][h][w] / 2.0f;
                }
            }

            for (int h = 0; h < bmp2.Height; h++)
            {
                for (int w = 0; w < bmp2.Width; w++)
                {
                    newimg[Normalize.RGBPLANE_RED][h][w] += img2[Normalize.RGBPLANE_RED][h][w] / 2.0f;
                    newimg[Normalize.RGBPLANE_GREEN][h][w] += img2[Normalize.RGBPLANE_GREEN][h][w] / 2.0f;
                    newimg[Normalize.RGBPLANE_BLUE][h][w] += img2[Normalize.RGBPLANE_BLUE][h][w] / 2.0f;
                }
            }

            x.CreateSibling(newimg, "Average of " + bmp.ToString() + " and " + bmp2.ToString());
        }

        public static void ChangeIntensity()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            frmIntensity fi = new frmIntensity(x.GetBitmap());
            fi.Show();
            return;
        }

        public static void ChangeIntensity(IOperand x, bool spawn=true)
        {
            if (x == null) return;
            frmIntensity fi = new frmIntensity(x.GetBitmap(), x, spawn);
            fi.Show();
            return;
        }

        public static void ChangeIntensity(float c = 0.0f, IOperand x=null, bool spawn=true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = x.GetBitmap();
            float[][][] img = Normalize.ToFloat(bmp);
            for (int h = 0; h < bmp.Height; h++)
            {
                for (int w = 0; w < bmp.Width; w++)
                {

                    img[Normalize.RGBPLANE_RED][h][w] *= (1.0f + c);
                    img[Normalize.RGBPLANE_GREEN][h][w] *= (1.0f + c);
                    img[Normalize.RGBPLANE_BLUE][h][w] *= (1.0f + c);
                    if (img[Normalize.RGBPLANE_RED][h][w] > 1.0f) img[Normalize.RGBPLANE_RED][h][w] = 1.0f;
                    if (img[Normalize.RGBPLANE_GREEN][h][w] > 1.0f) img[Normalize.RGBPLANE_GREEN][h][w] = 1.0f;
                    if (img[Normalize.RGBPLANE_BLUE][h][w] > 1.0f) img[Normalize.RGBPLANE_BLUE][h][w] = 1.0f;

                }
            }
            MessageBox.Show("Here.");
            if (spawn) x.CreateSibling(img, "Intensity change of " + bmp.ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);
        }
        
    }
}
