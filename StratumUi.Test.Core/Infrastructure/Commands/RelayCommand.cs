﻿using System;

namespace StratumUi.Test.Core.Infrastructure.Commands;

public class RelayCommand : Base.Command
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    /// <summary>  </summary>
    /// <param name="execute"></param>
    /// <param name="canExecute"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

    public override void Execute(object parameter) => _execute(parameter);
}