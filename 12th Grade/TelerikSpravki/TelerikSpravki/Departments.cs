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

namespace TelerikSpravki
{
    public partial class Departments : Form
    {
        SqlConnection con;
        public Departments(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();

        }
        private void Departments_Load(object sender, EventArgs e)
        {
            string q = "select name from Departments";
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
                join Departments b
                on a.DepartmentID = b.DepartmentID
                 where b.Name = @m";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@m", comboBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dr.Close();
            string p_ = @"select c.FirstName + ' ' + c.LastName
                from Employees a
                join Departments b
                on a.DepartmentID = b.DepartmentID
                join Employees c
                on b.ManagerID = c.EmployeeID
                where b.Name = @m";
            SqlCommand cmd_ = new SqlCommand(p_, con);
            cmd_.Parameters.AddWithValue("@m", comboBox1.Text);
            label2.Text = cmd_.ExecuteScalar().ToString();
            label3.Text = (dataGridView1.RowCount - 1).ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
