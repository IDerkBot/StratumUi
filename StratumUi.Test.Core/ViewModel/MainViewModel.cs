using StratumUi.Test.Core.ViewModel.Base;

namespace StratumUi.Test.Core.ViewModel;

public class MainViewModel : BaseViewModel
{
    #region IndexSelectedTab : int - Index selected tab

    private int _indexSelectedTab;

    /// <summary> Index selected tab </summary>
    public int IndexSelectedTab
    {
        get => _indexSelectedTab;
        set => SetField(ref _indexSelectedTab, value);
    }

    #endregion SelectedTab

    #region HomeVM : HomeViewModel - View model for home view

    private HomeViewModel _homeVm;

    /// <summary> View model for home view </summary>
    public HomeViewModel HomeVM
    {
        get => _homeVm;
        set => SetField(ref _homeVm, value);
    }

    #endregion HomeViewModel

    public MainViewModel()
    {
        HomeVM = new HomeViewModel(this);
    }
}