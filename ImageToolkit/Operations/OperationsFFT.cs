using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Exocortex.DSP;

namespace ImageToolkit.Operations
{

    public static class OperationsFFT
    {
        public static void RegisterOperations()
        {

            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "FFT", "FFT(X)", FFT, true));
            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "Inverse FFT", "IFFT(X)", IFFT, true));
            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "High Pass Filter", "HighPass(X,f)", HighPass, true));
            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "Low Pass Filter", "LowPass(X,f)", LowPass, true));
            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "Band Pass Filter", "BandPass(X,f1,f2)", BandPass, true));
            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "Notch Filter", "Notch(X,f1,f2)", Notch, true));
            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "Butterworth Low Pass (n=2) Filter", "ButterworthLow(X,f)", ButterworthLow, true));
            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "Butterworth High Pass (n=2) Filter", "ButterworthHigh(X,f)", ButterworthHigh, true));
            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "Butterworth Low Pass (n=4) Filter", "ButterworthLow4(X,f)", ButterworthLow4, true));
            OperationRegistry.RegisterOperation(new Operation("Frequency Domain Filters", "Butterworth High Pass (n=4) Filter", "ButterworthHigh4(X,f)", ButterworthHigh4, true));

        }

        public static void FFT()
        {
            FFT(null);
        }

        public static void IFFT()
        {
            IFFT(null);
        }

        public static void HighPass()
        {
            HighPass(null, true);
        }

        public static void LowPass()
        {
            LowPass(null, true);
        }

        public static void BandPass()
        {
            BandPass(null, true);
        }

        public static void Notch()
        {
            Notch(null, true);
        }

        public static void ButterworthLow()
        {
            ButterworthLow(null, true);
        }

        public static void ButterworthHigh()
        {
            ButterworthHigh(null, true);
        }

        public static void ButterworthLow4()
        {
            ButterworthLow4(null, true);
        }

        public static void ButterworthHigh4()
        {
            ButterworthHigh4(null, true);
        }

        public static void FFT(IOperand X = null, bool spawn = true)
        {
            if (X == null) X = ExecutionStack.X;
            if (X == null) return;
            Bitmap bmp = X.GetBitmap();
            float[][][] img = Normalize.ToFloat(bmp);

            int H = bmp.Height, W = bmp.Width;

            img = Pad(img,H,W);

            Bitmap temp = Normalize.FromFloat(img);

            Fourier.CImage[] cimg = new Fourier.CImage[3];
            for (int i = 0; i < 3; i++)
            {
                cimg[i] = new Fourier.CImage(img[i], temp.Height, temp.Width);
                cimg[i] = _FFT(cimg[i]);


                img[i] = cimg[i].FromFloatModulus();
            }


            if (spawn) ((frmStandard)X).CreateSibling(img, "( Magnitude) FFT of " + bmp.ToString(), cimg);
            else
            {
                ((frmStandard)X).Image = Normalize.FromFloat(img);
                ((frmStandard)X).cimg = cimg;
            }
        }

        public static Fourier.CImage _FFT(Fourier.CImage cimg)
        {
            float scale = 1f / (float)Math.Sqrt(cimg.Width * cimg.Height);
            ComplexF[] data = cimg.Data;

            int offset = 0;
            for (int y = 0; y < cimg.Height; y++)
            {
                for (int x = 0; x < cimg.Width; x++)
                {
                    if (((x + y) & 0x1) != 0)
                    {
                        data[offset] *= -1;
                    }
                    offset++;
                }
            }

            Exocortex.DSP.Fourier.FFT2(data, cimg.Width, cimg.Height, FourierDirection.Forward);

            cimg.FrequencySpace = true;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] *= scale;
            }

            return cimg;

        }

 
        public static void IFFT(IOperand X = null, bool spawn = true)
        {
            if (X == null) X = ExecutionStack.X;
            if (X == null) return;
            Bitmap bmp = X.GetBitmap();
            float[][][] img = Normalize.ToFloat(bmp);

            int H = bmp.Height, W = bmp.Width;

            img = Pad(img, H, W);

            Fourier.CImage[] cimg = ((frmStandard)X).cimg;//new Fourier.CImage(Normalize.FromFloat(img));

            for (int i = 0; i < 3; i++)
            {

                cimg[i] = _IFFT(cimg[i]);


                img[i] = cimg[i].FromFloatModulus();
            }

            if (spawn) X.CreateSibling(img, "Inverse FFT of " + bmp.ToString());
            else
            {
                ((frmStandard)X).Image = Normalize.FromFloat(img);
                ((frmStandard)X).cimg = cimg;
            }
        }

        private static Fourier.CImage _IFFT(Fourier.CImage cimg)
        {
            float scale = 1f / (float)Math.Sqrt(cimg.Width * cimg.Height);
            ComplexF[] data = cimg.Data;


            Exocortex.DSP.Fourier.FFT2(data, cimg.Width, cimg.Height, FourierDirection.Backward);

            cimg.FrequencySpace = false;

            for (int i = 0; i < data.Length; i++)
            {
                
                data[i] *= scale;

            }

            return cimg;
        }

        public static Bitmap ColorToGrayscale(Bitmap bmp)
        {
            int w = bmp.Width,
                h = bmp.Height,
                r, ic, oc, bmpStride, outputStride, bytesPerPixel;
            PixelFormat pfIn = bmp.PixelFormat;
            ColorPalette palette;
            Bitmap output;
            BitmapData bmpData, outputData;

            //Create the new bitmap
            output = new Bitmap(w, h, PixelFormat.Format8bppIndexed);

            //Build a grayscale color Palette
            palette = output.Palette;
            for (int i = 0; i < 256; i++)
            {
                Color tmp = Color.FromArgb(255, i, i, i);
                palette.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            output.Palette = palette;

            //No need to convert formats if already in 8 bit
            if (pfIn == PixelFormat.Format8bppIndexed)
            {
                output = (Bitmap)bmp.Clone();

                //Make sure the palette is a grayscale palette and not some other
                //8-bit indexed palette
                output.Palette = palette;

                return output;
            }

            //Get the number of bytes per pixel
            switch (pfIn)
            {
                case PixelFormat.Format24bppRgb: bytesPerPixel = 3; break;
                case PixelFormat.Format32bppArgb: bytesPerPixel = 4; break;
                case PixelFormat.Format32bppRgb: bytesPerPixel = 4; break;
                default: throw new InvalidOperationException("Image format not supported");
            }

            //Lock the images
            bmpData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly,
                                   pfIn);
            outputData = output.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly,
                                         PixelFormat.Format8bppIndexed);
            bmpStride = bmpData.Stride;
            outputStride = outputData.Stride;

            //Traverse each pixel of the image
            unsafe
            {
                byte* bmpPtr = (byte*)bmpData.Scan0.ToPointer(),
                outputPtr = (byte*)outputData.Scan0.ToPointer();

                if (bytesPerPixel == 3)
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = .299*R + .587*G + .114*B
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 3, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                (0.299f * bmpPtr[r * bmpStride + ic] +
                                 0.587f * bmpPtr[r * bmpStride + ic + 1] +
                                 0.114f * bmpPtr[r * bmpStride + ic + 2]);
                }
                else //bytesPerPixel == 4
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = alpha * (.299*R + .587*G + .114*B)
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 4, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                ((bmpPtr[r * bmpStride + ic] / 255.0f) *
                                (0.299f * bmpPtr[r * bmpStride + ic + 1] +
                                 0.587f * bmpPtr[r * bmpStride + ic + 2] +
                                 0.114f * bmpPtr[r * bmpStride + ic + 3]));
                }
            }

            //Unlock the images
            bmp.UnlockBits(bmpData);
            output.UnlockBits(outputData);

            return output;
        }

        public static int PowerOfTwo(int x)
        {
            int n = 0;
            while (Math.Pow(2, n) < x)
            {
                n++;
            }
            return (int)Math.Pow(2, n);
        }

        public static float[][][] Pad(float[][][] f, int H, int W)
        {
            int N = PowerOfTwo(H > W ? H : W);
            float[][][] img = new float[3][][];
            for (int c=0; c<3; c++)
            {
                img[c] = new float[N][];
                for (int h=0; h<N; h++)
                {
                    img[c][h] = new float[N];
                    for (int w=0; w<W; w++)
                    {
                        if (h < H && w < W)
                            img[c][h][w] = f[c][h][w];
                        else
                            img[c][h][w] = 0f;
                    }
                }
            }
            return img;
        }
    
        public static Complex[,] DFT2( Complex[,] data, int H, int W, int direction=1)
        {
            int n = W,  //Rows
                m = H;  //Cols
            float arg, cos, sin;
            Complex[] dst = new Complex[System.Math.Max(H, W)];


            // Go through rows
            for (int i=0; i<n; i++)
            {
                for (int j=0; j<m; j++)
                {
                    dst[j].Im = 0f;
                    dst[j].Re = 0f;
                    arg = -(float)(direction * 2.0 * System.Math.PI * (float)j / (float)m);
                    for (int k=0; k<m; k++)
                    {
                        cos = (float)System.Math.Cos(k * arg);
                        sin = (float)System.Math.Cos(k * arg);
                        dst[j].Re += (data[i, k].Re * cos - data[i, k].Im * sin);
                        dst[j].Im += (data[i, k].Re * sin + data[i, k].Im * cos);
                    }
                }


                for (int j=0; j<m; j++)
                {
                    data[i, j].Re = dst[j].Re / (direction == 1 ? m : 1f);
                    data[i, j].Re = dst[j].Im / (direction == 1 ? m : 1f);
                }

            }

            // Go through cols
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    dst[i].Im = 0f;
                    dst[i].Re = 0f;
                    arg = -(float)(direction * 2.0 * System.Math.PI * (float)i / (float)n);
                    for (int k = 0; k < n; k++)
                    {
                        cos = (float)System.Math.Cos(k * arg);
                        sin = (float)System.Math.Cos(k * arg);
                        dst[i].Re += (data[k, j].Re * cos - data[k, j].Im * sin);
                        dst[i].Im += (data[k, j].Re * sin + data[k, j].Im * cos);
                    }
                }


                for (int i = 0; i < n; i++)
                {
                    data[i, j].Re = dst[i].Re / (direction == 1 ? m : 1f);
                    data[i, j].Re = dst[i].Im / (direction == 1 ? m : 1f);
                }

            }
            return data;
        }

        public static void DFT()
        {
            DFT(null, true, 1);
        }

        public static void IDFT()
        {
            DFT(null, true, -1);  // Doesn't seem to....work...
        }

        public static void DFT(IOperand x = null, bool spawn = true, int direction=1)
        {
 
        }

        public static void HighPass(IOperand x=null, bool spawn=true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            frmFFTFilter ff = new frmFFTFilter(frmFFTFilter.Mode.High, cimg, (frmStandard)x, spawn);
            ff.Show();
        }

        public static void LowPass(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            frmFFTFilter ff = new frmFFTFilter(frmFFTFilter.Mode.Low, cimg, (frmStandard)x, spawn);
            ff.Show();
        }

        public static void BandPass(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            frmFFTFilter ff = new frmFFTFilter(frmFFTFilter.Mode.Narrow, cimg, (frmStandard)x, spawn);
            ff.Show();
        }

        public static void Notch(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            frmFFTFilter ff = new frmFFTFilter(frmFFTFilter.Mode.Notch, cimg, (frmStandard)x, spawn);
            ff.Show();
        }

        public static void ButterworthLow(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            frmFFTFilter ff = new frmFFTFilter(frmFFTFilter.Mode.ButterLow, cimg, (frmStandard)x, spawn);
            ff.Show();
        }

        public static void ButterworthHigh(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            frmFFTFilter ff = new frmFFTFilter(frmFFTFilter.Mode.ButterHigh, cimg, (frmStandard)x, spawn);
            ff.Show();
        }

        public static void ButterworthLow4(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            frmFFTFilter ff = new frmFFTFilter(frmFFTFilter.Mode.ButterLow4, cimg, (frmStandard)x, spawn);
            ff.Show();
        }

        public static void ButterworthHigh4(IOperand x = null, bool spawn = true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;

            Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            frmFFTFilter ff = new frmFFTFilter(frmFFTFilter.Mode.ButterHigh4, cimg, (frmStandard)x, spawn);
            ff.Show();
        }

        public static void HighPass(IOperand x, bool spawn = true, float radius=0f)
        {
            if (x == null) return;  // No tolerance!
            //Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            Fourier.CImage[] cimg = new Fourier.CImage[3];
            for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(Normalize.ToFloat(((frmStandard)x).Image)[i], ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
                if (!cimg[0].FrequencySpace)
                    for (int i = 0; i < 3; i++) cimg[i] = _FFT(cimg[i]);

            int H = cimg[0].Height, W = cimg[0].Width;

            for (int c = 0; c < 3; c++ )
            {
                for (int j=0; j<H; j++)
                {
                    for (int i=0; i<W; i++)
                    {
                        if ((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) < radius * radius && !((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) == 0))
                         {
                             cimg[c].Data[j * W + i].Re = 0f;
                             cimg[c].Data[j * W + i].Im = 0f;

                         }
                    }
                }
            }

           // Debug d1 = new Debug(cimg[0].ToBitmap());
           // d1.Show();

            for (int i = 0; i < 3; i++) cimg[i] = _IFFT(cimg[i]);

            float[][][] img = new float[3][][];
            for (int i = 0; i < 3; i++ )
            {
                img[i] = cimg[i].FromFloatModulus();
            }

            if (spawn) x.CreateSibling(img, "High Pass filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);

                /*

                float[][][] real = new float[3][][];
                float[][][] imag = new float[3][][];
                for (int c = 0; c < 3; c++ )
                {
                    real[c] = cimg[c].FromFloatReal();
                    imag[c] = cimg[c].FromFloatImag();
                }
                /*
                for (int c = 0; c < 3; c++ )
                    for (int j = 0; j < cimg[0].Height; j++)
                    {
                        for (int i = 0; i < cimg[0].Width; i++)
                        {
                            if ((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) < radius * radius)
                            {
                                real[c][j][i] = 0f;
                                imag[c][j][i] = 0f;
                            
                            }
                        }
                    }
                */
                /*
              Fourier.CImage[] cr = new Fourier.CImage[3], ci = new Fourier.CImage[3];
              float[][][] img = new float[3][][];
              for (int i = 0; i < 3; i++ )
              {
                  cr[i] = new Fourier.CImage(real[i], H, W);
                  ci[i] = new Fourier.CImage(imag[i], H, W);
                  for (int j=0; j<cr[i].Data.Length; j++)
                  {
                      cr[i].Data[j].Im = ci[i].Data[j].Re;
                  }
                  cr[i] = _FFT(cr[i]);
                  img[i] = cr[i].FromFloatModulus();
              }
              */
                //Fourier.CImage ci2 = new Fourier.CImage(Normalize.FromFloat(real), Normalize.FromFloat(imag));

                //ci2 = _IFFT(ci2);
                /*
                Fourier.CImage ci2 = new Fourier.CImage(Normalize.FromFloat(real), Normalize.FromFloat(imag));

                Debug w1 = new Debug(ci2.ToBitmap());
                Debug w2 = new Debug(ci2.Magnitude());
                Debug w3 = new Debug(Normalize.FromFloat(real));
                Debug w4 = new Debug(Normalize.FromFloat(real));
                Debug w5 = new Debug(Normalize.FromFloat(imag));

                ci2 = _IFFT(ci2);

                for (int i = 0; i < 3; i++ )
                    cimg = new Fourier.CImage(cimg.Real(), cimg.Imag());
                //cimg = _IFFT(cimg);
                float[][][] img = Normalize.ToFloat(cimg.ToBitmap());

                Debug w6 = new Debug(ci2.ToBitmap());
                Debug w7 = new Debug(ci2.Magnitude());
                Debug w8 = new Debug(ci2.Real());

                w1.Show();
                w2.Show();
                w3.Show();
                w4.Show();
                w5.Show(); 
                w6.Show();
                w7.Show();
                w8.Show();


                cimg = _IFFT(cimg);

                Debug w9 = new Debug(cimg.ToBitmap());
                Debug w10 = new Debug(cimg.Magnitude());
                Debug w11 = new Debug(cimg.Real());

                w9.Show();
                w10.Show();
                w11.Show();
                 */
                //float[][][] img = new float[3][][];
                //for (int i = 0; i < 3; i++)
                //{
                // img[i] = real[i];
                //}



        }

        public static void LowPass(IOperand x, bool spawn = true, float radius = 0f)
        {
            if (x == null) return;  // No tolerance!
            //Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            Fourier.CImage[] cimg = new Fourier.CImage[3];
            for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(Normalize.ToFloat(((frmStandard)x).Image)[i], ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
            if (!cimg[0].FrequencySpace)
                for (int i = 0; i < 3; i++) cimg[i] = _FFT(cimg[i]);

            int H = cimg[0].Height, W = cimg[0].Width;

            for (int c = 0; c < 3; c++)
            {
                for (int j = 0; j < H; j++)
                {
                    for (int i = 0; i < W; i++)
                    {
                        if ((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) > radius * radius)
                        {
                            cimg[c].Data[j * W + i].Re = 0f;
                            cimg[c].Data[j * W + i].Im = 0f;

                        }
                    }
                }
            }

            // Debug d1 = new Debug(cimg[0].ToBitmap());
            // d1.Show();

            for (int i = 0; i < 3; i++) cimg[i] = _IFFT(cimg[i]);

            float[][][] img = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                img[i] = cimg[i].FromFloatModulus();
            }

            if (spawn) x.CreateSibling(img, "Low Pass filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);
        }

        public static void BandPass(IOperand x, bool spawn = true, float radius1 = 0f, float radius2 = 0f)
        {
            if (x == null) return;  // No tolerance!
            //Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            Fourier.CImage[] cimg = new Fourier.CImage[3];
            for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(Normalize.ToFloat(((frmStandard)x).Image)[i], ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
            if (!cimg[0].FrequencySpace)
                for (int i = 0; i < 3; i++) cimg[i] = _FFT(cimg[i]);

            int H = cimg[0].Height, W = cimg[0].Width;

            for (int c = 0; c < 3; c++)
            {
                for (int j = 0; j < H; j++)
                {
                    for (int i = 0; i < W; i++)
                    {
                        if ((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) > radius2 * radius2 || ((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) < radius1 * radius1  && !((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) == 0)))
                        {
                            cimg[c].Data[j * W + i].Re = 0f;
                            cimg[c].Data[j * W + i].Im = 0f;

                        }
                    }
                }
            }

            // Debug d1 = new Debug(cimg[0].ToBitmap());
            // d1.Show();

            for (int i = 0; i < 3; i++) cimg[i] = _IFFT(cimg[i]);

            float[][][] img = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                img[i] = cimg[i].FromFloatModulus();
            }

            if (spawn) x.CreateSibling(img, "Band Pass filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);
        }

        public static void Notch(IOperand x, bool spawn = true, float radius1 = 0f, float radius2 = 0f)
        {
            if (x == null) return;  // No tolerance!
            //Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            Fourier.CImage[] cimg = new Fourier.CImage[3];
            for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(Normalize.ToFloat(((frmStandard)x).Image)[i], ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
            if (!cimg[0].FrequencySpace)
                for (int i = 0; i < 3; i++) cimg[i] = _FFT(cimg[i]);

            int H = cimg[0].Height, W = cimg[0].Width;

            for (int c = 0; c < 3; c++)
            {
                for (int j = 0; j < H; j++)
                {
                    for (int i = 0; i < W; i++)
                    {
                        if ((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) < radius2 * radius2 && (i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) > radius1 * radius1)
                        {
                            cimg[c].Data[j * W + i].Re = 0f;
                            cimg[c].Data[j * W + i].Im = 0f;

                        }
                    }
                }
            }

            // Debug d1 = new Debug(cimg[0].ToBitmap());
            // d1.Show();

            for (int i = 0; i < 3; i++) cimg[i] = _IFFT(cimg[i]);

            float[][][] img = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                img[i] = cimg[i].FromFloatModulus();
            }

            if (spawn) x.CreateSibling(img, "Notch filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);
        }

        public static void ButterworthLow(IOperand x, bool spawn = true, float radius = 0f)
        {
            if (x == null) return;  // No tolerance!
            //Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            Fourier.CImage[] cimg = new Fourier.CImage[3];
            for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(Normalize.ToFloat(((frmStandard)x).Image)[i], ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
            if (!cimg[0].FrequencySpace)
                for (int i = 0; i < 3; i++) cimg[i] = _FFT(cimg[i]);

            int H = cimg[0].Height, W = cimg[0].Width;

            for (int c = 0; c < 3; c++)
            {
                for (int j = 0; j < H; j++)
                {
                    for (int i = 0; i < W; i++)
                    {
                        int D = (i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2);
                        int D0 = (int)(radius * radius);
                        {
                            cimg[c].Data[j * W + i].Re *= 1.0f / (float)Math.Pow(1.0f + (D/D0), 4);
                            cimg[c].Data[j * W + i].Im *= 1.0f / (float)Math.Pow(1.0f + (D / D0), 4);

                        }
                    }
                }
            }

            // Debug d1 = new Debug(cimg[0].ToBitmap());
            // d1.Show();

            for (int i = 0; i < 3; i++) cimg[i] = _IFFT(cimg[i]);

            float[][][] img = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                img[i] = cimg[i].FromFloatModulus();
            }

            if (spawn) x.CreateSibling(img, "Butterworth Low Pass filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);
        }

        public static void ButterworthHigh(IOperand x, bool spawn = true, float radius = 0f)
        {
            if (x == null) return;  // No tolerance!
            //Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            Fourier.CImage[] cimg = new Fourier.CImage[3];
            for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(Normalize.ToFloat(((frmStandard)x).Image)[i], ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
            if (!cimg[0].FrequencySpace)
                for (int i = 0; i < 3; i++) cimg[i] = _FFT(cimg[i]);

            int H = cimg[0].Height, W = cimg[0].Width;

            for (int c = 0; c < 3; c++)
            {
                for (int j = 0; j < H; j++)
                {
                    for (int i = 0; i < W; i++)
                    {
                        int D = (i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2);
                        int D0 = (int)(radius * radius);
                        if ((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) != 0)
                        {
                            cimg[c].Data[j * W + i].Re *= (1.0f - 1.0f / (float)Math.Pow(1.0f + (D / D0), 4));
                            cimg[c].Data[j * W + i].Im *= (1.0f - 1.0f / (float)Math.Pow(1.0f + (D / D0), 4));

                        }
                    }
                }
            }

            // Debug d1 = new Debug(cimg[0].ToBitmap());
            // d1.Show();

            for (int i = 0; i < 3; i++) cimg[i] = _IFFT(cimg[i]);

            float[][][] img = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                img[i] = cimg[i].FromFloatModulus();
            }

            if (spawn) x.CreateSibling(img, "Butterworth High Pass filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);
        }

        public static void ButterworthLow4(IOperand x, bool spawn = true, float radius = 0.01f)
        {
            if (x == null) return;  // No tolerance!
            //Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            Fourier.CImage[] cimg = new Fourier.CImage[3];
            for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(Normalize.ToFloat(((frmStandard)x).Image)[i], ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
            if (!cimg[0].FrequencySpace)
                for (int i = 0; i < 3; i++) cimg[i] = _FFT(cimg[i]);

            int H = cimg[0].Height, W = cimg[0].Width;

            for (int c = 0; c < 3; c++)
            {
                for (int j = 0; j < H; j++)
                {
                    for (int i = 0; i < W; i++)
                    {
                        int D = (i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2);
                        int D0 = (int)(radius * radius);
                        if (D0 == 0) D0 = 1;
                        {
                            cimg[c].Data[j * W + i].Re *= 1.0f / (float)Math.Pow(1.0f + (D / D0), 8);
                            cimg[c].Data[j * W + i].Im *= 1.0f / (float)Math.Pow(1.0f + (D / D0), 8);

                        }
                    }
                }
            }

            // Debug d1 = new Debug(cimg[0].ToBitmap());
            // d1.Show();

            for (int i = 0; i < 3; i++) cimg[i] = _IFFT(cimg[i]);

            float[][][] img = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                img[i] = cimg[i].FromFloatModulus();
            }

            if (spawn) x.CreateSibling(img, "Butterworth Low Pass filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);
        }

        public static void ButterworthHigh4(IOperand x, bool spawn = true, float radius = 0f)
        {
            if (x == null) return;  // No tolerance!
            //Fourier.CImage[] cimg = ((frmStandard)x).cimg;
            Fourier.CImage[] cimg = new Fourier.CImage[3];
            for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(Normalize.ToFloat(((frmStandard)x).Image)[i], ((frmStandard)x).Image.Height, ((frmStandard)x).Image.Width);
            if (!cimg[0].FrequencySpace)
                for (int i = 0; i < 3; i++) cimg[i] = _FFT(cimg[i]);

            int H = cimg[0].Height, W = cimg[0].Width;

            for (int c = 0; c < 3; c++)
            {
                for (int j = 0; j < H; j++)
                {
                    for (int i = 0; i < W; i++)
                    {
                        int D = (i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2);
                        int D0 = (int)(radius * radius);
                        if ((i - W / 2) * (i - W / 2) + (j - H / 2) * (j - H / 2) != 0)
                        {
                            cimg[c].Data[j * W + i].Re *= (1.0f - 1.0f / (float)Math.Pow(1.0f + (D / D0), 8));
                            cimg[c].Data[j * W + i].Im *= (1.0f - 1.0f / (float)Math.Pow(1.0f + (D / D0), 8));

                        }
                    }
                }
            }

            // Debug d1 = new Debug(cimg[0].ToBitmap());
            // d1.Show();

            for (int i = 0; i < 3; i++) cimg[i] = _IFFT(cimg[i]);

            float[][][] img = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                img[i] = cimg[i].FromFloatModulus();
            }

            if (spawn) x.CreateSibling(img, "Butterworth High Pass filter of " + ((frmStandard)x).GetBitmap().ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);
        }
    }


    
}
