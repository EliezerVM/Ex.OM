using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Configurations
{
    public static class OnlyLetterCapitalizerFirstConfiguration
    {
        public static void AddOnlyLetterCapitalizerFirst(this TextBox textBox)
        {

            textBox.Text = textBox.Text.OnlyLetterCapitalizerFirst();
            textBox.PreviewTextInput += TextBox_PreviewTextInput;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.PreviewKeyUp += TextBox_PreviewKeyUp;
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
            DataObject.AddPastingHandler(textBox, OnPaste);
        }


        public static void RemoveOnlyLetterCapitalizerFirst(this TextBox textBox)
        {
            textBox.PreviewTextInput -= TextBox_PreviewTextInput;
            textBox.LostFocus -= TextBox_LostFocus;
            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            textBox.PreviewKeyUp -= TextBox_PreviewKeyUp;
            DataObject.RemovePastingHandler(textBox, OnPaste);
        }



        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            var data = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            DataObject d = new DataObject();
            d.SetData(DataFormats.Text, data.OnlyLetterCapitalizerFirst());
            e.DataObject = d;
            //if (!(sender is TextBox textBox)) return;
            //var options = textBox.InitializeOnPaste(e);
            //if (options.isNotText) return;

            //textBox.Text = textBox.Text.OnlyLetterCapitalizerFirst();
            //textBox.CaretIndex = options.caret + options.data.Length;
            //e.CancelCommand();
        }

        private static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var options = textBox.InitializeTextInput(e);
            textBox.Text = options.data.OnlyLetterCapitalizerFirst();
            textBox.CaretIndex = options.caret + options.character.Length;
            e.Handled = true;
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            textBox.Text.OnlyLetterCapitalizerFirst();
        }

        private static void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (!textBox.IsEnabled) { e.Handled = true; return; }
            if (textBox.IsReadOnly) { e.Handled = true; return; }
            if (!e.IsLetter() && !DigitConfiguration.IsControl) { e.Handled = true; return; }
            if (e.IsBackOrDeleteKey())
            {
                textBox.SelectionClear();
                var caret = textBox.CaretIndex;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);

                textBox.Text = textBox.Text.OnlyLetterCapitalizerFirst();
                caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);

                textBox.CaretIndex = caret;
            }
        }

        private static void TextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            if (e.IsBackOrDeleteKey())
            {
                var caret = textBox.CaretIndex;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);

                textBox.Text = textBox.Text.OnlyLetterCapitalizerFirst();
                caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);

                textBox.CaretIndex = caret;
            }

        }
    }
}
