using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UserControlTesterProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //advancedNumericBox1.NumberFormatSpecifier = "E3";
            advancedNumericBox1.EnterKeyUpCustom += AdvancedNumericBox1_EnterKeyUpCustom;
            advancedNumericBox1.FocusLostCustom += AdvancedNumericBox1_FocusLostCustom;
            advancedNumericBox1.InvalidInputOccured += AdvancedNumericBox1_InvalidInputOccured;


        }

        private void AdvancedNumericBox1_InvalidInputOccured(object sender, EventArgs e)
        {
            MessageBox.Show("Invalid input, not a number! Reverted to original value!");
        }

        private void AdvancedNumericBox1_FocusLostCustom(object sender, EventArgs e)
        {
            textBox1.Text = $"{advancedNumericBox1.GetValue()}";
        }

        private void AdvancedNumericBox1_EnterKeyUpCustom(object sender, EventArgs e)
        {
            textBox1.Text = $"{advancedNumericBox1.GetValue()}";
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            advancedNumericBox1.SetValue(0.000034567);
        }
    }
}
