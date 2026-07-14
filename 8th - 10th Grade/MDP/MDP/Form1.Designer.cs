namespace MDP
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
            label2 = new Label();
            comboBox1 = new ComboBox();
            label3 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(261, 21);
            label1.TabIndex = 0;
            label1.Text = "Пътуване от София до Мадрид";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(255, 224, 192);
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(200, 65);
            label2.Name = "label2";
            label2.Size = new Size(55, 23);
            label2.TabIndex = 1;
            label2.Text = "label2";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Влак", "Самолет", "Автобус" });
            comboBox1.Location = new Point(12, 59);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(172, 29);
            comboBox1.TabIndex = 5;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(12, 115);
            label3.Name = "label3";
            label3.Size = new Size(106, 21);
            label3.TabIndex = 6;
            label3.Text = "Колко пъти:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(166, 118);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(158, 29);
            textBox1.TabIndex = 7;
            // 
            // button1
            // 
            button1.Location = new Point(89, 216);
            button1.Name = "button1";
            button1.Size = new Size(105, 45);
            button1.TabIndex = 8;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(117, 335);
            label4.Name = "label4";
            label4.Size = new Size(53, 21);
            label4.TabIndex = 9;
            label4.Text = "label4";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(416, 417);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(comboBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = SystemColors.ControlDarkDark;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Автор: Марио Петров";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox comboBox1;
        private Label label3;
        private TextBox textBox1;
        private Button button1;
        private Label label4;
    }
}