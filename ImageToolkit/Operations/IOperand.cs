using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ImageToolkit
{
    public interface IOperand
    {
        // Required Properites

        string Description { get; set; }
        int ImageWidth { get; }
        int ImageHeight { get; }

        // Required Event

        event System.Windows.Forms.FormClosingEventHandler FormClosing;

        // Required Methods

        void Show();
        void Hide();
        void Close();
        void Dispose();
        string ToString();
        float[][][] GetFloat();     // Returns image as jagged array
        Bitmap GetBitmap();
        IOperand CreateSibling(float[][][] sourceImage, String description);    // like a clone with new content
    }


}
