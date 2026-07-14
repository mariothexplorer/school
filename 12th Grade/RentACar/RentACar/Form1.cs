using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentACar
{
    public partial class Form1 : Form
    {
        private DataTable _tables;
        public Form1()
        {
            InitializeComponent();
            var tables = new[]
            {
                "Cars",
                "Customers",
                "Reservations",
                "Brands",
                "Categories",
                "Fuels",
                "Gearboxes",
                "Locations"
            };
            textBox1.Hide();
            comboBox1.DataSource = tables.ToList();
        }
        RentACarDBEntities r = new RentACarDBEntities();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            string selected = comboBox1.SelectedItem.ToString();

            switch (selected)
            {
                case "Cars":
                    dataGridView1.DataSource = r.Cars
                        .Select(c => new
                        {
                            c.ID,
                            c.VIN,
                            c.Model,
                            c.Odometer,
                            c.YearOfProduction,
                            Brand = c.Brand.Name,
                            Category = c.Category.Name,
                            Fuel = c.Fuel.Name,
                            Gearbox = c.Gearbox.Name,
                            Location = c.Location.Name
                        })
                        .ToList();
                    break;

                case "Customers":
                    dataGridView1.DataSource = r.Customers
                        .Select(cu => new
                        {
                            cu.ID,
                            cu.FullName,
                            cu.PhoneNumber,
                            cu.LicenseNumber
                        })
                        .ToList();
                    break;

                case "Reservations":
                    dataGridView1.DataSource = r.Reservations
                        .Select(r => new
                        {
                            r.CustomerId,
                            Customer = r.Customer.FullName,
                            r.CarId,
                            Car = r.Car.Model,
                            r.FromDate,
                            r.ToDate,
                            r.Amount
                        })
                        .ToList();
                    break;

                case "Brands":
                    dataGridView1.DataSource = r.Brands
                        .Select(b => new { b.ID, b.Name })
                        .ToList();
                    break;

                case "Categories":
                    dataGridView1.DataSource = r.Categories
                        .Select(c => new { c.ID, c.Name })
                        .ToList();
                    break;

                case "Fuels":
                    dataGridView1.DataSource = r.Fuels
                        .Select(f => new { f.ID, f.Name })
                        .ToList();
                    break;

                case "Gearboxes":
                    dataGridView1.DataSource = r.Gearboxes
                        .Select(g => new { g.ID, g.Name })
                        .ToList();
                    break;

                case "Locations":
                    dataGridView1.DataSource = r.Locations
                        .Select(l => new { l.ID, l.Name })
                        .ToList();
                    break;
            }

        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            dataGridView1.DataSource = null;
        }
        SqlConnection con = new SqlConnection("Server=.\\sqlexpress; Database=RentACarDB; Integrated Security=true");

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(con);
            form2.ShowDialog();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Show();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "1234")
            {
                textBox1.Clear();
                textBox1.Hide();
                Form3 form3 = new Form3(r);
                form3.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(con);
            form4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(con);
            form5.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(con);
            form6.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(con);
            form7.ShowDialog();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8(con);
            form8.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9(con);
            form9.ShowDialog();
        }
    }
}
