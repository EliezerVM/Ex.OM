using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class MoneyDecimalPositiveConfiguration
    {
        public static void AddMoneyDecimalPositive(this TextBox textBox)
        
        {
            var numberDecimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var data = textBox.Text.MoneyPositiveDecimal(numberDecimals);
            var caret = textBox.CaretIndex;
            textBox.Text = data.data;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);

            if (textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign) && textBox.Text.Digits().Length > 0)
            {
                caret--;
            }

            textBox.CaretIndex = caret;

            textBox.PreviewTextInput += MoneyDecimalConfiguration.TextBox_PreviewTextInput;
            textBox.PreviewKeyDown += MoneyDecimalConfiguration.TextBox_PreviewKeyDown;
            textBox.PreviewKeyUp += MoneyDecimalConfiguration.TextBox_PreviewKeyUp;
            textBox.LostFocus += MoneyDecimalConfiguration.TextBox_LostFocus;
            textBox.Loaded += MoneyDecimalConfiguration.TextBox_Loaded;
            DataObject.AddPastingHandler(textBox, MoneyConfiguration.OnPaste);
            textBox.LoadConvertMoneyDecimal();
        }
        public static void RemoveMoneyDecimalPositive(this TextBox textBox)
        {
            textBox.PreviewTextInput -= MoneyDecimalConfiguration.TextBox_PreviewTextInput;
            textBox.PreviewKeyDown -= MoneyDecimalConfiguration.TextBox_PreviewKeyDown;
            DataObject.RemovePastingHandler(textBox, MoneyDecimalConfiguration.OnPaste);


            textBox.PreviewKeyUp -= MoneyDecimalConfiguration.TextBox_PreviewKeyUp;
            textBox.LostFocus -= MoneyDecimalConfiguration.TextBox_LostFocus;
            textBox.Loaded -= MoneyDecimalConfiguration.TextBox_Loaded;
        }
    }
}
