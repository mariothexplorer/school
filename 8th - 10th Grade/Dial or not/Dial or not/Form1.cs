using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dial_or_not
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        Button[] boxes = new Button[24];
        double[] sums = { 0.01, 0.10, 0.5, 1, 2, 5, 10, 50, 100, 250, 500, 750, 1000, 1500, 2500, 5000, 7500, 10000, 12500, 15000, 20000, 25000, 50000, 100000 };
        Label[] prizes = new Label[24];
        bool ok;
        int cnt;

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Hide();

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
            boxes[10] = button11;
            boxes[11] = button12;
            boxes[12] = button13;
            boxes[13] = button14;
            boxes[14] = button15;
            boxes[15] = button16;
            boxes[16] = button17;
            boxes[17] = button18;
            boxes[18] = button19;
            boxes[19] = button20;
            boxes[20] = button21;
            boxes[21] = button22;
            boxes[22] = button23;
            boxes[23] = button24;

            for (int i = 0; i < 24; i++)
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
            prizes[10] = label11;
            prizes[11] = label12;
            prizes[12] = label13;
            prizes[13] = label14;
            prizes[14] = label15;
            prizes[15] = label16;
            prizes[16] = label17;
            prizes[17] = label18;
            prizes[18] = label19;
            prizes[19] = label20;
            prizes[20] = label21;
            prizes[21] = label22;
            prizes[22] = label23;
            prizes[23] = label24;

            for (int i = 0; i < 24; i++)
            {
                prizes[i].Text = sums[i].ToString();
            }

        }

        private void button25_Click(object sender, EventArgs e)
        {
            makeChange = true;
            listBox1.Show();
            listBox1.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                boxes[i].Enabled = true;
                boxes[i].Text = (i+1).ToString();
                prizes[i].Show();

            }
            button25.Hide();
            ok = true;
            Shuffle();
            cnt = 0;
            end = 0;
            MessageBox.Show("Избери кутия!");

        }

        int myBox;

        int top, left, width, height;
        private void button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int number = int.Parse(b.Text) - 1;
            if (makeChange == false)
            {
                string temp = b.Text;
                b.Text = boxes[myBox].Text;
                boxes[myBox].Text = temp;
                double t = sums[number];
                sums[number] = sums[myBox];
                sums[myBox] = t ;
                makeChange = true;
                //Да се забави
                return;
            }
            
            b.Enabled = false;
            if (ok == true)
            {
                top = boxes[number].Top;
                left = boxes[number].Left;
                width = boxes[number].Width;
                height = boxes[number].Height;
                myBox = number;
                boxes[number].Parent = this;
                boxes[number].Top = button25.Top;
                boxes[number].Left = button25.Left;
                boxes[number].Width = button25.Width;
                boxes[number].Height = button25.Height;
                ok = false;
                MessageBox.Show("Избери шест кутии!");
            }
            else
            {
                b.Text = sums[number].ToString();
                for (int i = 0; i < 24; i++)
                {
                    if (prizes[i].Text==b.Text)
                    {
                        prizes[i].Hide();
                        break;                        
                    }
                }   
                    cnt++;
                    
                    if (cnt==6)
                    {
                        bool resuly = MakeDeal(cnt);
                        if (resuly==false)
                        {
                        MessageBox.Show("Отвори още 4 кутии!");

                        }
                        else
                        {

                         GameEnd();

                        }
                    }
                    if (cnt == 10)
                    {
                    bool resuly = MakeDeal(cnt);
                    if (resuly == false)
                    {
                        MessageBox.Show("Отвори още 3 кутии!");

                    }
                    else
                    {

                        GameEnd();

                    }
                    }
                    if (cnt == 13)
                    {
                    bool resuly = MakeDeal(cnt);
                    if (resuly == false)
                    {
                        MessageBox.Show("Отвори още 3 кутии!");

                    }
                    else
                    {

                        GameEnd();

                    }
                    }
                    if (cnt == 16)
                    {
                    bool resuly = MakeDeal(cnt);
                    if (resuly == false)
                    {
                        MessageBox.Show("Отвори още 3 кутии!");

                    }
                    else
                    {

                        GameEnd();

                    }
                    }
                    if (cnt == 19)
                    {
                    bool resuly = MakeDeal(cnt);
                    if (resuly == false)
                    {
                        MessageBox.Show("Отвори още 2 кутии!");

                    }
                    else
                    {

                        GameEnd();

                    }
                    }
                    if (cnt == 21)
                    {
                    bool resuly = MakeDeal(cnt);
                    if (resuly == false)
                    {
                        MessageBox.Show("Отвори още 1 кутии!");

                    }
                    else
                    {

                        GameEnd();

                    }
                    }
                    if (cnt == 22)
                    {
                    //krai
                    bool resuly = MakeDeal(cnt);
                    if (resuly == false)
                    {
                        end = 1;
                        MessageBox.Show("В твоята кутия има: " + sums[myBox] +" лева","Край на играта"); button25.Show();
                        


                    }
                     GameEnd();
                    }     

            }

        }


        int end;
        private void GameEnd()
        {
            
            if (end==0)
            {
                MessageBox.Show("Ти спечели: " + average + " лева", "Край на играта");
            }
            button25.Show();
            for (int i = 0; i < 24; i++)
            {
                boxes[i].Text = sums[i].ToString();
                boxes[i].Enabled = false;
            }
            boxes[myBox].Top = top;
            boxes[myBox].Left = left;
            boxes[myBox].Width = width;
            boxes[myBox].Height = height;
            boxes[myBox].Parent = panel1;
            
        }

        double average;
        int change;
        bool makeChange;
        private bool MakeDeal(int cnt)
        {
            change = r.Next(4);
            if (change != 2)
            {

                double sum = 0;
                for (int i = 0; i < 24; i++)
                {
                    if (prizes[i].Visible == true)
                    {
                        sum = sum + double.Parse(prizes[i].Text);

                    }
                }
                average = sum / (24 - cnt);
                // Да се оправи закръглянето!
                average = Math.Floor(average);
                listBox1.Show();
                listBox1.Items.Add(average);
                DialogResult d = MessageBox.Show("Предлагам ти сделка:" + average + " лева", "Сделка или не", MessageBoxButtons.YesNo);
                //listBox1.Items.Add(average);
                if (d == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                DialogResult d=MessageBox.Show("Смяна на кутиите?","", MessageBoxButtons.YesNo);
                if (d == DialogResult.Yes)
                {
                    MessageBox.Show("Избери нова кутия!");
                    makeChange = false;
                }
                return false;
            }

        }

        Random r = new Random();

        private void Shuffle()
        {
            for (int i = 0; i < 24; i++)
            {
                int x = r.Next(24);
                int y = r.Next(24);
                double temp = sums[x];
                 sums[x] = sums[y];
                sums[y] = temp;

            }
        }
    }
}


// Методът е особособена част от кода, която извършва нещо специофично.
// Изполвза се за стукториране на програма избягване поворението на
// код или рекурсия.Всеки метод има тип, име списък от аргументи,
// заградени в () и тяло заградено в {}. Събитията същ изпольват
// методи и всеки метод, който не е събитие се извиква чрез името си аргуенти.
