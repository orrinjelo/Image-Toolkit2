using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageToolkit
{
    public partial class FormStandard : Form, IOperand
    {
        const string FORM_TITLE = "Standard Image Form";
        static int nameCounter = 0;
        public FormMain myParent;
        readonly int formCounter;
        int originalWidth;
        int originalHeight;

        public Bitmap Image
        {
            get =>
                (Bitmap)picImage.Image;
            set
            {
                picImage.Image = value;
                lblHeight.Text = value.Height.ToString();
                lblWidth.Text = value.Width.ToString();
                lblPixels.Text = (value.Height * value.Width).ToString("#,##0");
                originalWidth = picImage.Width;
                originalHeight = picImage.Height;

                UpdateImages();
            }
        }

        public string Description { get; set; }

        public int ImageWidth =>
            Image.Width;

        public int ImageHeight =>
            Image.Height;

        public Fourier.CImage[] CImg { get; set; }


        public FormStandard()
        {
            InitializeComponent();
            formCounter = ++nameCounter;
            Name = "frmStandard" + formCounter.ToString("000");
            Text = FORM_TITLE + " : " + formCounter.ToString();
        }

        public FormStandard(FormMain parent) : this()
        {
            myParent = parent;
        }

        public FormStandard(Bitmap image, FormMain parent, Fourier.CImage[] dat = null) : this(parent)
        {
            Image = image;
            float[][][] img = Normalize.ToFloat(image);
            if (dat == null)
            {
                CImg = new Fourier.CImage[3];
                for (int i = 0; i < 3; i++) CImg[i] = new Fourier.CImage(img[i], image.Height, image.Width);
            }
            else CImg = dat;
        }

        public FormStandard(string imageName, Bitmap image, FormMain parent, string description = "", Fourier.CImage[] dat = null) : this(image, parent, dat)
        {
            Text = description;
            imgLabel.Text = imageName;
        }

        public override string ToString()
        {
            return formCounter.ToString() + ": " + imgLabel.Text;
        }

        private void HideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormStandard_FormClosing(object sender, FormClosingEventArgs e)
        {
            myParent.Remove(this);
            picImage.Image.Dispose();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        public void SaveImage(string filename = "")
        {
            DialogResult result;
            // frmStandard frm;

            try
            {
                saveFileDialog.Filter = MyConstants.FILE_FILTER_OUT;
                saveFileDialog.FilterIndex = MyConstants.FILTER_INDEX;

                if (filename == "")
                {
                    result = saveFileDialog.ShowDialog();
                    filename = saveFileDialog.FileName;
                }
                else
                    result = DialogResult.OK;

                if (result == DialogResult.OK)
                {
                    string extension = Path.GetExtension(saveFileDialog.FileName);
                    Image.Save(filename, GetFileType(extension));

                    // The following was the old way of doing stuff.
                    string oldName = ToString();
                    imgLabel.Text = Path.GetFileName(filename);
                    myParent.UpdatelstImages();

                    /*myParent.createFrmStandard(filename);*/  // Nah, went back to the other way.
                    MessageBox.Show("Saved image as " + filename);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Display(ex, "Error saving image.");
            }
        }

        private System.Drawing.Imaging.ImageFormat GetFileType(string fileType)
        {
            switch (fileType.ToLower())
            {
                case ".gif": return System.Drawing.Imaging.ImageFormat.Gif;
                case ".jpg": return System.Drawing.Imaging.ImageFormat.Jpeg;
                case ".bmp": return System.Drawing.Imaging.ImageFormat.Bmp;
                case ".tiff":
                case ".tif": return System.Drawing.Imaging.ImageFormat.Tiff;
                default: return System.Drawing.Imaging.ImageFormat.Bmp;
            };
        }


        public void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage(imgLabel.Text);
        }

        public Bitmap GetBitmap() =>
            Image;

        private void HideifMinimized(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void ZoomPic(object sender, EventArgs e)
        {
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.Width = Convert.ToInt32(zoomBar.Value * originalWidth / 100.0);
            picImage.Height = Convert.ToInt32(zoomBar.Value * originalHeight / 100.0);
            picRed.SizeMode = PictureBoxSizeMode.Zoom;
            picRed.Width = Convert.ToInt32(zoomBar.Value * originalWidth / 100.0);
            picRed.Height = Convert.ToInt32(zoomBar.Value * originalHeight / 100.0);
            picGreen.SizeMode = PictureBoxSizeMode.Zoom;
            picGreen.Width = Convert.ToInt32(zoomBar.Value * originalWidth / 100.0);
            picGreen.Height = Convert.ToInt32(zoomBar.Value * originalHeight / 100.0);
            picBlue.SizeMode = PictureBoxSizeMode.Zoom;
            picBlue.Width = Convert.ToInt32(zoomBar.Value * originalWidth / 100.0);
            picBlue.Height = Convert.ToInt32(zoomBar.Value * originalHeight / 100.0);
            picHue.SizeMode = PictureBoxSizeMode.Zoom;
            picHue.Width = Convert.ToInt32(zoomBar.Value * originalWidth / 100.0);
            picHue.Height = Convert.ToInt32(zoomBar.Value * originalHeight / 100.0);
            picSat.SizeMode = PictureBoxSizeMode.Zoom;
            picSat.Width = Convert.ToInt32(zoomBar.Value * originalWidth / 100.0);
            picSat.Height = Convert.ToInt32(zoomBar.Value * originalHeight / 100.0);
            picInt.SizeMode = PictureBoxSizeMode.Zoom;
            picInt.Width = Convert.ToInt32(zoomBar.Value * originalWidth / 100.0);
            picInt.Height = Convert.ToInt32(zoomBar.Value * originalHeight / 100.0);
        }

        public float[][][] GetFloat()     // Returns image as jagged array
        {
            return Normalize.ToFloat(GetBitmap());
        }

        public IOperand CreateSibling(float[][][] sourceImage, String description)
        {
            return CreateSibling(sourceImage, description, null);
        }

        public IOperand CreateSibling(float[][][] sourceImage, String description, Fourier.CImage[] dat = null)    // like a clone with new content
        {
            FormStandard frm;
            Bitmap image = Normalize.FromFloat(sourceImage);

            string imageName = imgLabel.Text;
            frm = new FormStandard(imageName, image, myParent, description, dat);

            frm.Show();

            myParent.AddToImages(frm);
            return frm;
        }

        private void ScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Operations.FormScale frmScale = new Operations.FormScale(originalWidth, originalHeight, this, newWinToolStripMenuItem.Checked))
            {
                Operations.FormScale fs = frmScale;
                fs.Show();
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e) // Crop
        {
            IOperand x = this;
            if (x == null) return;

            using (Operations.FormCrop fc = new Operations.FormCrop(x, newWinToolStripMenuItem.Checked))
            {
                fc.Show();
            }
        }

        private void ToGrayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float[][][] img = OperationsMathSimple.ToGrayScale((Bitmap)picImage.Image);
            if (newWinToolStripMenuItem.Checked)
            {
                CreateSibling(img, "Grayscale of " + imgLabel.Text.ToString());
            }
            else
            {
                picImage.Image = Normalize.FromFloat(img);
            }
        }

        private void InvertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float[][][] img = OperationsMathSimple.Invert((Bitmap)picImage.Image);
            if (newWinToolStripMenuItem.Checked)
            {
                CreateSibling(img, "Inversion of " + imgLabel.Text.ToString());
            }
            else
            {
                picImage.Image = Normalize.FromFloat(img);
            }
        }

        private void ChangeIntensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationsMathSimple.ChangeIntensity(this, newWinToolStripMenuItem.Checked);
        }

        private void HistogramToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Histogram.FormHistogram hist = new Histogram.FormHistogram(this))
            {
                hist.Show();
            }
        }

        private void RotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Operations.FormRotate fr = new Operations.FormRotate(this, newWinToolStripMenuItem.Checked))
            {
                fr.Show();
            }
        }

        private void MeanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.Mean(this, newWinToolStripMenuItem.Checked);
        }

        private void Laplacian4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.Laplacian4(this, newWinToolStripMenuItem.Checked);
        }

        private void Laplacian8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.Laplacian8(this, newWinToolStripMenuItem.Checked);
        }

        private void SobelXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.SobelX(this, newWinToolStripMenuItem.Checked);
        }

        private void SobelYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.SobelY(this, newWinToolStripMenuItem.Checked);
        }

        private void FrequencyDomainFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.FFT(this, newWinToolStripMenuItem.Checked);
        }

        private void InverseFFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.IFFT(this, newWinToolStripMenuItem.Checked);
        }

        private void HighPassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.HighPass(this, newWinToolStripMenuItem.Checked);
        }

        private void LowPassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.LowPass(this, newWinToolStripMenuItem.Checked);
        }

        private void BandPassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.BandPass(this, newWinToolStripMenuItem.Checked);
        }

        private void NotchFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.Notch(this, newWinToolStripMenuItem.Checked);
        }

        private void ButterworthHighPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.ButterworthHigh(this, newWinToolStripMenuItem.Checked);
        }

        private void ButterworthLowPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.ButterworthLow(this, newWinToolStripMenuItem.Checked);
        }

        private void MedianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.Median(this, newWinToolStripMenuItem.Checked);
        }

        private void MinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.Min(this, newWinToolStripMenuItem.Checked);
        }

        private void MaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.Max(this, newWinToolStripMenuItem.Checked);
        }

        private void MidpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.Midpoint(this, newWinToolStripMenuItem.Checked);
        }

        private void AlphaTrim2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.AlphaTrim(this, newWinToolStripMenuItem.Checked);
        }

        private void HSIViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorRoutines.FormHSI hsi = new ColorRoutines.FormHSI(Image, myParent))
            {
                hsi.Show();
            }
        }

        private void EnhanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationHSI.Enhance(this, newWinToolStripMenuItem.Checked);
        }

        private void ButterworthHighPass4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.ButterworthHigh4(this, newWinToolStripMenuItem.Checked);
        }

        private void ButterworthLowPass4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.ButterworthLow4(this, newWinToolStripMenuItem.Checked);
        }

        private void UpdateImages()
        {
            float[][][] img = Normalize.ToFloat(Image);
            float[][][] rimg = Normalize.ToFloat(Image);
            float[][][] gimg = Normalize.ToFloat(Image);
            float[][][] bimg = Normalize.ToFloat(Image);
            float[][][] hsimg = Normalize.ToFloat(Image);
            float[][][] himg = Normalize.ToFloat(Image);
            float[][][] simg = Normalize.ToFloat(Image);
            float[][][] iimg = Normalize.ToFloat(Image);
            float[][][] blank = Normalize.ToFloat(Image);

            for (int h = 0; h < originalHeight; h++)
            {
                for (int w = 0; w < originalWidth; w++)
                {
                    ColorRoutines.ImageLibrary.Pixel_RGBtoHSI(img[0][h][w], img[1][h][w], img[2][h][w], out hsimg[0][h][w], out hsimg[1][h][w], out hsimg[2][h][w]);
                }
            }

            for (int c = 0; c < 3; c++)
            {
                for (int h = 0; h < originalHeight; h++)
                {
                    for (int w = 0; w < originalWidth; w++)
                    {
                        bimg[c][h][w] = (c == 0) ? img[0][h][w] : 0;
                        gimg[c][h][w] = (c == 1) ? img[1][h][w] : 0;
                        rimg[c][h][w] = (c == 2) ? img[2][h][w] : 0;
                        himg[c][h][w] = hsimg[0][h][w];
                        simg[c][h][w] = hsimg[1][h][w];
                        iimg[c][h][w] = hsimg[2][h][w];
                        blank[c][h][w] = 0f;
                    }
                }
            }

            picRed.Image = Normalize.FromFloat(rimg);
            picGreen.Image = Normalize.FromFloat(gimg);
            picBlue.Image = Normalize.FromFloat(bimg);
            picHue.Image = Normalize.FromFloat(himg);
            picSat.Image = Normalize.FromFloat(simg);
            picInt.Image = Normalize.FromFloat(iimg);
        }
    }
}
