using Ex.OM.Extentions;
using Ex.OM.Extentions.Converts;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ex.OM.Configurations
{
    public static class MoneyDecimalConfiguration
    {
        public static void AddMoneyDecimal(this TextBox textBox)
        {
            var caret = textBox.CaretIndex;
            var typeTextBox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var numberDecimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalPositive)
            {
                var data = textBox.Text.MoneyPositiveDecimal(numberDecimals);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
            else if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalNegative)
            {
                var data = textBox.Text.MoneyNegativeDecimal(numberDecimals);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
            else
            {

                var data = textBox.Text.MoneyDecimal(numberDecimals);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }


            textBox.CaretIndex = caret;
            textBox.PreviewTextInput += TextBox_PreviewTextInput;
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBox.PreviewKeyUp += TextBox_PreviewKeyUp;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.Loaded += TextBox_Loaded;
            DataObject.AddPastingHandler(textBox, OnPaste);
            textBox.LoadConvertMoneyDecimal();

        }


        public static void RemoveMoneyDecimal(this TextBox textBox)
        {
            textBox.PreviewTextInput -= TextBox_PreviewTextInput;
            textBox.PreviewKeyUp -= TextBox_PreviewKeyUp;
            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            textBox.Loaded -= TextBox_Loaded;
            textBox.LostFocus -= TextBox_LostFocus;
            DataObject.RemovePastingHandler(textBox, OnPaste);
        }
        public static bool IsValidBinding(this TextBox textBox)
        {
            var binding = textBox.GetBindingExpression(TextBox.TextProperty);
            return binding.IsNotNull();
        }
        public static void LoadConvertMoneyDecimal(this TextBox textBox)
        {
            var numberDecimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);

            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);

            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            if (type == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalNegative)
            {
                textBox.LoadConvert(new ConvertMoneyDecimal { NumberDecimals = numberDecimals, TextBox = textBox, Money = ConvertMoneyDecimal.TypeMoney.NegativeDecimal, ShowSymbol = showSymbol });
            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalPositive)
            {
                textBox.LoadConvert(new ConvertMoneyDecimal { NumberDecimals = numberDecimals, TextBox = textBox, Money = ConvertMoneyDecimal.TypeMoney.PositiveDecimal, ShowSymbol = showSymbol });
            }
            else
            {
                textBox.LoadConvert(new ConvertMoneyDecimal { NumberDecimals = numberDecimals, TextBox = textBox, Money = ConvertMoneyDecimal.TypeMoney.MoneyDecimal, ShowSymbol = showSymbol });
            }
        }
        public static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var numberDecimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);

            textBox.LoadConvertMoneyDecimal();

            var caret = textBox.CaretIndex;
            var typeTextBox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalPositive)
            {
                var data = textBox.Text.MoneyPositiveDecimal(numberDecimals, showSymbol);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
            else if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalNegative)
            {
                var data = textBox.Text.MoneyNegativeDecimal(numberDecimals, showSymbol);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
            else
            {

                var data = textBox.Text.MoneyDecimal(numberDecimals, showSymbol);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
        }

        public static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            var typeTextBox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var caret = textBox.CaretIndex;
            var numberDecimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);


            if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalPositive)
            {
                var data = textBox.Text.MoneyPositiveDecimal(numberDecimals, showSymbol);
                textBox.Text = data.data;
            }
            else if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalNegative)
            {
                var data = textBox.Text.MoneyNegativeDecimal(numberDecimals, showSymbol);
                textBox.Text = data.data;
            }
            else
            {
                var data = textBox.Text.MoneyDecimal(numberDecimals, showSymbol);
                textBox.Text = data.data;
            }

            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, textBox.Text.Decimals().data.ToString());
            textBox.CaretIndex = caret;
        }

        public static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var negative = Methods.CultureInfo.NumberFormat.NegativeSign;
            var typeTextBox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var numberDecimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);
            var separator = Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator;
            var caret = textBox.CaretIndex;

            if (e.Text.Equals(Methods.CultureInfo.NumberFormat.PositiveSign) && typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimal && textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                textBox.Text = textBox.Text.Replace(Methods.CultureInfo.NumberFormat.NegativeSign, string.Empty);
                textBox.CaretIndex = caret > 0 ?(caret - 1) : 1;
                e.Handled = true;
                return;
            }

            if (e.Text.Equals(Methods.CultureInfo.NumberFormat.NegativeSign) && typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimal && !textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                caret = caret > 0 ? (caret ) : 2;
                e.Handled = true;
            }

            if (textBox.Text.DigitsPositive().Length == textBox.MaxLength && textBox.MaxLength > 0)
            {
                e.Handled = true;
                return;
            }

            if (!e.Text.IsDigit() &&
               !e.Text.Equals(Methods.CultureInfo.NumberFormat.NegativeSign) &&
               !e.Text.Equals(Methods.CultureInfo.NumberFormat.PositiveSign) &&
               !e.Text.Equals(Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator)
               )
            {
                e.Handled = true;
                return;
            }
            var indexSeparator = textBox.Text.IndexOf(separator);
            var length = textBox.Text.Length;
            if (length == caret && e.Text.Equals(separator) && textBox.Text.IsNotNull())
            {
                textBox.CaretIndex = indexSeparator + 1;
                e.Handled = true;
                return;
            }

            if (length == caret && e.Text.Equals(separator) && textBox.Text.IsNull())
            {
                var _string = $"0{Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator}";
                textBox.Text = _string;
                var _index = textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator);
                _index++;
                textBox.CaretIndex = _index;
                e.Handled = true;
                return;
            }

            if (e.Text.Equals(separator) && indexSeparator < caret)
            {
                textBox.CaretIndex = indexSeparator + 1;
                e.Handled = true;
                return;
            }

            if (e.Text.IsDigit() && textBox.Text.Contains(Methods.CultureInfo.NumberFormat.CurrencySymbol) && caret == 0)
            {
                textBox.Text = textBox.Text.Insert(caret, e.Text);
                textBox.CaretIndex = textBox.CaretIndex + 2;

                e.Handled = true;
                return;
            }

            var text = textBox.Text.Insert(caret, e.Text);



            if (string.IsNullOrEmpty(textBox.Text) && showSymbol && !textBox.Text.Contains(separator))
            {
                caret += 1;
            }
            if (textBox.Text.IsNull() && typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalNegative && (e.Text.IsDigit() || e.Text.Equals(negative)))
            {
                caret += 1;
            }
            if (!textBox.Text.Contains(negative) && typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalNegative && text.IsNotNull() && text.ToDecimal() > 0)
            {
                caret += 1;
            }

            if (textBox.IsValidBinding())
            {
                textBox.Text = text;
            }
            else
            {
                var data = text.MoneyPositiveDecimal(numberDecimals, showSymbol);
                textBox.Text = data.data;
            }
          

            if (e.Text.Equals(separator) && indexSeparator >= caret)
            {
                textBox.CaretIndex = caret + 1;
                e.Handled = true;
                return;
            }

            if (e.Text.IsDigit() && indexSeparator < caret)
            {
                textBox.CaretIndex = caret + 1;
                e.Handled = true;
                return;
            }


            if (textBox.IsValidBinding())
            {
                textBox.Text = text;
            }
            else
            {
                var data = text.MoneyPositiveDecimal(numberDecimals, showSymbol);
                textBox.Text = data.data;
            }
            var cal = textBox.Text.DigitIntegerLength();
            var caretCal = (((cal - 1) % 3 == 0 && (cal - 1) > 0) ? 2 : 1);


            textBox.CaretIndex = caret + caretCal;

            e.Handled = true;


        }
        public static bool IsNull(this object value)
        {
            if ((value is string _string))
            {
                return string.IsNullOrEmpty(_string);
            }
            else
            {
                return value == null;

            }
        }
        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }

        public static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {

            if (!(sender is TextBox textBox)) return;
            //var options = textBox.InitializeOnPaste(e);
            var typetextbox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var data = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            DataObject d = new DataObject();


            if (typetextbox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimal)
            {
                d.SetData(DataFormats.Text, data.MoneyDecimal().data);
            }
            else if (typetextbox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalNegative)
            {
                d.SetData(DataFormats.Text, data.MoneyNegativeDecimal().data);
            }
            else if (typetextbox == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalPositive)
            {
                d.SetData(DataFormats.Text, data.MoneyPositiveDecimal().data);
            }
            e.DataObject = d;

        }

        public static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (!textBox.IsEnabled) { e.Handled = true; return; }
            if (textBox.IsReadOnly) { e.Handled = true; return; }
            var caretCurrentSymbol = textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.CurrencySymbol);
            var tipo = ((TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty));
            var caret = textBox.CaretIndex;

            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
            var caretGroupSeparator = textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.CurrencyGroupSeparator, (caret > 0 ? caret - 1 : caret));
            var caretSeparator = textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator);

            if(e.Key == Key.Back && textBox.Text.ToDecimal() == 0)
            {
                textBox.Text = default;
                e.Handled = true;
            }

            if (e.Key == Key.Back && caretSeparator + 1 == caret && caret > 0)
            {
                textBox.CaretIndex -= 1;
            }

            if (e.Key == Key.Delete && caretSeparator == caret)
            {
                textBox.CaretIndex += 1;
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if ((e.IsArithmetic() || (e.IsLetter() && !DigitConfiguration.IsControl)) && textBox.SelectionLength > 0)
            {
                e.Handled = true;
            }

            if ((textBox.SelectionLength > 0 && e.IsBackOrDeleteKey()) && !DigitConfiguration.IsControl)
            {
                textBox.SelectionClear();
                textBox.CaretIndex = caret;
            }
            if (textBox.SelectionLength > 0 && e.Key == Key.Decimal)
            {
                textBox.SelectionClear();
                textBox.CaretIndex = caret;
            }

            if (textBox.SelectionLength > 0 && (e.IsDigit() && !DigitConfiguration.IsControl))
            {
                textBox.SelectionClear();
                textBox.CaretIndex = caret;
            }

            var caretNegative = textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.NegativeSign);
            if (e.Key == Key.Delete && caretCurrentSymbol == caret && textBox.Text.Length > 1)
            {
                textBox.CaretIndex += 1;
            }
            else if (e.Key == Key.Delete && caretNegative == caret && textBox.Text.Length > 2)
            {
                textBox.CaretIndex += 2;
            }
            else if (e.Key == Key.Delete && caretCurrentSymbol == caret)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Back && (caretGroupSeparator + 1) == caret)
            {
                textBox.CaretIndex -= caret == 0 ? 0 : 1;
            }

            if ((e.Key == Key.Add || e.Key == Key.OemPlus) && tipo == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalNegative)
            {
                e.Handled = true;
            }
            if ((e.Key == Key.OemMinus || e.Key == Key.Subtract) && tipo == TextBoxFieldAssistOM.FormatTextBox.MoneyDecimalPositive)
            {
                e.Handled = true;
            }
            if ((e.Key == Key.OemMinus || e.Key == Key.Subtract) && textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                e.Handled = true;
            }

            if ((e.Key == Key.OemPlus || e.Key == Key.Add) && !textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                e.Handled = true;
            }
            if (e.Key == Key.OemMinus || e.Key == Key.Subtract && textBox.Text.ToDecimal() == 0)
            {
                e.Handled = true;
            }
       
        }

        public static decimal? ToDecimal(this object value)
        {
            try
            {
                return Convert.ToDecimal(value.ToString().Decimals().data);
            }
            catch
            {
                return null;
            }
        }

        static bool IsArrowOrReturn(KeyEventArgs e)
        {
            return e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right;
        }


        public static void TextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            var caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);


        }


    }
}
