using System;
using System.Windows;
using System.Windows.Controls;

namespace StratumUi.Test.Framework.Views
{
    public partial class Badges : UserControl
    {
        public Badges()
        {
            InitializeComponent();
        }

        private void Badge_OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Success");
        }
    }
}