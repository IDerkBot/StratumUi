namespace StratumUi.Wpf.Core.Converters;

public class InvertBooleanConverter : BooleanConverter<bool>
{
    public InvertBooleanConverter() : base(false, true)
    {
    }
}