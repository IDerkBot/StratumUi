using System.Collections.Generic;
using StratumUi.Test.Framework.Models;
using StratumUi.Test.Framework.ViewModel.Base;

namespace StratumUi.Test.Framework.ViewModel
{
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
                new Person { Age = 20, Surname = "Малахов" },
                new Person { Age = 20, Surname = "Буданов" },
                new Person { Age = 24, Surname = "Лифанов" },
                new Person { Age = 21, Surname = "Малахов" },
                new Person { Age = 21, Surname = "Малахов" },
                new Person { Age = 21, Surname = "Малахов" },
                new Person { Age = 21, Surname = "Малахов" },
            };
        }
    }
}