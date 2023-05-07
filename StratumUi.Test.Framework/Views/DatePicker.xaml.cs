using System.Windows.Controls;

namespace StratumUi.Test.Framework.Views
{
    public partial class DatePicker : UserControl
    {
        public DatePicker()
        {
            InitializeComponent();
            DpDatePicker.BlackoutDates.AddDatesInPast();
        }
    }
}