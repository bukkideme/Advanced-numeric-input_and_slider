namespace UserControlTesterProject
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.advancedSlider1 = new UserControlTesterProject.AdvancedSlider();
            this.advancedNumericBox1 = new UserControlTesterProject.AdvancedNumericBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 92);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(165, 20);
            this.textBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "advancedNumericBox1:";
            // 
            // advancedSlider1
            // 
            this.advancedSlider1.CoerceOutOfRange = false;
            this.advancedSlider1.LargeChange = 10;
            this.advancedSlider1.Location = new System.Drawing.Point(277, 20);
            this.advancedSlider1.Name = "advancedSlider1";
            this.advancedSlider1.NumberFormatSpecifier = "G";
            this.advancedSlider1.Size = new System.Drawing.Size(259, 92);
            this.advancedSlider1.SmallChange = 1;
            this.advancedSlider1.TabIndex = 6;
            this.advancedSlider1.ValidatingColor = System.Drawing.Color.LightBlue;
            // 
            // advancedNumericBox1
            // 
            this.advancedNumericBox1.CoerceOutOfRange = false;
            this.advancedNumericBox1.Location = new System.Drawing.Point(13, 37);
            this.advancedNumericBox1.Name = "advancedNumericBox1";
            this.advancedNumericBox1.NumberFormatSpecifier = "G";
            this.advancedNumericBox1.Size = new System.Drawing.Size(121, 20);
            this.advancedNumericBox1.TabIndex = 3;
            this.advancedNumericBox1.ValidatingColor = System.Drawing.Color.LightBlue;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 139);
            this.Controls.Add(this.advancedSlider1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.advancedNumericBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private AdvancedNumericBox advancedNumericBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private AdvancedSlider advancedSlider1;
    }
}

