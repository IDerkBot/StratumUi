using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StratumUi.Wpf.Core.Converters.CircularProgressBar;

public class ArcSizeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double val and > 0.0)
            return new Size(val / 2, val / 2);

        return new Point();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}