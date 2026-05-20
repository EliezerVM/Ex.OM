using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class PercentDecimalPositiveConfiguration
    {

        public static void  AddPercentDecimalPositive(this TextBox textBox)
        {
            var caret = textBox.CaretIndex;
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var data = textBox.Text.PercentDecimalPositive(decimals, position);
           

            textBox.Text = data.data;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.decimalData);
            textBox.CaretIndex = caret;

            textBox.PreviewTextInput += PercentDecimalConfiguration.PreviewTextInputPercentDecimal;
            textBox.PreviewKeyDown += PercentDecimalConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded += PercentDecimalConfiguration.TextBox_Loaded;
            DataObject.AddPastingHandler(textBox, PercentDecimalConfiguration.OnPaste);

        }

        public static void RemovePercenDecimalPositive(this TextBox textBox)
        {
            textBox.PreviewTextInput -= PercentDecimalConfiguration.PreviewTextInputPercentDecimal;
            textBox.PreviewKeyDown -= PercentDecimalConfiguration.TextBox_PreviewKeyDown;
            textBox.Loaded -= PercentDecimalConfiguration.TextBox_Loaded;
            DataObject.RemovePastingHandler(textBox, PercentDecimalConfiguration.OnPaste);
        }

    }
}
