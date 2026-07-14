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
    public partial class Form7 : Form
    {
        SqlConnection con;
        public Form7(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            string q = @"select 
                c.FullName as 'Клиент',
                count(r.CarId) as 'Брой наеми'
                from Reservations r
                join Customers c on r.CustomerId = c.ID
                group by c.FullName
                order by count(r.CarId) desc";

            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            con.Close();
        }
    }
}
