using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace School_Book
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = true;
            comboBox3.SelectedIndex = -1;
            listBox2.Items.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (comboBox2.SelectedIndex == 0)
            {
               Add_Students(first);
            }
            if (comboBox2.SelectedIndex == 1)
            {
                Add_Students(second);
            }
        }

        private void Add_Students(string[] s)
        {
            if (comboBox3.SelectedIndex == 5)
            {
                for (int i = 0; i < num; i++)
                {
                    listBox2.Items.Add(names[i] + " " + s[i]);
                }
                return;
            }
            string m = comboBox3.Text;
            for (int i = 0; i < num; i++)
            {
                if (s[i].IndexOf(m) >= 0)
                {
                    listBox2.Items.Add(names[i] + " " + s[i]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = copy[n];
            n++;
            if (n==num)
            {
                n = 0;
                Shuffle();
            }
        }

        private void Shuffle()
        {
            for (int i = 0; i < num; i++)
            {
                int x = r.Next(num);
                int y = r.Next(num);
                string temp = copy[x];
                copy[x] = copy[y];
                copy[y] = temp;
            }
        }

        string[]names = new string[40];
        string[] first = new string[40];
        string[] second = new string[40];
        int num;
        Random r = new Random();
        string[] copy = new string[40];
        int n = 0;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = false;
            string file_name = comboBox1.Text + ".txt";
            StreamReader f = new StreamReader(file_name);
            num = 0;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            label4.Text = "";
            while (true)
            {
                string line = f.ReadLine();
                if (line == null) break;
                string[] temp = line.Split('>');
                names[num] = temp[0].Substring(0, temp[0].Length-1).Trim();
                copy[num] = names[num];
                first[num] = temp[1].Substring(0, temp[1].Length - 1).Trim();
                second[num] = temp[2].Trim();
                listBox1.Items.Add(line);
                num++;
            }
            f.Close();
            if (num==0)
            {
                return;
            }
            comboBox2.Enabled = true;
            //comboBox3.Enabled=true;
            button1.Enabled=true;
            Shuffle();
        }
    }
}
