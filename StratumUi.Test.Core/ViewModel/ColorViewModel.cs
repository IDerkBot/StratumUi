﻿using System.Windows;
using System.Windows.Input;
using StratumUi.Test.Core.ViewModel.Base;

namespace StratumUi.Test.Core.ViewModel;

public class ColorViewModel : BaseViewModel
{
    public static readonly RoutedCommand CopyColor = new();
        
    public static bool GetHandlesCopyColorCommand(DependencyObject obj)
        => (bool)obj.GetValue(HandlesCopyColorCommandProperty);

    public static void SetHandlesCopyColorCommand(DependencyObject obj, bool value)
        => obj.SetValue(HandlesCopyColorCommandProperty, value);

    public static readonly DependencyProperty HandlesCopyColorCommandProperty =
        DependencyProperty.RegisterAttached("HandlesCopyColorCommand", typeof(bool), typeof(ColorViewModel), new PropertyMetadata(false, OnHandlesClearCommandChanged));

    private static void OnHandlesClearCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not UIElement element) return;
        if ((bool)e.NewValue)
            element.CommandBindings.Add(new CommandBinding(CopyColor, OnCopyColorCommand));
        else
            for (var i = element.CommandBindings.Count - 1; i >= 0; i--)
                if (element.CommandBindings[i].Command == CopyColor)
                    element.CommandBindings.RemoveAt(i);
    }
        
    private static void OnCopyColorCommand(object sender, ExecutedRoutedEventArgs e)
    {
        // switch (e.Source)
        // {
        //     case DatePicker datePicker:
        //         datePicker.SetCurrentValue(DatePicker.SelectedDateProperty, null);
        //         break;
        //     case TextBox textBox:
        //         textBox.SetCurrentValue(TextBox.TextProperty, "");
        //         break;
        //     case ComboBox comboBox:
        //         comboBox.SetCurrentValue(ComboBox.TextProperty, null);
        //         comboBox.SetCurrentValue(Selector.SelectedItemProperty, null);
        //         break;
        //     case PasswordBox passwordBox:
        //         passwordBox.Password = "";
        //         break;
        // }
        // e.Handled = true;
    }
}