using System.Windows;

namespace StratumUi.Wpf.Core.AttachedProperties
{
    public class RadioButton
    {
        #region Description

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.RegisterAttached(
            "Description", typeof(string), typeof(RadioButton), new PropertyMetadata(default(string)));

        public static void SetDescription(DependencyObject element, string value)
        {
            element.SetValue(DescriptionProperty, value);
        }

        public static string GetDescription(DependencyObject element)
        {
            return (string)element.GetValue(DescriptionProperty);
        }

        #endregion
    }
}