using Microsoft.SqlServer.Server;
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
using System.Xml.Linq;

namespace UserControlTesterProject
{
    public partial class AdvancedSlider : UserControl
    {
        private double ActualValue;
        private int SliderResolution = 10_000;
        private double MinimumValue;
        private double MaximumValue;
        
        /// <summary>
        /// Use the required format specifier string. The default is "G": 
        /// https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#GFormatString.
        /// Use Precision specifier if required, for example scientific notation "E" has a default of 6 precision.
        /// If you need less or more, you can change this like "E3" for example (in this case the 3rd digit will be rounded).
        /// </summary>
        public string NumberFormatSpecifier { get; set; } = "G";

        public int SmallChange
        {
            get => trackBar.SmallChange;
            set => trackBar.SmallChange = value;
        }

        public int LargeChange
        {
            get => trackBar.LargeChange;
            set => trackBar.LargeChange = value;
        }
                

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

        public AdvancedSlider()
        {
            InitializeComponent();            
            textBox.BackColor = SystemColors.Window;
            trackBar.Minimum = 0;
            trackBar.Maximum = 10_000;
            MinimumValue = 0;
            MaximumValue = 10_000;
        }

        public void SetMinimum(double minVal)
        {
            MinimumValue = minVal;
            //mi van ha az aktuális érték nem rangebe esik??!
        }

        public void SetMaximum(double maxVal)
        {
            MaximumValue = maxVal;
        }

        private double CalcPercent(double value)
        {
            return (value - MinimumValue) / (MaximumValue - MinimumValue) * 100;
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
            trackBar.Value = (int)Math.Floor(CalcPercent(val) / 100 * SliderResolution);
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

                    //int tester = (int)Math.Floor(CalcPercent(newVal) / 100 * SliderResolution);
                    trackBar.Value = (int)Math.Floor(CalcPercent(newVal) / 100 * SliderResolution);
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
                trackBar.Value = (int)Math.Floor(CalcPercent(newVal) / 100 * SliderResolution);
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
