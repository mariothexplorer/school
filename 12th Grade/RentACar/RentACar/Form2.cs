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
    public partial class Form2 : Form
    {
        SqlConnection con;
        public Form2(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Всички");
            comboBox2.Items.Add("Всички");
            comboBox3.Items.Add("Всички");
            comboBox4.Items.Add("Всички");
            comboBox5.Items.Add("Всички");
            comboBox6.Items.Add("Всички");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;

            con.Open();

            // ========================
            // МАРКИ
            // ========================
            string g = @"SELECT Name FROM Brands";

            SqlCommand cmd = new SqlCommand(g, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }

            dr.Close();

            // ========================
            // КАТЕГОРИИ
            // ========================
            g = @"SELECT Name FROM Categories";

            cmd = new SqlCommand(g, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0]);
            }

            dr.Close();

            // ========================
            // ГОРИВА
            // ========================
            g = @"SELECT Name FROM Fuels";

            cmd = new SqlCommand(g, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox3.Items.Add(dr[0]);
            }

            dr.Close();

            // ========================
            // ЛОКАЦИИ
            // ========================
            g = @"SELECT Name FROM Locations";

            cmd = new SqlCommand(g, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox4.Items.Add(dr[0]);
            }

            dr.Close();

            // ========================
            // ГОДИНИ
            // ========================
            g = @"SELECT DISTINCT YearOfProduction 
                  FROM Cars
                  ORDER BY YearOfProduction";

            cmd = new SqlCommand(g, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox5.Items.Add(dr[0]);
            }
            dr.Close();
            // ========================
            // СКОРОСТНИ КУТИИ
            // ========================
            g = @"SELECT Name FROM Gearboxes";
            cmd = new SqlCommand(g, con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox6.Items.Add(dr[0]);
            }

            dr.Close();
        }   
       
      

        private void button1_Click(object sender, EventArgs e)
        {
            string brand = comboBox1.Text;
            string category = comboBox2.Text;
            string fuel = comboBox3.Text;
            string location = comboBox4.Text;
            string year = comboBox5.Text;
            string gearbox = comboBox6.Text;


            string g = @"
SELECT 
b.Name AS Марка,
c.Model AS Модел,
cat.Name AS Категория,
f.Name AS Гориво,
g.Name AS СкоростнаКутия,
l.Name AS Локация,
c.YearOfProduction AS Година,
c.Odometer AS Километри
FROM Cars c
JOIN Brands b ON c.BrandId = b.ID
JOIN Categories cat ON c.CategoryId = cat.ID
JOIN Fuels f ON c.FuelId = f.ID
JOIN Gearboxes g ON c.GearboxId = g.ID
JOIN Locations l ON c.LocationId = l.ID
WHERE b.Name LIKE @brand
AND cat.Name LIKE @category
AND f.Name LIKE @fuel
AND g.Name LIKE @gearbox
AND l.Name LIKE @location
AND CAST(c.YearOfProduction AS NVARCHAR) LIKE @year
";

            SqlCommand cmd = new SqlCommand(g, con);
            if (brand == "Всички")
                brand = "";

            if (category == "Всички")
                category = "";
            if (fuel == "Всички")
                fuel = "";
            if (location == "Всички")
                location = "";
            if (year == "Всички")
                year = "";
            if (gearbox == "Всички")
                gearbox = "";

            cmd.Parameters.AddWithValue("@brand", "%" + brand + "%");
            cmd.Parameters.AddWithValue("@category", "%" + category + "%");
            cmd.Parameters.AddWithValue("@fuel", "%" + fuel + "%");
            cmd.Parameters.AddWithValue("@location", "%" + location + "%");
            cmd.Parameters.AddWithValue("@year", "%" + year + "%");
            cmd.Parameters.AddWithValue("@gearbox", "%" + gearbox + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
