using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORM_3
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
            var q = from r in t.Towns
                    orderby r.Name ascending
                    select r.Name;
            comboBox1.DataSource = q.ToList();
            comboBox1.SelectedIndex = -1;
            dataGridView1.DataSource = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text;
            var q = from r in t.Employees
                    where r.Address.Town.Name == comboBox1.Text
                    select new
                    {
                        r.FirstName,
                        r.LastName,
                        r.Salary
                    };  
            dataGridView1.DataSource = q.ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Town town = new Town()
            { 
                Name = textBox1.Text 
            };
            t.Towns.Add(town);
            t.SaveChanges();
            MessageBox.Show("Записан");
            Form1_Load(sender, e);
           textBox1.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Town town = (from r in t.Towns
                         where r.Name == comboBox1.Text
                         select r).First();
            town.Name = textBox1.Text;
            t.SaveChanges();
            MessageBox.Show("Променен!");
            Form1_Load(sender, e);
            textBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Town town = (from r in t.Towns
                             where r.Name == textBox1.Text  
                             select r).First();
                t.Towns.Remove(town);
                t.SaveChanges();
                MessageBox.Show("Изтрито!");
                Form1_Load(sender, e);
                textBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невъзможно изтриване");
                t.Dispose();
                t = new TelerikAcademy12gEntities();
            }
        }
    }
}
