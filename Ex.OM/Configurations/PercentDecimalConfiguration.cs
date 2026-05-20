using Ex.OM.Extentions;
using Ex.OM.Extentions.Converts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ex.OM.Configurations
{
    public static class PercentDecimalConfiguration
    {
        public static void AddPercentDecimal(this TextBox textBox)
        {
            var caret = textBox.CaretIndex;
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var data = textBox.Text.PercentDecimal(decimals, position);

            textBox.Text = data.data;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, data.decimalData);
            textBox.CaretIndex = caret;

            textBox.PreviewTextInput += PreviewTextInputPercentDecimal;
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.Loaded += TextBox_Loaded;
            DataObject.AddPastingHandler(textBox, OnPaste);
        }

        public static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            var isFraction = (bool)textBox.GetValue(TextBoxFieldAssistOM.IsFractionValueBackProperty);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            var conv = new ConvertPercentDecimalOM();
           
            textBox.LoadConvert(conv);
            //textBox.LoadConvertPercent();
        }

        public static void RemovePercenDecimal(this TextBox textBox)
        {
            textBox.PreviewTextInput -= PreviewTextInputPercentDecimal;
            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            textBox.LostFocus -= TextBox_LostFocus;

            DataObject.RemovePastingHandler(textBox, OnPaste);
        }
        public static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);


            if (type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
            {
                textBox.Text = textBox.Text.PercentDecimalPositive(decimals, position).data;

            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalNegative)
            {
                textBox.Text = textBox.Text.PercentDecimalNegative(decimals, position).data;

            }
            else
            {
                textBox.Text = textBox.Text.PercentDecimal(decimals, position).data;

            }
            var originalValue = (System.Convert.ToDecimal(textBox.Text.Nvl("0").Digits()) / 100).ToString();
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, originalValue);
        }
        public static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var data = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            var xtract = data.ExtracDecimal(Methods.CultureInfo.NumberFormat.NumberDecimalSeparator);
            var decimalNumber = System.Convert.ToInt32((textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber)));
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);


            DataObject d = new DataObject();

            if (type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimal)
            {
                var options = xtract.Decimals(decimalNumber);
                d.SetData(DataFormats.Text, options.data.PercentDecimal(decimalNumber, position));

            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
            {
                var options = xtract.PercentDecimalPositive(decimalNumber, position);
                d.SetData(DataFormats.Text, options.data);

            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalNegative)
            {
                var options = xtract.PercentDecimalNegative(decimalNumber, position);
                d.SetData(DataFormats.Text, options.data);

            }

            e.DataObject = d;
            //if (!(sender is TextBox textBox)) return;
            //var options = textBox.InitializeOnPaste(e);
            //var typeDecimal = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            //if (options.isNotText) return;
            //var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
            //var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);


            //if (typeDecimal == TextBoxFieldAssistOM.FormatTextBox.PercentDecimal)
            //{
            //    var data = textBox.Text.PercentDecimal(decimals, position);
            //    textBox.Text = data.data;
            //    textBox.CaretIndex = options.caret + options.data.Length;
            //}
            //else if (typeDecimal == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
            //{
            //    var data = textBox.Text.PercentDecimalPositive(decimals, position);
            //    textBox.Text = data.data;
            //    textBox.CaretIndex = options.caret + options.data.Length;
            //}
            //else if (typeDecimal == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalNegative)
            //{

            //}

            //e.CancelCommand();
        }

        public static void PreviewTextInputPercentDecimal(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (e.Text == "-" && string.IsNullOrEmpty(textBox.Text)) { e.Handled = true; return; }
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, textBox.CaretIndex);

            var caret = textBox.CaretIndex;
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            //detect symbol negative
            if (e.Text == Methods.CultureInfo.NumberFormat.NegativeSign)
            {
                if (!textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
                {
                    var _data = textBox.Text;
                    _data = _data.Insert(0, Methods.CultureInfo.NumberFormat.NegativeSign);

                    if(type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
                    {
                        var data = _data.PercentDecimalNegative(decimals, position);

                        textBox.Text = data.data;
                        textBox.CaretIndex = data.isValid ? caret : 0;
                    }
                    else if(type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
                    {
                        var data = _data.PercentDecimalPositive(decimals, position);

                        textBox.Text = data.data;
                        textBox.CaretIndex = data.isValid ? caret : 0;
                    }
                    else
                    {
                        var data = _data.PercentDecimal(decimals, position);

                        textBox.Text = data.data;
                        textBox.CaretIndex = data.isValid ? caret : 0;
                    }
                  
                }
                e.Handled = true;

            }
            else if (e.Text == Methods.CultureInfo.NumberFormat.PositiveSign)
            {
                if (textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
                {
                    var _data = textBox.Text.Replace(Methods.CultureInfo.NumberFormat.NegativeSign, string.Empty);


                    if (type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
                    {
                        var data = _data.PercentDecimal(decimals, position);
                        textBox.Text = data.data;
                        textBox.CaretIndex = data.isValid ? caret : 0;
                    }
                    else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
                    {
                        var data = _data.PercentDecimal(decimals, position);
                        textBox.Text = data.data;
                        textBox.CaretIndex = data.isValid ? caret : 0;
                    }
                    else
                    {
                        var data = _data.PercentDecimal(decimals, position);
                        textBox.Text = data.data;
                        textBox.CaretIndex = data.isValid ? caret : 0;
                    }
                   
                }
                e.Handled = true;

            }
            else if (e.Text == Methods.CultureInfo.NumberFormat.NumberDecimalSeparator)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.CaretIndex = textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.NumberDecimalSeparator) + 1;
                   
                }
                e.Handled = true;
            }
            else
            {
                if (!e.Text.IsDigit())
                {
                    e.Handled = true;
                }
            }


        }

        public static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (!textBox.IsEnabled) { e.Handled = true; return; }
            if (textBox.IsReadOnly) { e.Handled = true; return; }
            if (e.Key == System.Windows.Input.Key.Space)
            {
                e.Handled = true;
            }


            if (textBox.SelectionLength > 0 || e.IsBackOrDeleteKey())
            {
                var decimalNumber = System.Convert.ToInt32(textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber));
                textBox.SelectionClear();
                textBox.Text = textBox.Text;
            }
        }
    }
}
