using System.Windows;

namespace StratumUi.Wpf.Core.Converters;

public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
{
    public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed)
    { }
}