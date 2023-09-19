using System.Threading;
using System.Windows;
using StratumUi.Wpf.Core.Controls;

namespace StratumUi.Test.Core.Views;

public partial class Dialogs
{
    public Dialogs()
    {
        InitializeComponent();
    }

    private void BtnOpenModalDialogInOtherThread_Onclick(object sender, RoutedEventArgs e)
    {
        new Thread(() =>
        {
            ModalDialog.Show("Уведомление", "Внимание\nВы уведомлены!");
        }).Start();
    }
        
    private void BtnOpenModalDialogInCurrentThread_Onclick(object sender, RoutedEventArgs e)
    {
        ModalDialog.Show("Уведомление", "Внимание\nВы уведомлены!");
    }
}