using Ex.OM.Extentions;
using Ex.OM.Extentions.Converts;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ex.OM.Configurations
{
    public static class MaskConfiguration
    {
        public static void AddMask(this TextBox textBox)
        {
            var mask = (string)textBox.GetValue(TextBoxFieldAssistOM.TextBoxMask);
            var promptChar = (char)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPromptCharMask);
            string unMaskValue = string.Empty;

            var options = textBox.Text.Mask(mask, promptChar);
            var unMask = textBox.GetValue(TextBoxFieldAssistOM.TextBoxUnMask).ToString();

            textBox.Text = options.data;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, options.unMaskValue.Trim());


            var _valueUnMask = string.IsNullOrEmpty(unMask) ? options.unMaskValue.Mask(mask) : options.unMaskValue.Mask(unMask);

            textBox.SetValue(TextBoxFieldAssistOM.TextBoxUnMaskValue, _valueUnMask.data);

            textBox.PreviewTextInput += TextBox_PreviewTextInput;
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBox.Loaded += TextBox_Loaded;
            DataObject.AddPastingHandler(textBox, OnPaste);
        }

        private static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            var mask = (string)textBox.GetValue(TextBoxFieldAssistOM.TextBoxMask);
            var promptChar = (char)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPromptCharMask);
            var unMask = textBox.GetValue(TextBoxFieldAssistOM.TextBoxUnMask).ToString();

            var options = textBox.Text.Mask(mask, promptChar);
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, options.unMaskValue.Trim());

            var _valueUnMask = string.IsNullOrEmpty(unMask) ? options.unMaskValue.Mask(mask) : options.unMaskValue.Mask(unMask);
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxUnMaskValue, _valueUnMask.data);
            textBox.LoadConvert(new ConvertMask { Mask = mask, PrompChar = promptChar, UnMask = unMask, TextBox = textBox });

            textBox.Text = options.data;


        }

        public static void RemoveMask(this TextBox textBox)
        {
            textBox.PreviewTextInput -= TextBox_PreviewTextInput;
            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            textBox.Loaded -= TextBox_Loaded;

            DataObject.RemovePastingHandler(textBox, OnPaste);
        }



        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            e.CancelCommand();
        }

        private static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var mask = (string)textBox.GetValue(TextBoxFieldAssistOM.TextBoxMask);
            var data = (string)textBox.GetValue(TextBoxFieldAssistOM.TextBoxOriginalValue);
            var promptChar = (char)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPromptCharMask);

            var caret = textBox.CaretIndex;


            var options = e.Text.ToCharArray()[0].Mask(mask, data, textBox.CaretIndex, promptChar);
            var unMask = textBox.GetValue(TextBoxFieldAssistOM.TextBoxUnMask).ToString();


            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, options.unMaskValue.Trim());
            textBox.Text = options.data;
            textBox.CaretIndex = options.lastIndex;
            //ctextBox.CaretIndex = options.discrepancies ? options.lastIndex + e.Text.Length : caret;


            var _valueUnMask = string.IsNullOrEmpty(unMask) ? options.unMaskValue.Mask(mask) : options.unMaskValue.Mask(unMask);

            textBox.SetValue(TextBoxFieldAssistOM.TextBoxUnMaskValue, _valueUnMask.data);

            e.Handled = true;
        }

        public static bool IsControl(this KeyEventArgs e)
        {
            return   e.Key == Key.F1 || 
                     e.Key == Key.F2 ||
                     e.Key == Key.F3 || 
                     e.Key == Key.F4 || 
                     e.Key == Key.F5 || 
                     e.Key == Key.F6 ||
                     e.Key == Key.F7 || 
                     e.Key == Key.F8 || 
                     e.Key == Key.F9 || 
                     e.Key == Key.F10 ||
                     e.Key == Key.F11 || 
                     e.Key == Key.F12 || 
                     e.Key == Key.F13 || 
                     e.Key == Key.F14 ||
                     e.Key == Key.F15 || 
                     e.Key == Key.F16 || 
                     e.Key == Key.F17 || 
                     e.Key == Key.F18 ||
                     e.Key == Key.F19 ||
                     e.Key == Key.F20 || 
                     e.Key == Key.F21 || 
                     e.Key == Key.F22 ||
                     e.Key == Key.F23 || 
                     e.Key == Key.F24 || 
                     e.Key == Key.Insert || 
                     e.Key == Key.Pause ||
                     e.Key == Key.PageUp || 
                     e.Key == Key.PageDown ||
                     e.Key == Key.Home || 
                     e.Key == Key.PrintScreen ||
                     e.Key == Key.Tab || 
                     e.Key == Key.RightShift || 
                     e.Key == Key.LeftShift ||
                     e.Key == Key.LeftAlt ||
                     e.Key == Key.RightAlt || 
                     e.Key == Key.CapsLock ||
                     e.Key == Key.Escape || 
                     e.Key == Key.LeftCtrl ||
                     e.Key == Key.RightCtrl || 
                     e.Key == Key.Divide || 
                     e.Key == Key.Multiply ||
                     e.Key == Key.OemMinus ||
                     e.Key == Key.OemPlus || 
                     e.Key == Key.Subtract ||
                     e.Key == Key.Add ||
                     e.Key == Key.Return || 
                     e.Key == Key.Enter ||
                     e.Key == Key.NumLock &&
                     !e.IsLetter() &&
                     !e.IsNumber();

        }

        public static bool IsNumber(this KeyEventArgs e)
        {
            return e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                   e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad5 ||
                   e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 ||
                   e.Key == Key.NumPad9 || e.Key == Key.NumPad0 || e.Key == Key.D1 ||
                   e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 ||
                   e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 ||
                   e.Key == Key.D0;
        }

        private static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (!textBox.IsEnabled) { e.Handled = true; return; }
            if (textBox.IsReadOnly) { e.Handled = true; return; }

            TextBox newTextBox = new TextBox();
            newTextBox.Text = textBox.Text;

            newTextBox.SelectionStart = textBox.SelectionStart;
            newTextBox.SelectionLength = textBox.SelectionLength;
            newTextBox.SelectedText = textBox.SelectedText;
            if (e.IsControl()) { e.Handled = true; return; }

            if (textBox.SelectionLength > 0 || e.IsBackOrDeleteKey())
            {


                newTextBox.SelectionClear();

                var caret = newTextBox.CaretIndex;
                var data = textBox.GetValue(TextBoxFieldAssistOM.TextBoxOriginalValue).ToString();

                var tmpOriginalValue = string.Empty;
                foreach (var item in newTextBox.Text)
                {

                    if (data.Any(y => y == item))
                    {
                        tmpOriginalValue = tmpOriginalValue.Insert(tmpOriginalValue.Length, item.ToString());
                    }
                }

                var mask = textBox.GetValue(TextBoxFieldAssistOM.TextBoxMask).ToString();
                var promptChar = (char)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPromptCharMask);

                var options2 = tmpOriginalValue.Mask(mask, promptChar);
                var unMask = textBox.GetValue(TextBoxFieldAssistOM.TextBoxUnMask).ToString();

                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, options2.unMaskValue.Trim());

                textBox.Text = options2.data;
                textBox.CaretIndex = caret;


                var _valueUnMask = string.IsNullOrEmpty(unMask) ? options2.unMaskValue.Mask(mask) : options2.unMaskValue.Mask(unMask);

                textBox.SetValue(TextBoxFieldAssistOM.TextBoxUnMaskValue, _valueUnMask.data);
            }

        }


    }
}
