using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class DigitNegativeConfiguration
    {
        public static void AddDigitNegative(this TextBox textBox)
        {

            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, textBox.CaretIndex);



            textBox.Text = textBox.Text.DigitsPositive().Truncate(textBox.MaxLength);
            textBox.Text = textBox.Text.DigitNegative();

            textBox.CaretIndex = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);

            textBox.PreviewTextInput += DigitConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus += DigitConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyDown += DigitConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded += DigitConfiguration.TextBox_Loaded;
            textBox.PreviewKeyUp += DigitConfiguration.TextBox_KeyUp;

            //DataObject.AddPastingHandler(textBox, DigitConfiguration.OnPaste);
        }


        public static void RemoveDigitNegative(this TextBox textBox)
        {
            textBox.PreviewTextInput -= DigitConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus -= DigitConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyDown -= DigitConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded -= DigitConfiguration.TextBox_Loaded;
            textBox.PreviewKeyUp -= DigitConfiguration.TextBox_KeyUp;
            //DataObject.RemovePastingHandler(textBox, DigitConfiguration.OnPaste);
        }

    


    }
}
