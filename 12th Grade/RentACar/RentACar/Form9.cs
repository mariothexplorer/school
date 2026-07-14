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
    public partial class Form9 : Form
    {
        SqlConnection con;

        public Form9(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            string q = @"select 
                l.Name as 'Локация',
                sum(r.Amount) as 'Общ приход'
                from Reservations r
                join Cars c on r.CarId = c.ID
                join Locations l on c.LocationId = l.ID
                group by l.Name
                order by sum(r.Amount) desc";

            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();

            string q = @"select l.Name as 'Локация',
                        count(r.CarId) as 'Брой наеми'
                        from Reservations r
                        join Cars c on r.CarId = c.ID
                        join Locations l on c.LocationId = l.ID
                        group by l.Name
                        order by count(r.CarId) desc";

            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();

            string q = @"select l.Name as 'Локация',
                        count(c.ID) as 'Брой коли'
                        from Cars c
                        join Locations l on c.LocationId = l.ID
                        group by l.Name
                        order by count(c.ID) desc";

            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();

            string q = @"
                        select l.Name as 'Локация'
                        from Locations l
                        left join Cars c on l.ID = c.LocationId
                        where c.ID is null";

            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}
