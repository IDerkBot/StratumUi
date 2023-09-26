using System.Windows.Input;
using StratumUi.Test.Core.Infrastructure.Commands;
using StratumUi.Test.Core.ViewModel.Base;

namespace StratumUi.Test.Core.ViewModel;

public class HomeViewModel : BaseViewModel
{
    private readonly MainViewModel _owner;

    #region Move - Перемещение на другую вкладку

    ///<summary> Перемещение на другую вкладку </summary>
    public ICommand MoveCommand { get; }

    private void OnMoveCommandExecuted(object parameter)
    {
        if (int.TryParse(parameter.ToString(), out var intValue))
            _owner.IndexSelectedTab = intValue;
    }

    private bool CanMoveCommandExecute(object parameter)
    {
        return true;
    }

    #endregion Move
    
    public HomeViewModel(MainViewModel owner)
    {
        _owner = owner;
        MoveCommand = new RelayCommand(OnMoveCommandExecuted, CanMoveCommandExecute);
    }
}