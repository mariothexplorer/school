using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ORM_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TelerikAcademy12gEntities t = new TelerikAcademy12gEntities();
        private void Form1_Load(object sender, EventArgs e)
        {
            var q = from r in t.Projects orderby r.Name ascending select r.Name;
            comboBox1.DataSource = q.ToList();
            comboBox1.SelectedIndex = -1;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            var q = (from r in t.Projects
                     where r.Name == comboBox1.Text
                     from x in r.Employees
                     orderby x.Salary descending
                     select new { x.FirstName,
                         x.LastName,
                         x.Salary });
            dataGridView1.DataSource = q.ToList();


            //MessageBox.Show(number.ToString());
            //listBox1.Items.Clear();
            //foreach (var x in t.Projects.Include("Employees"))
            //{

            //    if (x.Name == comboBox1.Text)
            //    {
            //        foreach (var y in x.Employees)
            //        {
            //            listBox1.Items.Add(y.FirstName + " " + y.LastName);


            //        }
            //    }
        }

          
        }
    }

