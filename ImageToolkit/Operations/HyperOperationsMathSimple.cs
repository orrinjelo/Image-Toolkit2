using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ImageToolkit
{
    public static class HyperOperationsMathSimple
    {
        public static frmMain main;
        // The following describes and registers all operations contained in this module.
        public static void RegisterOperations()
        {
            OperationRegistry.RegisterOperation(new Operation("Math Hyper Operations Simple", "To Gray Scale", "Z = ToGrayScale(X)", HyperWrapper(ToGrayScale), true));
            OperationRegistry.RegisterOperation(new Operation("Math Hyper Operations Simple", "Invert", "Z = -X", HyperWrapper(Invert), true));
            OperationRegistry.RegisterOperation(new Operation("Math Hyper Operations Simple", "Add", "Z = X + Y", HyperWrapper(Add), true));
            OperationRegistry.RegisterOperation(new Operation("Math Hyper Operations Simple", "Subtract", "Z = X - Y", HyperWrapper(Subtract), true));
            OperationRegistry.RegisterOperation(new Operation("Math Hyper Operations Simple", "Average", "Z = (X + Y) / 2", HyperWrapper(Average), true));
            OperationRegistry.RegisterOperation(new Operation("Math Hyper Operations Simple", "ChangeIntensity", "Z = X + c", HyperWrapper(ChangeIntensity), true));
        }

        public static Action HyperWrapper(Action F)
        {
            HyperOperation hop = new HyperOperation("Math Hyper Operations Simple", "Unknown", "Unknown", F, true);
            HyperDrive.cHyperDrive.AddTask(hop);
            return HyperDrive.cHyperDrive.Run;
        }

        public static float[][][] ToGrayScale(float[][][] img, int W, int H)
        {
            for (int h = 0; h < H; h++)
            {
                for (int w = 0; w < W; w++)
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

        public static void ToGrayScale()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            Bitmap grayScale = x.GetBitmap();
            float[][][] img = Normalize.ToFloat(grayScale);
            float[][][][] parts = new float[HyperDrive.cHyperDrive.Processors][][][];

            img = ToGrayScale(img, grayScale.Width, grayScale.Height);

            x.CreateSibling(img, "Grayscale of " + grayScale.ToString());
        }

        public static void Invert()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = x.GetBitmap();
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
            x.CreateSibling(img, "Inversion of " + bmp.ToString());
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
            /*
                frmIntensity fi = new frmIntensity();
                fi.Show();
                return;
            */
        }

        public static void ChangeIntensity(float c = 0.0f)
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = x.GetBitmap();
            float[][][] img = Normalize.ToFloat(bmp);
            for (int h = 0; h < bmp.Height; h++)
            {
                for (int w = 0; w < bmp.Width; w++)
                {

                    img[Normalize.RGBPLANE_RED][h][w] += c;
                    img[Normalize.RGBPLANE_GREEN][h][w] += c;
                    img[Normalize.RGBPLANE_BLUE][h][w] += c;
                }
            }
            x.CreateSibling(img, "Intensity change of " + bmp.ToString());
        }
        
    }
}
