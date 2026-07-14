using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //za rekord
            label6.Text = (string)Application.UserAppDataRegistry.GetValue(save_record,"0");
            record = int.Parse(label6.Text);
           

            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }  
        
        int m;
        Random r = new Random();
        int time;
        Button[] buttons;
        bool[] flag;
        int points;
        int level;
        string save_record;
        int record;


        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            points = 0;
            label4.Text = points.ToString();
            level = comboBox1.SelectedIndex+3;
            buttons = new Button[level*level];
            comboBox1.Enabled = false;


            flag = new bool[level*level];
            // true - skrit, false - vidim

            panel1.Controls.Clear();
            m = 60 / level;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Parent = panel1;
                buttons[i].Width = (panel1.Width - (level + 1) * m) / level;
                buttons[i].Height = (panel1.Height - (level + 1) * m) / level;
                buttons[i].Top = (i / level) * buttons[i].Height + (i / level + 1) * m;
                buttons[i].Left = (i % level) * buttons[i].Width + (i % level + 1) * m;
                int p = r.Next(2);
                if(p==0)
                {
                    buttons[i].Hide();
                    flag[i] = true;
                }

                timer1.Start();
                time = (int)Math.Ceiling(level/2.0);

                buttons[i].Click += Form1_Click;
                buttons[i].Tag = i;
            }

            
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled==true)
            {
                return;
            }
            Button b = (Button)sender;
            int number = (int)b.Tag;
            if (flag[number] == true)
            {
                buttons[number].Hide();
                points++;
            }
            else
            {
                points--;

            }
            label4.Text = points.ToString();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            if (time==0)
            {
                timer1.Stop();
                //MessageBox.Show("Махни излишните бутони!","Инструкции");
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].Show();
                }
                timer2.Start();
                time = level * 2;
                label5.Text = time.ToString();
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            //proverka dali sme poznali wsi`ki wuw wremeto;
            MessageBox.Show("Край на играта!");
            comboBox1.Enabled = true;
            button2.Enabled = false;
            panel1.Controls.Clear();
            //записване на рекорд;
            if (points > record)
            {
                record = points;
                label6.Text = record.ToString();
                Application.UserAppDataRegistry.SetValue(save_record, label6.Text);
            }




        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            time--;
            label5.Text=time.ToString();
            if (time==0)
            {
                button2.PerformClick();
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled=true;
        }
    }
}
