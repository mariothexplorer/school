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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        string[] name_book = new string[10000];
        string[] name_author = new string[10000];
        string[] publisher = new string[10000];
        string[] price = new string[10000];
        int num = 0;
        int cena = 0;
        int cena_pub = 0;
        int cena_au = 0;
        bool y=true;   

        private void Reports_Load(object sender, EventArgs e)
        {
            
            label5.Text = "";            
            label4.Text = "";
            try
            {

                listBox1.Items.Clear();
                listBox2.Items.Clear(); 
                StreamReader f = new StreamReader("Books.txt");
                while (true)
                {
                    string line = f.ReadLine();
                    if (line == null && num == 0)
                    {
                        Temp y = new Temp();
                        y.ShowDialog();
                        this.Close();
                        break;                     
                    }
                    if (line == null) {  break; }              
                    string[] temp = line.Split(',', '-');
                    name_book[num] = temp[0].Substring(0, temp[0].Length - 1).Trim();
                    name_author[num] = temp[1].Substring(0, temp[1].Length - 1).Trim();
                    publisher[num] = temp[2].Substring(0, temp[2].Length - 1).Trim();
                    price[num] = temp[3].Trim();
                    if (comboBox2.Items.Contains(name_author[num]) ==false)
                    {
                        comboBox2.Items.Add(name_author[num]);
                    }
                    num++;
                }
                f.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Липсва файл с данни, затова го създай!", "Error");
                this.Close();
                Input y = new Input();
                y.ShowDialog();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cena_pub = 0;
            label4.Text = "";
            num = 0;
            listBox1.Items.Clear();
            string a = comboBox1.SelectedItem.ToString();
            StreamReader f = new StreamReader("Books.txt");
            while (true)
            {
                string line = f.ReadLine();
                if (line == null) { break; }
                string[] temp = line.Split(',', '-');
                name_book[num] = temp[0].Substring(0, temp[0].Length - 1).Trim();
                name_author[num] = temp[1].Substring(0, temp[1].Length - 1).Trim();
                publisher[num] = temp[2].Substring(0, temp[2].Length - 1).Trim();
                price[num] = temp[3].Trim();
                if (publisher[num].Contains(comboBox1.SelectedItem.ToString()) == true)
                {
                    y = false;
                    listBox1.Items.Add( name_book[num]  + "\"" + " , " + name_author[num] + " - " + price[num].ToString());
                }
                if (publisher[num].Contains(comboBox1.SelectedItem.ToString()) == false )
                {
                    num++;
                }
                if (listBox1.Items.Contains(name_author))
                {
                    num++;
                    listBox1.Items.Add("Няма книги нa това издателство!");
                    break;
                }
                if (listBox1.Items.Contains("Няма книги нa това издателство!") == true &&
                    listBox1.Items.Count > 0)
                {
                    listBox1.Items.Remove("Няма книги нa това издателство!");
                }
                if (listBox1.Items.Count == 0)
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add("Няма книги нa това издателство!");
                }
                num++;
            }
        
}

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = "";
            listBox2.Items.Clear();
            cena_au = 0;
            cena = 0;
            num = 0;
            StreamReader w = new StreamReader("Books.txt");
            while (true)
            {
                string line = w.ReadLine();
                if (line == null) { break; }
                string[] temp = line.Split(',', '-');
                name_book[num] = temp[0].Substring(0, temp[0].Length - 1).Trim();
                name_author[num] = temp[1].Substring(0, temp[1].Length - 1).Trim();
                publisher[num] = temp[2].Substring(0, temp[2].Length - 1).Trim();
                price[num] = temp[3].Trim();
                if (name_author[num] == comboBox2.Text)
                {
                    listBox2.Items.Add(line.Replace(", "+ name_author[num] + " -", " -"));
                    num++;
                }  
                else
                {
                    num++;
                }
            }
            w.Close();
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            num = 0;
            StreamReader w = new StreamReader("Books.txt");
            while (true)
            {
                string line = w.ReadLine();
                if (line == null) { break; }
                string[] temp = line.Split(',', '-');
                name_book[num] = temp[0].Substring(0, temp[0].Length - 1).Trim();
                name_author[num] = temp[1].Substring(0, temp[1].Length - 1).Trim();
                publisher[num] = temp[2].Substring(0, temp[2].Length - 1).Trim();
                price[num] = temp[3].Trim();
                cena = cena + int.Parse(price[num].Replace(" лева", " ").Trim());
                label3.Text= cena.ToString() + " лева";
                num++;
            }
            w.Close();
            cena = 0;
        }
        */
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex==-1)
            {
                MessageBox.Show("Избери автор!","Error");
                return;
            }

            cena_au = 0;
            cena = 0;
            num = 0;
            label5.Text = "";
            StreamReader w = new StreamReader("Books.txt");
            while (true)
            {
                string line = w.ReadLine();
                if (line == null) { break; }
                string[] temp = line.Split(',', '-');
                name_book[num] = temp[0].Substring(0, temp[0].Length - 1).Trim();
                name_author[num] = temp[1].Substring(0, temp[1].Length - 1).Trim();
                publisher[num] = temp[2].Substring(0, temp[2].Length - 1).Trim();
                price[num] = temp[3].Trim();
                if (line.Contains(comboBox2.SelectedItem.ToString()) == true)
                {
                    cena_au = cena_au + int.Parse(price[num].Replace(" лева", "").Trim());
                    label5.Text = "Обща сума на книгите от този автор: " + cena_au + " лева";
                }

                num++;
            }
            w.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Избери издателство!", "Error");
                return;
            }

            cena_pub = 0;
            label4.Text = "";
            num = 0;
            string a = comboBox1.SelectedItem.ToString();
            StreamReader f = new StreamReader("Books.txt");
            while (true)
            {
                string line = f.ReadLine();
                if (line == null) { break; }
                string[] temp = line.Split(',', '-');
                name_book[num] = temp[0].Substring(0, temp[0].Length - 1).Trim();
                name_author[num] = temp[1].Substring(0, temp[1].Length - 1).Trim();
                publisher[num] = temp[2].Substring(0, temp[2].Length - 1).Trim();
                price[num] = temp[3].Trim();

                if (publisher[num].Contains(comboBox1.SelectedItem.ToString()) == true)
                {
                   
                    cena_pub = cena_pub + int.Parse(price[num].Replace(" лева", "").Trim());
                    label4.Text = "Обща сума на книгите на издателството: " + cena_pub + " лева";
                }
                
                num++;
            }


        }
    }
}
