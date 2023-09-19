using StratumUi.Test.Core.Models;

namespace StratumUi.Test.Core.ViewModel;

public class TextBoxViewModel
{
    public Person Person { get; set; }

    public TextBoxViewModel()
    {
        Person = new Person
        {
            Surname = "Малахов",
            Age = 20
        };
    }
}