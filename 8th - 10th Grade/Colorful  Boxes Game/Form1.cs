using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colorful__Boxes_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        int points;
        int _rec_;
        string _SA_rec_;

        private void button1_Click(object sender, EventArgs e)
        {
            /*
             * СКРИВАНЕ НА БИТОНА НАЧАЛО;
             * СТРАТИРАНЕ НА ТАЙМЕР;
             * new_box();
             * Точки = 0;
             */
            points = 0;
            button1.Visible = false;
            label1.Visible = true; 
            timer1.Start();
            points = 0;
            new_box();
            label3.Text = points.ToString();
            label3.Visible = true;
            timer1.Interval = 500;

        }

        private void new_box()
        {
            /*
             * ТОП = 0; 
             * НОВ ЦВЯТ;
             * АКО ИНТЕРВАЛЪТ Е ПО-ГОЛЯМ ОТ ДЕДЕНА СТОЙНОСТ => ПРОМЕНЯМЕ ИНТЕРВАЛА
             * 
             */

            if (timer1.Interval > 100 )
            {
                timer1.Interval = timer1.Interval - 100;
            }
            else
            {
                timer1.Interval += 0; 
            }
            label1.Top = 0;
            Random r = new Random();
            int a = r.Next(0, 3);
            if (a == 0)
            {
                label1.BackColor = Color.Blue;
            }
            if (a == 1)
            {
                label1.BackColor = Color.Red;
            }
            if (a == 2)
            {
                label1.BackColor = Color.Black;
            }
            Save_rec();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            /*
             * ПРЕМЕСТВАНЕ НА КУТИЯТА (label1) НАДОЛУ С ОПРЕДЕЛЕНО РАЗСТОЯНИЕ (label1.Top);
             * ПРОВЕРКА ДАЛИ КУТИЯТА Е СТИГНАЛА ДЪНОТО: 
             *          1. АКО НЕ Е ЧЕРНА --- end_game();
             *          2. AKO Е ЧЕРНА --- new_box();
             * 
             */

            label1.Top = label1.Top + 20;
            if (label1.Top >= panel1.Height)
            {
                if (label1.BackColor == Color.Black)
                {
                    new_box();
                }
                else
                {
                    end_Game();
                }

            }
        }



        private void end_Game()
        {
            /*
             * СПИРАНЕ НА ТАЙМЕРА;
             * СЪОБЩЕНИИЕ;
             * ПОКАЗВАНЕ НА БУТОНА ЗА НАЧАЛО;
             * 
             */

            label1.Hide();
            timer1.Stop();
            MessageBox.Show("Играта за теб приключи!","Край");
            button1.Visible = true;


        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label3.Visible = false;

            //ЗА РЕК
            label4.Text = (string)Application.UserAppDataRegistry.GetValue(_SA_rec_, "0");
            _rec_ = int.Parse(label4.Text);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            /*
             * АКО СМЕ НАПИСНАЛИ ЛЯВА СТРЕЛКА (e.Keychar == keys.Left;)
             *          AKO КУТИЯТА Е В СИН ЦВАТ
             *               => Увеличаваме точките;
             *               => New_Box();
             *          ELSE  
             *          {End_Game();}     
             *          
             * АКО СМЕ НАПИСНАЛИ ДЯСНА СТРЕЛКА (e.Keychar == keys.Right;)
             *          AKO КУТИЯТА Е В ЧЕРВЕНА ЦВАТ
             *               => Увеличаваме точките;
             *               => New_Box();
             *          ELSE  {End_Game();}         
             * АКО СМЕ НАТИСНАЛИ СТРЕЛКА НАДОЛУ И ЦВЕТЪТ Е ЧЕРЕН => NEW_BOX();
             */


            if (button1.Visible == true || label1.Visible == false)
            {
                this.KeyPreview = false;
                return;
            }
            if (e.KeyCode == Keys.Left)
            {

                if (label1.BackColor == Color.Blue)
                {
                    points++;
                    new_box();
                }
                else
                {
                    end_Game();
                }
            }
            if (Keys.Right.Equals(e.KeyCode) == true)
            {

                if (label1.BackColor == Color.Red)
                {
                    points++;
                    new_box();
                }
                else
                {
                    end_Game();
                }
            }
            if (Keys.Down.Equals(e.KeyCode) == true && label1.BackColor == Color.Black)
            {
                new_box();

            }
            Save_rec();
            label3.Text = points.ToString();
            label3.Visible=true;

        }

        private void Save_rec()
        {
            if (points > _rec_)
            {
                _rec_ = points;
                label4.Text = _rec_.ToString();
                Application.UserAppDataRegistry.SetValue(_SA_rec_, label4.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Hide();
            Application.UserAppDataRegistry.SetValue(_SA_rec_, "0");
            label4.Text = "0";
            _rec_ = 0;
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            button4.Visible = true;
            button4.Enabled = true;
        }
    }

}
