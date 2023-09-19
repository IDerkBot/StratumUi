using System.Windows;

namespace StratumUi.Wpf.Core.Converters;

public class ButtonIconVisibilityConverter : BooleanConverter<Visibility>
{
    public ButtonIconVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
    { }
}