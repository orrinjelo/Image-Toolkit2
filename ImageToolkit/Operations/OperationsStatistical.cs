using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToolkit.Operations
{
    public static class OperationsStatistical
    {
        public static void RegisterOperations()
        {
            OperationRegistry.RegisterOperation(new Operation("Statistical Filters", "Median", "Median(X)", Median, true));
            OperationRegistry.RegisterOperation(new Operation("Statistical Filters", "Min", "Min(X)", Min, true));
            OperationRegistry.RegisterOperation(new Operation("Statistical Filters", "Max", "Max(X)", Max, true));
            OperationRegistry.RegisterOperation(new Operation("Statistical Filters", "Midpoint", "Midpoint(X)", Midpoint, true));
            OperationRegistry.RegisterOperation(new Operation("Statistical Filters", "Alpha-trimmed (2)", "AlphaTrim(X)", AlphaTrim, true));
        }

        public static void Median()
        {
            Median(null);
        }

        public static void Min()
        {
            Min(null);
        }

        public static void Max()
        {
            Max(null);
        }

        public static void Midpoint()
        {
            Midpoint(null);
        }

        public static void AlphaTrim()
        {
            AlphaTrim(null);
        }

        public static float Mid(float[] sourceNumbers)
        {
            if (sourceNumbers.Length == 0 || sourceNumbers == null) return 0f;

            float[] sortedNums = (float[])sourceNumbers.Clone();
            sourceNumbers.CopyTo(sortedNums, 0);
            Array.Sort(sortedNums);

            int size = sortedNums.Length;
            int mid = size / 2;
            float median = (size % 2 != 0) ? (float)sortedNums[mid] : ((float)sortedNums[mid] + (float)sortedNums[mid - 1]) / 2;
            return median;
           
        }

        public static float Alpha2(float[] sourceNumbers)
        {
            if (sourceNumbers.Length == 0 || sourceNumbers == null) return 0f;

            float[] sortedNums = (float[])sourceNumbers.Clone();
            sourceNumbers.CopyTo(sortedNums, 0);
            Array.Sort(sortedNums);

            int size = sortedNums.Length;

            if (size <= 4) return sortedNums.Average();
            float sum = 0;
            for (int i = 2; i < size - 2; i++)
                sum += sortedNums[i];
            return sum / (size - 4);
        }

        public static void Median(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            float[][][] img = Normalize.ToFloat(x.GetBitmap());
            float[][][] omg = Normalize.ToFloat(x.GetBitmap());
            int H = x.GetBitmap().Height, W = x.GetBitmap().Width;

            for (int c=0; c<3; c++)
                for (int h = 0; h < H; h++)
                {
                    for (int w = 0; w < W; w++)
                    {
                        if (h == 0)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Mid(n);
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Mid(n);
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Mid(n);
                            }
                        }
                        else if (h == H - 1)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Mid(n);
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Mid(n);
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Mid(n);
                            }
                        }
                        else
                        {
                            if (w == 0)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Mid(n);
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Mid(n);
                            }
                            else
                            {
                                float[] n = new float[9];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Mid(n);
                            }
                        }
                    }
                }

            if (spawn) x.CreateSibling(omg, "Median filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(omg);
        }

        public static void Min(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            float[][][] img = Normalize.ToFloat(x.GetBitmap());
            float[][][] omg = Normalize.ToFloat(x.GetBitmap());
            int H = x.GetBitmap().Height, W = x.GetBitmap().Width;

            for (int c = 0; c < 3; c++)
                for (int h = 0; h < H; h++)
                {
                    for (int w = 0; w < W; w++)
                    {
                        if (h == 0)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Min();
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Min();
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Min();
                            }
                        }
                        else if (h == H - 1)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Min();
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Min();
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Min();
                            }
                        }
                        else
                        {
                            if (w == 0)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Min();
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Min();
                            }
                            else
                            {
                                float[] n = new float[9];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Min();
                            }
                        }
                    }
                }

            if (spawn) x.CreateSibling(omg, "Minimum filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(omg);
        }

        public static void Max(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            float[][][] img = Normalize.ToFloat(x.GetBitmap());
            float[][][] omg = Normalize.ToFloat(x.GetBitmap());
            int H = x.GetBitmap().Height, W = x.GetBitmap().Width;

            for (int c = 0; c < 3; c++)
                for (int h = 0; h < H; h++)
                {
                    for (int w = 0; w < W; w++)
                    {
                        if (h == 0)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Max();
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Max();
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Max();
                            }
                        }
                        else if (h == H - 1)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Max();
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Max();
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Max();
                            }
                        }
                        else
                        {
                            if (w == 0)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Max();
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Max();
                            }
                            else
                            {
                                float[] n = new float[9];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = n.Max();
                            }
                        }
                    }
                }

            if (spawn) x.CreateSibling(omg, "Maximum filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(omg);
        }

        public static void Midpoint(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            float[][][] img = Normalize.ToFloat(x.GetBitmap());
            float[][][] omg = Normalize.ToFloat(x.GetBitmap());
            int H = x.GetBitmap().Height, W = x.GetBitmap().Width;

            for (int c = 0; c < 3; c++)
                for (int h = 0; h < H; h++)
                {
                    for (int w = 0; w < W; w++)
                    {
                        if (h == 0)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = (n.Min() + n.Max())/2f;
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = (n.Min() + n.Max()) / 2f;
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = (n.Min() + n.Max()) / 2f;
                            }
                        }
                        else if (h == H - 1)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = (n.Min() + n.Max()) / 2f;
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = (n.Min() + n.Max()) / 2f;
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = (n.Min() + n.Max()) / 2f;
                            }
                        }
                        else
                        {
                            if (w == 0)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = (n.Min() + n.Max()) / 2f;
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = (n.Min() + n.Max()) / 2f;
                            }
                            else
                            {
                                float[] n = new float[9];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = (n.Min() + n.Max()) / 2f;
                            }
                        }
                    }
                }

            if (spawn) x.CreateSibling(omg, "Midpoint filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(omg);
        }

        public static void AlphaTrim(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            float[][][] img = Normalize.ToFloat(x.GetBitmap());
            float[][][] omg = Normalize.ToFloat(x.GetBitmap());
            int H = x.GetBitmap().Height, W = x.GetBitmap().Width;

            for (int c = 0; c < 3; c++)
                for (int h = 0; h < H; h++)
                {
                    for (int w = 0; w < W; w++)
                    {
                        if (h == 0)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Alpha2(n);
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Alpha2(n);
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = 0; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Alpha2(n);
                            }
                        }
                        else if (h == H - 1)
                        {
                            if (w == 0)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Alpha2(n);
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[4];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Alpha2(n);
                            }
                            else
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 1; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Alpha2(n);
                            }
                        }
                        else
                        {
                            if (w == 0)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = 0; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Alpha2(n);
                            }
                            else if (w == W - 1)
                            {
                                float[] n = new float[6];
                                int count = 0;
                                for (int i = -1; i < 1; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Alpha2(n);
                            }
                            else
                            {
                                float[] n = new float[9];
                                int count = 0;
                                for (int i = -1; i < 2; i++)
                                    for (int j = -1; j < 2; j++)
                                        n[count++] = img[c][h + j][w + i];
                                omg[c][h][w] = Alpha2(n);
                            }
                        }
                    }
                }

            if (spawn) x.CreateSibling(omg, "Alpha Trim (2) filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(omg);
        }
    }
}
