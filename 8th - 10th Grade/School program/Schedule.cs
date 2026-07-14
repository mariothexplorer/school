using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;

namespace School_program
{
    public partial class Schedule : Form
    {
        public Schedule()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text=="")
            {
                MessageBox.Show("Избери ден от седмицата","Enter data");
                return;

            }
            StreamWriter s = new StreamWriter(comboBox1.Text + ".txt");
            for (int i = 0; i < 9; i++)
            {
                s.WriteLine(listBox1.Items[i]);
            }
            s.Close();
            MessageBox.Show("Успешно записана!","Ready");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text=="" || comboBox2.Text=="" || comboBox3.Text=="")
            {
                MessageBox.Show("Избери от всяка кутия!", "Enter data");
                return;
            }
            int hour = comboBox3.SelectedIndex;
            listBox1.Items[hour] = (hour + 1) + ". " + comboBox2.Text;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 1; i < 10; i++)
            {
                 listBox1.Items.Add(i+". - ");

            }
        }

        private void Schedule_Load(object sender, EventArgs e)
        {

        }
    }
}
