namespace UserControlTesterProject
{
    partial class AdvancedSlider
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.parameterLabel = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBar.LargeChange = 10;
            this.trackBar.Location = new System.Drawing.Point(0, 47);
            this.trackBar.Maximum = 100;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(121, 45);
            this.trackBar.TabIndex = 0;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            this.trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseUp);
            // 
            // parameterLabel
            // 
            this.parameterLabel.AutoSize = true;
            this.parameterLabel.Location = new System.Drawing.Point(9, 2);
            this.parameterLabel.Name = "parameterLabel";
            this.parameterLabel.Size = new System.Drawing.Size(82, 13);
            this.parameterLabel.TabIndex = 1;
            this.parameterLabel.Text = "parameterName";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(11, 18);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(89, 20);
            this.textBox.TabIndex = 2;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyUp);
            this.textBox.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // AdvancedSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.parameterLabel);
            this.Controls.Add(this.trackBar);
            this.Name = "AdvancedSlider";
            this.Size = new System.Drawing.Size(121, 92);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Label parameterLabel;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
