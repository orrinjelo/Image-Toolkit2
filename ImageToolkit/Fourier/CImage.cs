using System;
using System.Drawing;
using System.Drawing.Imaging;
using Exocortex.DSP;

namespace Fourier
{
	/// <summary>
	/// Summary description for CImage.
	/// <p>Comments? Questions? Bugs? Tell Ben Houston at ben@exocortex.org</p>
	/// <p>Version: March 22, 2002</p>
	/// </summary>
	public class CImage {

		//--------------------------------------------------------------------------------------
		
		public unsafe CImage( string fileName ) {
			Bitmap bitmap = new Bitmap( fileName );
			Size correctSize = new Size(
				(int) Math.Pow( 2, Math.Ceiling( Math.Log( bitmap.Width, 2 ) ) ),
				(int) Math.Pow( 2, Math.Ceiling( Math.Log( bitmap.Height, 2 ) ) ) );
			if( correctSize != bitmap.Size ) {
				bitmap = new Bitmap( bitmap, correctSize );
			}

			_size = correctSize;
			_data = new ComplexF[ this.Width * this.Height ];
			Rectangle rect = new Rectangle( 0, 0, this.Width, this.Height );
			BitmapData bitmapData = bitmap.LockBits( rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb );
			int* colorData = (int*) bitmapData.Scan0.ToPointer();
			for( int i = 0; i < this.Width * this.Height; i ++ ) {
				Color c = Color.FromArgb( colorData[ i ] );
				_data[ i ].Re = ( (float)c.R + (float)c.G + (float)c.B ) / ( 3f * 256f );
			}
			bitmap.UnlockBits( bitmapData );
		}

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
            _data = new ComplexF[this.Width * this.Height];
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < this.Width * this.Height; i++)
            {
                Color c = Color.FromArgb(colorData[i]);
                _data[i].Re = ((float)c.R + (float)c.G + (float)c.B) / (3f * 256f);
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
            _data = new ComplexF[this.Width * this.Height];
           
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapDatai = bitmapi.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            int* colorDatai = (int*)bitmapDatai.Scan0.ToPointer();
            for (int i = 0; i < this.Width * this.Height; i++)
            {
                Color c = Color.FromArgb(colorData[i]);
                Color ci = Color.FromArgb(colorDatai[i]);
                _data[i].Re = ((float)c.R + (float)c.G + (float)c.B) / (3f * 256f) ;
                _data[i].Im = ((float)ci.R + (float)ci.G + (float)ci.B) / (3f * 256f) ; 
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

            _data = new ComplexF[this.Width * this.Height];

            int counter = 0;
            for (int h=0; h<this.Height; h++)
            {
                for (int w=0; w<this.Width; w++)
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

		protected ComplexF[]	_data = null;
		public ComplexF[]	Data {
			get	{	return	_data;	}
            set {   _data = value;  }
		}

		protected bool	_frequencySpace = false;
		public bool	FrequencySpace {
			get	{	return	_frequencySpace;	}
			set	{	_frequencySpace = value;	}
		}

		protected Size	_size = Size.Empty;
		public Size	Size {
			get	{	return	_size;	}
		}
		public int	Width {
			get	{	return	_size.Width;	}
		}
		public int	Height {
			get	{	return	_size.Height;	}
		}

		//--------------------------------------------------------------------------------------

        public unsafe Bitmap ToBitmap()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < this.Width * this.Height; i++)
            {
                int c = Math.Min(255, Math.Max(0, (int)(256 * _data[i].GetModulus())));
                colorData[i] = Color.FromArgb(c, c, c).ToArgb();
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }


        public unsafe Bitmap Magnitude()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < this.Width * this.Height; i++)
            {
                int c = Math.Min(255, Math.Max(0, (int)(256 * _data[i].GetModulus())));
                colorData[i] = Color.FromArgb(c, c, c).ToArgb();
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public unsafe Bitmap Real()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < this.Width * this.Height; i++)
            {
                int c = Math.Min(255, Math.Max(0, (int)(256 * _data[i].Re)));
                colorData[i] = Color.FromArgb(c, c, c).ToArgb();
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public unsafe Bitmap Imag()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < this.Width * this.Height; i++)
            {
                int c = Math.Min(255, Math.Max(0, (int)(256 * _data[i].Im)));
                colorData[i] = Color.FromArgb(c, c, c).ToArgb();
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        public unsafe Bitmap Phase()
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int* colorData = (int*)bitmapData.Scan0.ToPointer();
            for (int i = 0; i < this.Width * this.Height; i++)
            {
                int c = Math.Min(255, Math.Max(0, (int)(256 * Math.Atan2(_data[i].Im , _data[i].Re))));
                colorData[i] = Color.FromArgb(c, c, c).ToArgb();
            }
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }


		public void	Save( string fileName ) {
			this.ToBitmap().Save( fileName );
		}

        public float[][] FromFloatReal()
        {
            int count = 0;
            float[][] img = new float[this.Height][];
            for (int h=0; h<this.Height; h++)
            {
                img[h] = new float[this.Height];
                for (int w = 0; w < this.Width; w++)
                {
                    img[h][w] = (float)Math.Min(1.0, Math.Max(0.0, (_data[count++].Re)));
                }
            }
            return img;
        }

        public float[][] FromFloatImag()
        {
            int count = 0;
            float[][] img = new float[this.Height][];
            for (int h = 0; h < this.Height; h++)
            {
                img[h] = new float[this.Height];
                for (int w = 0; w < this.Width; w++)
                {
                    img[h][w] = (float)Math.Min(1.0, Math.Max(0.0, (_data[count++].Im)));
                }
            }
            return img;
        }

        public float[][] FromFloatModulus()
        {
            int count = 0;
            float[][] img = new float[this.Height][];
            for (int h = 0; h < this.Height; h++)
            {
                img[h] = new float[this.Height];
                for (int w = 0; w < this.Width; w++)
                {
                    img[h][w] = _data[count++].GetModulus();
                }
            }
            return img;
        }

        public float[][] FromFloatModulusSquared()
        {
            int count = 0;
            float[][] img = new float[this.Height][];
            for (int h = 0; h < this.Height; h++)
            {
                img[h] = new float[this.Height];
                for (int w = 0; w < this.Width; w++)
                {
                    img[h][w] = _data[count++].GetModulusSquared();
                }
            }
            return img;
        }
		
		//--------------------------------------------------------------------------------------

	}
}
