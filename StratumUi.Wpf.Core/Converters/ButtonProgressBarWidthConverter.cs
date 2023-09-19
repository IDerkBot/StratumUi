using System;
using System.Globalization;
using System.Windows.Data;

namespace StratumUi.Wpf.Core.Converters
{
    public class ButtonProgressBarWidthConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return 56;
            var width = (double)value;
            var progressWidth = width - 24;
            return progressWidth < 16 ? 16 : progressWidth;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}