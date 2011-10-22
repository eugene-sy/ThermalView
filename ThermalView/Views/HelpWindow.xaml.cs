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
using ThermalView.Controllers;

namespace ThermalView.Views
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
            this.Loaded +=new RoutedEventHandler(OnHelpWindowLoaded);
            this.OKBtn.Click += new RoutedEventHandler(OnOkBtnClick);
            this.SendFeetbackBtn.Click += new RoutedEventHandler(OnSendFeedbackBtnClick);
        }

        private void OnHelpWindowLoaded(object sender, RoutedEventArgs args)
        {
            //this.HelpContent;
        }

        private void OnOkBtnClick(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        private void OnSendFeedbackBtnClick(object sender, RoutedEventArgs args)
        {
            //hello world!
        }
    }
}
