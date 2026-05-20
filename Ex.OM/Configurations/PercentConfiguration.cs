using Ex.OM.Extentions;
using Ex.OM.Extentions.Converts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ex.OM.Configurations
{
    public static class PercentConfiguration
    {
        public static void AddPercent(this TextBox textBox)
        {
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            textBox.Loaded += TextBox_Loaded;

            if (type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
            {
                textBox.Text = textBox.Text.PercentPositive(position);

            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
            {
                textBox.Text = textBox.Text.PercentNegative(position);

            }
            else
            {
                textBox.Text = textBox.Text.Percent(position);

            }

            textBox.PreviewTextInput += TextBox_PreviewTextInput;
            textBox.LostFocus += TextBox_LostFocus;
            textBox.PreviewKeyUp += TextBox_PreviewKeyUp;
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
            DataObject.AddPastingHandler(textBox, OnPaste);
        }

        public static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var isFraction = (bool)textBox.GetValue(TextBoxFieldAssistOM.IsFractionValueBackProperty);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            var conv = new ConvertDigitPercent
            {
                IsFraction = isFraction,
                CustomTextBox = textBox,
                Type = type,
                NumberDecimal = decimals,
                PositionSymbol = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol),
                ShowSymbol = showSymbol
            };
            textBox.LoadConvert(conv);
        }
        public static void LoadConvertPercent(this TextBox textBox)
        {
            var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);

            var position = $"PositionSymbol:{textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol).ToString()}";
            var isFraction = (bool)textBox.GetValue(TextBoxFieldAssistOM.IsFractionValueBackProperty);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var decimals = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxDecimalNumber);
            var showSymbol = (bool)textBox.GetValue(TextBoxFieldAssistOM.ShowSymbolProperty);

            if (bindingExpression != null)
            {
                var binding = bindingExpression.ParentBinding;
                
                Binding newBinding = new Binding
                {
                    //Source = binding.Source,
                    Mode = binding.Mode == BindingMode.Default? BindingMode.TwoWay: binding.Mode,
                    Converter = new ConvertDigitPercent
                    {
                        IsFraction = isFraction,
                        CustomTextBox = textBox,
                        Type = type,
                        NumberDecimal = decimals,
                        PositionSymbol = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol),
                        ShowSymbol = showSymbol
                    },
                    AsyncState = binding.AsyncState,
                    BindingGroupName = binding.BindingGroupName,
                    BindsDirectlyToSource = binding.BindsDirectlyToSource,
                    ConverterCulture = binding.ConverterCulture,
                    ConverterParameter = position,
                    Delay = binding.Delay,
                    ElementName = binding.ElementName,
                    FallbackValue = binding.FallbackValue,
                    IsAsync = binding.IsAsync,
                    NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated,
                    NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated,
                    NotifyOnValidationError = binding.NotifyOnValidationError,
                    Path = binding.Path,
                    RelativeSource = binding.RelativeSource,
                    StringFormat = binding.StringFormat,
                    TargetNullValue = binding.TargetNullValue,
                    UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter,
                    UpdateSourceTrigger = binding.UpdateSourceTrigger == UpdateSourceTrigger.Default ? UpdateSourceTrigger.PropertyChanged: binding.UpdateSourceTrigger ,
                    ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
                    ValidatesOnExceptions = binding.ValidatesOnExceptions,
                    ValidatesOnNotifyDataErrors = binding.ValidatesOnNotifyDataErrors,
                    XPath = binding.XPath,
                };

                textBox.SetBinding(TextBox.TextProperty, newBinding);
            }
        }

        public static void RemovePercent(this TextBox textBox)
        {
            textBox.PreviewTextInput -= TextBox_PreviewTextInput;
            textBox.LostFocus -= TextBox_LostFocus;
            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
            textBox.PreviewKeyUp -= TextBox_PreviewKeyUp;
            DataObject.RemovePastingHandler(textBox, OnPaste);
        }



        public static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);

            var data = e.SourceDataObject.GetData(DataFormats.UnicodeText) as string;
            DataObject d = new DataObject();

            if (type == TextBoxFieldAssistOM.FormatTextBox.Percent)
            {
                d.SetData(DataFormats.Text, data.Percent(position));
            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
            {
                d.SetData(DataFormats.Text, data.PercentPositive(position));
            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
            {
                d.SetData(DataFormats.Text, data.PercentNegative(position));
            }
            e.DataObject = d;
            //if (!(sender is TextBox textBox)) return;
            //var options = textBox.InitializeOnPaste(e);
            //if (options.isNotText) return;
            //var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);


            //var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);


            //if (type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
            //{
            //    textBox.Text = textBox.Text.PercentPositive(position);

            //}
            //else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
            //{
            //    textBox.Text = textBox.Text.PercentNegative(position);

            //}
            //else
            //{
            //    textBox.Text = textBox.Text.Percent(position);

            //}

            //textBox.CaretIndex = options.caret + options.data.Length;
            //var originalValue = (System.Convert.ToDecimal(textBox.Text.Nvl("0").Digits()) / 100).ToString();
            //textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, originalValue);

            //e.CancelCommand();
        }

        public static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);

            var options = textBox.InitializeTextInput(e, () =>
            {



                if (type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
                {
                    textBox.Text = textBox.Text.PercentPositive(position);

                }
                else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
                {
                    textBox.Text = textBox.Text.PercentNegative(position);

                }
                else
                {
                    textBox.Text = textBox.Text.Percent(position);

                }
                return textBox.Text;

            });

            textBox.CaretIndex = options.discrepancies ? options.caretIndex : options.caret;
            var originalValue = (System.Convert.ToDecimal(textBox.Text.Nvl("0").Digits()) / 100).ToString();
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, originalValue);
            e.Handled = true;
        }

        public static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
            var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);


            if (type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
            {
                textBox.Text = textBox.Text.PercentPositive(position);

            }
            else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
            {
                textBox.Text = textBox.Text.PercentNegative(position);

            }
            else
            {
                textBox.Text = textBox.Text.Percent(position);

            }
            var originalValue = (System.Convert.ToDecimal(textBox.Text.Nvl("0").Digits()) / 100).ToString();
            textBox.SetValue(TextBoxFieldAssistOM.TextBoxOriginalValue, originalValue);

        }

        public static void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (!textBox.IsEnabled) { e.Handled = true; return; }
            if (textBox.IsReadOnly) { e.Handled = true; return; }
            if (e.IsBackOrDeleteKey() || textBox.SelectionLength > 0)
            {
                textBox.SelectionClear();
                var caret = textBox.CaretIndex;

                textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
                var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);


                if (type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
                {
                    textBox.Text = textBox.Text.PercentPositive(position);

                }
                else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
                {
                    textBox.Text = textBox.Text.PercentNegative(position);

                }
                else
                {
                    textBox.Text = textBox.Text.Percent(position);

                }
                caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);

                textBox.CaretIndex = caret;
            }
        }

        public static void TextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (e.IsBackOrDeleteKey())
            {
                var caret = textBox.CaretIndex;
                textBox.SetValue(TextBoxFieldAssistOM.TextBoxCaret, caret);
                var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);
                var type = (TextBoxFieldAssistOM.FormatTextBox)textBox.GetValue(TextBoxFieldAssistOM.TypeTextBoxProperty);


                if (type == TextBoxFieldAssistOM.FormatTextBox.PercentPositive)
                {
                    textBox.Text = textBox.Text.PercentPositive(position);

                }
                else if (type == TextBoxFieldAssistOM.FormatTextBox.PercentNegative)
                {
                    textBox.Text = textBox.Text.PercentNegative(position);

                }
                else
                {
                    textBox.Text = textBox.Text.Percent(position);

                }
                caret = (int)textBox.GetValue(TextBoxFieldAssistOM.TextBoxCaret);
                textBox.CaretIndex = caret;
            }

        }
    }
}
