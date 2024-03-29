﻿using System.Windows;
using StratumUi.Wpf.Core.Controls;

namespace StratumUi.Wpf.Core.AttachedProperties;

public static class TextBox
{
    #region Prefix

    public static readonly DependencyProperty PrefixProperty = DependencyProperty.RegisterAttached(
        "Prefix", typeof(string), typeof(TextBox), new PropertyMetadata(default(string)));

    public static void SetPrefix(DependencyObject element, string value)
    {
        element.SetValue(PrefixProperty, value);
    }

    public static string GetPrefix(DependencyObject element)
    {
        return (string)element.GetValue(PrefixProperty);
    }

    #endregion

    #region Suffix

    public static readonly DependencyProperty SuffixProperty = DependencyProperty.RegisterAttached(
        "Suffix", typeof(string), typeof(TextBox), new PropertyMetadata(default(string)));

    public static void SetSuffix(DependencyObject element, string value)
    {
        element.SetValue(SuffixProperty, value);
    }

    public static string GetSuffix(DependencyObject element)
    {
        return (string)element.GetValue(SuffixProperty);
    }

    #endregion

    #region CanClear

    public static readonly DependencyProperty CanClearProperty = DependencyProperty.RegisterAttached(
        "CanClear", typeof(bool), typeof(TextBox), new PropertyMetadata(default(bool)));

    public static void SetCanClear(DependencyObject element, bool value)
    {
        element.SetValue(CanClearProperty, value);
    }

    public static bool GetCanClear(DependencyObject element)
    {
        return (bool)element.GetValue(CanClearProperty);
    }

    #endregion

    #region Left Icon

    public static readonly DependencyProperty LeftIconProperty = DependencyProperty.RegisterAttached(
        "LeftIcon", typeof(EIcons), typeof(TextBox), new PropertyMetadata(default(EIcons)));

    public static void SetLeftIcon(DependencyObject element, EIcons value)
    {
        element.SetValue(LeftIconProperty, value);
    }

    public static EIcons GetLeftIcon(DependencyObject element)
    {
        return (EIcons)element.GetValue(LeftIconProperty);
    }

    #endregion

    #region Right Icon

    public static readonly DependencyProperty RightIconProperty = DependencyProperty.RegisterAttached(
        "RightIcon", typeof(EIcons), typeof(TextBox), new PropertyMetadata(default(EIcons)));

    public static void SetRightIcon(DependencyObject element, EIcons value)
    {
        element.SetValue(RightIconProperty, value);
    }

    public static EIcons GetRightIcon(DependencyObject element)
    {
        return (EIcons)element.GetValue(RightIconProperty);
    }

    #endregion

    #region Size Icon

    #region Width

    public static readonly DependencyProperty IconWidthProperty = DependencyProperty.RegisterAttached(
        "IconWidth", typeof(double), typeof(TextBox), new PropertyMetadata(default(double)));

    public static void SetIconWidth(DependencyObject element, double value)
    {
        element.SetValue(IconWidthProperty, value);
    }

    public static double GetIconWidth(DependencyObject element)
    {
        return (double)element.GetValue(IconWidthProperty);
    }

    #endregion

    #region Height

    public static readonly DependencyProperty IconHeightProperty = DependencyProperty.RegisterAttached(
        "IconHeight", typeof(double), typeof(TextBox), new PropertyMetadata(default(double)));

    public static void SetIconHeight(DependencyObject element, double value)
    {
        element.SetValue(IconHeightProperty, value);
    }

    public static double GetIconHeight(DependencyObject element)
    {
        return (double)element.GetValue(IconHeightProperty);
    }

    #endregion

    #endregion

    public static readonly DependencyProperty IsNumberProperty = DependencyProperty.RegisterAttached(
        "IsNumber", typeof(bool), typeof(TextBox), new PropertyMetadata(default(bool)));

    public static void SetIsNumber(DependencyObject element, bool value)
    {
        element.SetValue(IsNumberProperty, value);
    }

    public static bool GetIsNumber(DependencyObject element)
    {
        return (bool)element.GetValue(IsNumberProperty);
    }
}