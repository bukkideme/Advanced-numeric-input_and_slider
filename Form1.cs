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
            advancedNumericBox1.InvalidInput += AdvancedNumericBox1_InvalidInputOccured;
            advancedNumericBox1.OutOfRange += AdvancedNumericBox1_OutOfRange;

            advancedNumericBox1.SetMinimum(-100);
            advancedNumericBox1.SetMaximum(500);
            advancedNumericBox1.EnableToolTip($"Min: -100\r\nMax: 500\r\nOut of range will be coerced.");
            advancedNumericBox1.SetValue(100);
            advancedNumericBox1.CoerceOutOfRange = true;

            advancedSlider1.EnterKeyUpCustom += AdvancedNumericBox1_EnterKeyUpCustom;
            advancedSlider1.FocusLostCustom += AdvancedNumericBox1_FocusLostCustom;
            advancedSlider1.InvalidInput += AdvancedNumericBox1_InvalidInputOccured;
            advancedSlider1.OutOfRange += AdvancedNumericBox1_OutOfRange;

            advancedSlider1.SetLabel("parameter:");
            advancedSlider1.SetMinimum(-100);
            advancedSlider1.SetMaximum(500);
            advancedSlider1.EnableToolTip($"Min: -100\r\nMax: 500\r\nOut of range will be coerced.");
            advancedSlider1.SetValue(100);
            advancedSlider1.CoerceOutOfRange = true;
        }

        private void AdvancedNumericBox1_OutOfRange(object sender, EventArgs e)
        {
            MessageBox.Show("Invalid input, out of range!");
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
