using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToolkit.Operations
{
    class OperationsMorph
    {
        public static void RegisterOperations()
        {
            OperationRegistry.RegisterOperation(new Operation("Morphological Operations", "To Binary", "Bin(X)", Bin, true));
            OperationRegistry.RegisterOperation(new Operation("Morphological Operations", "Erode", "Erode(X)", Erode, true));
            OperationRegistry.RegisterOperation(new Operation("Morphological Operations", "Dilate", "Dilate(X)", Dilate, true));
            OperationRegistry.RegisterOperation(new Operation("Morphological Operations", "Open", "Open(X)", Open, true));
            OperationRegistry.RegisterOperation(new Operation("Morphological Operations", "Close", "Close(X)", Close, true));
        }

        public static void Bin()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            float[][][] img = Bin(x);
            x.CreateSibling(img, "Binary image of " + ((frmStandard)x).Image.ToString());
        }

        public static float[][][] Bin(IOperand x)
        {
            if (x == null) return null;
            Bitmap bmp = ((frmStandard)x).Image;
            float[][][] img = Normalize.ToFloat(bmp);
            img = Bin(img, bmp.Height, bmp.Width);
            return img;
        }

        private static float[][][] Bin(float[][][] img, int H, int W)
        {
            for (int c = 0; c < 3; c++)
                for (int h = 0; h < H; h++)
                    for (int w = 0; w < W; w++)
                        img[c][h][w] = (float)Math.Round((img[0][h][w] + img[1][h][w] + img[2][h][w]) / 3);
            return img;
        }

        public static void Erode()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            float[][][] img = Erode(x);
            x.CreateSibling(img, "Erosion image of " + ((frmStandard)x).Image.ToString());
        }

        public static float[][][] Erode(IOperand x)
        {
            float[][][] img = Bin(Normalize.ToFloat(((frmStandard)x).Image), x.ImageHeight, x.ImageWidth);
            return Erode(img, ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
        }

        public static float[][][] Erode(float[][][] img, int H, int W)
        {
            float[][][] newimg = new float[3][][];
            for (int c = 0; c < 3; c++)
            {
                newimg[c] = new float[H][];
                for (int h = 0; h < H; h++)
                {
                    newimg[c][h] = new float[W];
                }
            }
            try
            {
                for (int h = 0; h < H; h++)
                {

                    for (int w = 0; w < W; w++)
                    {
                        float sum = 0f;
                        float top = 0f;
                        for (int i = -1; i <= 1; i++)
                            for (int j = -1; j <= 1; j++)
                            {
                                if (!((h == 0 && j == -1) || (h == H - 1 && j == 1) || (w == 0 && i == -1) || (w == W - 1 && i == 1)))
                                {
                                    sum += img[0][h + j][w + i];
                                    top += 1f;
                                }
                            }
                        newimg[0][h][w] = (float)Math.Floor(sum / top);
                        newimg[1][h][w] = (float)Math.Floor(sum / top);
                        newimg[2][h][w] = (float)Math.Floor(sum / top);
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.ToString());
            }
            return newimg;
        }


        public static void Dilate()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            float[][][] img = Dilate(x);
            x.CreateSibling(img, "Erosion image of " + ((frmStandard)x).Image.ToString());
        }

        public static float[][][] Dilate(IOperand x)
        {
            float[][][] img = Bin(Normalize.ToFloat(((frmStandard)x).Image), x.ImageHeight, x.ImageWidth);
            return Dilate(img, ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
        }

        public static float[][][] Dilate(float[][][] img, int H, int W)
        {
            float[][][] newimg = new float[3][][];
            for (int c = 0; c < 3; c++) newimg[c] = new float[H][];
            for (int h = 0; h < H; h++)
            {
                newimg[0][h] = new float[W];
                newimg[1][h] = new float[W];
                newimg[2][h] = new float[W];

                for (int w = 0; w < W; w++)
                {
                    float sum = 0f;
                   
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                        {
                            if (!((h == 0 && j == -1) || (h == H - 1 && j == 1) || (w == 0 && i == -1) || (w == W - 1 && i == 1)))
                                sum += img[0][h + j][w + i];
                        }
                    newimg[0][h][w] = (float)Math.Ceiling(sum / 9f);
                    newimg[1][h][w] = (float)Math.Ceiling(sum / 9f);
                    newimg[2][h][w] = (float)Math.Ceiling(sum / 9f);
                }
            }
            return newimg;
        }

        public static void Open()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = ((frmStandard)x).Image;

            float[][][] img = Bin(x);
            img = Erode(img, bmp.Height, bmp.Width);
            img = Dilate(img, bmp.Height, bmp.Width);

            x.CreateSibling(img, "Opening of " + bmp.ToString());
        }

        public static void Close()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = ((frmStandard)x).Image;

            float[][][] img = Bin(x);
            img = Dilate(img, bmp.Height, bmp.Width);
            img = Erode(img, bmp.Height, bmp.Width);

            x.CreateSibling(img, "Closing of " + bmp.ToString());
        }

    }
}
