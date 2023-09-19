using System;
using System.Globalization;
using System.Windows.Data;

namespace StratumUi.Wpf.Core.Converters.CircularProgressBar;

public class RotateTransformCentreConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        //value == actual width
        if (value is double doubleValue)
            return doubleValue / 2.0;
        return 0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}