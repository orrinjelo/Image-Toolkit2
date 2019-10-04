using Exocortex.DSP;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fourier
{
    public class CImage
    {
        public unsafe CImage(string fileName) : this(new Bitmap(fileName)) { }

        public unsafe CImage(Bitmap bmp)
        {
            Bitmap bitmap = new Bitmap(bmp);
            Size correctSize = new Size(
                (int)Math.Pow(2, Math.Ceiling(Math.Log(bitmap.Width, 2))),
                (int)Math.Pow(2, Math.Ceiling(Math.Log(bitmap.Height, 2))));
            if (correctSize != bitmap.Size)
            {
                bitmap = new Bitmap(bitmap, correctSize);
            }

            _size = correctSize;
            _data = new ComplexF[Width * Height];
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < Width * Height; i++)
            {
                Color c = Color.FromArgb(colorData[i]);
                _data[i].Re = ((float)c.R + c.G + c.B) / (3f * 256f);
            }
            bitmap.UnlockBits(bitmapData);
        }

        public unsafe CImage(Bitmap bmp, Bitmap bmpi)
        {
            Bitmap bitmap = new Bitmap(bmp);
            Bitmap bitmapi = new Bitmap(bmpi);
            Size correctSize = new Size(
                (int)Math.Pow(2, Math.Ceiling(Math.Log(bitmap.Width, 2))),
                (int)Math.Pow(2, Math.Ceiling(Math.Log(bitmap.Height, 2))));
            if (correctSize != bitmap.Size)
            {
                bitmap = new Bitmap(bitmap, correctSize);
                bitmapi = new Bitmap(bitmapi, correctSize);
            }

            _size = correctSize;
            _data = new ComplexF[Width * Height];

            Rectangle rect = new Rectangle(0, 0, Width, Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDatai = bitmapi.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            int* colorDatai = (int*)bitmapDatai.Scan0.ToPointer();
            for (int i = 0; i < Width * Height; i++)
            {
                Color c = Color.FromArgb(colorData[i]);
                Color ci = Color.FromArgb(colorDatai[i]);
                _data[i].Re = ((float)c.R + c.G + c.B) / (3f * 256f);
                _data[i].Im = ((float)ci.R + ci.G + ci.B) / (3f * 256f);
            }
            bitmap.UnlockBits(bitmapData);
            bitmapi.UnlockBits(bitmapDatai);
        }

        public unsafe CImage(float[][] img, int H, int W)
        {
            Size correctSize = new Size(
                (int)Math.Pow(2, Math.Ceiling(Math.Log(W, 2))),
                (int)Math.Pow(2, Math.Ceiling(Math.Log(H, 2))));

            _size = correctSize;

            _data = new ComplexF[Width * Height];

            int counter = 0;
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    if (h < H && w < W)
                    {
                        _data[counter].Re = img[h][w];
                        _data[counter++].Im = 0;
                    }
                    else
                    {
                        _data[counter].Re = 0;
                        _data[counter].Im = 0;
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------

        protected ComplexF[] _data = null;
        public ComplexF[] Data
        {
            get => _data;
            set { _data = value; }
        }

        protected bool _frequencySpace = false;
        public bool FrequencySpace
        {
            get => _frequencySpace;
            set { _frequencySpace = value; }
        }

        protected Size _size = Size.Empty;
        public Size Size
            => _size;

        public int Width =>
             _size.Width;

        public int Height =>
            _size.Height;

        //--------------------------------------------------------------------------------------

        public unsafe Bitmap ToBitmap()
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < Width * Height; i++)
            {
                int c = Math.Min(255, Math.Max(0, (int)(256 * _data[i].GetModulus())));
                colorData[i] = Color.FromArgb(c, c, c).ToArgb();
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public unsafe Bitmap Magnitude()
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < Width * Height; i++)
            {
                int c = Math.Min(255, Math.Max(0, (int)(256 * _data[i].GetModulus())));
                colorData[i] = Color.FromArgb(c, c, c).ToArgb();
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public unsafe Bitmap Real() =>
            Image(false);

        public unsafe Bitmap Imag() =>
            Image(true);

        public unsafe Bitmap Image(bool mode)
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < Width * Height; i++)
            {
                int c = Math.Min(255, Math.Max(0, (int)(256 * (mode ? _data[i].Im : _data[i].Re))));
                colorData[i] = Color.FromArgb(c, c, c).ToArgb();
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public unsafe Bitmap Phase()
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < Width * Height; i++)
            {
                int c = Math.Min(255, Math.Max(0, (int)(256 * Math.Atan2(_data[i].Im, _data[i].Re))));
                colorData[i] = Color.FromArgb(c, c, c).ToArgb();
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public void Save(string fileName)
        {
            ToBitmap().Save(fileName);
        }

        public float[][] FromFloatImag() =>
            FromFloat(0);

        public float[][] FromFloatReal() =>
            FromFloat(1);

        public float[][] FromFloatModulus() =>
            FromFloat(3);

        public float[][] FromFloatModulusSquared() =>
            FromFloat(4);

        public float[][] FromFloat(int mode)
        {
            int count = 0;
            float[][] img = new float[Height][];
            for (int h = 0; h < Height; h++)
            {
                img[h] = new float[Height];
                for (int w = 0; w < Width; w++)
                {
                    img[h][w] =
                        mode == 0 ?
                            (float)Math.Min(1.0, Math.Max(0.0, (_data[count++].Im))) :
                        mode == 1 ?
                            (float)Math.Min(1.0, Math.Max(0.0, (_data[count++].Re))) :
                        mode == 2 ?
                            _data[count++].GetModulus() :
                            _data[count++].GetModulusSquared();

                }
            }
            return img;
        }
    }
}