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
using System.Data.SqlClient;

namespace TelerikSpravki
{
    
    public partial class Employees : Form
    {
        SqlConnection con;
        public Employees(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                if (textBox1.Text == "") return;
                if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked) return;
                if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked) return;
                if (checkBox1.Checked && checkBox3.Checked) return;
                string sign = "";
                if (checkBox1.Checked) sign += checkBox1.Text;
                if (checkBox2.Checked) sign += checkBox2.Text;
                if (checkBox3.Checked) sign += checkBox3.Text;
                decimal sum = decimal.Parse(textBox1.Text);
                string q = $"select * from Employees where salary {sign} {sum}";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                dr.Close();
                label4.Text = (dataGridView1.RowCount - 1).ToString();
            }
            else
            {
                string q = @"select a.*
                            from Employees a
                            join Employees b
                            on a.managerID=b.EmployeeID
                            where b.FirstName+' '+b.LastName= @m";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@m", comboBox1.Text); //създаване на променлива
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                dr.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            label4.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            string q = @"select b.FirstName + ' ' + b.LastName
                        from Employees a 
                         join Employees b
                    on a.ManagerID = b.EmployeeID
                    group by b.FirstName + ' ' + b.LastName";
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
            textBox1.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox2.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime d=dateTimePicker1.Value.Date;
            string q = $@"select *
                         from Employees
                         where HireDate<@d";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@d", d);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dr.Close();
            label4.Text = (dataGridView1.RowCount - 1).ToString();
        }
    }
}
