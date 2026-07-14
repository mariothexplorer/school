using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Temp : Form
    {
        public Temp()
        {
            InitializeComponent();
        }

        private void Temp_Load(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Input y = new Input();
            this.Close();
            y.ShowDialog();
        }
    }
}
