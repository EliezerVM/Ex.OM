using Ex.OM.Extentions;
using Ex.OM.Extentions.Converts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ex.OM.Configurations
{
    public static class PercentPositiveConfiguration
    {
        public static void AddPercentPositive(this TextBox textBox)
        {
            var position = (TextBoxFieldAssistOM.PositionSymbol)textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol);

            textBox.Loaded += PercentConfiguration.TextBox_Loaded;

            textBox.Text = textBox.Text.PercentPositive(position);
            textBox.PreviewTextInput += PercentConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus += PercentConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyUp += PercentConfiguration.TextBox_PreviewKeyUp;
            textBox.PreviewKeyDown += PercentConfiguration.TextBox_PreviewKeyDown;
            DataObject.AddPastingHandler(textBox, PercentConfiguration.OnPaste);
        }

        public static void LoadConvertPercentPositive(this TextBox textBox)
        {
            var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);

            var position = $"PositionSymbol:{textBox.GetValue(TextBoxFieldAssistOM.TextBoxPositionSymbol).ToString()}";

            if (bindingExpression != null)
            {
                var binding = bindingExpression.ParentBinding;
                Binding newBinding = new Binding
                {
                    //Source = binding.Source,
                    Mode = binding.Mode,
                    Converter = new ConvertDigitPercent(),
                    AsyncState = binding.AsyncState,
                    BindingGroupName = binding.BindingGroupName,
                    BindsDirectlyToSource = binding.BindsDirectlyToSource,
                    ConverterCulture = binding.ConverterCulture,
                    ConverterParameter = position,
                    Delay = binding.Delay,
                    //ElementName = binding.ElementName,
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
                    UpdateSourceTrigger = binding.UpdateSourceTrigger,
                    ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
                    ValidatesOnExceptions = binding.ValidatesOnExceptions,
                    ValidatesOnNotifyDataErrors = binding.ValidatesOnNotifyDataErrors,
                    XPath = binding.XPath,
                };

                textBox.SetBinding(TextBox.TextProperty, newBinding);
            }
        }

        public static void RemovePercentPositive(this TextBox textBox)
        {
            textBox.PreviewTextInput -= PercentConfiguration.TextBox_PreviewTextInput;
            textBox.LostFocus -= PercentConfiguration.TextBox_LostFocus;
            textBox.PreviewKeyDown -= PercentConfiguration.TextBox_PreviewKeyDown;
            textBox.PreviewKeyUp -= PercentConfiguration.TextBox_PreviewKeyUp;
            DataObject.RemovePastingHandler(textBox, PercentConfiguration.OnPaste);
        }



      
    }
}
