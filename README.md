# UserControlTesterProject

Tested some concepts for an advanced numeric input control, and also for a slider supporting double values.
This was only a test, so use it and change it if you find it useful for your tasks.
Note: you might need to rebuild the solution after cloning, if you cannot open the designer files.

Features for both UserControls:
- User inputs validated via both events lost focus and Enter key up
- Programmatic value setting
- Settable validation background color during edit mode
- Range limit min/max
- Optionally coerce or ignore out of range inputs
- Capture and ignore wrong inputs
- Set or hide tooltip
- Standard NumberFormatSpecifier option (default is "G")
- Controls on UI are resizable in design time (AdvancedSlider has only slider resize option)
- EnterKeyUpCustom, FocusLostCustom, InvalidInput, OutOfRange, ValueChangedFinal (not including slider scroll intermediate values), ValueChanged (including slider scroll events too) events

AdvancedSlider:
- Handles double values. TrackBar range is now fixed to 0-1000 (SliderResolution), so the slider double resolution is (Max-min)/1000. You can change this if needed.
- Set label text
- Set SmallChange/LargeChange for slider

https://user-images.githubusercontent.com/51174971/221413767-7f283c76-5ee3-48a0-b3e2-1fbfcab79bf2.mp4

Some usage examples in the project:

```public Form1()
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
            advancedSlider1.SetMinimum(-300);
            advancedSlider1.SetMaximum(500);
            advancedSlider1.EnableToolTip($"Min: -300\r\nMax: 500\r\nOut of range will be coerced.");
            advancedSlider1.SetValue(100);
            advancedSlider1.CoerceOutOfRange = true;
}
```


