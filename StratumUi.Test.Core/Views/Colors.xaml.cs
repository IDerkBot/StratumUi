using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StratumUi.Wpf.Core.Controls;

namespace StratumUi.Test.Core.Views;

public partial class Colors
{
    public Colors() => InitializeComponent();

    private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not StackPanel stackPanel) return;
        if (stackPanel.Children[1] is not TextBlock textBlock) return;
        Clipboard.SetText("{StaticResource " + textBlock.Text + "}");
        ModalDialog.Show("Copy", "Color copied");
    }
}