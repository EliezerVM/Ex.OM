using Ex.OM.Configurations;
using Ex.OM.Extentions.Converts;
using System.Windows;
using System.Windows.Controls;

namespace Ex.OM.Extentions
{
    public static class TextBoxFieldAssistOM
    {

        public enum PositionSymbol
        {
            BeforeSymbol,
            AfterSymbol
        }
        public enum FormatTextBox
        {
            String, // Verified
            Capitalizer, // => Verified
            CapitalizerFirst, // => Verified
            OnlyLetter, // => Verified
            OnlyLetterSpace, // => verified
            OnlyLetterCapitalizer, // => Verified
            OnlyLetterCapitalizerFirst, // => Verified
            OnlyLetterCapitalizerSpace, // => verified
            OnlyLetterCapitalizerFirstSpace, // => verified
            OnlyLetterUpper, // => Verified
            OnlyLetterLower, // => Verified
            OnlyLetterUpperSpace, // => Verified
            OnlyLetterLowerSpace, // => Verified
            LetterNumberSpaces, // => Verified
            TextWithoutSpace, // => Verified
            LetterNumber, // => Verified
            Lower, // => Verified
            Upper, // => Verified
            Digits, // => Verified
            DigitsPositive, // => Verified
            DigitsNegative, // => Verified
            Decimals, // => Verified
            DecimalsNegative, // => Verified
            DecimalsPositive, // => Verified
            Money, // => Verified
            MoneyNegative, // => Verified
            MoneyPositive,// => Verified 
            MoneyDecimal,// => Verified
            MoneyDecimalNegative, // => Verified
            MoneyDecimalPositive,// => Verified
            Percent, // => Verified
            PercentNegative, // => Verified
            PercentPositive, // => Verified
            PercentDecimal,// => Verified
            PercentDecimalNegative,// => Verified
            PercentDecimalPositive, // =>Verified
            Mask
        }


        #region ShowSymbol
        public static bool GetShowSymbol(DependencyObject el)
        {
            return (bool)el.GetValue(ShowSymbolProperty);
        }
        public static void SetShowSymbol(DependencyObject el, bool val)
        {
            el.SetValue(ShowSymbolProperty, val);
        }
        public static readonly DependencyProperty ShowSymbolProperty = DependencyProperty.RegisterAttached(
        "ShowSymbol",
        typeof(bool),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion


        #region TypeTextBox
        public static FormatTextBox GetTypeTextBox(DependencyObject el)
        {
            return (FormatTextBox)el.GetValue(TypeTextBoxProperty);
        }
        public static void SetTypeTextBox(DependencyObject el, FormatTextBox val)
        {
            el.SetValue(TypeTextBoxProperty, val);
        }
        public static readonly DependencyProperty TypeTextBoxProperty = DependencyProperty.RegisterAttached(
        "TypeTextBox",
        typeof(FormatTextBox),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata(FormatTextBox.String, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, MethodTypeTextBox));

        private static void MethodTypeTextBox(
            DependencyObject el,
            DependencyPropertyChangedEventArgs e)
        {

            if (el is TextBox textBox)
            {
                textBox.RemoveExtensions();
                Methods.CultureInfo.NumberFormat.CurrencyNegativePattern = 1;
                var format = (FormatTextBox)e.NewValue;


                switch (format)
                {
                    case FormatTextBox.String:

                        break;
                    case FormatTextBox.Capitalizer:
                        textBox.AddCapitalizer();

                        break;
                    case FormatTextBox.CapitalizerFirst:
                        textBox.AddCapitalizerFirst();

                        break;
                    case FormatTextBox.OnlyLetter:
                        textBox.AddOnlyLetter();
                        break;
                    case FormatTextBox.OnlyLetterSpace:
                        textBox.AddOnlyLetterSpace();

                        break;
                    case FormatTextBox.OnlyLetterCapitalizer:
                        textBox.AddOnlyLetterCapitalizer();

                        break;
                    case FormatTextBox.OnlyLetterCapitalizerFirst:
                        textBox.AddOnlyLetterCapitalizerFirst();

                        break;
                    case FormatTextBox.OnlyLetterCapitalizerSpace:
                        textBox.AddOnlyLetterCapitalizerSpace();

                        break;
                    case FormatTextBox.OnlyLetterCapitalizerFirstSpace:
                        textBox.AddOnlyLetterCapitalizerFirstSpace();

                        break;
                    case FormatTextBox.OnlyLetterUpper:
                        textBox.AddOnlyLetterUpper();

                        break;
                    case FormatTextBox.OnlyLetterLower:
                        textBox.AddOnlyLetterLower();

                        break;
                    case FormatTextBox.OnlyLetterUpperSpace:
                        textBox.AddOnlyLetterUpperSpace();

                        break;
                    case FormatTextBox.OnlyLetterLowerSpace:
                        textBox.AddOnlyLetterLowerSpace();

                        break;
                    case FormatTextBox.LetterNumberSpaces:
                        textBox.AddLetterNumberSpace();

                        break;
                    case FormatTextBox.TextWithoutSpace:
                        textBox.AddTextWithoutSpace();


                        break;
                    case FormatTextBox.LetterNumber:
                        textBox.AddLetterNumber();

                        break;
                    case FormatTextBox.Lower:
                        textBox.AddLower();

                        break;
                    case FormatTextBox.Upper:
                        textBox.AddUpper();

                        break;
                    case FormatTextBox.Digits:
                        textBox.AddDigit();

                        break;
                    case FormatTextBox.DigitsPositive:
                        textBox.AddDigitPositve();

                        break;
                    case FormatTextBox.DigitsNegative:
                        textBox.AddDigitNegative();

                        break;
                    case FormatTextBox.Decimals:
                        textBox.AddDecimal();

                        break;
                    case FormatTextBox.DecimalsNegative:
                        textBox.AddDecimalNegative();

                        break;
                    case FormatTextBox.DecimalsPositive:
                        textBox.AddDecimalPositive();

                        break;
                    case FormatTextBox.Money:
                        textBox.AddMoney();

                        break;
                    case FormatTextBox.MoneyNegative:
                        textBox.AddMoneyNegative();

                        break;
                    case FormatTextBox.MoneyPositive:
                        textBox.AddMoneyPositive();

                        break;
                    case FormatTextBox.MoneyDecimal:
                        textBox.AddMoneyDecimal();

                        break;
                    case FormatTextBox.MoneyDecimalNegative:
                        textBox.AddMoneyDecimalNegative();
                        break;

                    case FormatTextBox.MoneyDecimalPositive:
                        textBox.AddMoneyDecimalPositive();
                        break;

                    case FormatTextBox.Percent:
                        textBox.AddPercent();

                        break;
                    case FormatTextBox.PercentNegative:
                        textBox.AddPercentNegative();

                        break;
                    case FormatTextBox.PercentPositive:
                        textBox.AddPercentPositive();

                        break;
                    case FormatTextBox.PercentDecimal:
                        textBox.AddPercentDecimal();

                        break;
                    case FormatTextBox.PercentDecimalNegative:
                        textBox.AddPercentDecimalNegative();


                        break;
                    case FormatTextBox.PercentDecimalPositive:
                        textBox.AddPercentDecimalPositive();

                        break;

                    case FormatTextBox.Mask:
                        textBox.AddMask();

                        break;

                    default:
                        break;
                }
            }
        }

        #endregion

        #region IsFractionValueBack (Not Usage)
        public static readonly DependencyProperty IsFractionValueBackProperty = DependencyProperty.RegisterAttached(
       "IsFractionValueBack",
       typeof(bool),
       typeof(TextBoxFieldAssistOM),
       new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, FractionValueCallBack));

        private static void FractionValueCallBack(
  DependencyObject el,
  DependencyPropertyChangedEventArgs e)
        {
            if (!(el is TextBox textBox)) return;
            var isFraction = (bool)textBox.GetValue(TextBoxFieldAssistOM.IsFractionValueBackProperty);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            if (type == TextBoxFieldAssistOM.FormatTextBox.Percent ||
                type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive ||
                type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
            {
                var conv = new ConvertDigitPercent
                {
                    IsFraction = isFraction,
                    CustomTextBox = textBox,
                    Type = type,
                    NumberDecimal = decimals,
                    PositionSymbol = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol),
                    ShowSymbol = showSymbol
                };
                //textBox.LoadConvertPercent();
                textBox.LoadConvert(conv);

            }

        }
        public static bool GetIsFractionValueBack(DependencyObject el)
        {
            return (bool)el.GetValue(IsFractionValueBackProperty);
        }
        public static void SetIsFractionValueBack(DependencyObject el, bool val)
        {
            el.SetValue(IsFractionValueBackProperty, val);
        }
        #endregion

        #region PositionSymbol
        /// <summary>
        /// Percentage symbol position
        /// </summary>
        public static readonly DependencyProperty TextBoxPositionSymbol = DependencyProperty.RegisterAttached(
         "PositionSymbol",
         typeof(PositionSymbol),
         typeof(TextBoxFieldAssistOM),
         new FrameworkPropertyMetadata(PositionSymbol.AfterSymbol, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PositionSymbolCallBack));

        public static PositionSymbol GetPositionSymbol(DependencyObject el)
        {
            return (PositionSymbol)el.GetValue(TextBoxPositionSymbol);
        }
        public static void SetPositionSymbol(DependencyObject el, PositionSymbol val)
        {
            el.SetValue(TextBoxPositionSymbol, val);
        }
        private static void PositionSymbolCallBack(
          DependencyObject el,
          DependencyPropertyChangedEventArgs e)
        {
            if (!(el is TextBox textBox)) return;
            var symbol = (PositionSymbol)e.NewValue;
            var type = (FormatTextBox)textBox.GetValue(TypeTextBoxProperty);
            var caret = textBox.CaretIndex;
            var position = $"PositionSymbol:{textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol).ToString()}";
            var isFraction = (bool)textBox.GetValue(TextBoxFieldAssistOM.IsFractionValueBackProperty);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);
            if (type == FormatTextBox.Percent)
            {
                var conv = new ConvertDigitPercent
                {
                    IsFraction = isFraction,
                    CustomTextBox = textBox,
                    Type = type,
                    NumberDecimal = decimals,
                    PositionSymbol = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol),
                    ShowSymbol = showSymbol
                };
                //textBox.Text = textBox.Text.Percent(symbol);
                //textBox.LoadConvertPercent();
                textBox.LoadConvert(conv);

                textBox.CaretIndex = caret;

            }

        }
        #endregion

        #region Mask
        public static string GetMask(DependencyObject el)
        {
            return (string)el.GetValue(TextBoxMask);
        }
        public static void SetMask(DependencyObject el, string val)
        {
            el.SetValue(TextBoxMask, val);
        }

        public static readonly DependencyProperty TextBoxMask = DependencyProperty.RegisterAttached(
        "Mask",
        typeof(string),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region OriginalValue
        public static string GetOriginalValue(DependencyObject el)
        {
            return (string)el.GetValue(TextBoxOriginalValue);
        }
        public static void SetOriginalValue(DependencyObject el, string val)
        {
            el.SetValue(TextBoxOriginalValue, val);
        }

        public static readonly DependencyProperty TextBoxOriginalValue = DependencyProperty.RegisterAttached(
        "OriginalValue",
        typeof(string),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion


        #region PromptCharMask
        public static char GetPromptCharMask(DependencyObject el)
        {
            return (char)el.GetValue(TextBoxPromptCharMask);
        }
        public static void SetPromptCharMask(DependencyObject el, char val)
        {
            el.SetValue(TextBoxPromptCharMask, val);
        }

        public static readonly DependencyProperty TextBoxPromptCharMask = DependencyProperty.RegisterAttached(
        "PromptCharMask",
        typeof(char),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata('_', FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region UnMask
        public static string GetUnMask(DependencyObject el)
        {
            return (string)el.GetValue(TextBoxUnMask);
        }
        public static void SetUnMask(DependencyObject el, string val)
        {
            el.SetValue(TextBoxUnMask, val);
        }

        public static readonly DependencyProperty TextBoxUnMask = DependencyProperty.RegisterAttached(
        "UnMask",
        typeof(string),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region MaskValue

        public static string GetUnMaskValue(DependencyObject el)
        {
            return (string)el.GetValue(TextBoxUnMaskValue);
        }
        public static void SetUnMaskValue(DependencyObject el, string val)
        {
            el.SetValue(TextBoxUnMaskValue, val);
        }

        public static readonly DependencyProperty TextBoxUnMaskValue = DependencyProperty.RegisterAttached(
        "UnMaskValue",
        typeof(string),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region DecimalNumber
        public static int GetDecimalNumber(DependencyObject el)
        {
            return (int)el.GetValue(TextBoxDecimalNumber);
        }
        public static void SetDecimalNumber(DependencyObject el, int val)
        {
            el.SetValue(TextBoxDecimalNumber, val);
        }

        public static readonly DependencyProperty TextBoxDecimalNumber = DependencyProperty.RegisterAttached(
        "DecimalNumber",
        typeof(int),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata(2, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region Caret
        public static int GetCaret(DependencyObject el)
        {
            return (int)el.GetValue(TextBoxCaret);
        }
        public static void SetCaret(DependencyObject el, int val)
        {
            el.SetValue(TextBoxCaret, val);
        }

        public static readonly DependencyProperty TextBoxCaret = DependencyProperty.RegisterAttached(
        "Caret",
        typeof(int),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        #endregion

        #region NumberIntegers
        public static int GetNumberIntegers(DependencyObject el)
        {
            return (int)el.GetValue(TextBoxNumberIntegers);
        }
        public static void SetNumberIntegers(DependencyObject el, int val)
        {
            el.SetValue(TextBoxNumberIntegers, val);
        }

        public static readonly DependencyProperty TextBoxNumberIntegers = DependencyProperty.RegisterAttached(
        "NumberIntegers",
        typeof(int),
        typeof(TextBoxFieldAssistOM),
        new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region TextBoxSelectAllInFocus

        public static bool GetSelectAllInFocus(DependencyObject el)
        {
            return (bool)el.GetValue(TextBoxSelectAllInFocus);
        }
        public static void SetSelectAllInFocus(DependencyObject el, bool val)
        {
            el.SetValue(TextBoxSelectAllInFocus, val);
        }

        /// <summary>
        /// Decimal separator, default false "."
        /// </summary>
        public static readonly DependencyProperty TextBoxSelectAllInFocus = DependencyProperty.RegisterAttached(
         "SelectAllInFocus",
         typeof(bool),
         typeof(TextBoxFieldAssistOM),
         new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, SelectAllInFocusChangedCallback));

        private static void SelectAllInFocusChangedCallback(
DependencyObject el,
DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(el is TextBox textBox)) return;
            if ((bool)eventArgs.NewValue)
                textBox.AddSelectAllInFocus();
            else
                textBox.RemoveSelectAllInFocus();
        }
        #endregion

    }
}
