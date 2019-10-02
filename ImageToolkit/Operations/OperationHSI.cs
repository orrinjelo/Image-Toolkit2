using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageToolkit.Operations
{
    class OperationHSI
    {
        public static void RegisterOperations()
        {
            OperationRegistry.RegisterOperation(new Operation("Color Routines", "Convert To HSI (View Only)", "HSI(X)", HSI, true));
            OperationRegistry.RegisterOperation(new Operation("Color Routines", "HSI Color Enhance", "HSI_Enhance(X)", Enhance, true));
        }

        public static void HSI()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;

            ColorRoutines.frmHSI frm = new ColorRoutines.frmHSI(((frmStandard)x).GetBitmap(), ((frmStandard)x).myParent);
            frm.Show();
        }

        public static void Enhance()
        {
            Enhance(null, true);
        }

        public static void Enhance(IOperand X=null, bool spawn=true)
        {
            if (X == null) X = ExecutionStack.X;
            if (X == null) return;

            Bitmap bmp = ((frmStandard)X).GetBitmap();
            float[][][] img = Normalize.ToFloat(bmp);
            float[][][] hsi = ToHSI(img, bmp.Height, bmp.Width);

            for (int h=0; h<bmp.Height; h++)
                for (int w=0; w<bmp.Width; w++)
                {
                    hsi[1][h][w] = (hsi[1][h][w] * 1.10f > 1f ? 1f : hsi[1][h][w] * 1.10f);
                    hsi[2][h][w] = (hsi[2][h][w] * 1.10f > 1f ? 1f : hsi[2][h][w] * 1.10f);
                }

            img = FromHSI(hsi, bmp.Height, bmp.Width);

            if (spawn) X.CreateSibling(img, "HSI Color Enhancement of " + bmp.ToString());
            else
            {
                ((frmStandard)X).Image = Normalize.FromFloat(img);
                Fourier.CImage[] cimg = new Fourier.CImage[3];
                for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(img[i], bmp.Height, bmp.Width);
            }
        }

        public static float[][][] ToHSI(float[][][] img, int H, int W)
        {
            float[][][] hsi = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                hsi[i] = new float[H][];
                for (int h = 0; h < H; h++)
                {
                    hsi[i][h] = new float[W];
                }
            }
            for (int h = 0; h < H; h++)
            {
                for (int w = 0; w < W; w++)
                {
                    ColorRoutines.ImageLibrary.Pixel_RGBtoHSI(img[0][h][w], img[1][h][w], img[2][h][w], out hsi[0][h][w], out hsi[1][h][w], out hsi[2][h][w]);
                }
            }
            return hsi;
        }

        public static float[][][] FromHSI(float[][][] hsi, int H, int W)
        {
            float[][][] img = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                img[i] = new float[H][];
                for (int h = 0; h < H; h++)
                {
                    img[i][h] = new float[W];
                }
            }
            for (int h = 0; h < H; h++)
            {
                for (int w = 0; w < W; w++)
                {
                    ColorRoutines.ImageLibrary.Pixel_HSItoRGB(hsi[0][h][w], hsi[1][h][w], hsi[2][h][w], out img[0][h][w], out img[1][h][w], out img[2][h][w]);
                }
            }
            return img;
        }
    }
}
