using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StratumUi.Wpf.Core.Converters;

public class ComboBoxVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is -1 ? Visibility.Visible : Visibility.Collapsed;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}