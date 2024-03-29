﻿using System.Windows;
using StratumUi.Wpf.Core.Helpers;

namespace StratumUi.Test.Core.Views;

public partial class Buttons
{
    private readonly CallerOnlyOnce _ctxOnlyOnce;
    public Buttons()
    {
        InitializeComponent();
        _ctxOnlyOnce = new CallerOnlyOnce(500);
    }
        
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var message = $"Complete{++counter}";
        _ctxOnlyOnce.CallOnce(() => Debuger(message));
    }

    private int counter;
        
    private void Debuger(string message)
    {
        DisableButton.Content = message;
    }
}