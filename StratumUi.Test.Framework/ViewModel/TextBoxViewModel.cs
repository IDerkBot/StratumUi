using StratumUi.Test.Framework.Models;

namespace StratumUi.Test.Framework.ViewModel
{
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
}