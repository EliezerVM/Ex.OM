using Ex.OM.Extentions;
using Ex.OM.Extentions.Converts;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ex.OM.Configurations
{
    public static class DigitConfiguration
    {
        public static void AddDigit(this TextBox textBox)
        {

            textBox.Text = textBox.Text.DigitsPositive().Truncate(textBox.MaxLength);
            textBox.Text = textBox.Text.Digits();
            textBox.PreviewTextInput += TextBox_PreviewTextInput;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBox.Loaded += TextBox_Loaded;
            //textBox.PreviewKeyUp += TextBox_KeyUp;

            DataObject.AddPastingHandler(textBox, OnPaste);
        }


        public static void RemoveDigit(this TextBox textBox)
        {
            textBox.PreviewTextInput -= TextBox_PreviewTextInput;
            textBox.LostFocus -= TextBox_LostFocus;
            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            textBox.Loaded -= TextBox_Loaded;
            //textBox.PreviewKeyUp -= TextBox_KeyUp;
            DataObject.RemovePastingHandler(textBox, OnPaste);
        }

        public static void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            if ((e.Key == Key.Back || e.Key == Key.Delete) && textBox.Text.Equals(Methods.CultureInfo.NumberFormat.NegativeSign) && type ==  TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
            {
                textBox.Text = default;
            }
        }

        public static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            textBox.LoadConvert(new ConvertDigits { TextBox  =  textBox});

            if (type == TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
            {
                textBox.Text = textBox.Text.DigitNegative().Truncate(textBox.MaxLength);
            }
            else if(type == TextBoxFieldAssistOM.FormatTextBox.DigitsPositive)
            {
                textBox.Text = textBox.Text.DigitsPositive().Truncate(textBox.MaxLength);
            }
            else
            {
                textBox.Text = textBox.Text.Digits().Truncate(textBox.MaxLength);
            }


        }


        internal static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var data = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            DataObject d = new DataObject();

            if (type == TextBoxFieldAssistOM.FormatTextBox.Digits)
            {
                d.SetData(DataFormats.Text, data.Digits());

            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.DigitsPositive)
            {
                d.SetData(DataFormats.Text, data.DigitsPositive());
            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
            {
                d.SetData(DataFormats.Text, data.DigitNegative());
            }
            e.DataObject = d;


            //    //var caret = 0;
            //    //caret = textBox.CaretIndex;
            //    //var options = textBox.InitializeOnPaste(e);
            //    //if (options.isNotText) return;

            //    //if (type == TextBoxFieldAssistOM.FormatTextBox.Digits)
            //    //{
            //    //    textBox.Text = textBox.Text.Digits();

            //    //}
            //    //else if (type == TextBoxFieldAssistOM.FormatTextBox.DigitsPositive)
            //    //{
            //    //    textBox.Text = textBox.Text.DigitsPositive();
            //    //}
            //    //else if (type == TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
            //    //{
            //    //    textBox.Text = textBox.Text.DigitNegative();
            //    //}


            //    //textBox.CaretIndex = caret + options.data.Length;

            //    //if (Clipboard.GetDataObject().GetDataPresent(DataFormats.UnicodeText) || Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            //    //{
            //    //    if (textBox.SelectionLength > 0)
            //    //    {
            //    //        textBox.Paste();
            //    //    }
            //    //}

            //    //e.CancelCommand();
        }

        public static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (!e.Text.IsDigit() && (!e.Text.Equals(Methods.CultureInfo.NumberFormat.NegativeSign) || !e.Text.Equals(Methods.CultureInfo.NumberFormat.PositiveSign))) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            var caret = textBox.CaretIndex;
            if (textBox.MaxLength > 0 && textBox.Text.DigitsPositive().Length >= textBox.MaxLength + (e.Text == Methods.CultureInfo.NumberFormat.NegativeSign ? Methods.CultureInfo.NumberFormat.NegativeSign.Length : 0))
            {
                if (e.Text != Methods.CultureInfo.NumberFormat.PositiveSign)
                {
                    e.Handled = true;
                    return;
                }
            }

            var tmp = textBox.Text;


            if (e.Text == Methods.CultureInfo.NumberFormat.NegativeSign && (type != TextBoxFieldAssistOM.FormatTextBox.DigitsPositive))
            {
                textBox.Text = textBox.Text.DigitNegative();
                if (e.Text == Methods.CultureInfo.NumberFormat.NegativeSign && !tmp.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
                {
                    caret += Methods.CultureInfo.NumberFormat.NegativeSign.Length;
                }

            }
            else if (e.Text == Methods.CultureInfo.NumberFormat.PositiveSign && type != TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
            {
                textBox.Text = textBox.Text.DigitsPositive();
                if (e.Text == Methods.CultureInfo.NumberFormat.PositiveSign && tmp.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
                {
                    caret -= 1;
                }
            }

            else
            {
                if (e.Text.IsDigit())
                {
                  

                    var data = textBox.Text;
                    data = data.Insert(caret, e.Text);

                    if (string.IsNullOrEmpty(textBox.Text) && type != TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
                    {
                        caret += 1;
                    }
                

                    if (type == TextBoxFieldAssistOM.FormatTextBox.Digits)
                    {
                        textBox.Text = data.Digits();
                    }
                    else if (type == TextBoxFieldAssistOM.FormatTextBox.DigitsPositive)
                    {
                        textBox.Text = data.DigitsPositive();
                    }
                    else if (type == TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
                    {
                        textBox.Text = data.DigitNegative();
                        caret += 1;
                    }

                    if (textBox.Text.Digits().Length != tmp.Digits().Length)
                    {
                        caret += e.Text.Length;
                    }
                   

                }
            }

            textBox.CaretIndex = caret;

            e.Handled = true;
        }

        internal static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            if (type == TextBoxFieldAssistOM.FormatTextBox.Digits)
            {
                textBox.Text = textBox.Text.Digits();
            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.DigitsPositive)
            {
                textBox.Text = textBox.Text.DigitsPositive();
            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
            {
                textBox.Text = textBox.Text.DigitNegative();
            }
        }
        public static bool IsControl => (Keyboard.IsKeyDown(Key.RightCtrl) || 
                                  Keyboard.IsKeyDown(Key.LeftCtrl)  ||
                                  Keyboard.IsKeyDown(Key.LeftShift) ||
                                  Keyboard.IsKeyDown(Key.RightShift) ||
                                    Keyboard.IsKeyDown(Key.LeftAlt) ||
                                    Keyboard.IsKeyDown(Key.RightAlt) ||
            Keyboard.IsKeyDown(Key.Up) ||
            Keyboard.IsKeyDown(Key.Down)  ||
                Keyboard.IsKeyDown(Key.Left) ||
            Keyboard.IsKeyDown(Key.Right) ||
            Keyboard.IsKeyDown(Key.Home) ||
            Keyboard.IsKeyDown(Key.PageDown) ||
            Keyboard.IsKeyDown(Key.PageUp) ||
            Keyboard.IsKeyDown(Key.Delete) ||
            Keyboard.IsKeyDown(Key.Back) ||
            Keyboard.IsKeyDown(Key.End) ||
            Keyboard.IsKeyDown(Key.Tab) ||
           Keyboard.IsKeyDown(Key.F1) ||
             Keyboard.IsKeyDown(Key.F2) ||
            Keyboard.IsKeyDown(Key.F3) ||
            Keyboard.IsKeyDown(Key.F4) ||
            Keyboard.IsKeyDown(Key.F5) ||
            Keyboard.IsKeyDown(Key.F6) ||
            Keyboard.IsKeyDown(Key.F7) ||
            Keyboard.IsKeyDown(Key.F8) ||
            Keyboard.IsKeyDown(Key.F9) ||
            Keyboard.IsKeyDown(Key.F10) ||
            Keyboard.IsKeyDown(Key.F11) ||
            Keyboard.IsKeyDown(Key.F12) ||
            Keyboard.IsKeyDown(Key.Scroll) ||
                Keyboard.IsKeyDown(Key.PrintScreen) ||
            Keyboard.IsKeyDown(Key.Insert)||
            Keyboard.IsKeyDown(Key.System) ||
            Keyboard.IsKeyDown(Key.Escape));
       
        internal static void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if(!textBox.IsEnabled) { e.Handled = true; return; }
            if (textBox.IsReadOnly) { e.Handled = true; return; }   
            if (e.ValidateDigit() && !IsControl) e.Handled = true;

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            var caret = textBox.CaretIndex;

            if ((e.IsLetter() && !IsControl) && (textBox.SelectionLength > 0 || string.IsNullOrEmpty(textBox.Text) || Convert.ToDecimal(textBox.Text) <= 0m))
            {
                e.Handled = true;
            }
            else
            if (textBox.SelectionLength > 0 && (e.IsBackOrDeleteKey() || (e.IsDigit() && !IsControl)))
            {
                textBox.SelectionClear();
                textBox.CaretIndex = caret;
            }

            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            if (e.Key == Key.Back && textBox.CaretIndex == textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.NegativeSign) + 1 && type == TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Delete && textBox.CaretIndex == textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.NegativeSign) && !string.IsNullOrEmpty(textBox.Text) &&  type == TextBoxFieldAssistOM.FormatTextBox.DigitsNegative)
            {
                textBox.CaretIndex = 1;
            }


        }

        public static bool ValidateDigit(this System.Windows.Input.KeyEventArgs e)
        {
            return e.Key != Key.NumPad0 && e.Key != Key.NumPad1 && e.Key != Key.NumPad2 && e.Key != Key.NumPad3 && e.Key != Key.NumPad4 &&
                   e.Key != Key.NumPad5 && e.Key != Key.NumPad6 && e.Key != Key.NumPad7 && e.Key != Key.NumPad8 && e.Key != Key.NumPad9 &&
                   e.Key != Key.D0 && e.Key != Key.D1 && e.Key != Key.D2 && e.Key != Key.D3 && e.Key != Key.D4 && e.Key != Key.D5 && e.Key != Key.D6 &&
                   e.Key != Key.D7 && e.Key != Key.D8 && e.Key != Key.D9 && e.Key != Key.OemPlus && e.Key != Key.OemMinus &&
                   e.Key != Key.Up && e.Key != Key.Down && e.Key != Key.Right && e.Key != Key.Left && !e.IsControl() && e.Key != Key.Back && e.Key != Key.Delete;
        }

        public static bool IsDigit(this KeyEventArgs e)
        {
            return e.Key >= Key.NumPad0 &&
                   e.Key <= Key.NumPad9 ||
                   e.Key >= Key.D0 && 
                   e.Key <= Key.D9;
        }
        public static long ToLong(this string value)
        {
            return System.Convert.ToInt64(value);
        }

    }
}
