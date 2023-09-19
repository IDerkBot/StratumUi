using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StratumUi.Wpf.Core.Converters.CircularProgressBar;

public class StartPointConverter : IValueConverter
{
    [Obsolete]
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is double doubleValue and > 0.0 ? new Point(doubleValue / 2, 0) : new Point();
    }

    [Obsolete]
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Binding.DoNothing;
}