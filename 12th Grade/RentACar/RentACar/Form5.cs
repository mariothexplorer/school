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
    public partial class Form5 : Form
    {
        SqlConnection con;
        public Form5(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            con.Open();
            comboBox1.Items.Add("Всички");

            string g = @"SELECT Name FROM Brands";

            SqlCommand cmd = new SqlCommand(g, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }

            dr.Close();

            comboBox1.SelectedIndex = 0;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string brand = comboBox1.Text;

            if (brand == "Всички")
                brand = "";

            string g = @"

            SELECT 
            b.Name AS Марка,
            SUM(r.Amount) AS Приход

            FROM Reservations r

            JOIN Cars c ON r.CarId = c.ID
            JOIN Brands b ON c.BrandId = b.ID

            WHERE b.Name LIKE @brand

            GROUP BY b.Name

            ORDER BY Приход DESC

            ";

            SqlCommand cmd = new SqlCommand(g, con);

            cmd.Parameters.AddWithValue("@brand", "%" + brand + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }
    }
}
