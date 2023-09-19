using System;
using System.Windows;

namespace StratumUi.Test.Core.Views;

public partial class Badges
{
    public Badges() => InitializeComponent();

    private void Badge_OnClick(object sender, EventArgs e) => MessageBox.Show("Success");
}