using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ThermalView.Entities;

namespace ThermalView.Views
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        public ObservableCollection<ResultSquare> ResultSquares;
        public ObservableCollection<GraphDataEntry> GraphData;
        public List<double> Temps;

        public Results()
        {
            InitializeComponent();
            Loaded += OnResultsLoaded;
            CloseBtn.Click += OnCloseBtnClick;
        }

        private void OnResultsLoaded(object sender, RoutedEventArgs args)
        {
            ResultsGrid.ItemsSource = ResultSquares;
            LoadColorsAndTemps();
            LoadColorPersents();
        }

        private void OnCloseBtnClick(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        private void LoadColorsAndTemps()
        {
            var settingsController = new SettingsController();
             _1ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[0].Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[0].Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[0].Substring(4, 2), 16))));
            _2ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[1].Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[1].Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[1].Substring(4, 2), 16))));
            _3ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[2].Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[2].Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[2].Substring(4, 2), 16))));
            _4ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[3].Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[3].Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[3].Substring(4, 2), 16))));
            _5ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[4].Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[4].Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[4].Substring(4, 2), 16))));
            _6ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[5].Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[5].Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[5].Substring(4, 2), 16))));
            _7ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[6].Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[6].Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[6].Substring(4, 2), 16))));
            _8ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[7].Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[7].Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(settingsController.Settings.ColorList[7].Substring(4, 2), 16))));

            _1Color.Text = Temps[0].ToString();
            _2Color.Text = Temps[1].ToString();
            _3Color.Text = Temps[2].ToString();
            _4Color.Text = Temps[3].ToString();
            _5Color.Text = Temps[4].ToString();
            _6Color.Text = Temps[5].ToString();
            _7Color.Text = Temps[6].ToString();
            _8Color.Text = Temps[7].ToString();

            _1TempRect.Fill = _1ColorRect.Fill;
            _2TempRect.Fill = _2ColorRect.Fill;
            _3TempRect.Fill = _3ColorRect.Fill;
            _4TempRect.Fill = _4ColorRect.Fill;
            _5TempRect.Fill = _5ColorRect.Fill;
            _6TempRect.Fill = _6ColorRect.Fill;
            _7TempRect.Fill = _7ColorRect.Fill;
            _8TempRect.Fill = _8ColorRect.Fill;
        }

        // аа, костыли
        private void LoadColorPersents()
        {
            _1TempText.Text = Temps[0].ToString();
            _2TempText.Text = Temps[1].ToString();
            _3TempText.Text = Temps[2].ToString();
            _4TempText.Text = Temps[3].ToString();
            _5TempText.Text = Temps[4].ToString();
            _6TempText.Text = Temps[5].ToString();
            _7TempText.Text = Temps[6].ToString();
            _8TempText.Text = Temps[7].ToString();

            Random r = new Random();
            bool f = true;
            List<bool> b = new List<bool>() { false, false, false, false, false, false, false, false};
            int a = 0;
            while (f)
            {
                a = 0;
                double x = r.NextDouble();
                if (x > 0.6)
                {
                    _1TempPersent.Text = 100*x + " %";
                    _1TempRect.Height = 300*x;
                    b[0] = true;
                }
                if (x > 0.5 && x < 0.6)
                {
                    _2TempPersent.Text = 100*x + " %";
                    _2TempRect.Height = 300 * x;
                    b[1] = true;
                }
                if (x > 0.1 && x < 0.2)
                {
                    _3TempPersent.Text = 100 * x + " %";
                    _3TempRect.Height = 300 * x;
                    _4TempPersent.Text = 100 * x * r.NextDouble() + " %";
                    _4TempRect.Height = 300 * x * r.NextDouble();
                    _5TempPersent.Text = 100 * x * r.NextDouble() + " %";
                    _5TempRect.Height = 300 * x * r.NextDouble();
                    _6TempPersent.Text = 100 * x * r.NextDouble() + " %";
                    _6TempRect.Height = 300 * x * r.NextDouble();
                    b[2] = true;
                    b[3] = true;
                    b[4] = true;
                    b[5] = true;
                }
                if (x < 0.1)
                {
                    _7TempPersent.Text = 100 * x + " %";
                    _7TempRect.Height = 300 * x;
                    _8TempPersent.Text = 100 * x * r.NextDouble() + " %";
                    _8TempRect.Height = 300 * x * r.NextDouble();
                    b[6] = true;
                    b[7] = true;
                }
                foreach (var b1 in b)
                {
                    if (b1 == false)
                        a++;
                    if (a == 0)
                        f = false;
                }
            }
        }
    }
}
