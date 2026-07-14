using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TelerikSpravki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Server=.\\sqlexpress;DataBase=TelerikAcademy12g; Integrated Security=true"); 
        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
            DataTable d = con.GetSchema("Tables"); //взима имената на таблиците
            //DataRow[] rows = d.Select("Table_type='base table'");
            foreach (DataRow row in d.Rows)
            {
                if (row["table_name"].ToString() != "sysdiagrams") 
                comboBox1.Items.Add(row["table_name"]);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string q = $"select* from {comboBox1.Text}";
            SqlCommand cmd=new SqlCommand(q,con); //пишем заявката
            SqlDataReader dr=cmd.ExecuteReader();
            DataTable dt=new DataTable(); //създава празна таблица за БД
            dt.Load(dr); //запълва таблицата с резултата
            dataGridView1.DataSource = dt;//пълни таблицата в интерфейса
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employees em=new Employees(con);
            em.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Projects p = new Projects(con);
            p.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Towns p = new Towns(con);
            p.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Addresses p = new Addresses(con);
            p.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Departments p = new Departments(con);
            p.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Show();
            textBox1.Focus();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "123")
            {
                textBox1.Text = "";
                textBox1.Hide();
                Controls1 c1 = new Controls1(con);
                c1.ShowDialog();
            }
        }
    }
}
