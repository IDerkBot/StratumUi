using System.Windows;

namespace StratumUi.Wpf.Core.Converters;

public class BoolToHorizontalConverter : BooleanConverter<HorizontalAlignment>
{
    public BoolToHorizontalConverter() : base(HorizontalAlignment.Left, HorizontalAlignment.Right)
    { }
}