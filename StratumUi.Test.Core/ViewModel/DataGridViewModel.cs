using System.Collections.ObjectModel;
using StratumUi.Test.Core.Models;
using StratumUi.Test.Core.ViewModel.Base;

namespace StratumUi.Test.Core.ViewModel;

public class DataGridViewModel : BaseViewModel
{
    #region Persons : ObservableCollection<Person> - Коллекция

    private ObservableCollection<Person> _persons;

    /// <summary> Коллекция </summary>
    public ObservableCollection<Person> Persons
    {
        get => _persons;
        set => SetField(ref _persons, value);
    }

    #endregion Persons

    public DataGridViewModel()
    {
        Persons = new ObservableCollection<Person>()
        {
            new() { Age = 18, IsMale = true, Surname = "Иванов"},
            new() { Age = 18, IsMale = true, Surname = "Смирнов"},
            new() { Age = 18, IsMale = true, Surname = "Кузнецов"},
            new() { Age = 18, IsMale = true, Surname = "Попов"},
            new() { Age = 18, IsMale = true, Surname = "Васильев"},
            new() { Age = 18, IsMale = true, Surname = "Петров"},
            new() { Age = 18, IsMale = true, Surname = "Соколов"},
            new() { Age = 18, IsMale = true, Surname = "Михайлов"},
        };
    }
}