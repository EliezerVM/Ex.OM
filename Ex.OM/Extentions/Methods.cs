using Ex.OM.Configurations;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static Ex.OM.Extentions.TextBoxFieldAssistOM;

namespace Ex.OM.Extentions
{
    public static class Methods
    {
        public static CultureInfo CultureInfo { get; private set; } = Thread.CurrentThread.CurrentCulture.Clone() as CultureInfo;

        static NumberStyles style;

        public static TextInfo TextInfo { get; private set; } = CultureInfo.TextInfo;
        public static string RegexLetterMm { get; private set; }

        public static void RemoveExtensions(this TextBox textBox)
        {

            textBox.RemoveCapitalizer();
            textBox.RemoveCapitalizerFirst();
            textBox.RemoveUpper();
            textBox.RemoveLower();
            textBox.RemoveOnlyLetter();
            textBox.RemoveOnlyLetterLower();
            textBox.RemoveOnlyLetterUpper();
            textBox.RemoveOnlyLetterSpace();
            textBox.RemoveOnlyLetterLowerSpace();
            textBox.RemoveOnlyLetterUpperSpace();
            textBox.RemoveLetterNumber();
            textBox.RemoveLetterNumberSpace();
            textBox.RemoveTextWithoutSpace();
            textBox.RemoveOnlyletterCapitalizer();
            textBox.RemoveOnlyLetterCapitalizerFirst();
            textBox.RemoveOnlyLetterCapitalizerFirstSpace();
            textBox.RemoveOnlyLetterCapitalizerSpace();
            textBox.RemoveDigit();
            textBox.RemoveDigitPositve();
            textBox.RemoveDigitNegative();
            textBox.RemoveDecimal();
            textBox.RemoveDecimalPositive();
            textBox.RemoveDecimalNegative();
            textBox.RemovePercent();
            textBox.RemovePercentPositive();
            textBox.RemovePercentNegative();
            textBox.RemovePercenDecimal();
            textBox.RemovePercenDecimalPositive();
            textBox.RemovePercenDecimalNegative();
            textBox.RemoveMoney();
            textBox.RemoveMoneyNegative();
            textBox.RemoveMoneyPositive();
            textBox.RemoveMoneyDecimalNegative();
            textBox.RemoveMoneyDecimalPositive();
            textBox.RemoveMoneyDecimal();
            textBox.RemoveMask();

        }

        public static bool IsDigit(this string data)
        {
            return Regex.Match(data, @"\d|[\d+]$").Success;
        }
        public static string TruncateDecimal(this string data, int limit, int limitDecimals = 2)
        {
            var index = data.LastIndexOf(CultureInfo.NumberFormat.CurrencyDecimalSeparator);
            if (data.Length > limit && limit > 0)
            {
                index = index == -1 ? data.Length : index;
                var integers = data.Substring(0, index);
                var decimals = data.Substring(index, data.Length).Truncate(limitDecimals);

                integers = integers.Insert(integers.Length, CultureInfo.NumberFormat.CurrencyDecimalSeparator);
                data = integers.Insert(integers.Length, decimals);

                return data;
            }
            else
            {
                if (data.Length > 0 && index >= 0)
                {
                    var indexSeparator = data.IndexOf(CultureInfo.NumberFormat.CurrencyDecimalSeparator);
                    var integers = data.Substring(0, index);
                    var decimals = data.Remove(1, indexSeparator).Digits().Truncate(limitDecimals).Insert(0, CultureInfo.NumberFormat.CurrencyDecimalSeparator);
                    data = integers.Insert(integers.Length, decimals);
                    //data = tmp2.Insert(0, data);

                    //var integers = data.Substring(0, index);
                    //var decimals = data(index + 1, data.Length);
                    //decimals = decimals.Truncate(limitDecimals);

                    //integers = integers.Insert(integers.Length, CultureInfo.NumberFormat.CurrencyDecimalSeparator);
                    //data = integers.Insert(integers.Length, decimals);
                }
                return data;
            }
        }
        public static void LoadConvert(this TextBox textBox, IValueConverter convert)
        {
            var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);

            //var position = $"PositionSymbol:{textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol).ToString()}";

            if (bindingExpression != null)
            {
                var binding = bindingExpression.ParentBinding;

                Binding newBinding = new Binding
                {
                    //Source = binding.Source,
                    Mode = binding.Mode == BindingMode.Default ? BindingMode.TwoWay : binding.Mode,
                    Converter = convert,
                    AsyncState = binding.AsyncState,
                    BindingGroupName = binding.BindingGroupName,
                    BindsDirectlyToSource = binding.BindsDirectlyToSource,
                    ConverterCulture = binding.ConverterCulture,
                    ConverterParameter = binding.ConverterParameter,
                    Delay = binding.Delay,
                    FallbackValue = binding.FallbackValue,
                    IsAsync = binding.IsAsync,
                    NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated,
                    NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated,
                    NotifyOnValidationError = binding.NotifyOnValidationError,
                    Path = binding.Path,
                    StringFormat = binding.StringFormat,
                    TargetNullValue = binding.TargetNullValue,
                    ElementName = binding.ElementName,
                    UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter,
                    UpdateSourceTrigger = binding.UpdateSourceTrigger == UpdateSourceTrigger.Default ? UpdateSourceTrigger.PropertyChanged : binding.UpdateSourceTrigger,
                    ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
                    ValidatesOnExceptions = binding.ValidatesOnExceptions,
                    ValidatesOnNotifyDataErrors = binding.ValidatesOnNotifyDataErrors,
                };
                if (binding.ElementName.IsNull())
                {
                    //    newBinding.RelativeSource = binding.RelativeSource;
                    //newBinding.Source = binding.Source;
                }

                textBox.SetBinding(TextBox.TextProperty, newBinding);
            }
        }


        public static string Truncate(this string data, int limit)
        {
            if (data != null)
            {
                if (data.Length > limit && limit > 0)
                {
                    return data.Substring(0, limit);
                }
                else
                {
                    return data;
                }
            }
            else
            {
                return data;
            }
        }
        public static string DigitNegative(this string data)
        {
            var _string = data.Digits();
            if (!_string.Contains("-") && !string.IsNullOrEmpty(_string))
            {
                _string = string.Join("", _string.Prepend('-'));
            }
            return _string;
        }


        public static (string data, bool isValid) DecimalNegative(this string data, int decimals = 2)
        {
            if (string.IsNullOrEmpty(data)) return default;
            var _data = data.Decimals(decimals);

            _data.data = _data.data.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty).Insert(0, CultureInfo.NumberFormat.NegativeSign);
            return _data;

        }
        public static (string data, bool isValid) DecimalPositive(this string data, int decimals = 2)
        {
            var negative = CultureInfo.NumberFormat.NegativeSign;
            data = data.Replace(negative, string.Empty);
            var _data = data.Decimals(decimals);
            return _data;
        }

        public static (string data, bool isValid) Decimals(this string data, int decimals = 2)
        {
            var isNegativo = false;
            if (string.IsNullOrEmpty(data)) return ("", false);
            if (data.LastOrDefault() == CultureInfo.NumberFormat.CurrencyDecimalSeparator.ToCharArray().FirstOrDefault())
            {
                data = $"{data}0";
            }
            if (data.Contains(CultureInfo.NumberFormat.NegativeSign))
            {
                data = data.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
                isNegativo = true;
            }

            decimal currency;
            //string _regex = "[+-]?[0-9]{1,3}(?:,?[0-9]{3})*\\.[0-9]*|" + CultureInfo.NumberFormat.NegativeSign + @"?[0-9]\d*(\" + CultureInfo.NumberFormat.CurrencyDecimalSeparator + @"\d+)?";
            //string _regex = "[+-]?[0-9]{1,3}(?:,?[0-9]{3})*(?:"+CultureInfo.NumberFormat.CurrencyDecimalSeparator+"[0-9]*)?$";
            //string _regex = $"\\d+\\{CultureInfo.NumberFormat.CurrencyDecimalSeparator}\\d+|\\d+";
            string _regex = $@"(\d+)|(\{CultureInfo.NumberFormat.CurrencyDecimalSeparator})|(\d+)?";
            bool discovery = false;
            var matches = Regex.Matches(data, _regex);
            var _data = string.Join("", matches.Cast<Match>().Select(x =>
            {
                if (!discovery)
                {
                    var _value = x.Success && !discovery ? x.Value : string.Empty;
                    discovery = x.Value.Equals(CultureInfo.NumberFormat.CurrencyDecimalSeparator);
                    return _value;
                }
                else
                {
                    var _value = x.Success && !x.Value.Equals(CultureInfo.NumberFormat.CurrencyDecimalSeparator) ? x.Value : string.Empty;
                    return _value;
                }
            }));

            if (data.Contains(CultureInfo.NumberFormat.NegativeSign))
            {
                if (!_data.Contains(CultureInfo.NumberFormat.NegativeSign))
                {
                    _data = _data.Insert(0, CultureInfo.NumberFormat.NegativeSign);
                }
            }
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;


            decimals = decimals > 8 ? 8 : decimals;
            _data = _data.Truncate((28 - decimals));
            var valid = Decimal.TryParse(_data, style, CultureInfo, out currency);

            var _numberDecimal = Convert.ToInt64(10.ToString().PadRight(decimals + 1, '0'));
            currency = Math.Truncate(((currency * _numberDecimal))) / _numberDecimal;

            if (isNegativo)
                currency = currency * -1;

            if (!string.IsNullOrEmpty(_data))
                data = currency.ToString($"F{decimals}", CultureInfo);
            return (data, valid);
        }


        public static void RemoveSelectAllInFocus(this TextBox textBox)
        {
            textBox.GotKeyboardFocus -= TextBox_GotKeyboardFocus;
            textBox.GotFocus -= TextBox_GotFocus;
            textBox.PreviewMouseDown -= TextBox_PreviewMouseDown; 
            textBox.PreviewMouseDoubleClick -= TextBox_PreviewMouseDoubleClick;



        }
        public static void AddSelectAllInFocus(this TextBox textBox)
        {

            textBox.GotFocus += TextBox_GotFocus;
            textBox.GotKeyboardFocus += TextBox_GotKeyboardFocus;
            textBox.PreviewMouseDown += TextBox_PreviewMouseDown;
            textBox.PreviewMouseDoubleClick += TextBox_PreviewMouseDoubleClick;
        }

        private static void TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            e.Handled = true;
            textBox.Focus();
        }

        private static void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
           if(!textBox.IsFocused)
            e.Handled = true;
            //textBox.Focus();

        }



        private static void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            textBox.SelectAll();
        }

        private static void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            textBox.SelectAll();
        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            textBox.SelectAll();
        }

        public static string ExtracDecimal(this string data, string decimalSeparator)
        {
            var pattern = $@"[+-]?([0-9]+\{decimalSeparator}?[0-9]*|\{decimalSeparator}[0-9]+)";

            return string.Join("", Regex.Matches(data, pattern).Cast<Match>()
                .Select(x =>
                {
                    return x.Success ? x.Value : string.Empty;
                }));
        }

        public static (bool isArithmeticSymbols, int caret) DetectSymbolDecimal(this TextBox textBox, System.Windows.Input.TextCompositionEventArgs e, bool validateNegative = true)
        {
            var caret = textBox.CaretIndex;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);


            var isArithmeticSymbols = e.Text == Methods.CultureInfo.NumberFormat.NegativeSign || e.Text == Methods.CultureInfo.NumberFormat.PositiveSign;
            if (isArithmeticSymbols)
            {
                if (e.Text.StartsWith(Methods.CultureInfo.NumberFormat.NegativeSign))
                {
                    var index = textBox.Text.IndexOf(CultureInfo.NumberFormat.PositiveSign);
                    if (index >= 0)
                        textBox.Text = textBox.Text.Remove(index, index + 1);
                    if (!textBox.Text.Contains(CultureInfo.NumberFormat.PositiveSign))
                    {


                        textBox.Text = textBox.Text.Insert(0, CultureInfo.NumberFormat.NegativeSign);
                        caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);
                        textBox.CaretIndex = caret;
                    }
                }
                else if (validateNegative)
                {
                    var index = textBox.Text.IndexOf(CultureInfo.NumberFormat.NegativeSign);
                    if (index >= 0)
                        textBox.Text = textBox.Text.Remove(index, index + 1);

                    textBox.CaretIndex = caret;

                }
            }

            return (isArithmeticSymbols, caret);
        }

        public static (int decimalNumber, string numberSeparator) DetectDecimalNumber(this TextBox textBox)
        {
            var decimalNumber = System.Convert.ToInt32((textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber)));
            var numberSeparator = Methods.CultureInfo.NumberFormat.NumberDecimalSeparator;
            return (decimalNumber, numberSeparator);
        }



        public static string Xtract(this string data)
        {
            return data;
        }
        public static string Nvl(this string data, string replace)
        {
            if (string.IsNullOrEmpty(data)) return replace;
            else return data;
        }
        public static T Nvl<T>(this T data, T value)
        {
            return data == null ? value : data;
        }

        public static T Nvl<T>(this T data, T value, T modify)
        {
            return data == null ? modify : data;
        }

        public static (string data, string originalValue, bool isValid) MoneyNegativeDecimal(this string data, int decimals = 2, bool showSymbol = true)
        {
            if (string.IsNullOrEmpty(data)) return default;
            data = data.DecimalNegative().data;

            return data.MoneyDecimal(decimals, showSymbol);
        }
        public static (string data, string originalValue, bool isValid) MoneyDecimal(this string data, int decimals = 2, bool showSymbol = true)
        {
            if (string.IsNullOrEmpty(data)) return default;
            decimal currency;
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol | NumberStyles.Any;
            var cultura = CultureInfo.Clone() as CultureInfo;



            if (data.Contains(CultureInfo.NumberFormat.NegativeSign))
            {
                data = data.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
                data = data.Insert(0, CultureInfo.NumberFormat.NegativeSign);
            }


            var valid = Decimal.TryParse(data.Decimals(decimals).data, style, cultura, out currency);

            data = currency.ToString((showSymbol ? $"C{decimals}" : $"N{decimals}"), cultura);


            return (data, currency.ToString(), valid);
        }
        public static (string data, string originalValue, bool isValid) MoneyPositiveDecimal(this string data, int decimals = 2, bool showSymbol = true)
        {
            if (string.IsNullOrEmpty(data)) return default;
            //if (data.IndexOf(CultureInfo.NumberFormat.NegativeSign, StringComparison.CurrentCulture) >= 0)
            //{
            data = data.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
            //}
            return data.MoneyDecimal(decimals, showSymbol);
        }
        public static (string data, string originalValue, bool isValid) MoneyNegative(this string data, bool showSymbol = true)
        {
            if (string.IsNullOrEmpty(data)) return default;
            if (data.IndexOf(CultureInfo.NumberFormat.NegativeSign, StringComparison.CurrentCulture) >= 0)
            {
                data.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
                data = data.Insert(0, CultureInfo.NumberFormat.NegativeSign);
            }
            else
            {
                data = data.Insert(0, CultureInfo.NumberFormat.NegativeSign);
            }
            return data.Money(showSymbol);
        }
        public static (string data, string originalValue, bool isValid) MoneyPositive(this string data, bool showSymbol = true)
        {
            if (string.IsNullOrEmpty(data)) return default;
            if (data.IndexOf(CultureInfo.NumberFormat.NegativeSign, StringComparison.CurrentCulture) >= 0)
            {
                data.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
            }
            return data.Money(showSymbol);
        }
        public static int IndexMoneyInverse(this TextBox textBox, int caret, bool state = false)
        {
            return caret;
        }

        public static Int64 TryInteger(this string data)
        {
            Int64 currency;
            var cultura = CultureInfo.Clone() as CultureInfo;
            Int64.TryParse(data, style, cultura, out currency);
            return currency;
        }
        public static (string data, string originalValue, bool isValid) Money(this string data, bool showSymbol = true)
        {
            if (string.IsNullOrEmpty(data)) return ("", "", false);
            Int64 currency;
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol | NumberStyles.Any;
            var cultura = CultureInfo.Clone() as CultureInfo;



            if (data.Contains(CultureInfo.NumberFormat.NegativeSign))
            {
                data = data.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
                data = data.Insert(0, CultureInfo.NumberFormat.NegativeSign);
            }


            var valid = Int64.TryParse(data, style, cultura, out currency);
            if (!valid)
            {
                valid = Int64.TryParse(data.Remove(data.Length - 1), style, cultura, out currency);
            }

            data = currency.ToString((showSymbol ? "C0" : "N0"), cultura);


            return (data, currency.ToString(), valid);

        }
        public static (string data, string decimalData, bool isValid) PercentDecimalNegative(this string data, int decimals = 2, PositionSymbol position = PositionSymbol.BeforeSymbol)
        {
            data = data.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
            data = data.Insert(0, CultureInfo.NumberFormat.NegativeSign);
            var info = data.PercentDecimal(decimals, position);
            return info;
        }
        public static string PercentPositive(this string data, PositionSymbol position)
        {
            return data.Percent(position).Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
        }
        public static string Percent(this string data, PositionSymbol position)
        {
            if (!string.IsNullOrEmpty(data))
            {
                var _string = data.Digits();
                if (!string.IsNullOrEmpty(_string))
                {

                    if (position == PositionSymbol.BeforeSymbol)
                    {
                        _string = string.Join("", (CultureInfo.NumberFormat.PercentSymbol + _string));
                    }
                    else
                    {
                        _string = string.Join("", (_string + CultureInfo.NumberFormat.PercentSymbol));
                    }
                }
                else
                {
                    return string.Empty;
                }
                return _string;
            }
            else
            {
                return string.Empty;
            }



        }

        public static string PercentNegative(this string data, PositionSymbol position)
        {
            data = data.Percent(position);
            if (!string.IsNullOrEmpty(data))
            {
                if (!data.Contains(CultureInfo.NumberFormat.NegativeSign))
                {
                    data = data.Insert(0, "-");
                }
                return data;

            }
            else
            {
                return string.Empty;
            }



        }
        public static string Digits(this string data, bool onlyInteger = false)
        {
            var culture = CultureInfo.Clone() as CultureInfo;
            var isNegative = data.IndexOf(culture.NumberFormat.NegativeSign, StringComparison.CurrentCulture) >= 0;

            var _string = string.Join("", Regex.Matches(data, (onlyInteger && data.Contains(CultureInfo.NumberFormat.NumberDecimalSeparator) ? @"[\d]+[" + CultureInfo.NumberFormat.NumberDecimalSeparator + "]" : @"\d+")).Cast<Match>().Select(x =>
                  {
                      return x.Success ? x.Value : string.Empty;
                  }));
            if (isNegative && !string.IsNullOrEmpty(_string))
            {
                _string = string.Join("", _string.Prepend('-'));
            }

            if (onlyInteger && !string.IsNullOrEmpty(_string))
            {
                _string = _string.Replace(CultureInfo.NumberFormat.NumberDecimalSeparator, string.Empty);
            }

            return _string;
        }
        private static (bool state, string mask) ValidArrayMask(this string data, string masks)
        {
            if (string.IsNullOrEmpty(masks)) return default;
            var mask = masks.Split(',').Select(x =>
            {
                MaskedTextProvider maskLength = new MaskedTextProvider(x);
                data.ToCharArray().All(y => { return maskLength.Add(y); });
                var length = maskLength.ToString(true, false, false, 0, maskLength.Length);
                return (data.Length <= length.Length, x);
            });

            var _mask = mask.Where(x => x.Item1).FirstOrDefault();
            return _mask;

        }
        public static (string data, string unMaskValue, int lastIndex) Mask(this string data, string mask, char prompChar = '_')
        {
            string unMaskValue = string.Empty;
            int index = 0;
            //mask = data.ValidArrayMask(mask).mask;
            if (!string.IsNullOrEmpty(mask))
            {



                MaskedTextProvider maskedTextProvider = new MaskedTextProvider(mask);
                MaskedTextProvider maskedTmp = new MaskedTextProvider(mask);

                string tmpData = string.Empty;
                foreach (var item in data.ToCharArray())
                {
                    //tmpData += (item != prompChar ? item.ToString() : string.Empty);
                    var state = maskedTmp.Add(item);
                    tmpData += state ? item.ToString() : String.Empty;
                }

                maskedTextProvider.PromptChar = prompChar;
                //data = data.Truncate(maskedTextProvider.EditPositionCount);
                tmpData = tmpData.Truncate(maskedTextProvider.EditPositionCount);
                //maskedTextProvider.Set(data);
                maskedTextProvider.Set(tmpData);


                unMaskValue = maskedTextProvider.ToString(true, false, false, 0, maskedTextProvider.Length);

                data = maskedTextProvider.ToDisplayString();
                index = maskedTextProvider.LastAssignedPosition;
            }
            else
            {
                unMaskValue = string.Empty;
                data = string.Empty;
            }

            return (data, unMaskValue, index);
        }

        public static (string data, string unMaskValue, int lastIndex, bool discrepancies) Mask(this char data, string mask, string originalValue, int index, char prompChar = '_')
        {
            string unMaskValue = string.Empty;
            string _data = string.Empty;
            MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.InvalidInput;
            bool state = true;
            var caret = index;
            if (!string.IsNullOrEmpty(mask))
            {
                MaskedTextProvider maskedTextProvider = new MaskedTextProvider(mask);
                maskedTextProvider.PromptChar = prompChar;
                maskedTextProvider.Add(originalValue.Nvl(" "));
                //while (!maskedTextProvider.IsEditPosition(index) && index <= maskedTextProvider.EditPositionCount)
                //{
                //    index++;
                //}
                maskedTextProvider.Replace(data, index);
                state = maskedTextProvider.VerifyChar(data, index, out maskedTextResultHint);
                unMaskValue = maskedTextProvider.ToString(true, false, false, 0, maskedTextProvider.Length);
                _data = maskedTextProvider.ToDisplayString();

                var _caretEdit = maskedTextProvider.FindAssignedEditPositionFrom(index, true) + 1;
                var _caretEdit2 = maskedTextProvider.FindEditPositionFrom(index, true);
                var _caretEdit3 = maskedTextProvider.FindNonEditPositionFrom(index, true);
                var _caretEdit4 = maskedTextProvider.FindUnassignedEditPositionFrom(index, true);

                index = maskedTextProvider.FindUnassignedEditPositionFrom(index, true);

                switch (maskedTextResultHint)
                {
                    case MaskedTextResultHint.NonEditPosition:
                        index = _caretEdit4 < 0 ? _caretEdit : _caretEdit4;
                        break;

                    case MaskedTextResultHint.Success:
                        index = _caretEdit;
                        break;


                    case MaskedTextResultHint.NoEffect:
                        index = _caretEdit;
                        break;

                    case MaskedTextResultHint.PositionOutOfRange:
                        index = caret;
                        break;

                    default:
                        index = _caretEdit2;
                        break;
                }

                state = true;


            }
            else
            {
                unMaskValue = string.Empty;
                _data = string.Empty;
            }

            return (_data, unMaskValue, index, state);
        }

        public static void ResetTextBox(this TextBox textBox)
        {

        }
        public static (int startIndex, int endIndex) SelectionClear(this TextBox textBox)
        {

            var start = textBox.SelectionStart;
            var end = textBox.SelectionLength;
            if (textBox.SelectionLength > 0)
            {
                if (!string.IsNullOrEmpty(textBox.SelectedText))
                {
                    textBox.SelectedText = "";
                    //textBox.Text = textBox.Text.Remove(start, end);

                }
            }
            return (start, end);

        }

        public static void SelectionClear2(this TextBox textBox)
        {

            if (textBox.SelectionLength > 0)
            {
                if (!string.IsNullOrEmpty(textBox.SelectedText))
                {
                    textBox.SelectedText = string.Empty;
                }
            }

        }


        public static string NotSpaces(this string data)
        {
            return Regex.Match(data, @"\S").Value;
        }


        /// <summary>
        /// Convert string  to decimal
        /// </summary>
        /// <param name="data">string</param>
        /// <param name="decimals">number of decimals</param>
        /// <param name="separator">decimal separator</param>
        /// <param name="positionSymbol">symbol</param>
        /// <param name="limitIntegers">limit of integers</param>
        /// <returns>string</returns>
        public static (string data, string decimalData, bool isValid) PercentDecimal(this string data, int decimals, PositionSymbol position)
        {
            if (string.IsNullOrEmpty(data)) return default;
            decimal currency;
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol | NumberStyles.Any;
            var cultura = CultureInfo.Clone() as CultureInfo;



            if (data.Contains(CultureInfo.NumberFormat.NegativeSign))
            {
                data = data.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
                data = data.Insert(0, CultureInfo.NumberFormat.NegativeSign);
            }
            var symbol = data.IndexOf(cultura.NumberFormat.PercentSymbol);


            data = symbol >= 0 ? data.Remove(symbol, 1) : data;
            var regex = @"[" + CultureInfo.NumberFormat.PositiveSign + CultureInfo.NumberFormat.NegativeSign + @"]?([\d]+\" + CultureInfo.NumberFormat.NumberDecimalSeparator + @"?[\d]*|\" + CultureInfo.NumberFormat.NumberDecimalSeparator + @"[\d]+)";

            data = string.Join("", Regex.Matches(data, regex).Cast<Match>().Select(x =>
             {
                 return x.Success ? x.Value : string.Empty;
             }));



            /*            cultura.NumberFormat.PercentGroupSeparator = "";

                        var valid = Decimal.TryParse(data, style, cultura, out currency);
                        if (valid)
                            currency = currency / 100;


                        data = currency.ToString($"P{decimals}", cultura);*/
            var _data = data.Decimals(decimals);
            data = _data.data;

            if (position == PositionSymbol.AfterSymbol && !string.IsNullOrEmpty(data))
            {

                var index = data.IndexOf(CultureInfo.NumberFormat.PercentSymbol);
                if (index > -1)
                    data = data.Remove(index, 1);

                var startIndex = data.Contains("-") ? 1 : 0;
                data = data.Insert(startIndex, CultureInfo.NumberFormat.PercentSymbol);
            }

            return (data, _data.data, _data.isValid);

        }

        /// <summary>
        /// Convert string  to decimal, only positive
        /// </summary>
        /// <param name="data">string</param>
        /// <param name="decimals">number of decimals</param>
        /// <param name="positionSymbol">symbol</param>
        /// <returns>string</returns>
        public static (string data, string decimalData, bool isValid) PercentDecimalPositive(this string data, int decimals, PositionSymbol positionSymbol)
        {
            data = data.Replace("-", string.Empty);
            var info = data.PercentDecimal(decimals, positionSymbol);

            return info;
        }

        /// <summary>
        /// Convert string to digit positive
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string DigitsPositive(this string data, bool onlyInteger = false)
        {
            var _string = data.Digits(onlyInteger);
            _string = _string.Contains('-') ? _string.Replace("-", string.Empty) : _string;
            return _string;
        }
        /// <summary>
        /// Convert Text without space
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string TextWithoutSpace(this string data)
        {
            string oString = "";
            foreach (Match item in Regex.Matches(data, @"\S+"))
            {
                if (item.Success)
                    oString += item.Value;
            }
            return oString;
        }

        /// <summary>
        /// Convert string to letter, number and space
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string LetterNumberSpace(this string data, bool isUpper = true)
        {
            var _data = string.Join("", Regex.Matches(data, @"[a-zA-Z]|\s|\d").Cast<Match>().Select(x =>
           {
               return x.Success ? x.Value : string.Empty;
           }));
            return isUpper ? _data.ToUpper() : _data;
        }

        public static string OnlyLetterUpperSpace(this string data)
        {
            return string.Join("", Regex.Matches(data, @"[a-zA-Z]|\s").Cast<Match>().Select(x =>
             {
                 return x.Success ? x.Value : string.Empty;
             })).ToUpper();
        }
        public static int DigitIntegerLength(this string data)
        {
            var separatorIndex = data.IndexOf(CultureInfo.NumberFormat.CurrencyDecimalSeparator);
            if (separatorIndex > -1)
            {
                var _data = data.Substring(0, separatorIndex).DigitsPositive();
                return _data.Length;
            }
            else
            {
                return data.DigitsPositive().Length;
            }
        }
        /// <summary>
        /// Convert string to letter and number
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string LetterNumber(this string data, bool isUpper = true)
        {
            var _data = string.Join("", Regex.Matches(data, @"[a-zA-Z]|\d").Cast<Match>().Select(x =>
             {
                 return x.Success ? x.Value : string.Empty;
             }));
            return isUpper ? _data.ToUpper() : _data;
        }

        /// <summary>
        /// Convert string only letter
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string OnlyLetter(this string data)
        {
            return string.Join("", Regex.Matches(data, @"[a-zA-Z]").Cast<Match>().Select(x =>
             {
                 return x.Success ? x.Value : string.Empty;
             }));

        }

        /// <summary>
        /// Convert string only letter capitalizer with space
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string OnlyLetterCapitalizerSpace(this string data)
        {
            return data.OnlyLetterSpace().Capitalizer();
        }

        /// <summary>
        /// Convert string only letter first capitalizer with space
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string OnlyLetterCapitalizerFirstSpace(this string data)
        {
            return data.OnlyLetterSpace().CapitalizerFirst();
        }

        /// <summary>
        /// Convert string only letter first capitalizer
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string OnlyLetterCapitalizerFirst(this string data)
        {
            return data.OnlyLetter().CapitalizerFirst();
        }

        /// <summary>
        /// Convert Only Letter and Capitalizer
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string OnlyLetterCapitalizer(this string data)
        {
            return data.OnlyLetter().Capitalizer();
        }

        /// <summary>
        /// Convert string only letter uppercase
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string OnlyLetterUpper(this string data)
        {
            return data.OnlyLetter().ToUpper();
        }

        /// <summary>
        /// Convert string only letter lower
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string OnlyLetterLower(this string data)
        {
            return data.OnlyLetter().ToLower();
        }

        /// <summary>
        /// Convert string to letter lowercase, allows space
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string OnlyLetterLowerSpace(this string data)
        {
            return data.OnlyLetterSpace().ToLower();
        }

        public static string OnlyLetterSpace(this string data)
        {
            return string.Join("", Regex.Matches(data, @"[a-zA-Z]|[\s]").Cast<Match>().Select(x =>
              {
                  return x.Success ? x.Value : string.Empty;
              }));
        }

        public static (int caret, bool isNotText, string data) InitializeOnPaste(this TextBox textBox, DataObjectPastingEventArgs e)
        {
            textBox.SelectionClear();
            var caret = 0;
            caret = textBox.CaretIndex;
            var isNotText = !(bool)(e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText, true));

            if (isNotText == true)
            {
                return (0, isNotText, string.Empty);
            }

            string data = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            textBox.Text = textBox.Text.Insert(caret, data);
            textBox.Text = textBox.Text.Truncate(textBox.MaxLength);

            return (caret, isNotText, data);


        }



        public static (int caret, string data, string character, int caretIndex, string beforeData, string afterData) InitializeTextInput(this TextBox textBox, TextCompositionEventArgs e)
        {
            textBox.SelectionClear();
            int caret = textBox.CaretIndex;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);

            var beforeData = textBox.Text;
            textBox.Text = textBox.Text.Insert(caret, e.Text);
            textBox.Text = textBox.Text.Truncate(textBox.MaxLength);
            int caretIndex = default;

            caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);
            if (e.Text == "-")
            {
                caretIndex = 1;
            }
            else
            {
                caretIndex = caret + e.Text.Length;

            }
            var afterData = textBox.Text;

            return (caret, textBox.Text, e.Text, caretIndex, beforeData, afterData);

        }

        public static (int caret, string data, string character, int caretIndex, string beforeData, string afterData, bool discrepancies, bool quiet) InitializeTextInput(this TextBox textBox, TextCompositionEventArgs e, Func<string> func, bool onSMinusPlus = false)
        {

            int caret = textBox.CaretIndex;
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
            var beforeData = textBox.Text;
            textBox.Text = textBox.Text.Insert(caret, e.Text);

            var berfore = textBox.BeforeValidationMinusAndPlus(e, caret);
            func();
            var after = textBox.AfterValidationMinusAndPlus(e, caret, onSMinusPlus);



            return (after.caret, textBox.Text, e.Text, after.caretIndex, beforeData, after.afterData, beforeData != after.afterData, after.quiet);

        }

        static (int caret, string data) BeforeValidationMinusAndPlus(this TextBox textBox, TextCompositionEventArgs e, int caret)
        {
            if (!e.Text.Contains(CultureInfo.NumberFormat.NegativeSign))
                textBox.Text = textBox.Text.IndexOf(CultureInfo.NumberFormat.CurrencyDecimalSeparator) + 1 < caret ? textBox.Text.DigitsPositive(true).Truncate(textBox.MaxLength) : textBox.Text.DigitsPositive().Truncate(textBox.MaxLength);

            if (e.Text == CultureInfo.NumberFormat.PositiveSign && textBox.Text.Contains(CultureInfo.NumberFormat.NegativeSign))
            {
                textBox.Text = textBox.Text.Replace(CultureInfo.NumberFormat.NegativeSign, string.Empty);
            }

            caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);

            if (!textBox.Text.Contains(CultureInfo.NumberFormat.NegativeSign) && (FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty) == FormatTextBox.DigitsNegative)
            {
                caret += 1;
            }


            if (textBox.Text.IndexOf(CultureInfo.NumberFormat.NegativeSign[0]) == caret + 1)
            {
                caret++;
            }
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);

            return (caret, textBox.Text);
        }

        static (int caretIndex, int caret, string afterData, bool quiet) AfterValidationMinusAndPlus(this TextBox textBox, TextCompositionEventArgs e, int caret, bool onSMinusPlus)
        {
            bool quiet = false;
            int caretIndex = caret;

            caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);
            if ((e.Text == CultureInfo.NumberFormat.NegativeSign || e.Text == CultureInfo.NumberFormat.PositiveSign) && onSMinusPlus)
            {
                caretIndex = 1;
                caret = 1;
            }
            else
            {
                var symbol = Methods.CultureInfo.NumberFormat.PercentSymbol;
                if (textBox.Text.StartsWith(symbol) && caret == 0)
                {
                    caret = 2;
                    caretIndex = caret;
                }
                else if (textBox.Text.EndsWith(symbol) && caret > textBox.Text.Length)
                {
                    caret -= 2;
                    caretIndex = caret;
                }
                else if (textBox.Text.EndsWith(symbol) && caret == textBox.Text.Length)
                {
                    caret--;
                    caretIndex = caret;
                }
                else if (e.Text != CultureInfo.NumberFormat.PositiveSign && e.Text != CultureInfo.NumberFormat.NegativeSign)
                {
                    if (caret == 0 && textBox.Text.Contains(CultureInfo.NumberFormat.NegativeSign))
                    {
                        caret++;
                    }
                    caretIndex = caret + e.Text.Length;


                }
                else
                {
                    quiet = true;
                }
            }

            textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);

            var afterData = textBox.Text;

            return (caretIndex, caret, afterData, quiet);
        }

        /// <summary>
        /// Convert string to capitalized
        /// </summary>
        /// <param name="data">string</param>
        /// <returns>string</returns>
        public static string Capitalizer(this string data)
        {
            return TextInfo.ToTitleCase(data.ToLower());
        }

        public static bool IsBackOrDeleteKey(this KeyEventArgs e)
        {
            return (e.Key == Key.Back || e.Key == Key.Delete);
        }

        public static bool IsArithmetic(this KeyEventArgs e)
        {
            return (e.Key == Key.OemMinus || e.Key == Key.OemPlus || e.Key == Key.Multiply || e.Key == Key.Divide || e.Key == Key.Subtract || e.Key == Key.Add);
        }


        /// <summary>
        /// Check if it's lyrics
        /// </summary>
        /// <param name="letter">string</param>
        /// <returns>string</returns>
        public static bool IsLetter(this char letter)
        {
            return !string.IsNullOrEmpty(Regex.Match(letter.ToString(), @"^[a-zA-Z]?").Value);
        }

        public static bool SelectionClear(this TextBox textBox, KeyEventArgs e)
        {
            return true;
        }

        /// <summary>
        /// Check if it's lyrics
        /// </summary>
        /// <param name="letter">string</param>
        /// <returns>string</returns>
        public static bool IsLetter(this string letter)
        {

            return !string.IsNullOrEmpty(Regex.Match(letter, @"^[a-zA-Z]?").Value);
        }

        public static bool IsLetter(this Key letter)
        {

            return letter >= Key.A && letter <= Key.Z;
        }
        public static bool IsLetter(this KeyEventArgs e)
        {
            return ((e.Key >= Key.A && e.Key <= Key.Z));
        }

        /// <summary>
        /// Convert string to capitalized with first letter capitalized
        /// </summary>
        /// <param name="data">string</param>
        /// <param name="pattern"></param>
        /// <returns>string</returns>
        public static string CapitalizerFirst(this string data, string pattern = @"[a-zA-Z]+")
        {
            data = data.ToLower();
            bool first = false;

            data = string.Join("", data.Select(x =>
              {
                  if (!first)
                  {
                      first = x.IsLetter();
                      x = first ? char.ToUpper(x) : x;
                  }
                  return x;
              }));

            return data;

        }

        public static bool ContainerDigit(this string data)
        {
            return string.IsNullOrEmpty(Regex.Match(data, @"\d").Value);
        }
    }
}
