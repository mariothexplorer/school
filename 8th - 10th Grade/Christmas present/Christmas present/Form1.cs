using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Christmas_present
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
            Button[] boxes = new Button[10];
            double[] sums = {0.5, 1,2,5,10,20,50,100,500,1000};
            Label[] prizes = new Label[10];
            bool ok;
            int cnt;



        private void Form1_Load(object sender, EventArgs e)
        {
            label12.Hide();
            panel1.Hide();
            boxes[0] = button1;
            boxes[1] = button2;
            boxes[2] = button3;
            boxes[3] = button4;
            boxes[4] = button5;
            boxes[5] = button6;
            boxes[6] = button7;
            boxes[7] = button8;
            boxes[8] = button9;
            boxes[9] = button10;
            

            for (int i = 0; i < 10; i++)
            {
                boxes[i].Text = (i + 1).ToString();
                //boxes[i].BackColor = Color.Green;
                boxes[i].Enabled = false;
            }

            prizes[0] = label1;
            prizes[1] = label2;
            prizes[2] = label3;
            prizes[3] = label4;
            prizes[4] = label5;
            prizes[5] = label6;
            prizes[6] = label7;
            prizes[7] = label8;
            prizes[8] = label9;
            prizes[9] = label10;

            for (int i = 0; i < 10; i++)
            {
                prizes[i].Text = sums[i].ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label12.Show();
            Button b = (Button)sender;
            int number = int.Parse(b.Text) - 1;
            b.Enabled = false;
            b.Text = sums[number].ToString();
           
            for (int i = 0; i < 10; i++)
            {
                if (prizes[i].Text == b.Text)
                {
                    prizes[i].Hide();
                    break;
                }
            }
            cnt++;
           
            if (cnt>=1 && cnt<=4)
            {
                sum = sum + sums[number] * 4;
                label12.Text = "Натрупана сума: " + sum.ToString() + " лева";
            }
            if (cnt == 4)
            {
                    
                MessageBox.Show("Отвори още 3 кутии!");

            }
            if (cnt>=5 && cnt<=7)
            {
                sum = sum + sums[number] * 3;
                label12.Text = "Натрупана сума: " + sum.ToString() + " лева";
            }
            if (cnt == 7)
            {

                MessageBox.Show("Отвори още 2 кутии!");

            }
            if (cnt>= 8 && cnt <= 9)
            {
                sum = sum + sums[number] * 2;
                label12.Text = "Натрупана сума: " + sum.ToString() + " лева";
            }
            if (cnt == 9)
            {

                MessageBox.Show("Отвори последната кутия!");

            }
            if (cnt == 10)
            {

                button11.Show();
                for (int i = 0; i < 10; i++)
                {
                    boxes[i].Text = sums[i].ToString();
                    boxes[i].Enabled = false;                    
                    
                }
                sum = sum + sums[number];
                label12.Text = "Натрупана сума: " + sum.ToString() + " лева";
                MessageBox.Show(sum.ToString() + " лева", "Печелиш");
                panel1.Hide();
                label12.Text="Натрупана сума";
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            for (int i = 0; i < 10; i++)
            {
                boxes[i].Enabled = true;
                boxes[i].Text = (i + 1).ToString();
                prizes[i].Show();
            }
            Shuffel();
            cnt = 0;
            sum = 0;
            button11.Hide();
            panel1.Show();

            MessageBox.Show("Отвори 4 кутии!");
        }
        double sum;
        private void Shuffel()
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = r.Next(10);
                int y = r.Next(10);
                double temp = sums[x];
                sums[x] = sums[y];
                sums[y] = temp;

            }

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
