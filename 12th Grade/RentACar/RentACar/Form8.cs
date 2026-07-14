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
    public partial class Form8 : Form
    {
        SqlConnection con;
        public Form8(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();

            string q = @"select
                        c.Model as 'Кола',
                        count(r.CarId) as 'Брой наеми'
                        from Cars c
                        left join Reservations r on c.ID = r.CarId
                        group by c.Model
                        order by count(r.CarId) asc";

            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}
