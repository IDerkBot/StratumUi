using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace StratumUi.Wpf.Core.Controls;

public class RangeSliderAutoTooltipValues : INotifyPropertyChanged
{
    private string? lowerValue;

    /// <summary>
    /// Gets the lower value of the range selection.
    /// </summary>
    public string? LowerValue
    {
        get => this.lowerValue;
        set
        {
            if (value == this.lowerValue)
            {
                return;
            }

            this.lowerValue = value;
            this.OnPropertyChanged();
        }
    }

    private string? upperValue;

    /// <summary>
    /// Gets the upper value of the range selection.
    /// </summary>
    public string? UpperValue
    {
        get => this.upperValue;
        set
        {
            if (value == this.upperValue)
            {
                return;
            }

            this.upperValue = value;
            this.OnPropertyChanged();
        }
    }

    internal RangeSliderAutoTooltipValues(RangeSlider2 rangeSlider)
    {
        this.UpdateValues(rangeSlider);
    }

    internal void UpdateValues(RangeSlider2 rangeSlider)
    {
        this.LowerValue = rangeSlider.GetToolTipNumber(rangeSlider.LowerValue);
        this.UpperValue = rangeSlider.GetToolTipNumber(rangeSlider.UpperValue);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{this.LowerValue} - {this.UpperValue}";
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}