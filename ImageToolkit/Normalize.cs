using System;
using System.Drawing;

namespace ImageToolkit
{
	public static class Normalize
	{
		public const int RGBPLANE_LENGTH = 3;
		public const int RGBPLANE_BLUE = 0;
		public const int RGBPLANE_GREEN = 1;
		public const int RGBPLANE_RED = 2;

		/// <summary>
		/// This is a high speed rountine that converts the given bitmap image to a jagged array of single percision floating point numbers.
		/// </summary>
		/// <returns>Returns a three dimensional jagged array of float with values between 0.0 and 1.0.  The first dimension represents
		/// color plane (red, green, blue), second dimension height, and third dimension is a vector of values across the image row</returns>
		/// <remarks></remarks>
		public unsafe static float[][][] ToFloat(Bitmap sourceImage)
		{
			const int colorCount = 3;
			float[][][] destination;
			int c, h, w;
			Rectangle rect;
			byte* ptr;    // the use of pointers is what makes this "unsafe" code.
			System.Drawing.Imaging.BitmapData bmpData = null;  // will hold the data about the bitmap image stored in managed memory.

			try
			{
				// Force the image to 32 bit ARGB memory bitmap format, lock it in manage memory, and validate that it is the type of image data expected.

				rect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
				bmpData = sourceImage.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);  // lock it and get it's descriptor
				if (bmpData.Stride < 0) throw new ApplicationException("ToSingle only works on images with a positive stride.");
				if (bmpData.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb) throw new ApplicationException("Wrong Pixel Format. ToSingle only works on 32 bit ARGB pixels.");

				// Get a C style pointer to the image data

				ptr = (byte*)bmpData.Scan0.ToPointer();

				// Create destination array (jagged array of singles)

				destination = new float[RGBPLANE_LENGTH][][];  // a plane for each color: red, green, blue
				for (c = 0; c < colorCount; c++)
				{
					destination[c] = new float[sourceImage.Height][];
					for (h = 0; h < sourceImage.Height; h++) destination[c][h] = new float[sourceImage.Width];
				}

				// Fill array of floating point with normalized image data.
				// 32 bit Memory Bitmap image data is ordered blue byte, green byte, red byte, and Alpha channel byte (transparancy).

				for (h = 0; h < sourceImage.Height; h++)    // cross the image rows
				{
					for (w = 0; w < sourceImage.Width; w++) // cross the image columns
					{
						destination[RGBPLANE_BLUE][h][w] = ((float)*ptr++) / 255.0F;
						destination[RGBPLANE_GREEN][h][w] = ((float)*ptr++) / 255.0F;
						destination[RGBPLANE_RED][h][w] = ((float)*ptr++) / 255.0F;
						ptr++;  // skip the transparency byte
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (bmpData != null) sourceImage.UnlockBits(bmpData);     // Unlock the image data in managed code
			}

			return destination;
		}

		/// <summary>
		/// This is a high speed rountine that converts the given jagged array of single percision floating point numbers to a .NET bitmap image object.
		/// </summary>
		/// <param name="sourceImage">A three dimensional jagged array of float with values between 0.0 and 1.0.  The first dimension represents
		/// color plane (red, green, blue), second dimension height, and third dimension is a vector of values across the image row.</param>
		/// <returns>.NET bitmap image object of format 32 bit ARGB.</returns>
		public unsafe static Bitmap FromFloat(float[][][] sourceImage)
		{
			int h, w;
			byte* ptr;  // the use of pointers is what makes this "unsafe" code.
			System.Drawing.Imaging.BitmapData bmpData = null;
			Bitmap destination = null;
			Rectangle rect;

			try
			{
				// create an empty bitmap of the correct size and format.

				destination = new Bitmap(sourceImage[0][0].GetLength(0), sourceImage[0].GetLength(0), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				// Lock the image in managed memory

				rect = new Rectangle(0, 0, destination.Width, destination.Height);
				bmpData = destination.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);;
				ptr = (byte*)bmpData.Scan0.ToPointer();

				// Fill byte image from jagged array of normalized singles

				for (h = 0; h < destination.Height; h++)
				{
					for (w = 0; w < destination.Width; w++)
					{
						if (sourceImage.GetLength(0) >= 3)  // RGB Image
						{
							*ptr++ = (byte)(sourceImage[RGBPLANE_BLUE][h][w] * 255.0F);
							*ptr++ = (byte)(sourceImage[RGBPLANE_GREEN][h][w] * 255.0F);
							*ptr++ = (byte)(sourceImage[RGBPLANE_RED][h][w] * 255.0F);
							*ptr++ = 255;  // default for transparency number;
						}
						else if (sourceImage.GetLength(0) == 1)  // Gray Scale Image
						{
							*ptr++ = (byte)(sourceImage[0][h][w] * 255.0F);
							*ptr++ = (byte)(sourceImage[0][h][w] * 255.0F);
							*ptr++ = (byte)(sourceImage[0][h][w] * 255.0F);
							*ptr++ = 255;
						}
						else
						{                   // Undefined Image
							*ptr++ = 0;
							*ptr++ = 0;
							*ptr++ = 0;
							*ptr++ = 255;
						}
					}
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (bmpData != null) destination.UnlockBits(bmpData);     // Unlock managed bitmap memory
			}

			return destination;
		}

	}
}