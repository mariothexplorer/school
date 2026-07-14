using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TelerikSpravki
{
    public partial class Addresses : Form
    {
        SqlConnection con;
        public Addresses(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Addresses_Load(object sender, EventArgs e)
        {
            string q = "select AddressText from Addresses";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string q = @"select a.*
                from Employees a
                join Addresses b 
                on a.AddressID = b.AddressID
                join Towns c
                on b.TownID = c.TownID
                where b.AddressText=@m";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@m", comboBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dr.Close();
            string p_ = @"select c.Name
                from Employees a
                join Addresses b 
                on a.AddressID = b.AddressID
                join Towns c
                on b.TownID = c.TownID
                where b.AddressText=@m";
            SqlCommand cmd_ = new SqlCommand(p_, con);
            cmd_.Parameters.AddWithValue("@m", comboBox1.Text);
            label2.Text = cmd_.ExecuteScalar().ToString();





        }
    }
}
