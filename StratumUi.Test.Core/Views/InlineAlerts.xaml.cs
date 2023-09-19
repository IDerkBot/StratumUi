using System;
using StratumUi.Wpf.Core.Controls;

namespace StratumUi.Test.Core.Views;

public partial class InlineAlerts
{
    public InlineAlerts() => InitializeComponent();

    private void InlineAlert_OnPrimaryClick(object sender, EventArgs e) => ModalDialog.Show("", "Click to InlineAlert PRIMARY BUTTON");

    private void InlineAlert_OnSecondaryClick(object sender, EventArgs e) => ModalDialog.Show("","Click to InlineAlert SECONDARY BUTTON");
}