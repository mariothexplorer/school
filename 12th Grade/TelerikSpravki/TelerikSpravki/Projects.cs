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
    public partial class Projects : Form
    {
        SqlConnection con;
        public Projects(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string q = @"select c.*
                        from Projects a
                        join EmployeesProjects b
                        on a.ProjectID=b.ProjectID
                        join Employees c
                        on b.EmployeeID=c.EmployeeID
                        where a.Name=@m";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@m", comboBox1.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dr.Close();
            label4.Text = (dataGridView1.RowCount - 1).ToString();
        }

        private void Projects_Load(object sender, EventArgs e)
        {
            string q = "select name from Projects";
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
            dataGridView1.DataSource = null;
            label4.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
