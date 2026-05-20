using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class DecimalNegativeConfiguration
    {
        public static void AddDecimalNegative(this TextBox textBox)
        {
            var caret = textBox.CaretIndex;
            if (!textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                caret ++;
            }
            var decimals = System.Convert.ToInt32(textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber));
            var options = textBox.Text.DecimalNegative(decimals);
            textBox.Text = options.data.Truncate(textBox.MaxLength);

            textBox.CaretIndex = caret;

            textBox.PreviewTextInput += DecimalConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus += DecimalConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyDown += DecimalConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded += DecimalConfiguration.TextBox_Loaded;

            DataObject.AddPastingHandler(textBox, DecimalConfiguration.OnPaste);
            textBox.LoadConvertDecimal();

        }
        public static void RemoveDecimalNegative(this TextBox textBox)
        {
            textBox.PreviewTextInput -= DecimalConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus -= DecimalConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyDown -= DecimalConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded -= DecimalConfiguration.TextBox_Loaded;

            DataObject.RemovePastingHandler(textBox, DecimalConfiguration.OnPaste);
        }
    }
}
