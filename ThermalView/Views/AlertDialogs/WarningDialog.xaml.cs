using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ThermalView.Views.AlertDialogs
{
    /// <summary>
    /// Interaction logic for AlertDialog.xaml
    /// </summary>
    public partial class WarningDialog : Window
    {
        public WarningDialog(string alertText)
        {
            InitializeComponent();
            AlertTex.Text = alertText;
            OKBtn.Click += OnOkButtonClick;
        }

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public new void Show()
        {
            ShowDialog();
        }
    }
}
