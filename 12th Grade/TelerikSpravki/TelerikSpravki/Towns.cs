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

namespace TelerikSpravki
{
    public partial class Towns : Form
    {
        SqlConnection con;
        public Towns(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Towns_Load(object sender, EventArgs e)
        {
            string q = "select name from Towns";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string q = @"select a.*
from Employees a
join Addresses b 
on a.AddressID = b.AddressID
join Towns c
on b.TownID = c.TownID
                        where c.Name=@m";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@m", comboBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dr.Close();
        }
    }
}
