using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animation__Drawing____Examination
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Graphics g = panel1.CreateGraphics();
            g.Clear(Color.White);
            //Химикалка за рисуване
            Pen p1 = new Pen(Color.Black, 2);
            // Гума - да е същя цвят като фона
            Pen p2 = new Pen(Color.White, 2);
            // b1 - ГУМА
            Brush b1 = new SolidBrush(Color.White);
            Brush b2 = new SolidBrush(Color.Red);
            Brush b3 = new SolidBrush(Color.Yellow);
            Brush b4 = new SolidBrush(Color.Green);
            Brush b5 = new SolidBrush(Color.Gray);
            int x = 20;
            int y = 300;
            Korab(g, p1, x, y);
            g.FillRectangle(b5, panel1.Width-100, 20, 80, 120);
            g.FillRectangle(b5, panel1.Width-80, 120, 40, panel1.Width-120);

            g.DrawEllipse(p1, panel1.Width - 75, 30, 30,30 );
            g.DrawEllipse(p1, panel1.Width - 75, 60, 30, 30);
            g.DrawEllipse(p1, panel1.Width - 75, 90, 30, 30);

            g.FillEllipse(b2, panel1.Width - 75, 30, 30, 30);
            Thread.Sleep(2000);
            g.FillEllipse(b5, panel1.Width - 75, 30, 30, 30);

            g.FillEllipse(b3, panel1.Width - 75, 60, 30, 30);
            Thread.Sleep(2000);
            g.FillEllipse(b5, panel1.Width - 75, 60, 30, 30);

            g.FillEllipse(b4, panel1.Width - 75, 90, 30, 30);

            
                for (int j = 0; j < 56; j++)
                {
                    Korab(g, p1, x, y);
                    //КОНТРОЛИРА СКОРОСТТА
                    Thread.Sleep(50);
                    Korab(g, p2, x, y);
                    x = x + 10;
                }
                Korab(g, p1, x, y);
            button1.Enabled = true;

        }

        private static void Korab(Graphics g, Pen p1, int x, int y)
        {
            g.DrawEllipse(p1, x, y, 70, 70);
            g.DrawLine(p1, x+30, y, x+130, y);
            g.DrawEllipse(p1, x+90, y, 70, 70);
            g.DrawLine(p1, x + 130, y, x + 130, y - 102);
            g.DrawLine(p1, x + 100, y - 90, x + 160, y - 115);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    } 
}
