﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace StratumUi.Wpf.Core.Commands;

public static class ClearText
{
    public static readonly RoutedCommand ClearCommand = new();
        
    public static bool GetHandlesClearCommand(DependencyObject obj)
        => (bool)obj.GetValue(HandlesClearCommandProperty);

    public static void SetHandlesClearCommand(DependencyObject obj, bool value)
        => obj.SetValue(HandlesClearCommandProperty, value);

    public static readonly DependencyProperty HandlesClearCommandProperty =
        DependencyProperty.RegisterAttached("HandlesClearCommand", typeof(bool), typeof(ClearText), new PropertyMetadata(false, OnHandlesClearCommandChanged));

    private static void OnHandlesClearCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not UIElement element) return;
        if ((bool)e.NewValue)
            element.CommandBindings.Add(new CommandBinding(ClearCommand, OnClearCommand));
        else
        {
            for (var i = element.CommandBindings.Count - 1; i >= 0; i--)
            {
                if (element.CommandBindings[i].Command == ClearCommand) element.CommandBindings.RemoveAt(i);
            }
        }
    }
        
    private static void OnClearCommand(object sender, ExecutedRoutedEventArgs e)
    {
        switch (e.Source)
        {
            case DatePicker datePicker:
                datePicker.SetCurrentValue(DatePicker.SelectedDateProperty, null);
                break;
            case TextBox textBox:
                textBox.SetCurrentValue(TextBox.TextProperty, "");
                break;
            case ComboBox comboBox:
                comboBox.SetCurrentValue(ComboBox.TextProperty, null);
                comboBox.SetCurrentValue(Selector.SelectedItemProperty, null);
                break;
            case PasswordBox passwordBox:
                passwordBox.Password = "";
                break;
        }
        e.Handled = true;
    }
}