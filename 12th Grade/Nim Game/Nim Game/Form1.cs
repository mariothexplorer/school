using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Nim_Game
{
    public partial class Form1 : Form
    {
        private List<TextBox> pileTextBoxes = new List<TextBox>();
        private List<Panel> pilePanels = new List<Panel>();
        private List<int> piles = new List<int>();
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            groupBox2.Visible = false;
        }

        private void DrawPiles()
        {
            for (int i = 0; i < pilePanels.Count; i++)
            {
                Panel panel = pilePanels[i];
                panel.Controls.Clear();

                if (i >= piles.Count) continue;

                int count = piles[i];
                int size = 12;
                int margin = 5;
                int perRow = panel.Width / (size + margin);

                for (int j = 0; j < count; j++)
                {
                    Panel dot = new Panel();
                    dot.Width = size;
                    dot.Height = size;
                    dot.BackColor = Color.Black;

                    int row = j / perRow;
                    int col = j % perRow;

                    dot.Left = col * (size + margin);
                    dot.Top = row * (size + margin);

                    panel.Controls.Add(dot);
                }
            }
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            piles.Clear();

            for (int i = 0; i < pileTextBoxes.Count; i++)
            {
                int value;
                if (!int.TryParse(pileTextBoxes[i].Text, out value) || value < 1 || value > 12)
                {
                    MessageBox.Show($"Моля, въведете валиден брой предмети за купчина {i + 1} (≥1 и <12).");
                    return;
                }

                piles.Add(value);
            }

            comboBox2.Items.Clear();

            for (int i = 0; i < piles.Count; i++)
            {
                comboBox2.Items.Add($"Купчина {i + 1}");
            }

            groupBox2.Visible = true;
            textBox1.Text = "";

            Button btn = sender as Button;
            if (btn != null) btn.Visible = false;

            foreach (var tb in pileTextBoxes)
            {
                tb.Enabled = false;
            }

            DrawPiles();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!TryProcessPlayerMove())
            {
                if (piles.Sum() == 0)
                {
                    MessageBox.Show("Играта е приключила. Натиснете 'Започни игра', за да започнете нова.");
                }
                else if (comboBox2.SelectedIndex < 0)
                {
                    MessageBox.Show("Моля, изберете купчина за ход.");
                }
                else if (string.IsNullOrWhiteSpace(textBox1.Text) || !int.TryParse(textBox1.Text, out _))
                {
                    MessageBox.Show("Моля, въведете число за броя на предметите за вземане.");
                }
            }
        }

        private bool TryProcessPlayerMove()
        {
            if (piles.Sum() == 0)
            {
                ResetGame();
                return false;
            }

            if (comboBox2.SelectedIndex < 0)
            {
                return false;
            }

            int pileIndex = comboBox2.SelectedIndex;

            int moveCount;
            if (!int.TryParse(textBox1.Text, out moveCount))
            {
                return false;
            }

            if (moveCount < 1 || moveCount > piles[pileIndex])
            {
                MessageBox.Show($"Невалиден брой предмети. В купчина {pileIndex + 1} има {piles[pileIndex]} предмета.");
                return false;
            }

            piles[pileIndex] -= moveCount;
            pileTextBoxes[pileIndex].Text = piles[pileIndex].ToString();

            DrawPiles();

            comboBox2.SelectedIndex = -1;
            textBox1.Text = "";

            if (piles.Sum() == 0)
            {
                MessageBox.Show("Поздравления! Вие спечелихте!");
                ResetGame();
                return true;
            }

            ComputerMove();
            return true;
        }

        private void ComputerMove()
        {
            int nimSum = 0;
            foreach (int pile in piles)
            {
                nimSum ^= pile;
            }

            if (nimSum == 0)
            {
                // няма печеливш ход – взима случайно
                int index = piles.FindIndex(x => x > 0);
                int take = random.Next(1, piles[index] + 1);

                piles[index] -= take;
            }
            else
            {
                for (int i = 0; i < piles.Count; i++)
                {
                    int target = piles[i] ^ nimSum;

                    if (target < piles[i])
                    {
                        int take = piles[i] - target;
                        piles[i] = target;

                        pileTextBoxes[i].Text = piles[i].ToString();
                        MessageBox.Show($"Компютърът взема {take} предмета от купчина {i + 1}.");
                        break;
                    }
                }
            }

            DrawPiles();

            if (piles.Sum() == 0)
            {
                MessageBox.Show("Компютърът спечели! Опитайте пак.");
                ResetGame();
            }
        }

        private void ResetGame()
        {
            groupBox1.Controls.Clear();
            pileTextBoxes.Clear();
            pilePanels.Clear();
            piles.Clear();

            comboBox2.Items.Clear();
            comboBox2.SelectedIndex = -1;

            textBox1.Text = "";

            groupBox2.Visible = false;

            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = -1;
            groupBox1.Visible = false;

        }
        TextBox tb;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            pileTextBoxes.Clear();
            pilePanels.Clear();
            groupBox1.Visible = true;
            piles.Clear();

            int numPiles;

            if (comboBox1.SelectedIndex >= 0)
            {
                numPiles = int.Parse(comboBox1.Text);
            }
            else
            {
                return;
            }

            comboBox1.Enabled = false;

            for (int i = 0; i < numPiles; i++)
            {
                Label lbl = new Label();
                lbl.Text = $"Купчина {i + 1}:";
                lbl.Location = new Point(10, 25 + i * 80);
                lbl.AutoSize = true;

                tb = new TextBox();
                tb.Location = new Point(110, 20 + i * 80);
                tb.Width = 50;
                tb.KeyPress += textBox1_KeyPress;
                Panel panel = new Panel();
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Width = 200;
                panel.Height = 60;
                panel.Location = new Point(180, 15 + i * 80);

                groupBox1.Controls.Add(lbl);
                groupBox1.Controls.Add(tb);
                groupBox1.Controls.Add(panel);

                pileTextBoxes.Add(tb);
                pilePanels.Add(panel);
            }

            Button buttonConfirm = new Button();
            buttonConfirm.Text = "З A П О Ч Н И";
            buttonConfirm.Location = new Point(10, 30 + numPiles * 80);
            buttonConfirm.AutoSize = true;
            buttonConfirm.Click += ButtonConfirm_Click;

            groupBox1.Controls.Add(buttonConfirm);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Максимум 2 цифри
            if (tb.Text.Length >= 2)
            {
                e.Handled = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        

    }
}