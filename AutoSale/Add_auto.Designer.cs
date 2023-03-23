namespace AutoSales
{
    partial class Add_auto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_auto));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox_marka = new System.Windows.Forms.TextBox();
            this.textBox_model = new System.Windows.Forms.TextBox();
            this.textBox_strana = new System.Windows.Forms.TextBox();
            this.textBox_year = new System.Windows.Forms.TextBox();
            this.textBox_power = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(176, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 97);
            this.panel1.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Location = new System.Drawing.Point(434, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 59);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(531, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 59);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(0, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(313, 33);
            this.label6.TabIndex = 0;
            this.label6.Text = "Добавление автомобилей";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(163, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // textBox_marka
            // 
            this.textBox_marka.Location = new System.Drawing.Point(271, 179);
            this.textBox_marka.MaxLength = 40;
            this.textBox_marka.Name = "textBox_marka";
            this.textBox_marka.ShortcutsEnabled = false;
            this.textBox_marka.Size = new System.Drawing.Size(296, 31);
            this.textBox_marka.TabIndex = 2;
            this.textBox_marka.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_marka_KeyPress);
            // 
            // textBox_model
            // 
            this.textBox_model.Location = new System.Drawing.Point(271, 216);
            this.textBox_model.MaxLength = 70;
            this.textBox_model.Name = "textBox_model";
            this.textBox_model.ShortcutsEnabled = false;
            this.textBox_model.Size = new System.Drawing.Size(296, 31);
            this.textBox_model.TabIndex = 3;
            this.textBox_model.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_model_KeyPress);
            // 
            // textBox_strana
            // 
            this.textBox_strana.Location = new System.Drawing.Point(271, 253);
            this.textBox_strana.MaxLength = 40;
            this.textBox_strana.Name = "textBox_strana";
            this.textBox_strana.ShortcutsEnabled = false;
            this.textBox_strana.Size = new System.Drawing.Size(296, 31);
            this.textBox_strana.TabIndex = 4;
            this.textBox_strana.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_strana_KeyPress);
            // 
            // textBox_year
            // 
            this.textBox_year.Location = new System.Drawing.Point(271, 290);
            this.textBox_year.MaxLength = 4;
            this.textBox_year.Name = "textBox_year";
            this.textBox_year.ShortcutsEnabled = false;
            this.textBox_year.Size = new System.Drawing.Size(296, 31);
            this.textBox_year.TabIndex = 5;
            this.textBox_year.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_year_KeyPress);
            // 
            // textBox_power
            // 
            this.textBox_power.Location = new System.Drawing.Point(271, 327);
            this.textBox_power.MaxLength = 4;
            this.textBox_power.Name = "textBox_power";
            this.textBox_power.ShortcutsEnabled = false;
            this.textBox_power.Size = new System.Drawing.Size(296, 31);
            this.textBox_power.TabIndex = 6;
            this.textBox_power.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_power_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(176, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "Марка:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(166, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "Модель:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(41, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 22);
            this.label3.TabIndex = 9;
            this.label3.Text = "Страна-производитель:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(128, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 22);
            this.label4.TabIndex = 10;
            this.label4.Text = "Год выпуска:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(107, 330);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 22);
            this.label5.TabIndex = 11;
            this.label5.Text = "Мощность (л.с):";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(321, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(191, 47);
            this.button1.TabIndex = 12;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Add_auto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_power);
            this.Controls.Add(this.textBox_year);
            this.Controls.Add(this.textBox_strana);
            this.Controls.Add(this.textBox_model);
            this.Controls.Add(this.textBox_marka);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Add_auto";
            this.Text = "Add_auto";
            this.Load += new System.EventHandler(this.Add_auto_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private TextBox textBox_marka;
        private TextBox textBox_model;
        private TextBox textBox_strana;
        private TextBox textBox_year;
        private TextBox textBox_power;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
        private Button button3;
        private Button button2;
    }
}