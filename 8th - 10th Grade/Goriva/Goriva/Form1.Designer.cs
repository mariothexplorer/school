namespace Goriva
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            textBox1 = new TextBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            panel1 = new Panel();
            label3 = new Label();
            checkBox1 = new CheckBox();
            button1 = new Button();
            label4 = new Label();
            label5 = new Label();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 11);
            label1.Name = "label1";
            label1.Size = new Size(108, 21);
            label1.TabIndex = 0;
            label1.Text = "Вид гориво:";
            label1.UseWaitCursor = true;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Бензин", "Газ", "Дизел" });
            comboBox1.Location = new Point(25, 35);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(108, 29);
            comboBox1.Sorted = true;
            comboBox1.TabIndex = 1;
            comboBox1.UseWaitCursor = true;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(192, 192, 255);
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.Location = new Point(139, 37);
            label2.Name = "label2";
            label2.Size = new Size(101, 27);
            label2.TabIndex = 2;
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.UseWaitCursor = true;
            label2.Click += label2_Click;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(192, 255, 255);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(25, 74);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(215, 92);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Мерна единица";
            groupBox1.UseWaitCursor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(112, 40);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(87, 29);
            textBox1.TabIndex = 2;
            textBox1.UseWaitCursor = true;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(6, 59);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(85, 25);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "Левове";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.UseWaitCursor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(6, 28);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(79, 25);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "Литри";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.UseWaitCursor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(255, 192, 255);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(checkBox1);
            panel1.Location = new Point(23, 184);
            panel1.Name = "panel1";
            panel1.Size = new Size(215, 100);
            panel1.TabIndex = 4;
            panel1.UseWaitCursor = true;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(192, 192, 255);
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Location = new Point(100, 31);
            label3.Name = "label3";
            label3.Size = new Size(101, 27);
            label3.TabIndex = 3;
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.UseWaitCursor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(8, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(105, 25);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Отстъпка";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.UseWaitCursor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(23, 290);
            button1.Name = "button1";
            button1.Size = new Size(217, 63);
            button1.TabIndex = 5;
            button1.Text = "И З Ч  И С Л И";
            button1.UseVisualStyleBackColor = true;
            button1.UseWaitCursor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(177, 370);
            label4.Name = "label4";
            label4.Size = new Size(0, 21);
            label4.TabIndex = 6;
            label4.UseWaitCursor = true;
            // 
            // label5
            // 
            label5.BackColor = Color.FromArgb(192, 192, 255);
            label5.BorderStyle = BorderStyle.Fixed3D;
            label5.Location = new Point(25, 367);
            label5.Name = "label5";
            label5.Size = new Size(101, 27);
            label5.TabIndex = 7;
            label5.TextAlign = ContentAlignment.MiddleCenter;
            label5.UseWaitCursor = true;
            label5.Click += label5_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 192);
            ClientSize = new Size(266, 448);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Автор: Марио Петров";
            UseWaitCursor = true;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private TextBox textBox1;
        private Panel panel1;
        private Label label3;
        private CheckBox checkBox1;
        private Button button1;
        private Label label4;
        private Label label5;
    }
}