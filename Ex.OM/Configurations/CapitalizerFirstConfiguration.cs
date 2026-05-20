using Ex.OM.Extentions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Ex.OM.Configurations
{
    public static class CapitalizerFirstConfiguration
    {

        public static void AddCapitalizerFirst(this TextBox textBox)
        {

            textBox.Text = textBox.Text.CapitalizerFirst();
            textBox.PreviewTextInput += PreviewTextInputCapitalizerFirst;
            textBox.PreviewKeyUp += PreviewKeyUpCapitalizerFirst;
            textBox.PreviewKeyDown += TextBoxPreviewKeyDownCapitalizerFirst;
            textBox.LostFocus += TextBoxLostFocusCapitalizerFirts;
            DataObject.AddPastingHandler(textBox, OnPasteCapitalizerFirst);

        }

      

        public static void RemoveCapitalizerFirst(this TextBox textBox)
        {
            textBox.PreviewTextInput -= PreviewTextInputCapitalizerFirst;
            textBox.PreviewKeyUp -= PreviewKeyUpCapitalizerFirst;
            textBox.PreviewKeyDown -= TextBoxPreviewKeyDownCapitalizerFirst;
            textBox.LostFocus -= TextBoxLostFocusCapitalizerFirts;
            DataObject.RemovePastingHandler(textBox, OnPasteCapitalizerFirst);

        }
        private static void TextBoxPreviewKeyDownCapitalizerFirst(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (!textBox.IsEnabled) { e.Handled = true; return; }
            if (textBox.IsReadOnly) { e.Handled = true; return; }
            if (e.IsBackOrDeleteKey())
            {
                var caret = textBox.CaretIndex;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                textBox.Text = textBox.Text.CapitalizerFirst();
                caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);    
                textBox.CaretIndex = caret;
            }


        }

        private static void OnPasteCapitalizerFirst(object sender, DataObjectPastingEventArgs e)
        {
            var data = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            DataObject d = new DataObject();
            d.SetData(DataFormats.Text, data.CapitalizerFirst());
            e.DataObject = d;


            //if (!(sender is TextBox textBox)) return;

            //var options = textBox.InitializeOnPaste(e);

            //if (options.isNotText) return;

            //textBox.Text = textBox.Text.CapitalizerFirst();
            //textBox.CaretIndex = options.caret + options.data.Length;
            //e.CancelCommand();
        }


        private static void PreviewKeyUpCapitalizerFirst(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (e.IsBackOrDeleteKey())
            {
                var caret = textBox.CaretIndex;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                textBox.Text = textBox.Text.CapitalizerFirst();
                caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);    
                textBox.CaretIndex = caret;
            }

        }


        private static void PreviewTextInputCapitalizerFirst(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            var options = textBox.InitializeTextInput(e);
            textBox.Text = textBox.Text.CapitalizerFirst();
            textBox.CaretIndex = options.caret + options.character.Length;
            e.Handled = true;
        }

        private static void TextBoxLostFocusCapitalizerFirts(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            textBox.Text.CapitalizerFirst();
        }
    }
}
