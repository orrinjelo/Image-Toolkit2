using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageToolkit.Histogram
{
    public partial class frmHistogram : Form
    {
        frmStandard myParent;
        float[][][] img;
        float[][] histogram;
        float[][] cpfhistogram;

        float[] cpf;
        Bitmap bmp;
        enum level : int { white=0, red=1, green=2, blue=3 };
        float boxW;

        public frmHistogram(frmStandard parent)
        {
            myParent = parent;
            bmp = myParent.Image;
            img = Normalize.ToFloat(bmp);
            InitializeComponent();
            buildHistogram();
            cpf = new float[256];
            for (int i = 0; i < 256; i++)
                cpf[i] = (float)i / 255.0f;
        }

        private void buildHistogram()
        {
            histogram = new float[4][];
            cpfhistogram = new float[4][];

            for (int i = 0; i < 4; i++)
            {
                histogram[i] = new float[256];
                cpfhistogram[i] = new float[256];
                for (int j = 0; j < 256; j++)
                {
                    histogram[i][j] = 0;
                    cpfhistogram[i][j] = 0;
                }
            }

            int w = myParent.Image.Width,
                h = myParent.Image.Height;

            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                {
                    int r = (int)(img[Normalize.RGBPLANE_RED][y][x] * 255.0),
                        g = (int)(img[Normalize.RGBPLANE_GREEN][y][x] * 255.0),
                        b = (int)(img[Normalize.RGBPLANE_BLUE][y][x] * 255.0);
                    if (r >= 256) r = 255;
                    if (g >= 256) g = 255;
                    if (b >= 256) b = 255;
                    int k = (r + g + b) / 3;

                    histogram[(int)level.red][r] += 1.0f;
                    histogram[(int)level.green][g] += 1.0f;
                    histogram[(int)level.blue][b] += 1.0f;
                    histogram[(int)level.white][k] += 1.0f;

                    cpfhistogram[(int)level.red][r] += 1.0f;
                    cpfhistogram[(int)level.green][g] += 1.0f;
                    cpfhistogram[(int)level.blue][b] += 1.0f;
                    cpfhistogram[(int)level.white][k] += 1.0f;


                }
            for (int x = 1; x < 256; x++)
            {
                cpfhistogram[(int)level.red][x] += cpfhistogram[(int)level.red][x - 1];
                cpfhistogram[(int)level.green][x] += cpfhistogram[(int)level.green][x - 1];
                cpfhistogram[(int)level.blue][x] += cpfhistogram[(int)level.blue][x - 1];
                cpfhistogram[(int)level.white][x] += cpfhistogram[(int)level.white][x - 1];
            }

            for (int c = 0; c < 256; c++)
                for (int z = 0; z < 4; z++)
                {
                    histogram[z][c] /= (float)(w * h);
                    cpfhistogram[z][c] /= (float)(w * h);
                }

            int W = this.pnlHist.Width;
            int H = this.pnlHist.Height;

            boxW = (float)W / 256.0f;
        }

        public bool ThumbnailCallback()
        {
            MessageBox.Show("Callback...");
            return true;
        }

        private float cdf(int k, int color=(int)level.white)
        {
            float s = 0.0f;
            for (int i=0; i<=k; i++)
            {
                s += histogram[color][i];
            }
            return s;
        }

        private float sig(float x, float s=1.0f, float m=0.0f)
        {
            return (float)(1.0 / (1.0 + Math.Exp(-x*s - m)));
        }

        public float[][][] histogramEqualization()
        {
            float[][][] img2 = img;
            for (int y=0; y<bmp.Height; y++)
                for (int x=0; x<bmp.Width; x++)
                {
                    
                    img2[Normalize.RGBPLANE_RED][y][x] = cpfhistogram[(int)level.red][(int)(img[Normalize.RGBPLANE_RED][y][x] * 255f)];
                    img2[Normalize.RGBPLANE_GREEN][y][x] = cpfhistogram[(int)level.green][(int)(img[Normalize.RGBPLANE_GREEN][y][x] * 255f)];
                    img2[Normalize.RGBPLANE_BLUE][y][x] = cpfhistogram[(int)level.blue][(int)(img[Normalize.RGBPLANE_BLUE][y][x] * 255f)];
                    
                }
            return img2;
        }

        private void frmHistogram_Load(object sender, EventArgs e)
        {
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            this.picThumbnail.Image = myParent.Image.GetThumbnailImage(this.picThumbnail.Width, this.picThumbnail.Height, callback, new IntPtr());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g;
            g = e.Graphics;

            float max;
            if (this.rbIntensity.Checked)
            {
                max = histogram[0].Max();
            }
            else if (this.rbRed.Checked)
            {
                max = histogram[1].Max();
            }
            else if (this.rbGreen.Checked)
            {
                max = histogram[2].Max();
            }
            else max = histogram[3].Max();

            if (this.cbCPF.Checked)
                max = cpfhistogram[0].Max();

            float scale = this.pnlHist.Height / max; 

            if (this.rbIntensity.Checked)
            {
                Pen myPen = new Pen(Color.White, 1);
                for (int x = 0; x < 256; x++)
                    g.DrawRectangle(myPen, new Rectangle((int)(x * boxW),
                        this.pnlHist.Height - (this.cbCPF.Checked ? (int)(cpfhistogram[0][x] * scale) : (int)(histogram[0][x] * scale)), 
                        (int)boxW,
                        (int)((this.cbCPF.Checked ? cpfhistogram[0][x] : histogram[0][x]) * scale)));
            }
            else if (this.rbRed.Checked)
            {
                Pen myPen = new Pen(Color.Red, 1);
                for (int x = 0; x < 256; x++)
                    g.DrawRectangle(myPen, new Rectangle((int)(x * boxW), 
                        this.pnlHist.Height - (this.cbCPF.Checked ? (int)(cpfhistogram[1][x] * scale) : (int)(histogram[1][x] * scale)), 
                        (int)boxW, 
                        (int)((this.cbCPF.Checked ? cpfhistogram[1][x] : histogram[1][x]) * scale)));
            }
            else if (this.rbGreen.Checked)
            {
                Pen myPen = new Pen(Color.Green, 1);
                for (int x = 0; x < 256; x++)
                    g.DrawRectangle(myPen, new Rectangle((int)(x * boxW), 
                        this.pnlHist.Height - (int)((this.cbCPF.Checked ? cpfhistogram[2][x] : histogram[2][x]) * scale), 
                        (int)boxW, 
                        (int)((this.cbCPF.Checked ? cpfhistogram[2][x] : histogram[2][x]) * scale)));
            }
            else if (this.rbBlue.Checked)
            {
                Pen myPen = new Pen(Color.Blue, 1);
                for (int x = 0; x < 256; x++)
                    g.DrawRectangle(myPen, new Rectangle((int)(x * boxW), 
                        this.pnlHist.Height - (int)((this.cbCPF.Checked ? cpfhistogram[3][x] : histogram[3][x]) * scale), 
                        (int)boxW, 
                        (int)((this.cbCPF.Checked ? cpfhistogram[3][x] : histogram[3][x]) * scale)));
            }
            Pen pen2 = new Pen(Color.HotPink, 1);

            for (int x = 1; x < 256; x++)
                g.DrawLine(pen2, 
                           (int)((x - 1) * boxW),
                           (int)((1.0 - cpf[x - 1]) * (this.pnlHist.Height)),
                           (int)(x * boxW),
                           (int)((1.0 - cpf[x]) * this.pnlHist.Height));
            

        }

        private void modeChange(object sender, EventArgs e)
        {
            pnlHist.Refresh();
            
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            img = histogramEqualization();
            buildHistogram();
            pnlHist.Refresh();
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            this.picThumbnail.Image = Normalize.FromFloat(img).GetThumbnailImage(this.picThumbnail.Width, this.picThumbnail.Height, callback, new IntPtr());
            picThumbnail.Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            myParent.Image = Normalize.FromFloat(img);
            this.Close();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            bmp = myParent.Image;
            img = Normalize.ToFloat(bmp);
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            this.picThumbnail.Image = bmp.GetThumbnailImage(this.picThumbnail.Width, this.picThumbnail.Height, callback, new IntPtr());
            picThumbnail.Refresh();
            buildHistogram();
            cpf = new float[256];
            for (int i = 0; i < 256; i++)
                cpf[i] = (float)i / 255.0f;
            tbAdjust.Value = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private float trunc(float f)
        {
            if (f > 1.0f) return 1.0f;
            if (f < 0.0f) return 0.0f;
            return f;
        }

        private void reevaluateContrast(object sender, MouseEventArgs e)
        {
            int contrast = tbAdjust.Value;
            float factor = (259.0f * (contrast + 255.0f)) / (255.0f * (259.0f - contrast));
            float[][][] img2 = Normalize.ToFloat(myParent.Image);
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {

                    img2[Normalize.RGBPLANE_RED][y][x] = trunc((factor*img2[Normalize.RGBPLANE_RED][y][x] - 0.5f) + 0.5f);
                    img2[Normalize.RGBPLANE_GREEN][y][x] = trunc((factor*img2[Normalize.RGBPLANE_GREEN][y][x] - 0.5f) + 0.5f);
                    img2[Normalize.RGBPLANE_BLUE][y][x] = trunc((factor*img2[Normalize.RGBPLANE_BLUE][y][x] - 0.5f) + 0.5f);

                }
            img = img2;
            buildHistogram();
            pnlHist.Refresh();
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            this.picThumbnail.Image = Normalize.FromFloat(img).GetThumbnailImage(this.picThumbnail.Width, this.picThumbnail.Height, callback, new IntPtr());
            picThumbnail.Refresh();
        }
    }
}
