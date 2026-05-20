using Ex.OM.Extentions;
using Ex.OM.Extentions.Converts;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ex.OM.Configurations
{
    public static class MoneyConfiguration
    {
        public static void AddMoney(this TextBox textBox)
        {
            var caret = textBox.CaretIndex;
            var typeTextBox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);


            if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyPositive)
            {
                var data = textBox.Text.MoneyPositive(showSymbol);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
            else if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative)
            {
                var data = textBox.Text.MoneyNegative(showSymbol);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
            else
            {
                var data = textBox.Text.Money(showSymbol);
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
            textBox.LoadConvertMoney();


        }
        public static void LoadConvertMoney(this TextBox textBox)
        {
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            if (type == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative)
            {
                textBox.LoadConvert(new ConvertMoneyNegativeInteger { TextBox = textBox });
            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.MoneyPositive)
            {
                textBox.LoadConvert(new ConvertMoneyPositiveInteger { TextBox = textBox });

            }
            else
            {
                textBox.LoadConvert(new ConvertMoneyInteger { TextBox = textBox });
            }
        }


        public static void RemoveMoney(this TextBox textBox)
        {
            textBox.PreviewTextInput -= TextBox_PreviewTextInput;
            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            textBox.Loaded -= TextBox_Loaded;
            textBox.PreviewKeyUp -= TextBox_PreviewKeyUp;
            textBox.LostFocus -= TextBox_LostFocus;

            DataObject.RemovePastingHandler(textBox, OnPaste);
        }



        public static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {

            if (!(sender is TextBox textBox)) return;
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);


            var caret = textBox.CaretIndex;
            var typeTextBox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyPositive)
            {
                var data = textBox.Text.MoneyPositive(showSymbol);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
            else if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative)
            {
                var data = textBox.Text.MoneyNegative(showSymbol);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
            else
            {
                var data = textBox.Text.Money(showSymbol);
                textBox.Text = data.data;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.originalValue);
            }
        }

        public static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            var typeTextBox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);

            var caret = textBox.CaretIndex;

            if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyPositive)
            {
                var data = textBox.Text.MoneyPositive(showSymbol);
                textBox.Text = data.data;
            }
            else if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative)
            {
                var data = textBox.Text.MoneyNegative(showSymbol);
                textBox.Text = data.data;
            }
            else
            {
                var data = textBox.Text.Money(showSymbol);
                textBox.Text = data.data;
            }


            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, textBox.Text.TryInteger().ToString());
            textBox.CaretIndex = caret;
        }

        public static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var negative = Methods.CultureInfo.NumberFormat.NegativeSign;
            var typeTextBox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);


            if (textBox.Text.DigitsPositive().Length == textBox.MaxLength && textBox.MaxLength > 0)
            {
                e.Handled = true;
                return;
            }
            if (e.Text == negative
                && string.IsNullOrEmpty(textBox.Text) && typeTextBox != TextBoxFieldAssistOM.FormatTextBox.MoneyPositive)
            {
                e.Handled = true;
                return;
            }
            var dataOriginal = string.Empty;
            var caret = textBox.CaretIndex;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
            var cIndex = string.IsNullOrEmpty(textBox.Text) ? Methods.CultureInfo.NumberFormat.CurrencySymbol.Length : 0;


            if (e.Text.Equals(Methods.CultureInfo.NumberFormat.NegativeSign, StringComparison.CurrentCulture) && typeTextBox != TextBoxFieldAssistOM.FormatTextBox.MoneyPositive)
            {
                textBox.Text = Regex.Replace(textBox.Text, Methods.CultureInfo.NumberFormat.NegativeSign, string
                     .Empty, RegexOptions.CultureInvariant);
                //textBox.Text = textBox.Text.Replace(Methods.CultureInfo.NumberFormat.NegativeSign, string.Empty);
                textBox.Text = textBox.Text.Insert(0, Methods.CultureInfo.NumberFormat.NegativeSign);
                textBox.CaretIndex = caret + 1;
                e.Handled = true;
                return;

            }
            if (e.Text == Methods.CultureInfo.NumberFormat.PositiveSign && typeTextBox == TextBoxFieldAssistOM.FormatTextBox.Money)
            {

                caret = textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign) ? textBox.CaretIndex - 1 : textBox.CaretIndex;
                textBox.Text = textBox.Text.Replace(Methods.CultureInfo.NumberFormat.NegativeSign, string.Empty);
                textBox.CaretIndex = caret >= 0 ? caret : 1;

                goto OriginalValue;

            }

            if (!e.Text.IsDigit())
            {

                e.Handled = true;
                return;
            }
            var caret_tmp = 0;
            if (e.Text.IsDigit() && Convert.ToDecimal(textBox.Text.Digits().Nvl("0")) == 0 && typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative)
            {
                caret_tmp++;
            }
            textBox.Text = textBox.Text.Insert(caret, e.Text);

            if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyPositive)
            {
                var data = textBox.Text.MoneyPositive(showSymbol);
                dataOriginal = data.originalValue;

                textBox.Text = data.data;
            }
            else if (typeTextBox == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative)
            {
                var data = textBox.Text.MoneyNegative(showSymbol);
                dataOriginal = data.originalValue;
                textBox.Text = data.data;
            }
            else
            {
                var data = textBox.Text.Money(showSymbol);
                dataOriginal = data.originalValue;

                textBox.Text = data.data;

            }

            var cal = textBox.Text.DigitsPositive().Length;
            cIndex += ((cal - 1) % 3 == 0 && (cal - 1) > 0) ? 2 : 1;

            textBox.CaretIndex = caret + cIndex + caret_tmp;

        OriginalValue:

            dataOriginal = string.IsNullOrEmpty(dataOriginal) ? textBox.Text.Money(showSymbol).originalValue : dataOriginal;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, dataOriginal);
            e.Handled = true;
        }

        public static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            //var options = textBox.InitializeOnPaste(e);
            var typetextbox = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var data = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            DataObject d = new DataObject();


            if (typetextbox == TextBoxFieldAssistOM.FormatTextBox.Money)
            {
                d.SetData(DataFormats.Text, data.Money().data);
            }
            else if (typetextbox == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative)
            {
                d.SetData(DataFormats.Text, data.MoneyNegative().data);
            }
            else if (typetextbox == TextBoxFieldAssistOM.FormatTextBox.MoneyPositive)
            {
                d.SetData(DataFormats.Text, data.MoneyPositive().data);
            }
            e.DataObject = d;


            //if (options.isNotText) return;

            //if (typetextbox == TextBoxFieldAssistOM.FormatTextBox.Money)
            //{
            //    var data = textBox.Text.Money();
            //    textBox.Text = data.data;
            //    textBox.CaretIndex = options.caret + options.data.Length;
            //}
            //else if (typetextbox == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative)
            //{
            //    var data = textBox.Text.MoneyNegative();
            //    textBox.Text = data.data;
            //    textBox.CaretIndex = options.caret + options.data.Length;
            //}

        }

        public static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (!(sender is TextBox textBox)) return;
            if (!textBox.IsEnabled) { e.Handled = true; return; }
            if (textBox.IsReadOnly) { e.Handled = true; return; }
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, textBox.CaretIndex);
            var caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);
            var caretGroupSeparator = textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.CurrencyGroupSeparator, (caret > 0 ? caret - 1 : caret));
            var caretCurrentSymbol = textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.CurrencySymbol);
            var type  = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }

            if ((e.IsArithmetic() || (e.IsLetter() && !DigitConfiguration.IsControl)) && textBox.SelectionLength > 0)
            {
                e.Handled = true;
            }
            if ((textBox.SelectionLength > 0 || e.IsBackOrDeleteKey()) && !DigitConfiguration.IsControl)
            {
                textBox.SelectionClear();
                textBox.CaretIndex = caret;
            }

            if (e.Key == Key.Back && caretCurrentSymbol + 1 == caret)
            {
                e.Handled = true;
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
            if(type == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative && (e.Key == Key.OemPlus || e.Key == Key.Add))
            {
                e.Handled = true;
            }
            if(type == TextBoxFieldAssistOM.FormatTextBox.MoneyNegative && (e.Key == Key.OemMinus || e.Key == Key.Subtract) && textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                e.Handled = true;
            }
        }

        public static void TextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);
            var symbol = Methods.CultureInfo.NumberFormat.CurrencySymbol;
            var negative = Methods.CultureInfo.NumberFormat.NegativeSign;

            if ((e.Key == Key.Back || e.Key == Key.Delete) && (textBox.Text.Equals(symbol) || textBox.Text.Equals(negative+symbol) || textBox.Text.Equals(negative)))
            {
                textBox.Text = default;
            }
        }
    }
}