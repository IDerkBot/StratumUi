﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StratumUi.Wpf.Core.Converters;

public class TextVisibilityConverter : IValueConverter
{
    public Visibility IsEmptyValue { get; set; } = Visibility.Visible;
    public Visibility IsNotEmptyValue { get; set; } = Visibility.Hidden;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => string.IsNullOrEmpty((value ?? "").ToString()) ? IsEmptyValue : IsNotEmptyValue;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Binding.DoNothing;
}