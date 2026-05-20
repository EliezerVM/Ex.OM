using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class PercentDecimalNegativeConfiguration
    {

        public static void AddPercentDecimalNegative(this TextBox textBox)
        {
            var caret = textBox.CaretIndex;
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var data = textBox.Text.PercentDecimalNegative(decimals, position);

            textBox.Text = data.data;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.decimalData);
            textBox.CaretIndex = caret;

            textBox.PreviewTextInput += PercentDecimalConfiguration.PreviewTextInputPercentDecimal;
            textBox.PreviewKeyDown += PercentDecimalConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded += PercentDecimalConfiguration.TextBox_Loaded;
            DataObject.AddPastingHandler(textBox, PercentDecimalConfiguration.OnPaste);

        }

        public static void RemovePercenDecimalNegative(this TextBox textBox)
        {
            textBox.PreviewTextInput -= PercentDecimalConfiguration.PreviewTextInputPercentDecimal;
            textBox.PreviewKeyDown -= PercentDecimalConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded -= PercentDecimalConfiguration.TextBox_Loaded;

            DataObject.RemovePastingHandler(textBox, PercentDecimalConfiguration.OnPaste);
        }

    }
}