using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace ImageToolkit
{
    public partial class frmStandard : Form, IOperand
    {
        const string FORM_TITLE = "Standard Image Form";
        static int nameCounter = 0;
        public frmMain myParent;
        int formCounter;
        int originalWidth;
        int originalHeight;

        public Bitmap Image
        {
            get
            {
                return (Bitmap)picImage.Image;
            }
            set
            {
                picImage.Image = value;
                lblHeight.Text = value.Height.ToString();
                lblWidth.Text = value.Width.ToString();
                lblPixels.Text = (value.Height * value.Width).ToString("#,##0");
                originalWidth = picImage.Width;
                originalHeight = picImage.Height;

                updateImages();
            }
        }

        public Fourier.CImage[] cimg { get; set; }


        public frmStandard()
        {
            InitializeComponent();
            formCounter = ++nameCounter;
            this.Name = "frmStandard" + formCounter.ToString("000");
            this.Text = FORM_TITLE + " : " + formCounter.ToString();
        }

        public frmStandard(frmMain parent) : this()
        {
            myParent = parent;
        }

        public frmStandard(Bitmap image, frmMain parent, Fourier.CImage[] dat = null) : this(parent)
        {
            Image = image;
            float[][][] img = Normalize.ToFloat(image);
            if (dat == null)
            {
                cimg = new Fourier.CImage[3];
                for (int i = 0; i < 3; i++) cimg[i] = new Fourier.CImage(img[i], image.Height, image.Width);
            }
            else cimg = dat;
            
        }

        public frmStandard(string imageName, Bitmap image, frmMain parent, string description="", Fourier.CImage[] dat=null) : this(image, parent, dat)
        {
            this.Text = description;
            imgLabel.Text = imageName;
        }

        public override string ToString()
        {
            return formCounter.ToString() + ": " + imgLabel.Text;
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formStandard_FormClosing(object sender, FormClosingEventArgs e)
        {
            myParent.remove(this);
            picImage.Image.Dispose();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        public void saveImage(String filename = "")
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
                    String extension = Path.GetExtension(saveFileDialog.FileName);
                    Image.Save(filename, getFileType(extension));

                    // The following was the old way of doing stuff.
                    string oldName = this.ToString();
                    imgLabel.Text = Path.GetFileName(filename);
                    myParent.updatelstImages();
                     
                    /*myParent.createFrmStandard(filename);*/  // Nah, went back to the other way.
                    MessageBox.Show("Saved image as " + filename); 
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Display(ex, "Error saving image.");
            }
        }

        private System.Drawing.Imaging.ImageFormat getFileType(String fileType)
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


        public void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImage(imgLabel.Text);
        }

        public string Description { get; set; }

        public int ImageWidth 
        { 
            get
            {
                return Image.Width;
            }
        }

        public int ImageHeight 
        { 
            get
            {
                return Image.Height;
            }
        }

        public Bitmap GetBitmap()
        {
            return Image;
        }

        private void hideifMinimized(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                this.Hide();
        }

        private void zoomPic(object sender, EventArgs e)
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
            frmStandard frm;
            Bitmap image = Normalize.FromFloat(sourceImage) ;
            
            string imageName = imgLabel.Text;
            frm = new frmStandard(imageName, image, this.myParent, description, dat);
            
            frm.Show();

            int index = myParent.AddToImages(frm);
            return frm;
        }

        private void scaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.frmScale fs = new Operations.frmScale(originalWidth, originalHeight,(IOperand)this,newWinToolStripMenuItem.Checked);
            fs.Show();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) // Crop
        {
            IOperand x = this;
            if (x == null) return;

            Operations.frmCrop fc = new Operations.frmCrop(x,newWinToolStripMenuItem.Checked);

            fc.Show();

        }

        private void toGrayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float[][][] img = OperationsMathSimple.ToGrayScale((Bitmap)this.picImage.Image);
            if (this.newWinToolStripMenuItem.Checked)
                this.CreateSibling(img, "Grayscale of " + this.imgLabel.Text.ToString());
            else
                this.picImage.Image = Normalize.FromFloat(img);
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float[][][] img = OperationsMathSimple.Invert((Bitmap)this.picImage.Image);
            if (this.newWinToolStripMenuItem.Checked)
                this.CreateSibling(img, "Inversion of " + this.imgLabel.Text.ToString());
            else
                this.picImage.Image = Normalize.FromFloat(img);
        }

        private void changeIntensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationsMathSimple.ChangeIntensity((IOperand)this, this.newWinToolStripMenuItem.Checked);
        }

        private void histogramToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Histogram.frmHistogram hist = new Histogram.frmHistogram(this);
            hist.Show();
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.frmRotate fr = new Operations.frmRotate((IOperand)this, newWinToolStripMenuItem.Checked);
            fr.Show();
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.Mean((IOperand)this, newWinToolStripMenuItem.Checked);
        }

        private void laplacian4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.Laplacian4((IOperand)this, newWinToolStripMenuItem.Checked);
        }

        private void laplacian8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.Laplacian8((IOperand)this, newWinToolStripMenuItem.Checked);
        }

        private void sobelXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.SobelX((IOperand)this, newWinToolStripMenuItem.Checked);
        }

        private void sobelYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsSpaceDomainFilters.SobelY((IOperand)this, newWinToolStripMenuItem.Checked);
        }

        private void frequencyDomainFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.DFT((IOperand)this, newWinToolStripMenuItem.Checked);
        }

        private void inverseDFTSLOWToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Operations.OperationsFFT.DFT((IOperand)this, newWinToolStripMenuItem.Checked,-1);
        }

        private void fFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.FFT((IOperand)this, newWinToolStripMenuItem.Checked);
        }

        private void inverseFFTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.IFFT((IOperand)this, newWinToolStripMenuItem.Checked);
        }

        private void fFT2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Operations.OperationsFFT.FFT2((IOperand)this, newWinToolStripMenuItem.Checked,1);
        }

        private void inverseFFT2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Operations.OperationsFFT.FFT2((IOperand)this, newWinToolStripMenuItem.Checked, -1);
        }

        private void viewImaginaryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void highPassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.HighPass(this, this.newWinToolStripMenuItem.Checked);
        }

        private void lowPassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.LowPass(this, this.newWinToolStripMenuItem.Checked);
        }

        private void bandPassFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.BandPass(this, this.newWinToolStripMenuItem.Checked);
        }

        private void notchFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.Notch(this, this.newWinToolStripMenuItem.Checked);
        }

        private void butterworthHighPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.ButterworthHigh(this, this.newWinToolStripMenuItem.Checked);
        }

        private void butterworthLowPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.ButterworthLow(this, this.newWinToolStripMenuItem.Checked);
        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.Median(this, this.newWinToolStripMenuItem.Checked);
        }

        private void minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.Min(this, this.newWinToolStripMenuItem.Checked);
        }

        private void maxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.Max(this, this.newWinToolStripMenuItem.Checked);
        }

        private void midpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.Midpoint(this, this.newWinToolStripMenuItem.Checked);
        }

        private void alphaTrim2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsStatistical.AlphaTrim(this, this.newWinToolStripMenuItem.Checked);
        }

        private void hSIViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorRoutines.frmHSI hsi = new ColorRoutines.frmHSI((Bitmap)this.Image, myParent);
            hsi.Show();
        }

        private void enhanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationHSI.Enhance((IOperand)this, this.newWinToolStripMenuItem.Checked);
        }

        private void butterworthHighPass4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.ButterworthHigh4((IOperand)this, this.newWinToolStripMenuItem.Checked);
        }

        private void butterworthLowPass4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operations.OperationsFFT.ButterworthLow4((IOperand)this, this.newWinToolStripMenuItem.Checked);
        }

        private void updateImages()
        {
            float[][][] img = Normalize.ToFloat((Bitmap)this.Image);
            float[][][] rimg = Normalize.ToFloat((Bitmap)this.Image);
            float[][][] gimg = Normalize.ToFloat((Bitmap)this.Image);
            float[][][] bimg = Normalize.ToFloat((Bitmap)this.Image);
            float[][][] hsimg = Normalize.ToFloat((Bitmap)this.Image);
            float[][][] himg = Normalize.ToFloat((Bitmap)this.Image);
            float[][][] simg = Normalize.ToFloat((Bitmap)this.Image);
            float[][][] iimg = Normalize.ToFloat((Bitmap)this.Image);
            float[][][] blank = Normalize.ToFloat((Bitmap)this.Image);

            for (int h = 0; h < originalHeight; h++)
                for (int w = 0; w < originalWidth; w++)
                {
                    ColorRoutines.ImageLibrary.Pixel_RGBtoHSI(img[0][h][w], img[1][h][w], img[2][h][w], out hsimg[0][h][w], out hsimg[1][h][w], out hsimg[2][h][w]);
                }

            for (int c=0; c<3; c++)
                for (int h=0; h<originalHeight; h++)
                    for (int w=0; w<originalWidth; w++)
                    {
                        bimg[c][h][w] = (c==0) ? img[0][h][w] : 0;
                        gimg[c][h][w] = (c==1) ? img[1][h][w] : 0;
                        rimg[c][h][w] = (c==2) ? img[2][h][w] : 0;
                        himg[c][h][w] = hsimg[0][h][w];
                        simg[c][h][w] = hsimg[1][h][w];
                        iimg[c][h][w] = hsimg[2][h][w];
                        blank[c][h][w] = 0f;
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
