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

namespace School_program
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {   if(comboBox1.SelectedIndex == -1)
                {
                    return;
                }   
                listBox1.Items.Clear();
                StreamReader f = new StreamReader(comboBox1.Text + ".txt");
                while (true)
                {
                    string line = f.ReadLine();
                    if (line == null){ break; }
                    listBox1.Items.Add(line);
                }
                f.Close();  
            }
            catch (Exception)
            {

                MessageBox.Show("Липсва файл за избрания ден","Error");
            }
        }

        string[] days = { "Понеделник","Вторник","Сряда","Четвъртък","Петък","Събота", "Неделя"};


        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            listBox1.Items.Clear();
            comboBox1.SelectedIndex = -1;
            DateTime d = monthCalendar1.SelectionStart;
            DateTime d1 = new DateTime(2024, 2, 6);
            DateTime d2 = new DateTime(2024,6,30);
            if (d.CompareTo(d1)<0||d.CompareTo(d2)>0)
            {
                MessageBox.Show("Избраната дата е извън учебния срок!","Error");
                comboBox1.SelectedIndex=-1;
                listBox1.Items.Clear();
                return;
            }
            int p = (int) d.DayOfWeek;
            if (p == 6 || p == 0)
            {
                MessageBox.Show("Почивен ден!", "Error");
                return;
            }
            comboBox1.Text = days[p - 1];


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                string file_name = days[i] + ".txt";
                if (!File.Exists(file_name))
                {
                    continue;
                }

                int cnt = 0;
                string s = "";
                StreamReader sr = new StreamReader(file_name); 
                for (int j = 0; j<9; j++)
                {
                    string line = sr.ReadLine();
                    if (line.IndexOf(comboBox2.Text) >= 0)
                    {
                        cnt++;
                        if (cnt==1)
                        { 
                            s = s + days[i] + " - " + line.Substring(0, 1);
                        }
                        else
                        {
                            s = s + ", " +  line.Substring(0, 1);

                        }
                    }
                }
                if (s!="")
                {
                    listBox2.Items.Add(s);
                }
                sr.Close();
                     
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = "";
            comboBox4.SelectedIndex = -1;

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            label5.Text = "";
            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Избери ден от седмицата!","Error");
                return;
            }
            string file_name = comboBox3.Text + ".txt";
            if (!File.Exists(file_name))
            {
                return;
            }
            StreamReader sr = new StreamReader(file_name);
            for (int j = 0; j < 9; j++)
            {
                string line = sr.ReadLine();
                if (j == comboBox4.SelectedIndex)
                {
                    label5.Text = line.Substring(3);
                    break;
                }
            }
            sr.Close ();    
        }
    }
}
