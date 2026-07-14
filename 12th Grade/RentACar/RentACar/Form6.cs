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
    public partial class Form6 : Form
    {
        SqlConnection con;

        public Form6(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            con.Open();

            string q = @"select FullName from Customers order by FullName";

            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }

            dr.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();

            string q = @"select 
                c.Model as 'Кола',
                l.Name as 'Локация',
                r.FromDate as 'От дата',
                r.ToDate as 'До дата'
                from Reservations r
                join Cars c on r.CarId = c.ID
                join Locations l on c.LocationId = l.ID
                join Customers cu on r.CustomerId = cu.ID
                where cu.FullName = @name";

            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@name", comboBox1.Text);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}
