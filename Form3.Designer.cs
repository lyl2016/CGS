namespace ThirdGroup
{
    partial class Form3
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
			this.picBox = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.radioButton_16 = new System.Windows.Forms.RadioButton();
			this.radioButton_24 = new System.Windows.Forms.RadioButton();
			this.radioButton_32 = new System.Windows.Forms.RadioButton();
			this.radioButton_48 = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
			this.SuspendLayout();
			// 
			// picBox
			// 
			this.picBox.Location = new System.Drawing.Point(315, 60);
			this.picBox.Name = "picBox";
			this.picBox.Size = new System.Drawing.Size(372, 372);
			this.picBox.TabIndex = 0;
			this.picBox.TabStop = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(33, 413);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(90, 35);
			this.button1.TabIndex = 1;
			this.button1.Text = "形成";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "地",
            "理",
            "信",
            "息",
            "科",
            "学",
            "一",
            "班"});
			this.comboBox1.Location = new System.Drawing.Point(66, 64);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 23);
			this.comboBox1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(48, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(169, 20);
			this.label1.TabIndex = 4;
			this.label1.Text = "请输入或选择文字";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(58, 285);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(129, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "请选择分辨率";
			// 
			// radioButton_16
			// 
			this.radioButton_16.AutoSize = true;
			this.radioButton_16.Location = new System.Drawing.Point(56, 321);
			this.radioButton_16.Name = "radioButton_16";
			this.radioButton_16.Size = new System.Drawing.Size(68, 19);
			this.radioButton_16.TabIndex = 6;
			this.radioButton_16.TabStop = true;
			this.radioButton_16.Text = "16*16";
			this.radioButton_16.UseVisualStyleBackColor = true;
			// 
			// radioButton_24
			// 
			this.radioButton_24.AutoSize = true;
			this.radioButton_24.Location = new System.Drawing.Point(56, 365);
			this.radioButton_24.Name = "radioButton_24";
			this.radioButton_24.Size = new System.Drawing.Size(68, 19);
			this.radioButton_24.TabIndex = 7;
			this.radioButton_24.TabStop = true;
			this.radioButton_24.Text = "24*24";
			this.radioButton_24.UseVisualStyleBackColor = true;
			// 
			// radioButton_32
			// 
			this.radioButton_32.AutoSize = true;
			this.radioButton_32.Location = new System.Drawing.Point(149, 321);
			this.radioButton_32.Name = "radioButton_32";
			this.radioButton_32.Size = new System.Drawing.Size(68, 19);
			this.radioButton_32.TabIndex = 8;
			this.radioButton_32.TabStop = true;
			this.radioButton_32.Text = "32*32";
			this.radioButton_32.UseVisualStyleBackColor = true;
			// 
			// radioButton_48
			// 
			this.radioButton_48.AutoSize = true;
			this.radioButton_48.Location = new System.Drawing.Point(149, 365);
			this.radioButton_48.Name = "radioButton_48";
			this.radioButton_48.Size = new System.Drawing.Size(68, 19);
			this.radioButton_48.TabIndex = 9;
			this.radioButton_48.TabStop = true;
			this.radioButton_48.Text = "48*48";
			this.radioButton_48.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(54, 113);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 20);
			this.label3.TabIndex = 10;
			this.label3.Text = "编码：";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(65, 147);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(201, 120);
			this.textBox1.TabIndex = 11;
			// 
			// button3
			// 
			this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button3.Location = new System.Drawing.Point(202, 413);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(74, 35);
			this.button3.TabIndex = 12;
			this.button3.Text = "退出";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// Form3
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(715, 494);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.radioButton_48);
			this.Controls.Add(this.radioButton_32);
			this.Controls.Add(this.radioButton_24);
			this.Controls.Add(this.radioButton_16);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.picBox);
			this.Name = "Form3";
			this.Text = "Form3";
			((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton_16;
        private System.Windows.Forms.RadioButton radioButton_24;
        private System.Windows.Forms.RadioButton radioButton_32;
        private System.Windows.Forms.RadioButton radioButton_48;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
    }
}