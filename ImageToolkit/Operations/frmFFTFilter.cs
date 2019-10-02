using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exocortex.DSP;

namespace ImageToolkit.Operations
{
    public partial class frmFFTFilter : Form
    {
        public enum Mode { High, Low, Narrow, Notch, ButterHigh, ButterLow, ButterHigh4, ButterLow4 };
        public Fourier.CImage[] cimg;
        public float[][][] img;
        public frmStandard myParent;
        private Mode myMode;
        bool spawn;
        public frmFFTFilter(Mode mode, Fourier.CImage[] c, frmStandard f, bool spawn=true)
        {
            InitializeComponent();
            myMode = mode;
            this.spawn = spawn;
            //cimg = new Fourier.CImage(c.Real(), c.Imag());
            cimg = c;
            myParent = f;
            if (!cimg[0].FrequencySpace)
                for (int i=0; i<3; i++) cimg[i] = Operations.OperationsFFT._FFT(cimg[i]);
            img = new float[3][][];
            for (int i = 0; i < 3; i++ )
            {
                img[i] = cimg[i].FromFloatModulus();
            }
            picFFT.Image = Normalize.FromFloat(img) ;
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
            udLow.Maximum = (int)(1.0/Math.Sqrt(2) *  cimg[0].Width);
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

        private void FFT()
        {
            float scale = 1f / (float)Math.Sqrt(cimg[0].Width * cimg[0].Height);
            ComplexF[][] data = new ComplexF[3][];
            for (int i=0; i<3; i++) data[i] = cimg[i].Data;

            int offset = 0;
            for (int c = 0; c < 3; c++)
            {
                offset = 0;
                for (int y = 0; y < cimg[0].Height; y++)
                {
                    for (int x = 0; x < cimg[0].Width; x++)
                    {
                        if (((x + y) & 0x1) != 0)
                        {
                            data[c][offset] *= -1;
                        }
                        offset++;
                    }
                }


                Exocortex.DSP.Fourier.FFT2(data[c], cimg[0].Width, cimg[0].Height, FourierDirection.Forward);

                cimg[c].FrequencySpace = true;

                for (int i = 0; i < data.Length; i++)
                {
                    data[c][i] *= scale;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IOperand x = null;
            this.Hide();

            if (myParent != null)
            {
                x = (IOperand)myParent;
            }

            switch (myMode)
            {
                case Mode.High: Operations.OperationsFFT.HighPass(x, this.spawn, (float)udLow.Value); break;
                case Mode.Low: Operations.OperationsFFT.LowPass(x, this.spawn, (float)udHigh.Value); break;
                case Mode.Narrow: Operations.OperationsFFT.BandPass(x, this.spawn, (float)udLow.Value, (float)udHigh.Value); break;
                case Mode.Notch: Operations.OperationsFFT.Notch(x, this.spawn, (float)udLow.Value, (float)udHigh.Value); break;
                case Mode.ButterHigh: Operations.OperationsFFT.ButterworthHigh(x, this.spawn, (float)udLow.Value); break;
                case Mode.ButterLow: Operations.OperationsFFT.ButterworthLow(x, this.spawn, (float)udHigh.Value); break;
                case Mode.ButterHigh4: Operations.OperationsFFT.ButterworthHigh4(x, this.spawn, (float)udLow.Value); break;
                case Mode.ButterLow4: Operations.OperationsFFT.ButterworthLow4(x, this.spawn, (float)udHigh.Value); break;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
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

        private void drawCircles(object sender, PaintEventArgs e)
        {
            GraphicsPath path1 = new GraphicsPath();
            GraphicsPath path2 = new GraphicsPath();
            GraphicsPath box = new GraphicsPath();

            Graphics g = e.Graphics;

            Pen p = new Pen(Color.HotPink, 1);
            Brush b = new SolidBrush(Color.FromArgb(150,Color.HotPink));

            decimal scale = (decimal)picFFT.Width / (decimal)cimg[0].Width;

            path1.AddEllipse((int)picFFT.Width / 2 - (int)(udLow.Value * scale), (int)picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
            path2.AddEllipse((int)picFFT.Width / 2 - (int)(udHigh.Value * scale), (int)picFFT.Height / 2 - (int)(udHigh.Value * scale), (int)(udHigh.Value * 2 * scale), (int)(udHigh.Value * 2 * scale));
            box.AddRectangle(new Rectangle(0, 0, picFFT.Width, picFFT.Height));

            if (myMode == Mode.High || myMode == Mode.ButterHigh || myMode == Mode.ButterHigh4)
            {
                g.DrawEllipse(p, (int)picFFT.Width / 2 - (int)(udLow.Value * scale), (int)picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
                g.FillEllipse(b, (int)picFFT.Width / 2 - (int)(udLow.Value * scale), (int)picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
            }
            if (myMode == Mode.Low || myMode == Mode.ButterLow || myMode == Mode.ButterLow4)
            {
                System.Drawing.Region r = new Region(box);
                r.Exclude(path2);
                g.DrawEllipse(p, (int)picFFT.Width / 2 - (int)(udHigh.Value * scale), (int)picFFT.Height / 2 - (int)(udHigh.Value * scale), (int)(udHigh.Value * 2 * scale), (int)(udHigh.Value * 2 * scale));
                g.FillRegion(b, r);
            }
            if (myMode == Mode.Narrow)
            {
                System.Drawing.Region r = new Region(box);
                r.Exclude(path2);
                g.DrawEllipse(p, (int)picFFT.Width / 2 - (int)(udHigh.Value * scale), (int)picFFT.Height / 2 - (int)(udHigh.Value * scale), (int)(udHigh.Value * 2 * scale), (int)(udHigh.Value * 2 * scale));
                g.FillRegion(b, r);
                g.DrawEllipse(p, (int)picFFT.Width / 2 - (int)(udLow.Value * scale), (int)picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
                g.FillEllipse(b, (int)picFFT.Width / 2 - (int)(udLow.Value * scale), (int)picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
            }
            if (myMode == Mode.Notch)
            {
                System.Drawing.Region r = new Region(path2);
                r.Exclude(path1);
                g.DrawEllipse(p, (int)picFFT.Width / 2 - (int)(udHigh.Value * scale), (int)picFFT.Height / 2 - (int)(udHigh.Value * scale), (int)(udHigh.Value * 2 * scale), (int)(udHigh.Value * 2 * scale));
                g.FillRegion(b, r);
                g.DrawEllipse(p, (int)picFFT.Width / 2 - (int)(udLow.Value * scale), (int)picFFT.Height / 2 - (int)(udLow.Value * scale), (int)(udLow.Value * 2 * scale), (int)(udLow.Value * 2 * scale));
            }
        }

        private void updateCanvas(object sender, EventArgs e)
        {
            picFFT.Update();
            picFFT.Refresh();
            if ( udHigh.Enabled ) udLow.Maximum = udHigh.Value;
            if ( udLow.Enabled ) udHigh.Minimum = udLow.Value;
        }
    }
}
