namespace MDP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        double priceT = 240.46, priceB = 321.20, priceA = 625.25;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Автобус")
            {
                label2.Text = priceB + " лева";
            }
            if (comboBox1.Text == "Самолет")
            {
                label2.Text = priceA + " лева";

            }
            if (comboBox1.Text == "Влак")
            {
                label2.Text = priceT + " лева";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                double price = double.Parse(label2.Text.Substring(0, 5));
                int t = int.Parse(textBox1.Text);
                label4.Text = ToString(t * price);



            //1 - 64
            // 2 - Program.cs
            // 3- exe
            // 4- Да превежда от човешки на машинен език;

        }
    }
}