using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToolkit.Operations
{
    public static class OperationsTransformations
    {
        // The following describes and registers all operations contained in this module.
        public static void RegisterOperations()
        {
            OperationRegistry.RegisterOperation(new Operation("Transformations", "Scale", "Scale(X,p)", Scale, true));
            OperationRegistry.RegisterOperation(new Operation("Transformations", "Rotate", "Rotate(X,T)", Rotate, true));
            OperationRegistry.RegisterOperation(new Operation("Transformations", "Crop", "Crop(X,(x1,y1,x2,y2))", Crop, true));
        }

        public static void Scale()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;

            frmScale fs = new frmScale(x.ImageWidth,x.ImageHeight);

            fs.Show();

            return;            
        }

        public static void Scale(int w, int h, IOperand x=null, bool spawn=true)
        {
            if (x == null) 
                x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = new Bitmap(x.GetBitmap(), new Size(w,h));
            if (spawn) x.CreateSibling(Normalize.ToFloat(bmp), "Scale change of " + bmp.ToString());
            else ((frmStandard)x).Image = bmp;
        }

        public static void Rotate()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;

            frmRotate fr = new frmRotate(x);
            fr.Show();
            return;
        }

        public static void Rotate(int angle, IOperand x=null, bool spawn=true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;
            Bitmap bmp = new Bitmap(x.GetBitmap());
            switch(angle)
            {
                case 90: bmp.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                case 180: bmp.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                case -90: bmp.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                default: bmp = Rotate(bmp, angle); break;
            };
            if (spawn) x.CreateSibling(Normalize.ToFloat(bmp), "Rotation of " + bmp.ToString());
            else ((frmStandard)x).Image = bmp;
        }

        public static Bitmap Rotate(Bitmap bmp, int angle, float bkcolor=0.0f)
        {
            
            float[][][] img = Normalize.ToFloat(bmp);
            int originalH = bmp.Height;
            int originalW = bmp.Width;
            double sin = Math.Sin((double)angle * Math.PI / 180.0);
            double cos = Math.Cos((double)angle * Math.PI / 180.0);
            int newH = Math.Abs(Convert.ToInt32(originalH * Math.Abs(cos) + originalW * Math.Abs(sin)));
            int newW = Math.Abs(Convert.ToInt32(originalH * Math.Abs(sin) + originalW * Math.Abs(cos)));
            float[][][] newimg = new float[Normalize.RGBPLANE_LENGTH][][];
            for (int c=0; c<Normalize.RGBPLANE_LENGTH; c++)
            {
                newimg[c] = new float[newH][];
                for (int h=0; h<newH; h++)
                {
                    newimg[c][h] = new float[newW];
                    for (int w = 0; w < newW; w++)
                        newimg[c][h][w] = 0.0f;
                }
            }
            for (int c=0; c<Normalize.RGBPLANE_LENGTH; c++)
                for (int h = 0; h < originalH; h++)
                {
                    for (int w = 0; w < originalW; w++)
                    {
                        int xX = w - originalW / 2;
                        int yY = h - originalH / 2;
                        int X = Convert.ToInt32(-sin * yY + cos * xX);
                        int Y = Convert.ToInt32(sin * xX + cos * yY);
                        Y += newH/2;
                        X += newW/2;
                        if (X < 0 || Y < 0 || X >= newW || Y >= newH) continue;
                        newimg[c][Y][X] = img[c][h][w];
                        if (Y > 0)
                        {
                            if (newimg[c][Y - 1][X] == 0.0f) newimg[c][Y - 1][X] = img[c][h][w];
                            newimg[c][Y - 1][X] += img[c][h][w];
                            newimg[c][Y - 1][X] /= 2;
                        }
                        if (X > 0)
                        {
                            if (newimg[c][Y][X - 1] == 0.0f) newimg[c][Y][X - 1] = img[c][h][w];
                            newimg[c][Y][X - 1] += img[c][h][w];
                            newimg[c][Y][X - 1] /= 2;
                        }

                        if (Y < newH - 1)
                        {
                            if (newimg[c][Y + 1][X] == 0.0f) newimg[c][Y + 1][X] = img[c][h][w];
                            newimg[c][Y + 1][X] += img[c][h][w];
                            newimg[c][Y + 1][X] /= 2;
                        }
                        if (X > newW - 1)
                        {
                            if (newimg[c][Y][X + 1] == 0.0f) newimg[c][Y][X + 1] = img[c][h][w];
                            newimg[c][Y][X + 1] += img[c][h][w];
                            newimg[c][Y][X + 1] /= 2;
                        }
                    }
                }
            return Normalize.FromFloat(newimg);
        }


        public static void Crop()
        {
            IOperand x = ExecutionStack.X;
            if (x == null) return;

            frmCrop fc = new frmCrop(x);
            //MessageBox.Show("Test");
            fc.Show();
            return;

        }

        public static void Crop(int x1, int y1, int x2, int y2, IOperand x=null, bool spawn=true)
        {
            if (x == null) x = ExecutionStack.X;
            if (x == null) return;
            Rectangle crect = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            Bitmap cbmp = new Bitmap(crect.Width, crect.Height);
            float[][][] old = Normalize.ToFloat(x.GetBitmap());
            float[][][] img = Normalize.ToFloat(cbmp);
            for (int c = 0; c < Normalize.RGBPLANE_LENGTH; c++)
                for (int w = 0; w < crect.Width; w++)
                    for (int h = 0; h < crect.Height; h++)
                        img[c][h][w] = old[c][h + y1][w + x1];
            if (spawn) x.CreateSibling(img, "Crop of " + x.ToString());
            else ((frmStandard)x).Image = Normalize.FromFloat(img);
        }

    }
}
