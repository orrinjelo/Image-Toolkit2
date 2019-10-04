using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ImageToolkit.Histogram
{
    public partial class FormHistogram : Form
    {
        readonly FormStandard myParent;
        float[][][] img;
        float[][] histogram;
        float[][] cpfhistogram;

        float[] cpf;
        Bitmap bmp;
        enum Level : int { white = 0, red = 1, green = 2, blue = 3 };
        float boxW;

        public FormHistogram(FormStandard parent)
        {
            myParent = parent;
            bmp = myParent.Image;
            img = Normalize.ToFloat(bmp);
            InitializeComponent();
            BuildHistogram();
            cpf = new float[256];
            for (int i = 0; i < 256; i++)
                cpf[i] = i / 255.0f;
        }

        private void BuildHistogram()
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
            {
                for (int y = 0; y < h; y++)
                {
                    int r = (int)(img[Normalize.RGBPLANE_RED][y][x] * 255.0),
                        g = (int)(img[Normalize.RGBPLANE_GREEN][y][x] * 255.0),
                        b = (int)(img[Normalize.RGBPLANE_BLUE][y][x] * 255.0);
                    if (r >= 256) r = 255;
                    if (g >= 256) g = 255;
                    if (b >= 256) b = 255;
                    int k = (r + g + b) / 3;

                    histogram[(int)Level.red][r] += 1.0f;
                    histogram[(int)Level.green][g] += 1.0f;
                    histogram[(int)Level.blue][b] += 1.0f;
                    histogram[(int)Level.white][k] += 1.0f;

                    cpfhistogram[(int)Level.red][r] += 1.0f;
                    cpfhistogram[(int)Level.green][g] += 1.0f;
                    cpfhistogram[(int)Level.blue][b] += 1.0f;
                    cpfhistogram[(int)Level.white][k] += 1.0f;


                }
            }

            for (int x = 1; x < 256; x++)
            {
                cpfhistogram[(int)Level.red][x] += cpfhistogram[(int)Level.red][x - 1];
                cpfhistogram[(int)Level.green][x] += cpfhistogram[(int)Level.green][x - 1];
                cpfhistogram[(int)Level.blue][x] += cpfhistogram[(int)Level.blue][x - 1];
                cpfhistogram[(int)Level.white][x] += cpfhistogram[(int)Level.white][x - 1];
            }

            for (int c = 0; c < 256; c++)
            {
                for (int z = 0; z < 4; z++)
                {
                    histogram[z][c] /= w * h;
                    cpfhistogram[z][c] /= w * h;
                }
            }

            int W = pnlHist.Width;

            boxW = W / 256.0f;
        }

        public bool ThumbnailCallback()
        {
            MessageBox.Show("Callback...");
            return true;
        }

        public float[][][] HistogramEqualization
        {
            get
            {
                float[][][] img2 = img;
                for (int y = 0; y < bmp.Height; y++)
                    for (int x = 0; x < bmp.Width; x++)
                    {

                        img2[Normalize.RGBPLANE_RED][y][x] = cpfhistogram[(int)Level.red][(int)(img[Normalize.RGBPLANE_RED][y][x] * 255f)];
                        img2[Normalize.RGBPLANE_GREEN][y][x] = cpfhistogram[(int)Level.green][(int)(img[Normalize.RGBPLANE_GREEN][y][x] * 255f)];
                        img2[Normalize.RGBPLANE_BLUE][y][x] = cpfhistogram[(int)Level.blue][(int)(img[Normalize.RGBPLANE_BLUE][y][x] * 255f)];

                    }
                return img2;
            }
        }

        private void FrmHistogram_Load(object sender, EventArgs e)
        {
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            picThumbnail.Image = myParent.Image.GetThumbnailImage(picThumbnail.Width, picThumbnail.Height, callback, new IntPtr());
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g;
            g = e.Graphics;

            float max;
            if (rbIntensity.Checked)
            {
                max = histogram[0].Max();
            }
            else if (rbRed.Checked)
            {
                max = histogram[1].Max();
            }
            else if (rbGreen.Checked)
            {
                max = histogram[2].Max();
            }
            else max = histogram[3].Max();

            if (cbCPF.Checked)
            {
                max = cpfhistogram[0].Max();
            }

            float scale = pnlHist.Height / max;

            if (rbIntensity.Checked)
            {
                using (Pen myPen = new Pen(Color.White, 1))
                {
                    for (int x = 0; x < 256; x++)
                    {
                        g.DrawRectangle(myPen, new Rectangle((int)(x * boxW),
                            pnlHist.Height - (cbCPF.Checked ? (int)(cpfhistogram[0][x] * scale) : (int)(histogram[0][x] * scale)),
                            (int)boxW,
                            (int)((cbCPF.Checked ? cpfhistogram[0][x] : histogram[0][x]) * scale)));
                    }
                }
            }
            else if (rbRed.Checked)
            {
                using (Pen myPen = new Pen(Color.Red, 1))
                {
                    for (int x = 0; x < 256; x++)
                    {
                        g.DrawRectangle(myPen, new Rectangle((int)(x * boxW),
                            pnlHist.Height - (cbCPF.Checked ? (int)(cpfhistogram[1][x] * scale) : (int)(histogram[1][x] * scale)),
                            (int)boxW,
                            (int)((cbCPF.Checked ? cpfhistogram[1][x] : histogram[1][x]) * scale)));
                    }
                }
            }
            else if (rbGreen.Checked)
            {
                using (Pen myPen = new Pen(Color.Green, 1))
                {
                    for (int x = 0; x < 256; x++)
                    {
                        g.DrawRectangle(myPen, new Rectangle((int)(x * boxW),
                            pnlHist.Height - (int)((cbCPF.Checked ? cpfhistogram[2][x] : histogram[2][x]) * scale),
                            (int)boxW,
                            (int)((cbCPF.Checked ? cpfhistogram[2][x] : histogram[2][x]) * scale)));
                    }
                }
            }
            else if (rbBlue.Checked)
            {
                using (Pen myPen = new Pen(Color.Blue, 1))
                {
                    for (int x = 0; x < 256; x++)
                    {
                        g.DrawRectangle(myPen, new Rectangle((int)(x * boxW),
                            pnlHist.Height - (int)((cbCPF.Checked ? cpfhistogram[3][x] : histogram[3][x]) * scale),
                            (int)boxW,
                            (int)((cbCPF.Checked ? cpfhistogram[3][x] : histogram[3][x]) * scale)));
                    }
                }
            }
            using (Pen pen2 = new Pen(Color.HotPink, 1))
            {
                for (int x = 1; x < 256; x++)
                {
                    g.DrawLine(pen2,
                               (int)((x - 1) * boxW),
                               (int)((1.0 - cpf[x - 1]) * (pnlHist.Height)),
                               (int)(x * boxW),
                               (int)((1.0 - cpf[x]) * pnlHist.Height));
                }
            }
        }

        private void ModeChange(object sender, EventArgs e)
        {
            pnlHist.Refresh();
        }

        private void BtnAuto_Click(object sender, EventArgs e)
        {
            img = HistogramEqualization;
            BuildHistogram();
            pnlHist.Refresh();
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            picThumbnail.Image = Normalize.FromFloat(img).GetThumbnailImage(picThumbnail.Width, picThumbnail.Height, callback, new IntPtr());
            picThumbnail.Refresh();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            myParent.Image = Normalize.FromFloat(img);
            Close();
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            bmp = myParent.Image;
            img = Normalize.ToFloat(bmp);
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            picThumbnail.Image = bmp.GetThumbnailImage(picThumbnail.Width, picThumbnail.Height, callback, new IntPtr());
            picThumbnail.Refresh();
            BuildHistogram();
            cpf = new float[256];
            for (int i = 0; i < 256; i++)
                cpf[i] = i / 255.0f;
            tbAdjust.Value = 0;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private float Trunc(float f)
        {
            if (f > 1.0f) return 1.0f;
            if (f < 0.0f) return 0.0f;
            return f;
        }

        private void ReevaluateContrast(object sender, MouseEventArgs e)
        {
            int contrast = tbAdjust.Value;
            float factor = (259.0f * (contrast + 255.0f)) / (255.0f * (259.0f - contrast));
            float[][][] img2 = Normalize.ToFloat(myParent.Image);
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {

                    img2[Normalize.RGBPLANE_RED][y][x] = Trunc((factor * img2[Normalize.RGBPLANE_RED][y][x] - 0.5f) + 0.5f);
                    img2[Normalize.RGBPLANE_GREEN][y][x] = Trunc((factor * img2[Normalize.RGBPLANE_GREEN][y][x] - 0.5f) + 0.5f);
                    img2[Normalize.RGBPLANE_BLUE][y][x] = Trunc((factor * img2[Normalize.RGBPLANE_BLUE][y][x] - 0.5f) + 0.5f);

                }
            img = img2;
            BuildHistogram();
            pnlHist.Refresh();
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            picThumbnail.Image = Normalize.FromFloat(img).GetThumbnailImage(picThumbnail.Width, picThumbnail.Height, callback, new IntPtr());
            picThumbnail.Refresh();
        }
    }
}