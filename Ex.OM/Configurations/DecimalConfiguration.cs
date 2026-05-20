using Ex.OM.Extentions;
using Ex.OM.Extentions.Converts;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ex.OM.Configurations
{
    public static class DecimalConfiguration
    {
        public static void AddDecimal(this TextBox textBox)
        {
            var caret = textBox.CaretIndex;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
            var decimals = System.Convert.ToInt32(textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber));
            var data = textBox.Text.DecimalPositive(decimals);
            textBox.Text = data.data.Truncate(textBox.MaxLength);


            var options = textBox.Text.Decimals(decimals);
            textBox.Text = options.data;

            caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);

            textBox.CaretIndex = caret;
            textBox.PreviewTextInput += TextBox_PreviewTextInput;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.PreviewKeyDown+= TextBox_PreviewKeyDown;
            textBox.Loaded += TextBox_Loaded;
            DataObject.AddPastingHandler(textBox, OnPaste);
            textBox.LoadConvertDecimal();

        }



        public static void RemoveDecimal(this TextBox textBox)
        {
            textBox.PreviewTextInput -= TextBox_PreviewTextInput;
            textBox.LostFocus -= TextBox_LostFocus;
            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            textBox.Loaded -= TextBox_Loaded;

            DataObject.RemovePastingHandler(textBox, OnPaste);
        }
        public static void LoadConvertDecimal(this TextBox textBox)
        {
            var decimals = System.Convert.ToInt32(textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber));
            textBox.LoadConvert(new ConvertDecimal { NumberDecimal = decimals });
        }
        public static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            var decimals = System.Convert.ToInt32(textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber));

            var data = textBox.Text.Decimals(decimals);

            if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsPositive)
            {
                data = textBox.Text.DecimalPositive(decimals);
            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative)
            {
                data = textBox.Text.DecimalNegative(decimals);
            }

            textBox.Text = data.data.Truncate(textBox.MaxLength);
            textBox.LoadConvertDecimal();


        }
        internal static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var data = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            var xtract = data.ExtracDecimal(Methods.CultureInfo.NumberFormat.NumberDecimalSeparator);
            var decimalNumber = System.Convert.ToInt32((textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber)));

            DataObject d = new DataObject();

            if (type == TextBoxFieldAssistOM.FormatTextBox.Decimals)
            {
                var options = xtract.Decimals(decimalNumber);
                d.SetData(DataFormats.Text, options.data);

            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsPositive)
            {
                var options = xtract.DecimalPositive(decimalNumber);
                d.SetData(DataFormats.Text, options.data);

            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative)
            {
                var options = xtract.DecimalNegative(decimalNumber);
                d.SetData(DataFormats.Text, options.data);

            }

            e.DataObject = d;

           
        }

        public static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;


            var caret = textBox.CaretIndex;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
            var numberInteger = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxNumberIntegers);
            var numberDecimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative && e.Text.IsDigit() && string.IsNullOrEmpty(textBox.Text))
            {
                caret += 1;
                //textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                textBox.CaretIndex = caret;
            }


            var distinct = type != TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative && type != TextBoxFieldAssistOM.FormatTextBox.DecimalsPositive;

            if (e.Text == Methods.CultureInfo.NumberFormat.NegativeSign && type == TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative)
            {
                if (!textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign) && distinct)
                {
                    textBox.Text = textBox.Text.DecimalNegative(numberDecimals).data;
                    caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);
                    caret += 1;
                    //textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                }
                textBox.CaretIndex = caret;
                e.Handled = true;
                return;
            }

            if (e.Text == Methods.CultureInfo.NumberFormat.PositiveSign && type == TextBoxFieldAssistOM.FormatTextBox.DecimalsPositive)
            {
                if (textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign) && distinct)
                {
                    textBox.Text = textBox.Text.DecimalPositive(numberDecimals).data;
                    caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);

                    caret -= 1;
                    //textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                }
                textBox.CaretIndex = caret;
                e.Handled = true;
                return;
            }

            var options = textBox.DetectSymbolDecimal(e);

            if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative && !textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = textBox.Text.Insert(0, Methods.CultureInfo.NumberFormat.NegativeSign);
                }

            }
            if (!options.isArithmeticSymbols)
            {


                var options3 = textBox.DetectDecimalNumber();
                if (e.Text != options3.numberSeparator)
                {
                    if (e.Text.IsDigit() == false && e.Text != Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator)
                    {
                        e.Handled = true;
                        return;
                    }
                    var point = textBox.Text;
                    var tmp_caret = 0;
                    var _caretInsert = textBox.CaretIndex;
                    textBox.Text = textBox.Text.Insert(textBox.CaretIndex, e.Text);

                    if (_caretInsert == 0 && e.Text.IsDigit() && textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
                    {
                        tmp_caret = _caretInsert + 1;
                    }
                   
                    caret += point == options3.numberSeparator ? 1 : 0;


                    var options2 = textBox.Text.Decimals(numberDecimals);

                    if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsPositive)
                    {
                        options2 = textBox.Text.DecimalPositive(numberDecimals);
                    }
                    else if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative)
                    {
                        options2 = textBox.Text.DecimalNegative(numberDecimals);
                    }


                    if (numberInteger > 0)
                    {

                        textBox.Text = options2.data.TruncateDecimal(numberInteger);
                    }
                    else
                    {
                        textBox.Text = options2.data.Truncate(textBox.MaxLength);
                    }
                    //caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);
                    caret = options2.isValid && textBox.Text == $"0{options3.numberSeparator}00" ?
                    caret + e.Text.Length + options3.numberSeparator.Length : caret + e.Text.Length;

                    if (textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator) + 1 == caret && e.Text.IsDigit())
                        caret -= 1;


                        caret = caret + tmp_caret;
                    //textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                    textBox.CaretIndex = caret;
                }
                else
                {
                    if (textBox.Text.Contains(options3.numberSeparator))
                    {
                        //caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);
                        caret = textBox.CaretIndex;

                        var _caret = textBox.Text.IndexOf(options3.numberSeparator);
                        if (_caret > caret)
                        {
                            textBox.Text = textBox.Text.Insert(textBox.CaretIndex, e.Text);
                            textBox.Text = textBox.Text.Decimals(numberDecimals).data;

                        }
                        else
                        {
                            caret = _caret;
                        }
                        caret = caret + 1;
                        //textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                        textBox.CaretIndex = caret;
                    }
                    else if (e.Text == Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator)
                    {

                        if (!textBox.Text.Contains(Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator) && !string.IsNullOrEmpty(textBox.Text) && !textBox.Text.Equals("0"))
                        {

                            textBox.Text = textBox.Text.Insert(caret, "0");
                            textBox.Text = textBox.Text.Insert((caret--), e.Text);
                            caret = caret + 1 + options3.numberSeparator.Length;
                            //textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                            textBox.CaretIndex = caret;

                        }
                        else if ((!textBox.Text.Contains(Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator) && string.IsNullOrEmpty(textBox.Text)) || textBox.Text.Equals("0"))
                        {
                            var _caret = (textBox.Text.Equals("0") ? 0 : 1);
                            textBox.Text = textBox.Text.Insert(caret, "0.01");
                            caret = caret + _caret + options3.numberSeparator.Length;
                            //textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                            textBox.CaretIndex = caret;

                        }

                    }
                }

            }
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, textBox.CaretIndex);
            e.Handled = true;
        }

        internal static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var decimalNumber = System.Convert.ToInt32(textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber));

            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            if (type == TextBoxFieldAssistOM.FormatTextBox.Decimals)
            {
                var options = textBox.Text.Decimals(decimalNumber);
                textBox.Text = options.data;

            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative)
            {
                var options = textBox.Text.DecimalNegative(decimalNumber);
                textBox.Text = options.data;
            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.DecimalsPositive)
            {
                var options = textBox.Text.DecimalPositive(decimalNumber);
                textBox.Text = options.data;
            }
        }

        internal static void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (!textBox.IsEnabled) { e.Handled = true; return; }
            if (textBox.IsReadOnly) { e.Handled = true; return; }
            //if (e.ValidateDigit()) e.Handled = true;

            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, textBox.CaretIndex);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            var caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if ((/*e.IsArithmetic() ||*/ e.IsLetter() && !DigitConfiguration.IsControl) && (textBox.SelectionLength > 0 || string.IsNullOrEmpty(textBox.Text) || Convert.ToDecimal(textBox.Text) <= 0m))
            {
                e.Handled = true;
            }
            else
            if (textBox.SelectionLength > 0 && (e.IsBackOrDeleteKey() || e.Key == Key.Decimal || (e.IsDigit() && !DigitConfiguration.IsControl)) )
            {
                textBox.SelectionClear();
                if(e.Key == Key.Decimal)
                {
                    caret += 1;
                }
                textBox.CaretIndex = caret;
            }
            if ((e.Key == Key.Add || e.Key == Key.OemPlus) && type == TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back  && type== TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative && textBox.CaretIndex == textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.NegativeSign) + 1)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Delete && type == TextBoxFieldAssistOM.FormatTextBox.DecimalsNegative && textBox.CaretIndex == textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                e.Handled = true;
            }

            if ((e.Key == Key.Subtract || e.Key == Key.OemMinus) && type == TextBoxFieldAssistOM.FormatTextBox.DecimalsPositive)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Back && !string.IsNullOrEmpty(textBox.Text) && textBox.CaretIndex == textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator) + 1)
            {
                textBox.CaretIndex -= 1;
            }
            if(e.Key == Key.Delete && !string.IsNullOrEmpty(textBox.Text) && textBox.CaretIndex == textBox.Text.IndexOf(Methods.CultureInfo.NumberFormat.CurrencyDecimalSeparator))
            {
                textBox.CaretIndex += 1;
            }

            if((e.Key == Key.Subtract || e.Key == Key.OemMinus) && textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                e.Handled = true;
            }
            if((e.Key == Key.Subtract || e.Key == Key.OemMinus) && !textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                if (e.IsDigit())
                {
                    if(Convert.ToDecimal(textBox.Text) != 0)
                    {
                        textBox.CaretIndex += 1;
                    }
                }
            }
            if ((e.Key == Key.Add || e.Key == Key.OemPlus) && textBox.Text.Contains(Methods.CultureInfo.NumberFormat.NegativeSign) && type == TextBoxFieldAssistOM.FormatTextBox.Decimals && Convert.ToDecimal(textBox.Text) != 0)
            {
                textBox.CaretIndex -= 1;
            }

        }

        public static void TextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            if ((e.Key == Key.Back || e.Key == Key.Delete) && textBox.Text.Equals(Methods.CultureInfo.NumberFormat.NegativeSign))
            {
                textBox.Text = default;
            }

        }
    }
}
