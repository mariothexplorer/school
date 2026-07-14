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

namespace Library
{
    public partial class Input : Form
    {
        public Input()
        {
            InitializeComponent();
        }






        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text=="")
            {
                return;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        

        private void button1_Click(object sender, EventArgs e)
        {

            if(comboBox1.SelectedIndex == -1 || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "") 
            {
                MessageBox.Show("Въведи всички данни за книгата!", "Error");
                return;
            }
            listBox1.Items.Add("\"" + textBox1.Text + "\", " + textBox2.Text + " - " + comboBox1.SelectedItem + " - " + textBox3.Text + " лева");
            comboBox1.SelectedIndex = -1;
            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

  
        private void button2_Click(object sender, EventArgs e)
        {
            
           
            if (listBox1.Items.Count>0)
            {
                var debugFolderPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var filePath = Path.Combine(debugFolderPath, "Books.txt");

                using (var sw = new StreamWriter(filePath))
                {
                    foreach (var item in listBox1.Items)
                    {
                        sw.WriteLine(item.ToString());
                    }
                }
                MessageBox.Show("Успешно записана!", "Ready");

            }
            else
            {
                MessageBox.Show("Въведи поне една книга!","Error");
            }


        }

        

        private void Input_Load(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                StreamReader f = new StreamReader("Books.txt");
                while (true)
                {
                    string line = f.ReadLine();
                    if (line == null) { break; }
                    listBox1.Items.Add(line);
                }
                f.Close();
            }
            catch (Exception)
            {

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

    }
}
