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

namespace UserControlTesterProject
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AdvancedNumericBox : UserControl
    {
        private double ActualValue;

        /// <summary>
        /// Use the required format specifier string. The default is "G": 
        /// https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#GFormatString.
        /// Use Precision specifier if required, for example scientific notation "E" has a default of 6 precision.
        /// If you need less or more, you can change this like "E3" for example (in this case the 3rd digit will be rounded).
        /// </summary>
        public string NumberFormatSpecifier { get; set; } = "G";

        /// <summary>
        /// Color used to indicate edit mode of the control. Default is Color.LightBlue.
        /// </summary>
        public Color ValidatingColor { get; set; } = Color.LightBlue;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when Enter key is released")]
        public event EventHandler EnterKeyUpCustom;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when the focus is lost")]
        public event EventHandler FocusLostCustom;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when input was not parsable as a number")]
        public event EventHandler InvalidInputOccured;

        public AdvancedNumericBox()
        {
            InitializeComponent();
            SetValue(0);
            textBox.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// Use this to programmatically set the value of the user control.
        /// </summary>
        /// <param name="val"></param>
        public void SetValue(double val)
        {
            ActualValue = val;
            textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);
            textBox.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// Get the double value the user control currently holds.
        /// </summary>
        /// <returns></returns>
        public double GetValue()
        {
            return ActualValue;
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double newVal))
                {
                    ActualValue = newVal;
                    //Invoke the Event which will bubble up in the chain, to the calling Form.
                    EnterKeyUpCustom?.Invoke(this, e);
                }
                else InvalidInputOccured?.Invoke(this, e);

                textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);
                textBox.BackColor = SystemColors.Window;
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double newVal))
            {
                ActualValue = newVal;
                //Invoke the Event which will bubble up in the chain, to the calling Form.
                FocusLostCustom?.Invoke(this, e);
            }
            else InvalidInputOccured?.Invoke(this, e);

            textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);
            textBox.BackColor = SystemColors.Window;
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            textBox.BackColor = ValidatingColor;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            textBox.BackColor = ValidatingColor;
        }
    }
}
