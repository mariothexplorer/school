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

namespace Animation__Drawing_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random r = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();

            // САМО ОЧЕРТАНИЯ - ХИМИКАЛ
            // ЗАПЪЛНЕНО - ЧЕТКА

            //ФОН
            g.Clear(Color.White);
            Pen p1 = new Pen(Color.Green, 5);

            // Гума - да е същя цвят като фона
            Pen p2 = new Pen(Color.White, 5);
            int x = 20;
            int y = 20;

            for (int i = 0; i < 2; i++)
            {
       
             Korab(g, p1, x, y);

                //КОНТРОЛИРА СКОРОСТТА
                Thread.Sleep(1000);
                Korab(g, p2, x, y);
                x = r.Next(0, panel1.Width-100);
                y = r.Next(0, panel1.Height - 100);


            }


            //g.DrawEllipse(p1, x, 20, 50, 50);
        }

        private static void Korab(Graphics g, Pen p1, int x, int y)
        {
            
            g.DrawEllipse(p1, x, y, 100, 70);
            int x1 = x + 10;
            int y1 = y + 25;
            g.DrawEllipse(p1, x1, y1, 20, 20);
            g.DrawEllipse(p1, x + 40, y1, 20, 20);
            g.DrawEllipse(p1, x + 70, y1, 20, 20);
        }
    }
}
