using Ex.OM.Configurations;
using Ex.OM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using static Ex.OM.Extentions.TextBoxFieldAssistOM;

namespace Ex.OM.Extentions.Converts
{
    public class ConvertCapitalizer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToString(value).Capitalizer();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToString(value).Capitalizer();
        }

    }

    public class ConvertDigits : IValueConverter

    {
        public TextBox TextBox { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           var _parameter =  ConverterParameterOM.Transform(parameter?.ToString());
            if (value == null) return null;
            if (!string.IsNullOrEmpty(_parameter.PadLeft))
            {
               if (_parameter.LostFocus && !TextBox.IsFocused)
                {
                    return value.ToString().Digits().ToLong().ToString(_parameter.PadLeft);
                }
                else
                {
                    return value.ToString().Digits();
                }
            }
            else if(!string.IsNullOrEmpty(_parameter.PadLeft) && !_parameter.LostFocus)
            {
                return value.ToString().Digits().ToLong().ToString(_parameter.PadLeft);
            }
            else
            {
                return value.ToString().Digits();
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return value.ToString().Digits();
        }

    }

    public class ConvertCapitalizerFirst : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToString(value).CapitalizerFirst();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToString(value).CapitalizerFirst();
        }

    }

    public class ConvertDigitPercent : IValueConverter
    {
        public bool IsFraction { get; set; }
        public int NumberDecimal { get; set; }
        public TextBox CustomTextBox { get; set; }
        public bool ShowSymbol { get; set; } = true;


        public TextBoxFieldAssistOM.PositionSymbol PositionSymbol { get; set; } = TextBoxFieldAssistOM.PositionSymbol.AfterSymbol;
        public TextBoxFieldAssistOM.FormatTextBox Type { get; set; } = TextBoxFieldAssistOM.FormatTextBox.Percent;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var format = new Format().Load((string)parameter);

            IsFraction = CustomTextBox != null ? (bool)CustomTextBox.GetValue(TextBoxFieldAssistOM.IsFractionValueBackProperty) : IsFraction;

            if (value != null)
            {
                if (IsFraction)
                {
                    var percent = value.ToString();
                    if (!string.IsNullOrEmpty(percent))
                    {
                        if (!string.IsNullOrEmpty(percent.DecimalPositive().data))
                        {
                            var data = System.Convert.ToDecimal(percent);
                            data *= 100;

                            if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
                            {
                                var result = System.Convert.ToInt64(data).ToString().PercentPositive(PositionSymbol);
                                return ReplaceSymbol(result);

                            }
                            else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
                            {
                                var result = System.Convert.ToInt64(data).ToString().PercentNegative(PositionSymbol);
                                return ReplaceSymbol(result);

                            }
                            else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimal)
                            {
                                var result = data.ToString().PercentDecimal(NumberDecimal, PositionSymbol).data;
                                return ReplaceSymbol(result);
                            }
                            else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
                            {
                                var result = data.ToString().PercentDecimalPositive(NumberDecimal, PositionSymbol).data;
                                return ReplaceSymbol(result);
                            }
                            else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalNegative)
                            {
                                var result = data.ToString().PercentDecimalNegative(NumberDecimal, PositionSymbol).data;
                                return ReplaceSymbol(result);
                            }
                            else
                            {
                                var result = System.Convert.ToInt64(data).ToString().Percent(PositionSymbol);
                                return ReplaceSymbol(result);

                            }
                        }
                        else
                        {
                            return value;
                        }

                    }
                    return null;


                }
                else
                {
                    if (value != null)
                        if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
                        {
                            var result = System.Convert.ToInt64(value).ToString().PercentPositive(PositionSymbol);
                            return ReplaceSymbol(result);

                        }
                        else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
                        {

                            var result = System.Convert.ToInt64(value).ToString().PercentNegative(PositionSymbol);
                            return ReplaceSymbol(result);

                        }
                        else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimal)
                        {
                            var result = value.ToString().PercentDecimal(NumberDecimal, PositionSymbol).data;
                            return ReplaceSymbol(result);
                        }
                        else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
                        {
                            var result = value.ToString().PercentDecimalPositive(NumberDecimal, PositionSymbol).data;
                            return ReplaceSymbol(result);
                        }
                        else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalNegative)
                        {
                            var result = value.ToString().PercentDecimalNegative(NumberDecimal, PositionSymbol).data;
                            return ReplaceSymbol(result);
                        }

                        else
                        {
                            var result = System.Convert.ToInt64(value).ToString().Percent(PositionSymbol);
                            return ReplaceSymbol(result);

                        }
                }
            }

            return null;

        }

        string ReplaceSymbol(string data)
        {
            var cultura = Methods.CultureInfo.Clone() as CultureInfo;
            data = !ShowSymbol ? data.Replace(cultura.NumberFormat.PercentSymbol, string.Empty) : data;
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                IsFraction = CustomTextBox != null ? (bool)CustomTextBox.GetValue(TextBoxFieldAssistOM.IsFractionValueBackProperty) : IsFraction;


                if (IsFraction)
                {
                    var percent = string.Empty;
                    if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
                    {
                        percent = value.ToString().DigitsPositive();

                    }
                    else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
                    {
                        percent = value.ToString().DigitNegative();

                    }
                    else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimal)
                    {
                        var data = value.ToString().Decimals(NumberDecimal).data;
                        return !string.IsNullOrEmpty(data) ? (System.Convert.ToDecimal(data) / 100).ToString() : data;
                    }
                    else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
                    {

                        var data = value.ToString().DecimalPositive(NumberDecimal).data;
                        return !string.IsNullOrEmpty(data) ? (System.Convert.ToDecimal(data) / 100).ToString() : data;
                    }
                    else if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalNegative)
                    {
                        var data = value.ToString().DecimalNegative(NumberDecimal).data;
                        return !string.IsNullOrEmpty(data) ? (System.Convert.ToDecimal(data) / 100).ToString() : data;
                    }
                    else
                    {
                        percent = value.ToString().Digits();

                    }
                    if (!string.IsNullOrEmpty(percent))
                    {
                        var digits = percent.DigitsPositive();
                        var stateDigit = digits.IsDigit();
                        if (stateDigit)
                        {

                            var data = System.Convert.ToDecimal(percent) / 100;
                            return data;


                        }
                        else
                        {
                            return value;
                        }

                    }
                    return null;
                }
                else
                {
                    if (Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimal ||
                        Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalNegative ||
                        Type == TextBoxFieldAssistOM.FormatTextBox.PercentDecimalPositive)
                    {
                        var data = value.ToString().Decimals(NumberDecimal).data;
                        return data;
                    }
                    else
                    {

                        var data = value.ToString().Digits();
                        return data;

                    }

                }
            }
            return null;
        }

    }

    public class ConvertToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

    }

    public class ConvertMoneyInteger : IValueConverter
    {
        public TextBox TextBox { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var show = (bool)TextBox.GetValue(ShowSymbolProperty);
                var data = value.ToString().Money(show);
                return data.data;
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString().Digits();
            }
            else
            {
                return string.Empty;
            }
        }

    }

    public class ConvertDecimal : IValueConverter
    {
        public int NumberDecimal { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var _data = value.ToString().Decimals(NumberDecimal);
                return _data.data;
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var data =  value.ToString().Decimals(NumberDecimal).data;
                return data;
            }
            else
            {
                return string.Empty;
            }
        }

    }

    public class ConvertMoneyDecimal : IValueConverter
    {
        public int NumberDecimals { get; set; } = 2;
        public int MaxLength { get; set; }
        public enum TypeMoney
        {
            PositiveDecimal,
            NegativeDecimal,
            MoneyDecimal
        }
        public bool ShowSymbol { get; set; } = true;
        public TypeMoney Money { get; set; }
        public TextBox TextBox { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return default;

            if (Money == TypeMoney.NegativeDecimal)
            {
                var data = value.ToString().MoneyNegativeDecimal(NumberDecimals, ShowSymbol);
                data.data = IsShowSymbol(data.data);
                return data.data;
            }
            else if (Money == TypeMoney.PositiveDecimal)
            {
                var data = value.ToString().MoneyPositiveDecimal(NumberDecimals, ShowSymbol);
                data.data = IsShowSymbol(data.data);

                return data.data;
            }
            else
            {
                var data = value.ToString().MoneyDecimal(NumberDecimals,ShowSymbol);
                data.data = IsShowSymbol(data.data);

                return data.data;
            }


        }

        string IsShowSymbol(string data)
        {
            var _culture = Methods.CultureInfo.Clone() as CultureInfo;
            data = ShowSymbol ? data : data.Replace(_culture.NumberFormat.CurrencySymbol, string.Empty);
            return data;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return default;

            var data = value.ToString().MoneyDecimal(NumberDecimals, ShowSymbol).data.Decimals();

            return data.data;

        }

    }

    public class ConvertMoneyNegativeInteger : IValueConverter
    {
        public TextBox TextBox { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var show = (bool)TextBox.GetValue(ShowSymbolProperty);

                var data = value.ToString().MoneyNegative(show);
                return data.data;
            }
            else
            {
                return string.Empty;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString().DigitNegative();

            }
            else
            {
                return string.Empty;
            }
        }

    }

    public class ConvertMoneyPositiveInteger : IValueConverter
    {
        public TextBox TextBox { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var show = (bool)TextBox.GetValue(ShowSymbolProperty);

                var data = value.ToString().MoneyPositive(show);
                return data.data;
            }
            else
            {
                return string.Empty;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString().DigitsPositive();

            }
            else
            {
                return string.Empty;
            }
        }

    }

    public class ConvertMask : IValueConverter
    {
        public string Mask { get; set; }
        public string UnMask { get; set; }
        public char PrompChar { get; set; } = '_';
        public TextBox TextBox { get; set; }
        string originalValue;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {

                //var data = (string)TextBox.GetValue(TextBoxFieldAssistOM.TextBoxOriginalValue);
                //originalValue = data;
                //return data.Mask(Mask, PrompChar).data;
                return value.ToString().Mask(Mask, PrompChar).data;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var data = (string)TextBox.GetValue(TextBoxFieldAssistOM.TextBoxOriginalValue);
            return value.ToString().Mask(UnMask, PrompChar).data;
        }
    }


    #region ConvertOM
    public class ConvertMoneyOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().Money().data;
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }
            if (ConverterParameter.PositionSymbol == PositionSymbol.AfterSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
                data = data.Insert(data.Length, Methods.CultureInfo.NumberFormat.CurrencySymbol);
            }
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return value.ToString().Digits();
        }
    }

    public class ConvertMoneyNegativeOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().MoneyNegative().data;
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }
            if (ConverterParameter.PositionSymbol == PositionSymbol.AfterSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
                data = data.Insert(data.Length, Methods.CultureInfo.NumberFormat.CurrencySymbol);
            }
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return value.ToString().DigitNegative();
        }
    }

    public class ConvertMoneyPositiveOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().MoneyPositive().data;
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }
            if (ConverterParameter.PositionSymbol == PositionSymbol.AfterSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
                data = data.Insert(data.Length, Methods.CultureInfo.NumberFormat.CurrencySymbol);
            }
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return value.ToString().DigitsPositive();
        }
    }

    public class ConvertMoneyDecimalOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().MoneyDecimal(ConverterParameter.Decimals).data;
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }
            if (ConverterParameter.PositionSymbol == PositionSymbol.AfterSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
                data = data.Insert(data.Length, Methods.CultureInfo.NumberFormat.CurrencySymbol);
            }
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());

            var data = value.ToString().Decimals(ConverterParameter.Decimals).data;

            return data;
        }
    }

    public class ConvertMoneyDecimalPositiveOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return default;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().MoneyPositiveDecimal(ConverterParameter.Decimals).data;
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }
            if (ConverterParameter.PositionSymbol == PositionSymbol.AfterSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
                data = data.Insert(data.Length, Methods.CultureInfo.NumberFormat.CurrencySymbol);
            }
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return default;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());

            var data = value.ToString().DecimalPositive(ConverterParameter.Decimals).data;

            return data;
        }
    }

    public class ConvertMoneyDecimalNegativeOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().MoneyNegativeDecimal(ConverterParameter.Decimals).data;
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }
            if (ConverterParameter.PositionSymbol == PositionSymbol.AfterSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
                data = data.Insert(data.Length, Methods.CultureInfo.NumberFormat.CurrencySymbol);
            }
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());

            var data = value.ToString().DecimalNegative(ConverterParameter.Decimals).data;

            return data;
        }
    }

    //---------------------------------Percent

    public class ConvertPercentOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().Percent(ConverterParameter.PositionSymbol);
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return value.ToString().Digits();
        }
    }
    public class ConvertPercentPositiveOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().PercentPositive(ConverterParameter.PositionSymbol);
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return value.ToString().DigitsPositive();
        }
    }

    public class ConvertPercentNegativeOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().PercentNegative(ConverterParameter.PositionSymbol);
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return value.ToString().DigitNegative();
        }
    }

    //----------------------------------------



    //---------------------------PercentDecimal
    public class ConvertPercentDecimalOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().PercentDecimal(ConverterParameter.Decimals, ConverterParameter.PositionSymbol).data;
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            return value.ToString().Decimals(ConverterParameter.Decimals).data;
        }
    }
    public class ConvertPercentDecimalPositiveOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().PercentDecimalPositive(ConverterParameter.Decimals, ConverterParameter.PositionSymbol).data;
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            return value.ToString().DecimalPositive(ConverterParameter.Decimals).data;
        }
    }
    public class ConvertPercentDecimalNegativeOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().PercentDecimalNegative(ConverterParameter.Decimals, ConverterParameter.PositionSymbol).data;
            if (!ConverterParameter.ShowSymbol)
            {
                data = data.Replace(Methods.CultureInfo.NumberFormat.CurrencySymbol, string.Empty);
            }

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            return value.ToString().DecimalNegative(ConverterParameter.Decimals).data;
        }
    }
    //-----------------------------------------



    //----------------------------------Decimal

    public class ConvertDecimalOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().Decimals(ConverterParameter.Decimals).data;
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            return value.ToString().Decimals(ConverterParameter.Decimals).data;
        }
    }
    public class ConvertDecimalPositiveOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().DecimalPositive(ConverterParameter.Decimals).data;


            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            return value.ToString().DecimalPositive(ConverterParameter.Decimals).data;
        }
    }
    public class ConvertDecimalNegativeOM : IValueConverter
    {
        public ConverterParameterOM ConverterParameter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            var data = value.ToString().DecimalNegative(ConverterParameter.Decimals).data;
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            ConverterParameter = ConverterParameterOM.Transform(parameter.ToString());
            return value.ToString().DecimalNegative(ConverterParameter.Decimals).data;
        }
    }

    //-----------------------------------------

    //-----------------------------------Digits
    public class ConvertDigitOM : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            var data = value.ToString().Digits();
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return value.ToString().Digits();
        }
    }
    public class ConvertDigitPositiveOM : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            var data = value.ToString().DigitsPositive();
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return value.ToString().DigitsPositive();
        }
    }
    public class ConvertDigitNegativeOM : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            var data = value.ToString().DigitNegative();
            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            return value.ToString().DigitNegative();
        }
    }

    //-----------------------------------------
    #endregion

    public class ConverterParameterOM
    {
        public bool ShowSymbol { get; set; }
        public int Decimals { get; set; }
        public int LimitIntegers { get; set; }
        public PositionSymbol PositionSymbol { get; set; }
        public string SeparatorDecimal { get; set; }
        public string PadLeft { get; set; }
        public bool LostFocus { get; set; } = true;

        public static ConverterParameterOM Transform(string parameter)
        {
            var converter = new ConverterParameterOM();

            if (parameter == null) return converter;
            var split = parameter.Split(',');
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var item in split)
            {
                var value = item.Split(':');
                dictionary.Add(value[0], value[1]);

            }

            var info = dictionary.Where(x => x.Key.ToUpper() == "SHOWSYMBOL").Select(x => x.Value).FirstOrDefault();
            converter.ShowSymbol = info?.ToUpper() == "TRUE";

            info = dictionary.Where(x => x.Key.ToUpper() == "DECIMALS").Select(x => x.Value).FirstOrDefault();
            var decimals = Convert.ToInt32(info);
            converter.Decimals = decimals <= 0 ? 2 : decimals;

            info = dictionary.Where(x => x.Key.ToUpper() == "LIMITINTEGERS").Select(x => x.Value).FirstOrDefault();
            var intgers = Convert.ToInt32(info);
            converter.Decimals = intgers <= 0 ? 2 : intgers;

            info = dictionary.Where(x => x.Key.ToUpper() == "SHOWSYMBOL").Select(x => x.Value).FirstOrDefault();
            converter.PositionSymbol = info?.ToUpper() == "AFTERSYMBOL" ? PositionSymbol.AfterSymbol : PositionSymbol.BeforeSymbol;

            info = dictionary.Where(x => x.Key.ToUpper() == "SEPARATORDECIMAL").Select(x => x.Value).FirstOrDefault();
            converter.SeparatorDecimal = info;

            info = dictionary.Where(x => x.Key.ToUpper() == "PADLEFT").Select(x => x.Value).FirstOrDefault();
            converter.PadLeft = info?.ToUpper();

            info = dictionary.Where(x => x.Key.ToUpper() == "LOSTFOCUS").Select(x => x.Value).FirstOrDefault();
            converter.LostFocus = !(info?.ToUpper() == "FALSE");

            return converter;
        }
    }

}




