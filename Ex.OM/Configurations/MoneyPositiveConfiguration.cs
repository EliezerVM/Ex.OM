using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class MoneyPositiveConfiguration
    {
        public static void AddMoneyPositive(this TextBox textBox)
        {
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);

            var data = textBox.Text.MoneyPositive(showSymbol);
            var caret = textBox.CaretIndex;
            textBox.Text = data.data;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);

            if (textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign) && textBox.Text.Digits().Length > 0)
            {
                caret--;
            }

            textBox.CaretIndex = caret;

            textBox.PreviewTextInput += MoneyConfiguration.TextBox_PreviewTextInput;
            textBox.PreviewKeyDown += MoneyConfiguration.TextBox_PreviewKeyDown;
            textBox.PreviewKeyUp += MoneyConfiguration.TextBox_PreviewKeyUp;
            textBox.LostFocus += MoneyConfiguration.TextBox_LostFocus;
            textBox.Loaded += MoneyConfiguration.TextBox_Loaded;
            DataObject.AddPastingHandler(textBox, MoneyConfiguration.OnPaste);
            textBox.LoadConvertMoney();
        }
        public static void RemoveMoneyPositive(this TextBox textBox)
        {
            textBox.PreviewTextInput -= MoneyConfiguration.TextBox_PreviewTextInput;
            textBox.PreviewKeyDown -= MoneyConfiguration.TextBox_PreviewKeyDown;
            DataObject.RemovePastingHandler(textBox, MoneyConfiguration.OnPaste);


            textBox.PreviewKeyUp -= MoneyConfiguration.TextBox_PreviewKeyUp;
            textBox.LostFocus -= MoneyConfiguration.TextBox_LostFocus;
            textBox.Loaded -= MoneyConfiguration.TextBox_Loaded;
        }
    }
}
