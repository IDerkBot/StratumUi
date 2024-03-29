﻿using System.Windows;
using System.Windows.Media;
using StratumUi.Wpf.Core.AttachedProperties.Enum;
using StratumUi.Wpf.Core.Controls;

namespace StratumUi.Wpf.Core.AttachedProperties;

public class Switcher
{
    #region Position

    public static readonly DependencyProperty PositionProperty = DependencyProperty.RegisterAttached(
        "Position", typeof(Position), typeof(Switcher), new PropertyMetadata(default(Position)));

    public static void SetPosition(DependencyObject element, Position value)
    {
        element.SetValue(PositionProperty, value);
    }

    public static Position GetPosition(DependencyObject element)
    {
        return (Position)element.GetValue(PositionProperty);
    }

    #endregion

    #region Left Icon

    public static readonly DependencyProperty LeftIconProperty = DependencyProperty.RegisterAttached(
        "LeftIcon", typeof(EIcons), typeof(Switcher), new PropertyMetadata(default(EIcons)));

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
        "RightIcon", typeof(EIcons), typeof(Switcher), new PropertyMetadata(default(EIcons)));

    public static void SetRightIcon(DependencyObject element, EIcons value)
    {
        element.SetValue(RightIconProperty, value);
    }

    public static EIcons GetRightIcon(DependencyObject element)
    {
        return (EIcons)element.GetValue(RightIconProperty);
    }

    #endregion

    #region Center Icon

    public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
        "Icon", typeof(EIcons), typeof(Switcher), new PropertyMetadata(default(EIcons)));

    public static void SetIcon(DependencyObject element, EIcons value)
    {
        element.SetValue(IconProperty, value);
    }

    public static EIcons GetIcon(DependencyObject element)
    {
        return (EIcons)element.GetValue(IconProperty);
    }

    #endregion

    #region IconWidth

    public static readonly DependencyProperty IconWidthProperty = DependencyProperty.RegisterAttached(
        "IconWidth", typeof(double), typeof(Switcher), new PropertyMetadata(default(double)));

    public static void SetIconWidth(DependencyObject element, double value)
    {
        element.SetValue(IconWidthProperty, value);
    }

    public static double GetIconWidth(DependencyObject element)
    {
        return (double)element.GetValue(IconWidthProperty);
    }

    #endregion IconWidth

    #region IconHeight

    public static readonly DependencyProperty IconHeightProperty = DependencyProperty.RegisterAttached(
        "IconHeight", typeof(double), typeof(Switcher), new PropertyMetadata(default(double)));

    public static void SetIconHeight(DependencyObject element, double value)
    {
        element.SetValue(IconHeightProperty, value);
    }

    public static double GetIconHeight(DependencyObject element)
    {
        return (double)element.GetValue(IconHeightProperty);
    }

    #endregion IconHeight

    #region IconStrokeColor

    public static readonly DependencyProperty IconStrokeColorProperty = DependencyProperty.RegisterAttached(
        "IconStrokeColor", typeof(SolidColorBrush), typeof(Switcher), new PropertyMetadata(default(SolidColorBrush)));

    public static void SetIconStrokeColor(DependencyObject element, SolidColorBrush value)
    {
        element.SetValue(IconStrokeColorProperty, value);
    }

    public static SolidColorBrush GetIconStrokeColor(DependencyObject element)
    {
        return (SolidColorBrush)element.GetValue(IconStrokeColorProperty);
    }

    #endregion IconStrokeColor

    #region IconStrokeThickness

    public static readonly DependencyProperty IconStrokeThicknessProperty = DependencyProperty.RegisterAttached(
        "IconStrokeThickness", typeof(double), typeof(Switcher), new PropertyMetadata(default(double)));

    public static void SetIconStrokeThickness(DependencyObject element, double value)
    {
        element.SetValue(IconStrokeThicknessProperty, value);
    }

    public static double GetIconStrokeThickness(DependencyObject element)
    {
        return (double)element.GetValue(IconStrokeThicknessProperty);
    }

    #endregion IconStrokeThickness

    #region BorderThicknessSelected

    public static readonly DependencyProperty BorderThicknessSelectedProperty = DependencyProperty.RegisterAttached(
        "BorderThicknessSelected", typeof(Thickness), typeof(Switcher), new PropertyMetadata(default(Thickness)));

    public static void SetBorderThicknessSelected(DependencyObject element, Thickness value)
    {
        element.SetValue(BorderThicknessSelectedProperty, value);
    }

    public static Thickness GetBorderThicknessSelected(DependencyObject element)
    {
        return (Thickness)element.GetValue(BorderThicknessSelectedProperty);
    }

    #endregion BorderThicknessSelected
}