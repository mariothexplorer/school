using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shkolo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            groupBox2.Location = groupBox1.Location;
            groupBox3.Location = groupBox1.Location;
            groupBox4.Location = groupBox1.Location;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            StreamReader reader = new StreamReader("students.txt");
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                int pos = line.IndexOf(":");
                comboBox4.Items.Add(line.Substring(0, pos));
                
            }
            reader.Close();
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            if (comboBox1.SelectedIndex == 0)
            {
                groupBox1.Show();
                groupBox2.Hide();
                groupBox3.Hide();
                groupBox4.Hide();
                comboBox2.Parent = groupBox1;
                label3.Parent = groupBox1;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                groupBox1.Hide();
                groupBox2.Show();
                groupBox3.Hide();
                groupBox4.Hide();
                comboBox2.Parent = groupBox2;
                label3.Parent = groupBox2;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                groupBox1.Hide();
                groupBox3.Show();
                groupBox2.Hide();
                groupBox4.Hide();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                groupBox1.Hide();
                groupBox3.Hide();
                groupBox2.Hide();
                groupBox4.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           StreamReader reader = new StreamReader("teachers.txt");
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {

                int pos= line.IndexOf(":");
                comboBox2.Items.Add(line.Substring(0, pos));
                comboBox3.Items.Add(line.Substring(pos+1, line.Length-pos-1));
            }
            reader.Close();
           
        }
        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox2.SelectedIndex == -1)
            { 
                return; 
            }
            StreamWriter writer = new StreamWriter("students.txt", true);
            writer.WriteLine(textBox1.Text+":"+comboBox2.Text);
            writer.Close();
            MessageBox.Show("Ученикът е записан!");
            textBox1.Text = "";
            comboBox2.SelectedIndex = -1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = "Класен ръководител:";
            listBox1.Items.Clear();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            StreamReader reader = new StreamReader("teachers.txt");
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                int pos = line.IndexOf(":");
                if (line.Substring(0, pos) == comboBox2.Text)
                {
                    label4.Text = label4.Text+ " " +line.Substring(pos+1);
                    break;
                }
            }
            reader.Close();
            reader = new StreamReader("students.txt");
            line = "";
            while ((line = reader.ReadLine()) != null)
            {
                int pos = line.IndexOf(":");
                if (line.Substring(pos+1) == comboBox2.Text)
                {
                    listBox1.Items.Add(line.Substring(0, pos));
                }
            }
            reader.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            StreamReader reader = new StreamReader("teachers.txt");
            string line = "";
            string klass = "";
            while ((line = reader.ReadLine()) != null)
            {
                int pos = line.IndexOf(":");
                if (line.Substring(pos+1, line.Length-pos-1) == comboBox3.Text)
                {
                    klass = line.Substring(0, pos);
                    label6.Text = "Клас: " + klass;
                    break;
                }
            }
            reader.Close();
            reader = new StreamReader("students.txt");
            line = "";
            while ((line = reader.ReadLine()) != null)
            {
                int pos = line.IndexOf(":");
                if (line.Substring(pos+1) == klass)
                {
                    listBox2.Items.Add(line.Substring(0, pos));
                }
            }
            reader.Close();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Text = "Клас: ";
            listBox2.Items.Clear();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = "Клас: ";
            label9.Text = "Класен ръководител: ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("students.txt");
            string line = "";
            string klass = "";
            while ((line = reader.ReadLine()) != null)
            {
                int pos = line.IndexOf(":");
                if (line.Substring(0, pos) == comboBox4.Text)
                {
                    klass = line.Substring(pos + 1);
                    label7.Text = "Клас: " + klass;
                    break;
                }
            }
            reader.Close();
            reader= new StreamReader("teachers.txt");
            line = "";
            while ((line = reader.ReadLine()) != null)
            {
                int pos = line.IndexOf(":");
                if (line.Substring(0, pos) == klass)
                {
                    label9.Text = "Класен ръководител: " + line.Substring(pos + 1);
                    break;
                }
            }
        }
    }
}
