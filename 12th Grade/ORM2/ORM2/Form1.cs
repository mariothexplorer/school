using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORM2
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
            var q = from r in t.Employees
                    select new { r.FirstName, r.LastName };
            dataGridView1.DataSource = q.ToList();
            var s = from r in t.Departments
                    select r.Name;
            comboBox1.DataSource = s.ToList();
            comboBox1.SelectedIndex = -1;
            var q2 = from r in t.Employees
                     select r.FirstName + " " + r.LastName;
            comboBox2.DataSource = q2.ToList();
            comboBox2.SelectedIndex = -1;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            string first = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string last = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            var q = from r in t.Employees
                    where r.FirstName == first && r.LastName == last
                    from x in r.Projects
                    select  x.Name ;
            listBox1.DataSource = q.ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nameDep = textBox1.Text;
            var q = from r in t.Employees
                    where r.FirstName + " " + r.LastName == comboBox2.Text
                    select r.EmployeeID;
            int managerID = (int)q.First();
            Department d = new Department()
            { 
                Name = nameDep,
                ManagerID = managerID
            };
            t.Departments.Add(d);
            t.SaveChanges();
            MessageBox.Show("Записан");



        }
    }
}
