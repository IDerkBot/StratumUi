using System;
using System.Globalization;
using System.Windows.Data;

namespace StratumUi.Wpf.Core.Converters;

public class IsGreaterThanConverter : IValueConverter
{
    public static readonly IValueConverter Instance = new IsGreaterThanConverter();
    
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var doubleValue = System.Convert.ToDouble(value);
        var compareToValue = System.Convert.ToDouble(parameter);

        return doubleValue > compareToValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IsLessThanConverter : IValueConverter
{
    public static readonly IValueConverter Instance = new IsLessThanConverter();
    
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var doubleValue = System.Convert.ToDouble(value);
        var compareToValue = System.Convert.ToDouble(parameter);

        return doubleValue < compareToValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}