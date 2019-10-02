using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;

namespace ImageToolkit.ColorRoutines
{

	public class ImageLibrary
	{


		/// <summary>
		/// Convert RGB pixel values to HSI.
		/// </summary>
		/// <param name="red">IN: Scaled Red Value (0.0 to 1.0).</param>
		/// <param name="green">IN: Scaled Green Value (0.0 to 1.0)</param>
		/// <param name="Blue">IN: Scaled Blue Value (0.0 to 1.0)</param>
		/// <param name="hue">OUT: Scaled HSI Hue Value (0.0 to 1.0)</param>
		/// <param name="saturation">OUT: Scaled HSI Saturation Value (0.0 to 1.0)</param>
		/// <param name="intensity">OUT: Scaled HSI Intensity Value (0.0 to 1.0)</param>
		/// <remarks></remarks>
		public static void Pixel_RGBtoHSI(
			float red,
			float green,
			float Blue,
			out float hue,
			out float saturation,
			out float intensity)
		{
			// In this function a small constant (1.0E-11F) is added occasionally to prevent division by 0
			float ratio, theta, numerator, denominator;
			float rgb;

			numerator = ((red - green) + (red - Blue)) / 2.0F;
			denominator = (float)Math.Sqrt(Math.Pow((red - green), 2) + ((red - Blue) * (green - Blue))) + 1.0E-11F;
			ratio = (numerator / denominator);

			theta = (float)ToDegrees(Math.Acos(ratio));

			hue = ((Blue <= green ? theta : 360.0F - theta) / 360.0F);
			rgb = red + green + Blue + 1.0E-11F;
			saturation = 1.0F - (3.0F / (rgb) * (Math.Min(red, Math.Min(green, Blue))));
			intensity = (rgb) / 3.0F;
			if (saturation < 0.0F) saturation = 0.0F;
			if (intensity < 0.0F) intensity = 0.0F;
		}

		/// <summary>
		/// Covert HSI pixel values to RGB
		/// </summary>
		/// <param name="hue">IN: Scaled HSI Hue Value (0.0 to 1.0)</param>
		/// <param name="saturation">IN: Scaled HSI Saturation Value (0.0 to 1.0)</param>
		/// <param name="intensity">IN: Scaled HSI Intensity Value (0.0 to 1.0)</param>
		/// <param name="red">OUT: Scaled Red Value (0.0 to 1.0).</param>
		/// <param name="green">OUT: Scaled Green Value (0.0 to 1.0)</param>
		/// <param name="blue">OUT: Scaled Blue Value (0.0 to 1.0)</param>
		/// <remarks></remarks>
		public static void Pixel_HSItoRGB(
				float hue,
				float saturation,
				float intensity,
				out float red,
				out float green,
				out float blue)
		{
			double h, hueRatio;
			red = 0.0F;
			green = 0.0F;
			blue = 0.0F;

			h = hue * 360.0F;

			if (0.0D <= h && h < 120.0D)     // RG Sector
			{
				blue = intensity * (1.0F - saturation);
				hueRatio = (saturation * Math.Cos(ToRadians(h))) / Math.Cos(ToRadians(60.0D - h));
				red = intensity * ((float)(1.0D + hueRatio));
				green = 3.0F * intensity - (red + blue);
			}
			else if (120.0D <= h && h < 240.0D)  // GB Sector
			{
				h = h - 120.0F;
				red = intensity * (1.0F - saturation);
				hueRatio = (saturation * Math.Cos(ToRadians(h))) / Math.Cos(ToRadians(60.0D - h));
				green = intensity * (1.0F + (float)hueRatio);
				blue = 3.0F * intensity - (red + green);
			}
			else if (240.0 <= h && h <= 360.0)       // BR Sector
			{
				h = h - 240.0D;
				green = intensity * (1.0F - saturation);
				hueRatio = (float)(((double)saturation) * Math.Cos(ToRadians(h))) / Math.Cos(ToRadians(60.0D - h));
				blue = intensity * (1.0F + (float)hueRatio);
				red = 3.0F * intensity - (green + blue);
			}

			if (red < 0.0) red = 0.0F;
			if (red > 1.0) red = 1.0F;
			if (green < 0.0) green = 0.0F;
			if (green > 1.0) green = 1.0F;
			if (blue < 0.0) blue = 0.0F;
			if (blue > 1.0) blue = 1.0F;
		}

		/// <summary>
		/// Checks to see if an image is a grayscale image even if it has multiple planes.
		/// </summary>
		/// <param name="testImage">Image to test</param>
		/// <returns>true if grayscale.</returns>
		public static bool IsGrayScale(float[][][] testImage)
		{
			if (testImage.GetLength(0) == 1) return true;

			bool result = true;
			int maxC, maxH, maxW, c, h, w;
			float margin = 2.0F / 255.0F;   // margin for error
			float r, g, b;

			maxC = testImage.GetLength(0);
			maxH = testImage[0].GetLength(0);
			maxW = testImage[0][0].GetLength(0);

			for (c = 0; c < maxC; c++)
			{
				for (h = 0; h < maxH; h += 4)
				{
					for (w = 0; w < maxW; w += 4)
					{
						r = testImage[Normalize.RGBPLANE_RED][h][w];
						g = testImage[Normalize.RGBPLANE_GREEN][h][w];
						b = testImage[Normalize.RGBPLANE_BLUE][h][w];
						if ((Math.Abs(r - b) + Math.Abs(b - g) + Math.Abs(g - r)) > margin) result = false;
						if (!result) break;
					}
					if (!result) break;
				}
				if (!result) break;
			}
			return result;
		}

		/// <summary>
		/// Checks to see if bitmap contians a grayscale image.
		/// </summary>
		/// <param name="testImage">Image to test</param>
		/// <returns>true if grayscale.</returns>
		public unsafe static bool IsGrayScale(Bitmap testImage)
		{
			bool result = true;
			int h, w;
			int r, g, b;
			int margin = 2;   // margin for error in intensity values (pixel color's within two points of eachother
			Rectangle rect;
			byte* ptr;    // the use of pointers is what makes this "unsafe" code.
			System.Drawing.Imaging.BitmapData bmpData = null;  // will hold the data about the bitmap image stored in managed memory.

			try
			{
				// Force the image to 32 bit ARGB memory bitmap format, lock it in manage memory, and validate that it is the type of image data expected.

				rect = new Rectangle(0, 0, testImage.Width, testImage.Height);
				bmpData = testImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);  // lock it and get it's descriptor
				if (bmpData.Stride < 0) throw new ApplicationException("ToSingle only works on images with a positive stride.");
				if (bmpData.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb) throw new ApplicationException("Wrong Pixel Format. ToSingle only works on 32 bit ARGB pixels.");

				// Get a C style pointer to the image data

				ptr = (byte*)bmpData.Scan0.ToPointer();

				// Test pixel color planes for same value
				// 32 bit Memory Bitmap image data is ordered blue byte, green byte, red byte, and Alpha channel byte (transparancy).

				for (h = 0; h < testImage.Height; h++)    // cross the image rows
				{
					for (w = 0; w < testImage.Width; w++) // cross the image columns
					{
						b = (int)*ptr++;
						g = (int)*ptr++;
						r = (int)*ptr++;
						ptr++;  // skip the transparency byte
						if ((Math.Abs(r - b) + Math.Abs(b - g) + Math.Abs(g - r)) > margin) result = false;
						if (!result) break;
					}
					if (!result) break;
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (bmpData != null) testImage.UnlockBits(bmpData);     // Unlock the image data in managed code
			}
			return result;
		}

		public static double ToDegrees(double radians)
		{
			//degrees = radians *(180 / Math.PI)
			return radians * 180.0F / Math.PI;
		}

		public static double ToRadians(double degrees)
		{
			//radians = degrees *(Math.PI / 180)
			return degrees * (Math.PI / 180.0F);
		}

		public static double ToPolarAngle(int x, int y)
		{
			double angle;     //In radians
			angle = Math.Atan2((double)y, (double)x);
			if (angle < 0D) angle = 2D * Math.PI + angle;
			return ToDegrees((float)angle);
		}

		public static double ToPolarRadius(int x, int y)
		{
			double radius = Math.Sqrt((double)(x * x + y * y));
			return radius;
		}

		public static System.Drawing.Point ToCartesian(double theta, double radius)
		{
			//x = r cos θ and y = r sin θ.
			double radAngle = ToRadians(theta);
			int x = (int)(radius * Math.Cos(radAngle));
			int y = (int)(radius * Math.Sin(radAngle));
			return new Point(x, y);
		}

		public class cImageMinMax
		{
			public float min;
			public float max;

			public cImageMinMax(float min, float max)
			{
				this.min = min;
				this.max = max;
			}
		}

		public static cImageMinMax GetMinMax(float[][][] sourceImage)
		{
			float min = float.MaxValue;
			float max = float.MinValue;

			int h, w, c, maxC, maxW, maxH;

			// Get dimensions of sourceImage levels

			maxC = sourceImage.GetLength(0);
			maxH = sourceImage[0].GetLength(0);
			maxW = sourceImage[0][0].GetLength(0);

			// Gather min and max values across each color plane

			for (c = 0; c < maxC; c++)
			{
				for (h = 0; h < maxH; h++)
				{
					for (w = 0; w < maxW; w++)
					{
						if (sourceImage[c][h][w] < min) min = sourceImage[c][h][w];
						if (sourceImage[c][h][w] > max) max = sourceImage[c][h][w];
					}
				}
			}
			return new cImageMinMax(min, max);
		}

		/// <summary>
		/// Load an image from a file ensuring file is not left locked
		/// </summary>
		/// <param name="fileName">File specification (path and filename)</param>
		/// <returns>image as Bitmap</returns>
		public static Bitmap LoadImageFromFile(string fileName)
		{
			Graphics g;
			Bitmap sourceImage = null, tempImage = null;
			System.IO.FileStream fs = null;
			try
			{
				// This is a long work around for a bug in VS2005 and VS2003
				// that would cause GDI errors if you did a new Bitmap from a Bitmap
				tempImage = new Bitmap(fileName);
				sourceImage = new Bitmap(tempImage.Size.Width, tempImage.Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				sourceImage.SetResolution(tempImage.HorizontalResolution, tempImage.VerticalResolution);
				g = Graphics.FromImage(sourceImage);
				g.DrawImage(tempImage, 0, 0);
				tempImage.Dispose();
				g.Dispose();

				// Specify a valid picture file path on your computer.  Using this process does not leave image file locked
				//fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				//sourceImage = (Bitmap)System.Drawing.Image.FromStream(fs);
			}
			catch (Exception ex)
			{
				ErrorMessage.Display(ex, "frmImageColorRGB(fileName)");
			}
			finally
			{
				if (fs != null)
				{
					fs.Close();
					fs.Dispose();
				}
			}
			return sourceImage;
		}

		/// <summary>
		/// Generate a resized image for display in a form control that maintains the aspect ratio of the image
		/// </summary>
		/// <param name="sourceImage">Image to create new, resized, image from</param>
		/// <param name="sizeToFit">Size of the form control to fit.</param>
		/// <returns>Copy of image sized to fit inside control.</returns>
		/// <remarks></remarks>
		public static Bitmap GetBestFitImage(Bitmap sourceImage, Size sizeToFit, int resolution)
		{
			int maxW, maxH;
			int displayMaxWidth = sizeToFit.Width, displayMaxHeight = sizeToFit.Height;
			float controlpectRatio, imagepectRatio, scale;
			Bitmap tempImage = null;
			Graphics g = null;

			if (sourceImage.Width > displayMaxWidth || sourceImage.Height > displayMaxHeight)
			{
				imagepectRatio = Convert.ToSingle(sourceImage.Width) / Convert.ToSingle(sourceImage.Height);
				controlpectRatio = Convert.ToSingle(displayMaxWidth) / Convert.ToSingle(displayMaxHeight);

				if (imagepectRatio > controlpectRatio)
				{
					// image shape is more horizontal than control//s shape;
					scale = Convert.ToSingle(displayMaxWidth) / Convert.ToSingle(sourceImage.Width);
					maxW = displayMaxWidth;
					maxH = Convert.ToInt32(Convert.ToSingle(sourceImage.Height) * scale);  //CSng(displayMaxWidth) / imagepectRatio;
				}
				else
				{
					// image is taller than control
					scale = Convert.ToSingle(displayMaxHeight) / Convert.ToSingle(sourceImage.Height);
					maxW = Convert.ToInt32(Convert.ToSingle(sourceImage.Width) * scale);     //CSng(displayMaxHeight) * imagepectRatio;
					maxH = displayMaxHeight;
				}
			}
			else
			{
				maxW = sourceImage.Width;
				maxH = sourceImage.Height;
			}

			tempImage = new Bitmap(maxW, maxH, PixelFormat.Format32bppArgb);
			tempImage.SetResolution(resolution, resolution);
			g = Graphics.FromImage(tempImage);
			g.DrawImage(sourceImage, 0, 0, maxW, maxH);
			g.Dispose();
			g = null;

			return tempImage;
		}

	}
}

