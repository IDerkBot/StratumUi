using System;
using System.Windows.Controls;

namespace StratumUi.Test.Framework.Views
{
    public partial class DisplayControls : UserControl
    {
        public DisplayControls()
        {
            InitializeComponent();
        }
        
        private void DisplayControlFull_OnTargetValueChange(object sender, EventArgs e)
        {
            // MessageBox.Show("Change");
        }
    }
}