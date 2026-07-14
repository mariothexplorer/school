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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;

namespace Reports
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        string file_name;
        int num = 0;
        private void Show__12()
        {
            file_name = a + ".txt";
            if (a == "Sunday" || a == "Saturday")
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Почивен ден!");
                Change__12();
                label1.Text = a;
                return;
            }
            Change__12();
            label1.Text = a;

            StreamReader f = new StreamReader(file_name);
            num = 0;
            listBox1.Items.Clear();
            while (true)
            {
                string line = f.ReadLine();
                if (line == null) break;
                listBox1.Items.Add(line);
                num++;
            }
            f.Close();
            if (num == 0)
            {
                return;
            }
        }

        private void Change__12()
        {
            if (a=="Monday") 
            {
                a = "Понеделник";
            }
            if (a == "Tuesday")
            {
                a = "Вторник";

            }
            if (a == "Wednesday")
            {
                a = "Сряда";

            }
            if (a == "Thursday")
            {
                a = "Четвъртък";

            }
            if (a == "Friday")
            {
                a = "Петък";

            }
            if (a == "Saturday")
            {
                a = "Събота";

            }
            if (a == "Sunday")
            {
                a = "Неделя";

            }

        }

        string day_of_week;
        string a;
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionRange.Start;
            day_of_week = selectedDate.DayOfWeek.ToString();
            a = day_of_week;
            Show__12();


            /*if (selectedDate < new DateTime(2024, 2, 6))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Избери дата, която е");
                listBox1.Items.Add("от втори срок на");
                listBox1.Items.Add("текущата учебна година!");


            }
            if (selectedDate > new DateTime(2024, 6, 30))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Избери дата, която е");
                listBox1.Items.Add("от втори срок на");
                listBox1.Items.Add("текущата учебна година!");
            }
            */
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionRange.Start;
            day_of_week = selectedDate.DayOfWeek.ToString();
            a = day_of_week;
            Show__12();
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string[] dni = { "Monday","Tuesday","Wednesday","Thursday","Friday"};
            string[] dni_ = { "Понеделник", "Вторник", "Сряда", "Четвъртък", "Петък" };
            string fl;
            for (int i = 0; i < dni.Length; i++)
            {
                fl = dni[i] + ".txt";
                StreamReader s = new StreamReader(fl);
                int  z = 0;
                while (true)
                {
                    string ln = s.ReadLine();
                    if (ln == null) break;
                    if (ln.Contains(comboBox1.SelectedItem.ToString()))
                    {
                        listBox2.Items.Add(dni_[i] + " -> " + z);
                    }
                    z++;
                }
            }
        }

    }
}
