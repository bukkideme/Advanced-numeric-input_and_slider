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
    public partial class AdvancedNumericBox : UserControl
    {
        private double ActualValue;

        /// <summary>
        /// Use the required format specifier string. The default is "G": 
        /// https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#GFormatString.
        /// Use Precision specifier if required, for example scientific notation "E" has a default of 6 precision.
        /// If you need less or more, you can change this like "E3" for example (in this case the 3rd digit will be rounded).
        /// </summary>
        [Description("Use the required format specifier string.")]
        public string NumberFormatSpecifier { get; set; } = "G";
        public double MinimumValue { get; private set; } = double.MinValue;
        public double MaximumValue { get; private set; } = double.MaxValue;
        /// <summary>
        /// If false, out of range values will be ignored. If true, value will be coerced to actual min or max limit.
        /// Default is false.
        /// </summary>
        [Description("If false, out of range values will be ignored. If true, value will be coerced to actual MinimumValue or MaximumValue limit.")]
        public bool CoerceOutOfRange { get; set; } = false;

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
        public event EventHandler InvalidInput;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when input is out of range")]
        public event EventHandler OutOfRange;

        public AdvancedNumericBox()
        {
            InitializeComponent();
            SetValue(0);
            textBox.BackColor = SystemColors.Window;                       
        }

        /// <summary>
        /// Sets the minimum limit of the accepted input range.
        /// </summary>
        /// <param name="minLimit">Requested new minimum limit.</param>
        /// <returns>Returns false if actual value is below the new requested minimum limit, and ignores the new limit value.</returns>
        public bool SetMinimum(double minLimit)
        {
            if (ActualValue < minLimit) return false;            
            MinimumValue = minLimit;
            return true;
        }

        /// <summary>
        /// Sets the maximum limit of the accepted input range.
        /// </summary>
        /// <param name="maxLimit">Requested new minimum limit.</param>
        /// <returns>Returns false if actual value is above the requested new maximum limit, and ignores the new limit value.</returns>
        public bool SetMaximum(double maxLimit)
        {
            if (ActualValue > maxLimit) return false;
            MaximumValue = maxLimit;
            return true;
        }

        /// <summary>
        /// Use this to programmatically set the value of the user control.
        /// </summary>
        /// <param name="newVal"></param>
        public void SetValue(double newVal)
        {            
            if (newVal > MaximumValue)
            {
                if (CoerceOutOfRange) ActualValue = MaximumValue;
                OutOfRange?.Invoke(this, null);                
            }
            else if (newVal < MinimumValue)
            {
                if (CoerceOutOfRange) ActualValue = MinimumValue;
                OutOfRange?.Invoke(this, null);               
            }
            else ActualValue = newVal;

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

        public void EnableToolTip(string text)
        {
            toolTip1.SetToolTip(textBox, text);
        }

        public void DisableToolTip()
        {
            toolTip1.RemoveAll();
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double newVal))
                {
                    if (newVal > MaximumValue)
                    {
                        if (CoerceOutOfRange) ActualValue = MaximumValue;
                        OutOfRange?.Invoke(this, null);
                    }
                    else if (newVal < MinimumValue)
                    {
                        if (CoerceOutOfRange) ActualValue = MinimumValue;
                        OutOfRange?.Invoke(this, null);
                    }
                    else ActualValue = newVal;  
                }
                else InvalidInput?.Invoke(this, e);

                EnterKeyUpCustom?.Invoke(this, e);
                textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);
                textBox.BackColor = SystemColors.Window;
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double newVal))
            {
                if (newVal > MaximumValue)
                {
                    if (CoerceOutOfRange) ActualValue = MaximumValue;
                    OutOfRange?.Invoke(this, null);
                }
                else if (newVal < MinimumValue)
                {
                    if (CoerceOutOfRange) ActualValue = MinimumValue;
                    OutOfRange?.Invoke(this, null);
                }
                else ActualValue = newVal;                
            }
            else InvalidInput?.Invoke(this, e);

            FocusLostCustom?.Invoke(this, e);
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
