namespace Goriva
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double priceD = 2.92, priceB = 2.87, priceG = 1.12, discount = 0.07;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label2.Text = priceB + " лв/л";
            }
            if (comboBox1.Text == "Газ")
            {
                label2.Text = priceG + " лв/л";

            }
            if (comboBox1.Text == "Дизел")
            {
                label2.Text = priceD + " лв/л";

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = " лева";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "литри";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label3.Text = discount + " лева";
            }
            else
            {
                label3.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double pricePerLiter = double.Parse(label2.Text.Substring(0, 4));
                if (checkBox1.Checked)
                {
                    pricePerLiter -= discount;
                }
                int t = int.Parse(textBox1.Text);
                if (!radioButton1.Checked && !radioButton2.Checked)
                {
                    throw new Exception();
                }
                if (radioButton1.Checked)
                {
                    label5.Text = Math.Round(pricePerLiter * t).ToString();
                }
                if (radioButton2.Checked)
                {
                    label5.Text = Math.Round(t / pricePerLiter).ToString();
                }
                
            }
            catch 
            {
                MessageBox.Show("Въведете всички данни!");
            }
        }
    }
}