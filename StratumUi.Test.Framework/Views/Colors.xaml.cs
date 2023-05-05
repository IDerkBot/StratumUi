using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StratumUi.Test.Framework.Views
{
    public partial class Colors : UserControl
    {
        public Colors()
        {
            InitializeComponent();
        }
        
        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is StackPanel stackPanel)
            {
                if (stackPanel.Children[1] is TextBlock textBlock)
                {
                    Clipboard.SetText("{StaticResource " + textBlock.Text + "}");
                    ModalDialog.Show("Copy", "Color copied");
                }
            }
        }
    }
}