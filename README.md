![OM](https://gitlab.com/ElierVM/Ex.OM/-/blob/master/OM.png)
# Ex.OM
Extensions and tools for wpf controls

### Installation
![Instalacion](https://gitlab.com/ElierVM/Ex.OM/-/blob/master/Intalacion.PNG)

### namespace
``` xaml
xmlns:exOM="clr-namespace:Ex.OM.Extentions;assembly=Ex.OM"
```



### TextBox Formats

TextBoxType |  Formats       | Binding Mode
------------ | ------------- | -------------
String | `"First example"` | TwoWay
Capitalizer | `"Second Example"`| TwoWay
CapitalizerFirst | `"Second Example"` | TwoWay
OnlyLetter | `"SecondExample"` | TwoWay
OnlyLetterSpace | `"second example"` | TwoWay
OnlyLetterCapitalizer | `"SecondExample"` | TwoWay
OnlyLetterCapitalizerFirst | `"Secondexample"` | TwoWay
OnlyLetterCapitalizerSpace | `"Secondexample"` | TwoWay
OnlyLetterCapitalizerFirstSpace | `"Second example"` | TwoWay
OnlyLetterUpper | `"SECONDEXAMPLE"` | TwoWay
OnlyLetterLower | `"secondexample"` | TwoWay
OnlyLetterUpperSpace | `"SECOND EXAMPLE"` | TwoWay
OnlyLetterLowerSpace | `"second example"` | TwoWay
LetterNumberSpaces | `"Second Example 2"` | TwoWay
TextWithoutSpace | `"Second third Example"` | TwoWay
LetterNumber | `"Example2"` | TwoWay
Lower | `"example second"` | TwoWay
Upper | `"EXAMPLE SECOND"` | TwoWay
Digits | `"123" or "-123"` | TwoWay
DigitsPositive | `"123"` | TwoWay
DigitsNegative | `"-123"` | TwoWay
Decimals | `"123.45" or "-123.45"` | TwoWay
DecimalsNegative | `"-123.45"` | TwoWay
DecimalsPositive | `"123.45"` | TwoWay
Money | `"$123" or "-$123"` | TwoWay 
MoneyNegative | `"-$123"` | TwoWay 
MoneyPositive | `"$123"` | TwoWay 
MoneyDecimal | `"$123.45" or "-$123.45"` | TwoWay 
MoneyDecimalNegative | `"-$123.45"`                         | TwoWay       
MoneyDecimalPositive | `"$123.45"`                          | TwoWay       
Percent | `"12%"` | TwoWay 
PercentNegative | `"-12%"` | TwoWay 
PercentPositive | `"12%"` | TwoWay 
PercentDecimal | `"12.34%" or "-12.34%"` | TwoWay 
PercentDecimalNegative | `"-12.34%"` | TwoWay 
PercentDecimalPositive | `"12.34%"` | TwoWay 
Mask | `"(000) 000-0000" ` ~ (790) 555-5555 | TwoWay 


## Special properties
Properties | Default | Description
---------- | :-----: | :----------
CurrencySymbol | `$` or `%` | symbol that represents the currency or percentage
Caret | 0 | Index in which the caret is located 
Mask | `string.Empty` | Mask 
DecimalNumber | `2` | Number decimals 
NumberIntegers | `0` | Represents whole numbers in a decimal expression, 0 indicates infinity 
OriginalValue | `string.Empty` | Extract original value of text property: example. coin $2,020.12 is equal to 2020.12 in original value 
PositionSymbol | `BeforeSymbol` or `AfterSymbol`| Enumeration that provides the location of the symbol 
PromptCharMask | `'_'` | PromptCharMask of the mask 
RemoveLeadingZeros | `false` | remove leading zeros
SelectAllInFocus | `false` | selects all text value, when it gets focus
ShowSymbol | `true` | Displays the currency 
TypeTextBox | `FormatTextBox` | Format type 
UnMask | `string.Empty` | Unmask 
UnMaskValue | `string.Empty` | Value outside the mask 

## Convert

- `MoneyExOM`
- `MoneyDecimalExOM`
- `MoneyDecimalNegativeExOM`
- `MoneyDecimalPositiveExOM`
- `MoneyPositiveExOM`
- `MoneyNegativeExOM`
- `PercentExOM`
- `PercentPositiveExOM`
- `PercentNegativeExOM`
- `PercentDecimalExOM`
- `PercentDecimalPositiveExOM`
- `PercentDecimalNegativeExOM`
- `DecimalExOM`
- `DecimalPositiveExOM`
- `DecimalNegativeExOM`
- `DigitExOM`
- `DigitPositiveExOM`

- `DigitNegativeExOM`



## ConverterParameter

The format is key and value

- `Decimals` 
- `Symbol` 
- `LimitIntegers` 
- `PositionSymbol` 
- `ShowSymbol` 
- `SeparatorDecimal`

##### Example

```xaml
<TextBox Text="{Binding example, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged, Converter={StaticResource MoneyDecimalExOM}, ConverterParameter='Decimals:3, Symbol:%'}"/>
```

![Result](https://gitlab.com/ElierVM/Ex.OM/-/blob/master/SubTotal.png)



## Known Limitations

**TextBox**: You cannot assign convert to the text property as this is used for the plugin to work properly.
