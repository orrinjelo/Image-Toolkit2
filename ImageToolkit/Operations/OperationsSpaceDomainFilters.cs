using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageToolkit.Operations
{

    /* Laplacian 4
    Mean
    Sobel
    Laplacian 8
     */
    class OperationsSpaceDomainFilters
    {
        public static void RegisterOperations()
        {
            OperationRegistry.RegisterOperation(new Operation("Space Domain Filters", "Laplacian 4", "Laplacian4(X)", Laplacian4, true));
            OperationRegistry.RegisterOperation(new Operation("Space Domain Filters", "Mean", "Mean(X)", Mean, true));
            OperationRegistry.RegisterOperation(new Operation("Space Domain Filters", "SobelX", "SobelX(X)", SobelX, true));
            OperationRegistry.RegisterOperation(new Operation("Space Domain Filters", "SobelY", "SobelY(X)", SobelY, true));
            OperationRegistry.RegisterOperation(new Operation("Space Domain Filters", "Laplacian 8", "Laplacian8(X)", Laplacian8, true));
        }

        public static void Laplacian4()
        {
            Laplacian4(null, true);
        }

        public static void Laplacian4(IOperand x = null, bool spawn=true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = x.GetBitmap();

            float[] filter = new float[9] { 0f, -1f, 0f, -1f, 4f, -1f, 0f, -1f, 0f };
            //float[] filter = new float[9] { 0f, -1f / scale, 0f, -1f / 4f, 4f / 4f, -1f / 4f, 0f, -1f / 4f, 0f };
            Filter f = new Filter(filter);

            bmp = Normalize.FromFloat(f.Apply(Normalize.ToFloat(bmp), bmp.Height, bmp.Width));

            if (spawn) x.CreateSibling(Normalize.ToFloat(bmp), "Laplacian(4) of " + bmp.ToString());
            else ((frmStandard)x).Image = bmp;
        }

        public static void Mean()
        {
            Mean(null, true);
        }

        public static void Mean(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = x.GetBitmap();
            float[][][] img = Normalize.ToFloat(bmp), result = Normalize.ToFloat(bmp);

            
            float weight = 1f / 9f;
            for (int h=0; h < bmp.Height; h++)
                for (int w=0; w < bmp.Width; w++)
                    for (int c=0; c<3; c++)
                    { 
                        result[c][h][w] =
                            ((h > 0 && w > 0 ? weight * img[c][h - 1][w - 1] : 0) + (h > 0 ? weight * img[c][h - 1][w] : 0) + (h > 0 && w + 1 < bmp.Width ? weight * img[c][h - 1][w + 1] : 0)  + 
                            (w > 0 ? weight * img[c][h][w-1] : 0) + ( weight * img[c][h][w] ) +  (w + 1 < bmp.Width ?  weight * img[c][h][w+1] : 0) +
                            (w > 0 && h + 1 < bmp.Height ? weight * img[c][h + 1][w - 1] : 0) + (h + 1 < bmp.Height ? weight * img[c][h + 1][w] : 0) + (h + 1 < bmp.Height && w + 1 < bmp.Width ? weight * img[c][h + 1][w + 1] : 0));
                        if (result[c][h][w] < 0.0f) result[c][h][w] = 0.0f;
                        if (result[c][h][w] > 1.0f) result[c][h][w] = 1.0f;

                     }
             /*
            float weight = 1f / 9f;
            float[] filter = new float[9] { weight, weight, weight, weight, weight, weight, weight, weight, weight };
            Filter f = new Filter(filter);

            bmp = Normalize.FromFloat(f.Apply(Normalize.ToFloat(bmp), bmp.Height, bmp.Width));

 */

            if (spawn) x.CreateSibling(result, "Mean of " + bmp.ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(result);
        }

        public static void SobelX()
        {
            SobelX(null, true);
        }

        public static void SobelX(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = x.GetBitmap();

            float[] filter = new float[9] { -1f, 0f, 1f, -2f, 0f, 2f, -1f, 0f, 1f };
            Filter f = new Filter(filter);

            bmp = Normalize.FromFloat(f.Apply(Normalize.ToFloat(bmp), bmp.Height, bmp.Width));

 

            if (spawn) x.CreateSibling(Normalize.ToFloat(bmp), "Sobel of " + bmp.ToString());
            else ((frmStandard)x).Image = bmp;
        }

        public static void SobelY()
        {
            SobelY(null, true);
        }

        public static void SobelY(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = x.GetBitmap();

            float[] filter = new float[9] { -1f, -2f, -1f, 0f, 0f, 0f, 1f, 2f, 1f };
            Filter f = new Filter(filter);

            bmp = Normalize.FromFloat(f.Apply(Normalize.ToFloat(bmp), bmp.Height, bmp.Width));

            if (spawn) x.CreateSibling(Normalize.ToFloat(bmp), "Sobel of " + bmp.ToString());
            else ((frmStandard)x).Image = bmp;
        }

        public static void Sobel()
        {
            Sobel(null, true);
        }

        public static void Sobel(IOperand x = null, bool spawn = true )
        {

        }

        public static void Laplacian8()
        {
            Laplacian8(null, true);
        }

        public static void Laplacian8(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = x.GetBitmap();


            float[] filter = new float[9] { -1f, -1f, -1f, -1f, 8f, -1f, -1f, -1f, -1f };
            Filter f = new Filter(filter);

            bmp = Normalize.FromFloat(f.Apply(Normalize.ToFloat(bmp), bmp.Height, bmp.Width));

 
            if (spawn) x.CreateSibling(Normalize.ToFloat(bmp), "Laplacian(8) of " + bmp.ToString());
            else ((frmStandard)x).Image = bmp;
        }
    }


}
