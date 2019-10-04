using Exocortex.DSP;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImageToolkit.Operations
{
    public partial class FormFFTFilter : Form
    {
        public enum Mode { High, Low, Narrow, Notch, ButterHigh, ButterLow, ButterHigh4, ButterLow4 };
        public Fourier.CImage[] cimg;
        public float[][][] img;
        public FormStandard myParent;
        private readonly Mode myMode;
        readonly bool spawn;
        public FormFFTFilter(Mode mode, Fourier.CImage[] c, FormStandard f, bool spawn = true)
        {
            InitializeComponent();
            myMode = mode;
            this.spawn = spawn;
            cimg = c;
            myParent = f;
            if (!cimg[0].FrequencySpace)
                for (int i = 0; i < 3; i++) cimg[i] = Operations.OperationsFFT._FFT(cimg[i]);
            img = new float[3][][];
            for (int i = 0; i < 3; i++)
            {
                img[i] = cimg[i].FromFloatModulus();
            }
            picFFT.Image = Normalize.FromFloat(img);
            switch (myMode)
            {
                case Mode.ButterHigh4:
                case Mode.ButterHigh:
                case Mode.High: InitHigh(); break;
                case Mode.ButterLow4:
                case Mode.ButterLow:
                case Mode.Low: InitLow(); break;
                case Mode.Narrow: InitNarrow(); break;
                case Mode.Notch: InitNotch(); break;
            }
        }

        public void InitHigh()
        {
            udHigh.Enabled = false;
            udLow.Enabled = true;
            udLow.Maximum = (int)(1.0 / Math.Sqrt(2) * cimg[0].Width);
            udLow.Value = 50;
            udLow.Minimum = 0;
        }

        public void InitLow()
        {
            udHigh.Enabled = true;
            udLow.Enabled = false;
            udHigh.Maximum = (int)(1.0 / Math.Sqrt(2) * cimg[0].Width);
            udHigh.Value = 50;
            udHigh.Minimum = 0;
        }

        public void InitNarrow()
        {
            udLow.Enabled = true;
            udHigh.Enabled = true;
            udLow.Minimum = 0;
            udHigh.Minimum = 0;
            udLow.Maximum = (int)(1.0 / Math.Sqrt(2) * cimg[0].Width);
            udHigh.Maximum = (int)(1.0 / Math.Sqrt(2) * cimg[0].Width);
            udLow.Value = 25;
            udHigh.Value = 50;
        }

        public void InitNotch()
        {
            udLow.Enabled = true;
            udHigh.Enabled = true;
            udLow.Minimum = 0;
            udHigh.Minimum = 0;
            udLow.Maximum = (int)(1.0 / Math.Sqrt(2) * cimg[0].Width);
            udHigh.Maximum = (int)(1.0 / Math.Sqrt(2) * cimg[0].Width);
            udLow.Value = 25;
            udHigh.Value = 50;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            IOperand x = null;
            Hide();

            if (myParent != null)
            {
                x = myParent;
            }

            switch (myMode)
            {
                case Mode.High: Operations.OperationsFFT.HighPass(x, spawn, (float)udLow.Value); break;
                case Mode.Low: Operations.OperationsFFT.LowPass(x, spawn, (float)udHigh.Value); break;
                case Mode.Narrow: Operations.OperationsFFT.BandPass(x, spawn, (float)udLow.Value, (float)udHigh.Value); break;
                case Mode.Notch: Operations.OperationsFFT.Notch(x, spawn, (float)udLow.Value, (float)udHigh.Value); break;
                case Mode.ButterHigh: Operations.OperationsFFT.ButterworthHigh(x, spawn, (float)udLow.Value); break;
                case Mode.ButterLow: Operations.OperationsFFT.ButterworthLow(x, spawn, (float)udHigh.Value); break;
                case Mode.ButterHigh4: Operations.OperationsFFT.ButterworthHigh4(x, spawn, (float)udLow.Value); break;
                case Mode.ButterLow4: Operations.OperationsFFT.ButterworthLow4(x, spawn, (float)udHigh.Value); break;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            switch (myMode)
            {
                case Mode.ButterHigh4:
                case Mode.ButterHigh:
                case Mode.High: InitHigh(); break;
                case Mode.ButterLow4:
                case Mode.ButterLow:
                case Mode.Low: InitLow(); break;
                case Mode.Narrow: InitNarrow(); break;
                case Mode.Notch: InitNotch(); break;
            }
        }

        private void DrawCircles(object sender, PaintEventArgs e)
        {
            using (GraphicsPath path1 = new GraphicsPath())
            {
                using (GraphicsPath path2 = new GraphicsPath())
                {
                    using (GraphicsPath box = new GraphicsPath())
                    {
                        Graphics g = e.Graphics;

                        using (Pen p = new Pen(Color.HotPink, 1))
                        {
                            using (Brush b = new SolidBrush(Color.FromArgb(150, Color.HotPink)))
                            {
                                decimal scale = picFFT.Width / (decimal)cimg[0].Width;

                                path1.AddEllipse(picFFT.Width / 2 - (int)(udLow.Value * scale), picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
                                path2.AddEllipse(picFFT.Width / 2 - (int)(udHigh.Value * scale), picFFT.Height / 2 - (int)(udHigh.Value * scale), (int)(udHigh.Value * 2 * scale), (int)(udHigh.Value * 2 * scale));
                                box.AddRectangle(new Rectangle(0, 0, picFFT.Width, picFFT.Height));

                                if (myMode == Mode.High || myMode == Mode.ButterHigh || myMode == Mode.ButterHigh4)
                                {
                                    g.DrawEllipse(p, picFFT.Width / 2 - (int)(udLow.Value * scale), picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
                                    g.FillEllipse(b, picFFT.Width / 2 - (int)(udLow.Value * scale), picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
                                }
                                if (myMode == Mode.Low || myMode == Mode.ButterLow || myMode == Mode.ButterLow4)
                                {
                                    System.Drawing.Region r = new Region(box);
                                    r.Exclude(path2);
                                    g.DrawEllipse(p, picFFT.Width / 2 - (int)(udHigh.Value * scale), picFFT.Height / 2 - (int)(udHigh.Value * scale), (int)(udHigh.Value * 2 * scale), (int)(udHigh.Value * 2 * scale));
                                    g.FillRegion(b, r);
                                }
                                if (myMode == Mode.Narrow)
                                {
                                    System.Drawing.Region r = new Region(box);
                                    r.Exclude(path2);
                                    g.DrawEllipse(p, picFFT.Width / 2 - (int)(udHigh.Value * scale), picFFT.Height / 2 - (int)(udHigh.Value * scale), (int)(udHigh.Value * 2 * scale), (int)(udHigh.Value * 2 * scale));
                                    g.FillRegion(b, r);
                                    g.DrawEllipse(p, picFFT.Width / 2 - (int)(udLow.Value * scale), picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
                                    g.FillEllipse(b, picFFT.Width / 2 - (int)(udLow.Value * scale), picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
                                }
                                if (myMode == Mode.Notch)
                                {
                                    System.Drawing.Region r = new Region(path2);
                                    r.Exclude(path1);
                                    g.DrawEllipse(p, picFFT.Width / 2 - (int)(udHigh.Value * scale), picFFT.Height / 2 - (int)(udHigh.Value * scale), (int)(udHigh.Value * 2 * scale), (int)(udHigh.Value * 2 * scale));
                                    g.FillRegion(b, r);
                                    g.DrawEllipse(p, picFFT.Width / 2 - (int)(udLow.Value * scale), picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateCanvas(object sender, EventArgs e)
        {
            picFFT.Update();
            picFFT.Refresh();
            if (udHigh.Enabled) udLow.Maximum = udHigh.Value;
            if (udLow.Enabled) udHigh.Minimum = udLow.Value;
        }
    }
}