using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageToolkit.Operations
{
    /*
     * float[][] filter = new float[][] { { 0f, -1f, 0f }, { -1f, 4f, -1f }, { 0f, -1f, 0f } };
    result[h][w] = 
    (filter[fh - 1][fw - 1] * p[h - 1][w - 1] + filter[fh - 1][fw] * p[h - 1][w] + filter[fh - 1][fw + 1] * p[h - 1][w + 1] +
    filter[fh][fw - 1] * p[h][w - 1] + filter[fh][fw] * p[h][w] + filter[fh][fw + 1] * p[h][w + 1] +
    filter[fh + 1][fw - 1] * p[h + 1][w - 1] + filter[fh + 1][fw] * p[h + 1][w] + filter[fh + 1][fw + 1] * p[h + 1][w + 1]);
     */
    class Filter
    {
        float[][] filter
        { get; set; }
        public Filter(float[] f)
        {
            float scale = 1f;
            filter = new float[3][];
            for (int i=0; i<3; i++)
            {
                filter[i] = new float[3];
                for (int j=0; j<3; j++)
                {
                    filter[i][j] = f[3 * i + j] / scale;
                }
            }
        }

        public float[][][] Apply(float[][][] f, int H, int W)
        {
            float[][] result = new float[H][];

            float min = 0f, max = 0f;
            
                
            for (int h = 0; h < H; h++)
            {
                result[h] = new float[W];

                for (int w = 0; w < W; w++)
                {
                    result[h][w] = 0f;
                }
            }
            
            
                
            for (int h = 0; h < H; h++)
            {
                for (int w = 0; w < W; w++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        result[h][w] += ((h > 0 && w > 0 ? filter[0][0] * f[c][h - 1][w - 1] : filter[0][0] * f[c][h][w]) + (h > 0 ? filter[0][1] * f[c][h - 1][w] : filter[0][0] * f[c][h][w]) + (h > 0 && w + 1 < W ? filter[0][2] * f[c][h - 1][w + 1] : filter[0][0] * f[c][h][w])
                            + (w > 0 ? filter[1][0] * f[c][h][w - 1] : filter[0][0] * f[c][h][w]) + filter[1][1] * f[c][h][w] + (w + 1 < W ? filter[1][2] * f[c][h][w + 1] : filter[0][0] * f[c][h][w])
                            + (h + 1 < H && w > 0 ? filter[2][0] * f[c][h + 1][w - 1] : filter[0][0] * f[c][h][w]) + (h + 1 < H ? filter[2][1] * f[c][h + 1][w] : filter[0][0] * f[c][h][w]) + (h + 1 < H && w + 1 < W ? filter[2][2] * f[c][h + 1][w + 1] : filter[0][0] * f[c][h][w])) / 3f;
                        

                    }
                    //result[h][w] /= 3f;
                    if (result[h][w] < min) min = result[h][w];
                    if (result[h][w] > max) max = result[h][w];
                }
            }

            MessageBox.Show("Min: " + min.ToString() + " Max: " + max.ToString());

            for (int h = 0; h < H; h++)
            {
                for (int w = 0; w < W; w++)
                {
                    result[h][w] += min;
                    result[h][w] *= 1f / (max - min);
                    //result[h][w] += 0.5f;
                }
            }

            float[][][] res = new float[3][][];
            for (int c = 0; c < 3; c++)
                res[c] = result;

            return res;
        }
    }
}
