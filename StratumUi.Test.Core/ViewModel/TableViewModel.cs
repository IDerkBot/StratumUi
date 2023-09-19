using System.Collections.Generic;
using StratumUi.Test.Core.Models;
using StratumUi.Test.Core.ViewModel.Base;

namespace StratumUi.Test.Core.ViewModel;

public class TableViewModel : BaseViewModel
{
    private List<Person> _persons;

    public List<Person> Persons
    {
        get => _persons;
        set => SetField(ref _persons, value);
    }

    public TableViewModel()
    {
        Persons = new List<Person>
        {
            new() { Age = 20, Surname = "Малахов" },
            new() { Age = 20, Surname = "Буданов" },
            new() { Age = 24, Surname = "Лифанов" },
            new() { Age = 21, Surname = "Малахов" },
            new() { Age = 21, Surname = "Малахов" },
            new() { Age = 21, Surname = "Малахов" },
            new() { Age = 21, Surname = "Малахов" },
        };
    }
}