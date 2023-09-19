﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StratumUi.Wpf.Core.Converters;

public class PasswordBoxPlaceholderVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolean)
            return boolean ? Visibility.Visible : Visibility.Collapsed;

        return Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}