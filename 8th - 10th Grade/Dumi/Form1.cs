using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dumi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] right = { "пицария", "техникум", "теоретично", "мерудия", "асфалт", "бенка", "сграда", 
            "аргумент", "лавина", "пухкав", "отвертка", "възпирам", "безпокоя", "розов", "случай",
            "фина", "калдъръм", "флумастер", "папиьонка", "удвоен"};

        string[] wrong = { "еденица", "анцук", "акомулатор", "буболечка", "не винаги", "предтекст",
            "незнам", "абитюриент", "валчер", "падпадък", "безпорно", "подтискам", "полюлей",
            "съжелявам", "особенно", "кьополу", "опастност", "пластелин", "оредели", "кафеви"};

        int level;
        Random r = new Random();
        int x;
        int number;
        int points;
        int _rec_;
        string _SA_rec_;

        private void button1_Click(object sender, EventArgs e)
        {

            level = comboBox1.SelectedIndex + 5;
            Shuffle();
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            label2.Text = points.ToString();
            comboBox1.Enabled = false;
            x = r.Next(0, 2);
            if (x == 0)
            {
                label1.Text = right[0];
            }
            if (x == 1)
            {
                label1.Text = wrong[0];
            }
        }
        private void Shuffle()
        {
            int n1 = right.Length;
            int n2 = wrong.Length;

            for (int i = 0; i < n1; i++)
            {
                int x = r.Next(n1);
                int y = r.Next(n1);
                string temp = right[x];
                right[x] = right[y];
                right[y] = temp.ToString();
            }

            for (int i = 0; i < n2; i++)
            {
                int x = r.Next(n2);
                int y = r.Next(n2);
                string temp = wrong[x];
                wrong[x] = wrong[y];
                wrong[y] = temp.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            number++;            
            if (x == 0)
            {
                points++;
                button1.Enabled = true;
                button1.PerformClick();
                button1.Enabled = false;
                label2.Text = points.ToString();
                               
            }
            else
            {
                points--;
                button1.Enabled = true;
                button1.PerformClick();
                button1.Enabled = false;
                label2.Text = points.ToString();
            }
            zapis_na_rec();
            End(); 
        }

        private void End()
        {
            if (number == level && points > 0 &&  _rec_>= points)
            {
                label1.Text = "";
                label2.Text = points.ToString();
                MessageBox.Show("Браво! Подобри рекорда на: " + _rec_ + ".","Край на играта");
                MultyGame();
            }
            if (number == level && points > 0)
            {
                label1.Text = "";
                label2.Text = points.ToString();
                MessageBox.Show("Играта за теб приключи и твоите точки са: " + points + " ! Oпитай отново да подобриш рекорда.", "Край на играта");
                MultyGame();
            }        
            if (number == level && points <= 0)
            {
                label1.Text = "";
                label2.Text = points.ToString();
                MessageBox.Show("Изгуби! Oпитай отново да подобриш рекорда!  ", "Край на играта");
                MultyGame();
            }
        }

        private void zapis_na_rec()
        {            
            if (points > _rec_)
            {
                _rec_ = points;
                label3.Text = _rec_.ToString();
                Application.UserAppDataRegistry.SetValue(_SA_rec_, label3.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            number++;
            if (x == 1)
            {
                points++;
                label2.Text = points.ToString();
                button1.Enabled = true;
                button1.PerformClick();
                button1.Enabled = false;
            }
            else
            {
                points--;
                label2.Text = points.ToString();
                button1.Enabled = true;
                button1.PerformClick();
                button1.Enabled = false;
            }
            zapis_na_rec();
            End();
        }
        private void MultyGame()
        {
            comboBox1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            number = 0;
            points = 0;
            label2.Text = "";
            label1.Text = "";
            Shuffle();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            points = 0;
            label2.Text = points.ToString();
            comboBox1.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //za rec
            label3.Text= (string)Application.UserAppDataRegistry.GetValue(_SA_rec_, "0");
            _rec_=int.Parse(label3.Text);

            comboBox1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Hide();
            Application.UserAppDataRegistry.SetValue(_SA_rec_, "0");
            label3.Text = "0";
            _rec_ = 0;
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            button4.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}