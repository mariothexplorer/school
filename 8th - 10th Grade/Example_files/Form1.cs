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
using System.Web;

namespace Example_files
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StreamReader s;
        double[] marks = new double[40];
        int number;
        string [] names = new string[40];

        private void button1_Click(object sender, EventArgs e)
        {
            s = new StreamReader("students.txt");
            listBox1.Items.Clear();
            number = 0;
            while (true)
            {
                string line = s.ReadLine();

                //Ако няма на другия ред нищо спира
                if (line==null)
                {
                    break;
                }

                string[] temp = line.Split(new char[] { '-', ','});
                names[number] = temp[0].Trim();
                marks[number] = double.Parse(temp[1]) ;
                if (names[number].Length <= 8)
                {
                    listBox1.Items.Add(names[number] + "\t\t" + marks[number]);
                }
                else
                {
                     listBox1.Items.Add(names[number] + "\t" + marks[number]);
                }
                number++;
            }
            s.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double sum = 0;
            for (int i = 0; i < number; i++)
            {
                sum = sum + marks[i];
            }

            label1.Text = Math.Round(sum / number, 2).ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int m = int.Parse(comboBox1.Text);
            listBox2.Items.Clear();
            int cnt = 0;
            for (int i = 0; i < number; i++)
            {
                if (marks[i]==m)
                {
                    cnt++;
                    listBox2.Items.Add(names[i]);
                }
            }
            label2.Text = cnt.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        { 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter w = new StreamWriter(textBox1.Text +".txt",true);
            w.WriteLine("Брой оценки от вида - " + comboBox1.Text);
            w.WriteLine("----------------------");
            foreach (object m in listBox2.Items)
            {
                w.WriteLine(m);
            }
            w.Close();
            MessageBox.Show("Готово!");
        }
    }
}
