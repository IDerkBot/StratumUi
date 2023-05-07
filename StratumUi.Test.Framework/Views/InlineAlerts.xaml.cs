using System;
using System.Windows.Controls;
using StratumUi.Wpf.Controls;

namespace StratumUi.Test.Framework.Views
{
    public partial class InlineAlerts : UserControl
    {
        public InlineAlerts()
        {
            InitializeComponent();
        }
        
        private void InlineAlert_OnPrimaryClick(object sender, EventArgs e)
        {
            ModalDialog.Show("", "Click to InlineAlert PRIMARY BUTTON");
        }

        private void InlineAlert_OnSecondaryClick(object sender, EventArgs e)
        {
            ModalDialog.Show("","Click to InlineAlert SECONDARY BUTTON");
        }
    }
}