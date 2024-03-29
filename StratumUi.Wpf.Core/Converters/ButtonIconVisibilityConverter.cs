﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using StratumUi.Wpf.Core.Controls;

namespace StratumUi.Wpf.Core.Converters;

public class ButtonIconVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return Visibility.Collapsed;
        var icon = (EIcons)value;
        return icon == EIcons.NULL ? Visibility.Collapsed : Visibility.Visible;

    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}