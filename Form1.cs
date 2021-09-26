using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wavelets2D
{
    public partial class Form1 : Form
    {
        static string fileToDisplay = "Face1.jpg";
        static Bitmap MyImage = new Bitmap(fileToDisplay);
        static List<List<double>> Pixels = new List<List<double>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Image = (Image)MyImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap c = new Bitmap(MyImage);
            int x, y;

            for (x = 0; x < c.Width; x++)
            {
                var tempList = new List<double>();
                for (y = 0; y < c.Height; y++)
                {
                    Color pc = c.GetPixel(x, y);
                    double avg = (pc.R + pc.G + pc.B) / 3;
                    Color newColor = Color.FromArgb( (int)avg, (int)avg, (int)avg);
                    tempList.Add(avg);
                    c.SetPixel(x, y, newColor);
                }
                Pixels.Add(tempList);
            }
            pictureBox1.Image = c;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var resList = new List<List<List<double>>>();
            foreach (var col in Pixels)
            {
                var res = WaveletsLogic.DisreteTransform(col, WaveletsLogic.Wave);
                resList.Add(res);
            }

            var singlePixelsRes = new List<List<double>>();
            for (int i = 0; i < resList.Count; i++)
            {
                singlePixelsRes.Add(resList[i][0]);
            }
            
            Bitmap newC = new Bitmap(MyImage.Width, MyImage.Height);
            for (int x = 0; x < MyImage.Width; x++)
            {
                for (int y = 0; y < MyImage.Height; y++)
                {
                    var newAvg = singlePixelsRes[x][y];
                    newAvg = Math.Abs(newAvg);
                    newAvg = newAvg > 255 ? 255 : newAvg;
                    Color newColor = Color.FromArgb((int)newAvg, (int)newAvg, (int)newAvg);
                    newC.SetPixel(x, y, newColor);
                }
            }
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.Image = newC;
        }
    }
}
