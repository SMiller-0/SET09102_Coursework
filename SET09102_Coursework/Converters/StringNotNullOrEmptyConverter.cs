using System;
using System.Globalization;
using Microsoft.Maui.Controls; 

namespace SET09102_Coursework.Converters;

public class StringNotNullOrEmptyConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var str = value as string;
        return !string.IsNullOrWhiteSpace(str); 
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException(); 
    }
}
