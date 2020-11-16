![OM](https://raw.githubusercontent.com/EliezerVM/Ex.OM/master/OM.png)
# Ex.OM
Extensions and tools for wpf controls

### Installation
![Instalacion](https://raw.githubusercontent.com/EliezerVM/Ex.OM/master/Intalacion.PNG)

### namespace
> `xmlns:exOM="clr-namespace:Ex.OM.Extentions;assembly=Ex.OM"`


### TextBox Formats

TextBoxtType | Formats       | Binding Mode
------------ | ------------- | ------------
String | `"example"`| TwoWay
Capitalizer | `"Second Example"`| TwoWay
CapitalizerFirst | `"second Example"` | TwoWay
OnlyLetter | `"SecondExample"` | TwoWay
OnlyLetterSpace | `"second example"` | TwoWay
OnlyLetterCapitalizer | `"secondExample"` | TwoWay
OnlyLetterCapitalizerFirst | `"secondExample"` | TwoWay
OnlyLetterCapitalizerSpace | `"Second Example"` | TwoWay
OnlyLetterCapitalizerFirstSpace | `"Second example"`| TwoWay
OnlyLetterUpper | `"SECONDEXAMPLE"` | TwoWay
OnlyLetterLower | `"secondexample"` | TwoWay
OnlyLetterUpperSpace | `"SECOND EXAMPLE"` | TwoWay
OnlyLetterLowerSpace | `"second example"` | TwoWay
LetterNumberSpaces | `"Second Example 2"` | TwoWay
TextWithoutSpace | `"Second third Example"` | TwoWay
LetterNumber | `"Example2Second"` | TwoWay
Lower | `"example second"` | TwoWay
Upper | `"EXAMPLE SECOND"` | TwoWay
Digits | `"123" or "-123"` | TwoWay
DigitsPositive | `"123"` | TwoWay
DigitsNegative | `"-123"` | TwoWay
Decimals | `"123.45" or "-123.45"` | TwoWay
DecimalsNegative | `"-123.45"` | TwoWay
DecimalsPositive | `"123.45"` | TwoWay
Money | `"$ 123" or "-$ 123"` | OneWay and OriginalValue TwoWay
MoneyNegative | `"-$ 123"` | OneWay and OriginalValue TwoWay
MoneyPositive | `"$ 123"` | OneWay and OriginalValue TwoWay
MoneyDecimal | `"$ 123.45" or "-$ 123.45"` | OneWay and OriginalValue TwoWay
MoneyDecimalNegative | `"-$ 123.45"` | OneWay and OriginalValue TwoWay
MoneyDecimalPositive | `"$ 123.45"` | OneWay and OriginalValue TwoWay
Percent | `"12%"` | OneWay and OriginalValue TwoWay
PercentNegative | `"-12%"` | OneWay and OriginalValue TwoWay
PercentPositive | `"12%"` | OneWay and OriginalValue TwoWay
PercentDecimal | `"12.34%" or "-12.34%"` | OneWay and OriginalValue TwoWay
PercentDecimalNegative | `"-12.34%"` | OneWay and OriginalValue TwoWay
PercentDecimalPositive | `"12.34%"` | OneWay and OriginalValue TwoWay

##### example <br>
`<TextBox  Text="{Binding example, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
           exOM:TextBoxFieldAssistOM.TypeTextBox="Money"/>`

## Special properties
Properties | Default | Description
---------- | ------- | -----------
CurrencySymbol | `$` or `%` | symbol that represents the currency or percentage
DecimalSeparator | `.` | decimal separator
NumberDecimals | `2` | number decimals
NumberIntegers | `0` | represents whole numbers in a decimal expression, 0 indicates infinity
OriginalValue | `null` | extract original value of text property: example. coin $2,020.12 is equal to 2020.12 in original value
PositionSymbol | `BeforeSymbol` or `AfterSymbol`| enumeration that provides the location of the symbol
RemoveLeadingZeros | `false` | remove leading zeros
SelectAllInFocus | `false` | selects all text value, when it gets focus
ShowCurrencySymbol | `true` | displays the currency or percent symbol
ThousandthSeparator | `,` | thousandth separator
TypeTextBox | `Text` | type of textbox
Mask | `null` | mask
UnMask | `null` | unmask

## Convert

### Resource.
add resource in app.xaml `<ResourceDictionary Source="pack://application:,,,/Ex.OM;component/Ex.OM.Resource.xaml" />`


Convert <br>
`MoneyExOM` <br>
`MoneyDecimalExOM`<br>
`MoneyDecimalNegativeExOM` <br>
`MoneyDecimalPositiveExOM` <br>
`MoneyPositiveExOM` <br>
`MoneyNegativeExOM` <br>
`PercentExOM` <br>
`PercentPositiveExOM` <br>
`PercentNegativeExOM` <br>
`PercentDecimalExOM` <br>
`PercentDecimalPositiveExOM` <br>
`PercentDecimalNegativeExOM` <br>
`DecimalExOM` <br>
`DecimalPositiveExOM`<br>
`DecimalNegativeExOM`<br>
`DigitExOM`<br>
`DigitPositiveExOM`<br>
`DigitNegativeExOM`<br>

## ConverterParameter
The format is key and value

`Decimals`
`Symbol`
`LimitIntegers`
`PositionSymbol`
`ShowSymbol`
`SeparatorDecimal`

example

`<TextBox
Text="{Binding example, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged, Converter={StaticResource MoneyDecimalExOM}, ConverterParameter='Decimals:3, Symbol:%'}"/>`
<br>


![Result](https://raw.githubusercontent.com/EliezerVM/Ex.OM/master/SubTotal.png)

<br>


When you use conversions, you don't need dependency properties

