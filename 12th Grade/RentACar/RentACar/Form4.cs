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
    public partial class Form4 : Form
    {
        SqlConnection con;

        public Form4(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Всички");
            comboBox2.Items.Add("Всички");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            con.Open();

            // ========================
            // ЛОКАЦИИ
            // ========================
            string g = @"SELECT Name FROM Locations";
            SqlCommand cmd = new SqlCommand(g, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();

            // ========================
            // МОДЕЛИ
            // ========================
            g = @"SELECT DISTINCT Model FROM Cars";
            cmd = new SqlCommand(g, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateTimePicker1.Value;
            DateTime toDate = dateTimePicker2.Value;

            string location = comboBox1.Text;
            string model = comboBox2.Text;

            if (location == "Всички")
                location = "";

            if (model == "Всички")
                model = "";

            string g = @"
                    SELECT 
                    c.ID,
                    b.Name AS Марка,
                    c.Model AS Модел,
                    cat.Name AS Категория,
                    f.Name AS Гориво,
                    l.Name AS Локация,
                    c.YearOfProduction AS Година,
                    c.Odometer AS Километри
                    FROM Cars c
                    JOIN Brands b ON c.BrandId = b.ID
                    JOIN Categories cat ON c.CategoryId = cat.ID
                    JOIN Fuels f ON c.FuelId = f.ID
                    JOIN Locations l ON c.LocationId = l.ID
                    WHERE c.ID NOT IN
                    (
                        SELECT CarId
                        FROM Reservations
                        WHERE (FromDate <= @toDate AND ToDate >= @fromDate)
                    )
                    AND l.Name LIKE @location
                    AND c.Model LIKE @model
                    ";

            SqlCommand cmd = new SqlCommand(g, con);

            cmd.Parameters.AddWithValue("@fromDate", fromDate);
            cmd.Parameters.AddWithValue("@toDate", toDate);
            cmd.Parameters.AddWithValue("@location", "%" + location + "%");
            cmd.Parameters.AddWithValue("@model", "%" + model + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

        }
    }
}
