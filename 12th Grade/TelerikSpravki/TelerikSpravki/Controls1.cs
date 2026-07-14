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
    public partial class Controls1 : Form
    {
        SqlConnection con;
        public Controls1(SqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void Controls1_Load(object sender, EventArgs e)
        {
            DataTable d = con.GetSchema("Tables"); //взима имената на таблиците
            //DataRow[] rows = d.Select("Table_type='base table'");
            foreach (DataRow row in d.Rows)
            {
                if (row["table_name"].ToString() != "sysdiagrams")
                    comboBox1.Items.Add(row["table_name"]);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
          
        }
        Label[] labels;
        TextBox[] textBoxes;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            ShowTable();
            int columsCnt = dataGridView1.Columns.Count - 1;
            labels = new Label[columsCnt];
            textBoxes = new TextBox[columsCnt];
            for (int i = 0; i < columsCnt; i++)
            {
                labels[i] = new Label();
                labels[i].Parent = panel1;
                labels[i].Height = 20;
                labels[i].Width = 120;
                labels[i].Left = 10;
                labels[i].Top = i * labels[i].Height + (i + 1) * 10;
                labels[i].Text = dataGridView1.Columns[i + 1].HeaderText;
                textBoxes[i] = new TextBox();
                textBoxes[i].Parent = panel1;
                textBoxes[i].Height = 20;
                textBoxes[i].Width = 120;
                textBoxes[i].Left = labels[i].Right + 10;
                textBoxes[i].Top = labels[i].Top;
            }
        }

        private void ShowTable()
        {
            string q = $"select* from {comboBox1.Text}";
            SqlCommand cmd = new SqlCommand(q, con); //пишем заявката
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable(); //създава празна таблица за БД
            dt.Load(dr); //запълва таблицата с резултата
            dataGridView1.DataSource = dt;//пълни таблицата в интерфейса
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < textBoxes.Length; i++)
            {
                if (textBoxes[i].Text == "") return;
            }
            if (comboBox1.Text == "Employees")
            {
                string q = @"insert into Employees
                            values(@1, @2, @3, @4,
                        @5, @6, @7, @8, @9)";
                SqlCommand cmd = new SqlCommand(q, con);
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    if (i == 6)
                    {
                        string s = textBoxes[i].Text;
                        string[] parts = s.Split('.');
                        DateTime d = new DateTime(int.Parse(parts[2]), int.Parse(parts[1]), int.Parse(parts[0]));
                        cmd.Parameters.AddWithValue("@7", d);
                    }
                    else { 
                    cmd.Parameters.AddWithValue($"@{i + 1}", textBoxes[i].Text);
                }
                }
                cmd.ExecuteNonQuery();
               MessageBox.Show("Успешен запис.");
               for (int i = 0; i < textBoxes.Length; i++)
               {
                    textBoxes[i].Clear();
               }
                ShowTable();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string col = dataGridView1.Columns[0].HeaderText;
            object val = dataGridView1.SelectedRows[0].Cells[0].Value;
          string q = $@"delete from {comboBox1.Text} where {col}=@val";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@val", val);
            cmd.Parameters.AddWithValue("@name", comboBox1.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Успешно изтриване.");
            ShowTable();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i].Text = dataGridView1.SelectedRows[0].Cells[i + 1].Value.ToString();
            }
        }
    }
}
