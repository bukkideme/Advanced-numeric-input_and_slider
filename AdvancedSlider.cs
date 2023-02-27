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
        
        /// <summary>
        /// Use the required format specifier string. The default is "G": 
        /// https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#GFormatString.
        /// Use Precision specifier if required, for example scientific notation "E" has a default of 6 precision.
        /// If you need less or more, you can change this like "E3" for example (in this case the 3rd digit will be rounded).
        /// </summary>
        [Description("Use the required format specifier string. The default is \"G\"")]
        public string NumberFormatSpecifier { get; set; } = "G";
        public double MinimumValue { get; private set; }
        public double MaximumValue { get; private set; }
        /// <summary>
        /// If false, out of range values will be ignored. If true, value will be coerced to actual min or max limit.
        /// Default is false.
        /// </summary>
        [Description("If false, out of range values will be ignored. If true, value will be coerced to actual MinimumValue or MaximumValue limit.")]
        public bool CoerceOutOfRange { get; set; } = false;

        /// <summary>
        /// Sets the small steps for the trackBar integer range between 0-1000
        /// </summary>
        public int SmallChange
        {
            get => trackBar.SmallChange;
            set => trackBar.SmallChange = value;
        }
        /// <summary>
        /// Sets the large steps for the trackBar integer range between 0-1000
        /// </summary>
        public int LargeChange
        {
            get => trackBar.LargeChange;
            set => trackBar.LargeChange = value;
        }
                
        [Description("Sets the trackBar/slider steps. The slider resolution will be (MaximumValue-MinimumValue)/sliderResolution. Default is 1000.")]
        public int SliderResolution { get; private set; } = 1000;
        /// <summary>
        /// Sets the trackBar/slider steps. 
        /// The slider resolution will be (MaximumValue-MinimumValue)/sliderResolution. Default is 1000.
        /// </summary>
        public void SetSliderResolution(int sliderResolution)
        {
            SliderResolution = sliderResolution;
            trackBar.Maximum = SliderResolution; 
            //recalc slider position
            trackBar.Value = (int)Math.Floor(CalcPercent(ActualValue) / 100 * SliderResolution);            
        }
        
        /// <summary>
        /// Color used to indicate edit mode of the control. Default is Color.LightBlue.
        /// </summary>
        public Color ValidatingColor { get; set; } = Color.LightBlue;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked each time when the value changes. Slider scrolling changes are included")]
        public event EventHandler ValueChanged;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked each time when the value changes. Slider scrolling not included!")]
        public event EventHandler ValueChangedFinal;

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

        public AdvancedSlider()
        {
            InitializeComponent();            
            textBox.BackColor = SystemColors.Window;
            trackBar.Minimum = 0;
            trackBar.Maximum = SliderResolution;
            MinimumValue = 0;
            MaximumValue = SliderResolution;
        }

        /// <summary>
        /// Sets the minimum limit of the accepted input range. If actual value is below the new limit, we set it to the new limit.
        /// </summary>
        /// <param name="minLimit">Requested new minimum limit.</param>
        public void SetMinimum(double minLimit)
        {
            if (ActualValue < minLimit)
            {
                ActualValue = minLimit;
                ValueChanged?.Invoke(this, null);
                ValueChangedFinal?.Invoke(this, null);
                textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);
                textBox.BackColor = SystemColors.Window;
                trackBar.Value = (int)Math.Floor(CalcPercent(ActualValue) / 100 * SliderResolution);
            }
            MinimumValue = minLimit;
        }

        /// <summary>
        /// Sets the maximum limit of the accepted input range. If actual value is above the new limit, we set it to the new limit.
        /// </summary>
        /// <param name="maxLimit">Requested new minimum limit.</param>
        public void SetMaximum(double maxLimit)
        {
            if (ActualValue > maxLimit)
            {
                ActualValue = maxLimit;
                ValueChanged?.Invoke(this, null);
                ValueChangedFinal?.Invoke(this, null);
                textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);
                textBox.BackColor = SystemColors.Window;
                trackBar.Value = (int)Math.Floor(CalcPercent(ActualValue) / 100 * SliderResolution);
            }
            MaximumValue = maxLimit;
        }

        private void HandleOutOfRange(double newVal)
        {
            if (newVal > MaximumValue)
            {
                if (CoerceOutOfRange)
                {
                    ActualValue = MaximumValue;
                    ValueChanged?.Invoke(this, null);
                    ValueChangedFinal?.Invoke(this, null);
                }
                OutOfRange?.Invoke(this, null);
            }
            else if (newVal < MinimumValue)
            {
                if (CoerceOutOfRange)
                {
                    ActualValue = MinimumValue;
                    ValueChanged?.Invoke(this, null);
                    ValueChangedFinal?.Invoke(this, null);
                }
                OutOfRange?.Invoke(this, null);
            }
            else
            {
                ActualValue = newVal;
                ValueChanged?.Invoke(this, null);
                ValueChangedFinal?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Use this to programmatically set the value of the user control.
        /// </summary>
        /// <param name="newVal"></param>
        public void SetValue(double newVal)
        {
            HandleOutOfRange(newVal);

            textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);
            textBox.BackColor = SystemColors.Window;
            trackBar.Value = (int)Math.Floor(CalcPercent(ActualValue) / 100 * SliderResolution);
        }

        private double CalcPercent(double value)
        {
            return (value - MinimumValue) / (MaximumValue - MinimumValue) * 100;
        }

        private double CalcValueFromPercent(double percent)
        {
            return percent / 100 * (MaximumValue - MinimumValue) + MinimumValue;            
        }

        /// <summary>
        /// Get the double value the user control currently holds.
        /// </summary>
        /// <returns></returns>
        public double GetValue()
        {
            return ActualValue;
        }

        public void SetLabel(string label)
        {
            parameterLabel.Text = label;
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
                    HandleOutOfRange(newVal);
                }
                else InvalidInput?.Invoke(this, e);

                EnterKeyUpCustom?.Invoke(this, e);
                textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);
                textBox.BackColor = SystemColors.Window;
                trackBar.Value = (int)Math.Floor(CalcPercent(ActualValue) / 100 * SliderResolution);
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double newVal))
            {
                HandleOutOfRange(newVal);
            }
            else InvalidInput?.Invoke(this, e);

            FocusLostCustom?.Invoke(this, e);
            textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);            
            trackBar.Value = (int)Math.Floor(CalcPercent(ActualValue) / 100 * SliderResolution);
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

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            double percent = ((double)trackBar.Value / SliderResolution) * 100;
            ActualValue = CalcValueFromPercent(percent);
            ValueChanged?.Invoke(this, null);            
            textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            double percent = ((double)trackBar.Value / SliderResolution) * 100;
            ActualValue = CalcValueFromPercent(percent);
            ValueChanged?.Invoke(this, null);
            ValueChangedFinal?.Invoke(this, null);
            textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);

            textBox.BackColor = SystemColors.Window;
        }

        private void trackBar_KeyUp(object sender, KeyEventArgs e)
        {
            double percent = ((double)trackBar.Value / SliderResolution) * 100;
            ActualValue = CalcValueFromPercent(percent);
            ValueChanged?.Invoke(this, null);
            ValueChangedFinal?.Invoke(this, null);
            textBox.Text = ActualValue.ToString(NumberFormatSpecifier, CultureInfo.InvariantCulture);

            textBox.BackColor = SystemColors.Window;
        }
    }
}
