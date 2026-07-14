using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_program
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="1")
            {
                this.Close();
                Schedule s = new Schedule();    
                s.ShowDialog(); 
            }
            else 
            {
                MessageBox.Show("Въведи правилна парола!","Wrong password");
                textBox1.Focus();
                textBox1.SelectAll();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Focus();    
        }
    }
}
