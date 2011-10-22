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
using ThermalView.SetUp;
using ThermalView.Controllers;
using ThermalView.Entities;
using ThermalView.Views.AlertDialogs;

namespace ThermalView.Views
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        private Settings _settings;

        public SettingsDialog()
        {
            InitializeComponent();
            Init();
            Loaded += OnSettingsDialogLoaded;
            ApplySettingsBtn.Click += OnApplySettingsBtnClick;
            OKBtn.Click += OnOkBtnClick;
            DefaultResolution.Click += OnDefaultResolutionChecked;
            CustomResolution.Click += OnCustomResolutionSelected;
            _4colors.Click += On4ColorsSelected;
            _8colors.Click += On8ColorsSelected;
            _1Color.LostFocus += On1ColorBoxFocusLost;
            _2Color.LostFocus += On2ColorBoxFocusLost;
            _3Color.LostFocus += On3ColorBoxFocusLost;
            _4Color.LostFocus += On4ColorBoxFocusLost;
            _5Color.LostFocus += On5ColorBoxFocusLost;
            _6Color.LostFocus += On6ColorBoxFocusLost;
            _7Color.LostFocus += On7ColorBoxFocusLost;
            _8Color.LostFocus += On8ColorBoxFocusLost;
        }


        #region        // delegated functions

        private void OnSettingsDialogLoaded(object sender, RoutedEventArgs args)
        {
            InitResolutionRb();
            InitColorPallet();
            InitColorList();
        }

        private void OnApplySettingsBtnClick(object sender, RoutedEventArgs args)
        {
            UpdateSettingsFromUI();
            var settingsController = new SettingsController(_settings);
            settingsController.UpdateSettingsFile(_settings);
            InitResolutionRb();
            InitColorPallet();
            InitColorList();
        }

        private void OnOkBtnClick(object sender, RoutedEventArgs args)
        {
            UpdateSettingsFromUI();
            var settingsController = new SettingsController();
            settingsController.UpdateSettingsFile(_settings);
            Close();
        }

        private void OnDefaultResolutionChecked(object sender, RoutedEventArgs args)
        {
            CustomResolutionTB.IsEnabled = false;
        }

        private void OnCustomResolutionSelected(object sender, RoutedEventArgs args)
        {
            CustomResolutionTB.IsEnabled = true;
        }

        private void On4ColorsSelected(object sender, RoutedEventArgs args)
        {
            _5Color.IsEnabled = false;
            _6Color.IsEnabled = false;
            _7Color.IsEnabled = false;
            _8Color.IsEnabled = false;
            _5Color.Visibility = Visibility.Collapsed;
            _6Color.Visibility = Visibility.Collapsed;
            _7Color.Visibility = Visibility.Collapsed;
            _8Color.Visibility = Visibility.Collapsed;
            _5ColorRect.Visibility = Visibility.Collapsed;
            _6ColorRect.Visibility = Visibility.Collapsed;
            _7ColorRect.Visibility = Visibility.Collapsed;
            _8ColorRect.Visibility = Visibility.Collapsed;
        }

        private void On8ColorsSelected(object sender, RoutedEventArgs args)
        {
            _5Color.IsEnabled = true;
            _6Color.IsEnabled = true;
            _7Color.IsEnabled = true;
            _8Color.IsEnabled = true;
            _5Color.Visibility = Visibility.Visible;
            _6Color.Visibility = Visibility.Visible;
            _7Color.Visibility = Visibility.Visible;
            _8Color.Visibility = Visibility.Visible;
            _5ColorRect.Visibility = Visibility.Visible;
            _6ColorRect.Visibility = Visibility.Visible;
            _7ColorRect.Visibility = Visibility.Visible;
            _8ColorRect.Visibility = Visibility.Visible;
        }

        #region //focuslost region
        private void On1ColorBoxFocusLost(object sender, RoutedEventArgs args)
        {
            try 
            {
                _1ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_1Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_1Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_1Color.Text.Substring(4, 2), 16))));
            } 
            catch(Exception e)
            {
                _1Color.Text = _settings.ColorList[0];
                _1ColorRect.Fill = new SolidColorBrush(
                    Color.FromRgb(
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[0].Substring(0, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[0].Substring(2, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[0].Substring(4, 2), 16))));
                var alertDlg =
                    new AlertDialog(
                        "Неверный формат кода цвета.");
                alertDlg.Show();
            }
        }

        private void On2ColorBoxFocusLost(object sender, RoutedEventArgs args)
        {
            try
            {
                _2ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_2Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_2Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_2Color.Text.Substring(4, 2), 16))));
            }
            catch (Exception)
            {
                _2Color.Text = _settings.ColorList[1];
                _2ColorRect.Fill = new SolidColorBrush(
                    Color.FromRgb(
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[1].Substring(0, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[1].Substring(2, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[1].Substring(4, 2), 16))));
                var alertDlg =
                    new AlertDialog(
                        "Неверный формат кода цвета.");
                alertDlg.Show();
            }
        }

        private void On3ColorBoxFocusLost(object sender, RoutedEventArgs args)
        {
            try
            {
                _3ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_3Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_3Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_3Color.Text.Substring(4, 2), 16))));
            }
            catch (Exception)
            {
                _3Color.Text = _settings.ColorList[2];
                _3ColorRect.Fill = new SolidColorBrush(
                    Color.FromRgb(
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[2].Substring(0, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[2].Substring(2, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[2].Substring(4, 2), 16))));
                var alertDlg =
                    new AlertDialog(
                        "Неверный формат кода цвета.");
                alertDlg.Show();
            }
        }

        private void On4ColorBoxFocusLost(object sender, RoutedEventArgs args)
        {
            try
            {
                _4ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_4Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_4Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_4Color.Text.Substring(4, 2), 16))));
            }
            catch (Exception)
            {
                _4Color.Text = _settings.ColorList[3];
                _4ColorRect.Fill = new SolidColorBrush(
                    Color.FromRgb(
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[3].Substring(0, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[3].Substring(2, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[3].Substring(4, 2), 16))));
                var alertDlg =
                    new AlertDialog(
                        "Неверный формат кода цвета.");
                alertDlg.Show();
            }
        }

        private void On5ColorBoxFocusLost(object sender, RoutedEventArgs args)
        {
            try
            {
                _5ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_5Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_5Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_5Color.Text.Substring(4, 2), 16))));
            }
            catch (Exception)
            {
                _5Color.Text = _settings.ColorList[4];
                _5ColorRect.Fill = new SolidColorBrush(
                    Color.FromRgb(
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[4].Substring(0, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[4].Substring(2, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[4].Substring(4, 2), 16))));
                var alertDlg =
                    new AlertDialog(
                        "Неверный формат кода цвета.");
                alertDlg.Show();
            }
        }

        private void On6ColorBoxFocusLost(object sender, RoutedEventArgs args)
        {
            try
            {
                _6ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_6Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_6Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_6Color.Text.Substring(4, 2), 16))));
            }
            catch (Exception)
            {
                _6Color.Text = _settings.ColorList[5];
                _6ColorRect.Fill = new SolidColorBrush(
                    Color.FromRgb(
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[5].Substring(0, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[5].Substring(2, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[5].Substring(4, 2), 16))));
                var alertDlg =
                    new AlertDialog(
                        "Неверный формат кода цвета.");
                alertDlg.Show();
            }
        }

        private void On7ColorBoxFocusLost(object sender, RoutedEventArgs args)
        {
            try
            {
                _7ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_7Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_7Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_7Color.Text.Substring(4, 2), 16))));
            }
            catch (Exception)
            {
                _7Color.Text = _settings.ColorList[6];
                _7ColorRect.Fill = new SolidColorBrush(
                    Color.FromRgb(
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[6].Substring(0, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[6].Substring(2, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[6].Substring(4, 2), 16))));
                var alertDlg =
                    new AlertDialog(
                        "Неверный формат кода цвета.");
                alertDlg.Show();
            }
        }

        private void On8ColorBoxFocusLost(object sender, RoutedEventArgs args)
        {
            try
            {
                _8ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_8Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_8Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_8Color.Text.Substring(4, 2), 16))));
            }
            catch (Exception)
            {
                _8Color.Text = _settings.ColorList[7];
                _8ColorRect.Fill = new SolidColorBrush(
                    Color.FromRgb(
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[7].Substring(0, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[7].Substring(2, 2), 16)),
                    Convert.ToByte(Convert.ToInt32(_settings.ColorList[7].Substring(4, 2), 16))));
                var alertDlg =
                    new AlertDialog(
                        "Неверный формат кода цвета.");
                alertDlg.Show();
            }
        }
        #endregion
        #endregion

        #region //initializers
        /// <summary>
        /// Overwrites show method of Window class
        /// Written to make invisible evocation modal window
        /// </summary>
        public new void Show()
        {
            ShowDialog();
        }

        private void Init()
        {
            var settingController = new SettingsController();
            _settings = settingController.GetSettings();
        }

        #endregion

        #region //helpers
        // updates settings instance
        private void UpdateSettingsFromUI()
        {
            SetDpi();
            SetColorPalletNumber();
            SetColors();
        }

        // sets up dpi property
        private void SetDpi()
        {
            if (DefaultResolution.IsChecked != null)
            {
                if (!((bool) DefaultResolution.IsChecked))
                {
                    try
                    {
                        _settings.DPI = int.Parse(CustomResolutionTB.Text);
                    }
                    catch (Exception)
                    {
                        _settings.DPI = 96;
                        const string alertText = "Custom DPI was not set correctly, DPI was set to default value (96)";
                        var alertDialog = new AlertDialog(alertText);
                        alertDialog.Show();
                    }
                }
                else
                {
                    _settings.DPI = 96;
                }
            }
        }

        // sets up color pallet color number
        private void SetColorPalletNumber()
        {
            if (_4colors.IsChecked != null && _8colors.IsChecked != null)
                if ((bool) _4colors.IsChecked)
                {
                    _settings.ColorPallet = 4;
                }
                else
                {
                    _settings.ColorPallet = 8;
                }
        }

        // saves colors to settings instance
        private void SetColors()
        {
            _settings.ColorList[0] = _1Color.Text;
            _settings.ColorList[1] = _2Color.Text;
            _settings.ColorList[2] = _3Color.Text;
            _settings.ColorList[3] = _4Color.Text;
            _settings.ColorList[4] = _5Color.Text;
            _settings.ColorList[5] = _6Color.Text;
            _settings.ColorList[6] = _7Color.Text;
            _settings.ColorList[7] = _8Color.Text;
        }

        // Initialization helpers
        private void InitResolutionRb()
        {
            if (_settings.DPI == 96)
            {
                DefaultResolution.IsChecked = true;
                CustomResolutionTB.IsEnabled = false;
            }
            else
            {
                CustomResolution.IsChecked = true;
                CustomResolutionTB.IsEnabled = true;
                CustomResolutionTB.Text = _settings.DPI.ToString();
            }    
        }

        // updates color pallet selection
        private void InitColorPallet()
        {
            if (_settings.ColorPallet == 4)
            {
                _4colors.IsChecked = true;
                _8colors.IsChecked = false;

                // color pickers
                _5Color.IsEnabled = false;
                _6Color.IsEnabled = false;
                _7Color.IsEnabled = false;
                _8Color.IsEnabled = false;
                _5Color.Visibility = Visibility.Collapsed;
                _6Color.Visibility = Visibility.Collapsed;
                _7Color.Visibility = Visibility.Collapsed;
                _8Color.Visibility = Visibility.Collapsed;
                _5ColorRect.Visibility = Visibility.Collapsed;
                _6ColorRect.Visibility = Visibility.Collapsed;
                _7ColorRect.Visibility = Visibility.Collapsed;
                _8ColorRect.Visibility = Visibility.Collapsed;
            }
            else
            {
                _4colors.IsChecked = false;
                _8colors.IsChecked = true;

                // color pickers
                _5Color.IsEnabled = true;
                _6Color.IsEnabled = true;
                _7Color.IsEnabled = true;
                _8Color.IsEnabled = true;
                _5Color.Visibility = Visibility.Visible;
                _6Color.Visibility = Visibility.Visible;
                _7Color.Visibility = Visibility.Visible;
                _8Color.Visibility = Visibility.Visible;
                _5ColorRect.Visibility = Visibility.Visible;
                _6ColorRect.Visibility = Visibility.Visible;
                _7ColorRect.Visibility = Visibility.Visible;
                _8ColorRect.Visibility = Visibility.Visible;
            }
        }

        // updates colors from list
        private void InitColorList()
        {
            _1Color.Text = _settings.ColorList[0];
            _2Color.Text = _settings.ColorList[1];
            _3Color.Text = _settings.ColorList[2];
            _4Color.Text = _settings.ColorList[3];
            _5Color.Text = _settings.ColorList[4];
            _6Color.Text = _settings.ColorList[5];
            _7Color.Text = _settings.ColorList[6];
            _8Color.Text = _settings.ColorList[7];
            _1ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_1Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_1Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_1Color.Text.Substring(4, 2), 16))));
            _2ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_2Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_2Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_2Color.Text.Substring(4, 2), 16))));
            _3ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_3Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_3Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_3Color.Text.Substring(4, 2), 16))));
            _4ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_4Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_4Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_4Color.Text.Substring(4, 2), 16))));
            _5ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_5Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_5Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_5Color.Text.Substring(4, 2), 16))));
            _6ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_6Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_6Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_6Color.Text.Substring(4, 2), 16))));
            _7ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_7Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_7Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_7Color.Text.Substring(4, 2), 16))));
            _8ColorRect.Fill = new SolidColorBrush(
                Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(_8Color.Text.Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_8Color.Text.Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(_8Color.Text.Substring(4, 2), 16))));
        }

        #endregion
    }
}
