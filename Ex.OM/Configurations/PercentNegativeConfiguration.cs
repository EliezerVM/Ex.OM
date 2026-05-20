using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class PercentNegativeConfiguration
    {
        public static void AddPercentNegative(this TextBox textBox)
        {
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);



            textBox.Text = textBox.Text.PercentNegative(position);



            textBox.Loaded += PercentConfiguration.TextBox_Loaded;

            textBox.Text = textBox.Text.PercentNegative(position);
            textBox.PreviewTextInput += PercentConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus += PercentConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyUp += PercentConfiguration.TextBox_PreviewKeyUp;
            textBox.PreviewKeyDown += PercentConfiguration.TextBox_PreviewKeyDown;
            DataObject.AddPastingHandler(textBox, PercentConfiguration.OnPaste);
        }

        public static void RemovePercentNegative(this TextBox textBox)
        {
            textBox.PreviewTextInput -= PercentConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus -= PercentConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyDown -= PercentConfiguration.TextBox_PreviewKeyDown;
            textBox.PreviewKeyUp -= PercentConfiguration.TextBox_PreviewKeyUp;
            DataObject.RemovePastingHandler(textBox, PercentConfiguration.OnPaste);
        }


    }
}
