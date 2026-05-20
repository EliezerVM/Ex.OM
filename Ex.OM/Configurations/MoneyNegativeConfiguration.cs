using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class MoneyNegativeConfiguration
    {
        public static void AddMoneyNegative(this TextBox textBox)
        {
            var data = textBox.Text.MoneyNegative();
            var caret = textBox.CaretIndex;
            textBox.Text = data.data;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);

            if (!textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                caret++;
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
        public static void RemoveMoneyNegative(this TextBox textBox)
        {
            textBox.PreviewTextInput -= MoneyConfiguration.TextBox_PreviewTextInput;
            textBox.PreviewKeyDown -= MoneyConfiguration.TextBox_PreviewKeyDown;

            textBox.PreviewKeyUp -= MoneyConfiguration.TextBox_PreviewKeyUp;
            textBox.LostFocus -= MoneyConfiguration.TextBox_LostFocus;
            textBox.Loaded -= MoneyConfiguration.TextBox_Loaded;

            DataObject.RemovePastingHandler(textBox, MoneyConfiguration.OnPaste);
        }
    }
}
