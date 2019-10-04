using System.Drawing;
using System.Windows.Forms;

namespace ImageToolkit
{
    public partial class Debug : Form
    {
        public Debug(Bitmap b)
        {
            InitializeComponent();
            pictureBox1.Image = b;
        }
    }
}