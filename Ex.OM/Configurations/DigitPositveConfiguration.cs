using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class DigitPositveConfiguration
    {
        public static void AddDigitPositve(this TextBox textBox)
        {

            textBox.Text = textBox.Text.DigitsPositive().Truncate(textBox.MaxLength);
            textBox.Text = textBox.Text.DigitsPositive();

            textBox.PreviewTextInput += DigitConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus += DigitConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyDown += DigitConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded += DigitConfiguration.TextBox_Loaded;
            DataObject.AddPastingHandler(textBox, DigitConfiguration.OnPaste);
        }


        public static void RemoveDigitPositve(this TextBox textBox)
        {
            textBox.PreviewTextInput -= DigitConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus -= DigitConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyDown -= DigitConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded -= DigitConfiguration.TextBox_Loaded;

            DataObject.RemovePastingHandler(textBox, DigitConfiguration.OnPaste);
        }
    }
}
