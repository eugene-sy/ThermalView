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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using ThermalView.Views;
using System.IO;
using ThermalView.Helpers;
using ThermalView.Entities;
using ThermalView.Controllers;
using ThermalView.Views.AlertDialogs;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
//using Rectangle = System.Drawing.Rectangle;
using System.Collections.ObjectModel;


namespace ThermalView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitMapSource Bitmap { get; set; }
        private string _fileRoute;
        private bool _isPictureLoaded;
        private bool _drawing;
        private ObservableCollection<Area> _areaList;
        private int _index;

        public MainWindow()
        {
            InitializeComponent();
            Init();
            Loaded += OnMainWindowLoaded;
            UploadPicture.Click += OnUploadPictureClick;
            Solve.Click += onSolveClick;
            SettingsBtn.Click += onSettingsBtnClick;
            HelpBtn.Click += onHelpBtnClick;
            InfoBtn.Click += onInfoBtnClick;
            RectSelect.Checked += OnRectSelectChecked;
            RectSelect.Unchecked += OnRectSelectUnchecked;
            AddArea.Click += OnAddAreaClick;
            ClearBtn.Click += OnClearClick;
        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            UpdateColorsFromSettings(new SettingsController());
        }

        private void Init()
        {
            _fileRoute = null;
            _isPictureLoaded = false;
            _index = 0;
            _areaList = new ObservableCollection<Area>();
            AddArea.IsEnabled = false;
            OnAreaListUpdate();
        }
        
        // event handler for click on Upload Picture Button
        /// <summary>
        /// Opens upload picture dialog window
        /// </summary>
        private void OnUploadPictureClick(object sender, RoutedEventArgs args)
        {
            var uploadPictureWnd = new OpenFileDialog();
            bool? fileDialogOpened = uploadPictureWnd.ShowDialog();
            uploadPictureWnd.Multiselect = false;
            uploadPictureWnd.InitialDirectory = @"\Desktop";

            if (fileDialogOpened == true)
            {
                _fileRoute = uploadPictureWnd.FileName;
                SetBitMap(_fileRoute);
                _isPictureLoaded = true;
            }
        }


        /// <summary>
        /// Starts solve process
        /// </summary>
        private void onSolveClick(object sender, RoutedEventArgs args)
        {
            if (_isPictureLoaded == false || _areaList.Count == 0)
            {
                var aD = new AlertDialog("Изображение не загружено или нет выделенных областей.");
                aD.Show();
                return;
            }


            var controller = new SolverController(_areaList, Bitmap.BitsPerPix);
            controller.Solve();

            var solvedWindow = new Results()
                                   {
                                       ResultSquares = controller.GetResultSquares(),
                                       Temps = GetTempList() };
            solvedWindow.Activate();
            solvedWindow.Show();
        }

        private void OnClearClick(object sender, RoutedEventArgs args)
        {
            ClearWithoutPic();
        }

        

        #region support windows
        /// <summary>
        /// Opens Settings dialog window
        /// </summary>
        private void onSettingsBtnClick(object sender, RoutedEventArgs args)
        {
            var settingsWindow = new SettingsDialog(); // here should be a Settings Window dialog class constructor!
            settingsWindow.Activate();
            settingsWindow.Show();
            UpdateColorsFromSettings(new SettingsController());
        }

        /// <summary>
        /// opens Help Dialog
        /// </summary>
        private void onHelpBtnClick(object sender, RoutedEventArgs args)
        {
            var helpDlg = new HelpWindow();
            helpDlg.Activate();
            helpDlg.Show();
        }

        /// <summary>
        /// opens info dialog
        /// </summary>
        private void onInfoBtnClick(object sender, RoutedEventArgs args)
        {
            var infoDlg = new InfoWindow();
            infoDlg.Activate();
            infoDlg.Show();
        }
        #endregion

        #region Selectors
        private void OnRectSelectChecked(object sender, RoutedEventArgs args)
        {
            if (!_isPictureLoaded)
            {
                RectSelect.IsChecked = false;
                var alertDlg = new AlertDialog("Изображение не загружено.");
                alertDlg.Show();
                return;
            }
            AddArea.IsEnabled = true;
            RectSelector.IsEnabled = true;
        }

        private void OnRectSelectUnchecked(object sender, RoutedEventArgs args)
        {
            AddArea.IsEnabled = false;
            RectSelector.IsEnabled = false;
        }

        private void OnAddAreaClick(object sender, RoutedEventArgs args)
        {
            if (RectSelector.SelectedArea == null)
            {
                const string str = "Область не выбрана.";
                var alert = new AlertDialog(str);
                alert.Show();
                return;
            }

            var newArea = GetSelectedAreaImage(Bitmap.BmSource, RectSelector.StartPos, RectSelector.EndPos);
            if (newArea == null)
                return;

            var area = new Area();
            foreach (var p in newArea)
            {
                area.Points.Add((byte) p);
            }
            area.Name = "Область " + _index;
            area.AreaID = _index;
            if (_areaList.Count == 0)
                area.Main = true;
            _areaList.Add(area);
            OnAreaListUpdate();
            _index++;
        }
        #endregion

        #region //helpers
        /// <summary>
        ///     generates bitmap
        ///     sets up BitMap Entity
        ///     uploads picture to Image tag
        /// </summary>
        /// <param name="routeToFile">route to graphic file</param>
        private void SetBitMap(string routeToFile)
        {
            ClearWithoutPic();
            Bitmap = new BitMapSource(routeToFile);
            string fileName = ImageFileHelper.GetFileName(routeToFile);

            PictureName.Text = "Изображение: " + fileName;
            ImageBoxScrollContainer.Visibility = Visibility.Visible;
            ImageBox.Visibility = Visibility.Visible;
            ImageBox.Source = Bitmap.BmSource;
            ImageBox.Stretch = Stretch.Uniform;
        }

        #region temp-color hash
        private void UpdateColorsFromSettings(SettingsController settingsController)
        {
            ShowColorRect(settingsController, _1ColorRect, 0);
            ShowColorRect(settingsController, _2ColorRect, 1);
            ShowColorRect(settingsController, _3ColorRect, 2);
            ShowColorRect(settingsController, _4ColorRect, 3);
            ShowColorRect(settingsController, _5ColorRect, 4);
            ShowColorRect(settingsController, _6ColorRect, 5);
            ShowColorRect(settingsController, _7ColorRect, 6);
            ShowColorRect(settingsController, _8ColorRect, 7);
            ShowPalletTable(settingsController);
        }

        private static bool CheckPalletSettings(SettingsController settingsController)
        {
            var palletColors = settingsController.GetPalletRange();
            if (palletColors == 4)
                return false;
            return true;
        }

        private void ShowPalletTable(SettingsController controller)
        {
            if (CheckPalletSettings(controller))
            {
                ShowPalletRow(_5Color, _5ColorRect, _5C);
                ShowPalletRow(_6Color, _6ColorRect, _6C);
                ShowPalletRow(_7Color, _7ColorRect, _7C);
                ShowPalletRow(_8Color, _8ColorRect, _8C);
            }
            else
            {
                ClosePalletRow(_5Color, _5ColorRect, _5C);
                ClosePalletRow(_6Color, _6ColorRect, _6C);
                ClosePalletRow(_7Color, _7ColorRect, _7C);
                ClosePalletRow(_8Color, _8ColorRect, _8C);
            }
        }

        private static void ShowPalletRow(FrameworkElement tempElemant, FrameworkElement colorElement, FrameworkElement cElement)
        {
            tempElemant.IsEnabled = true;
            tempElemant.Visibility = Visibility.Visible;
            colorElement.IsEnabled = true;
            colorElement.Visibility = Visibility.Visible;
            cElement.IsEnabled = true;
            cElement.Visibility = Visibility.Visible;
        }

        private static void ClosePalletRow(FrameworkElement tempElemant, FrameworkElement colorElement, FrameworkElement cElement)
        {
            tempElemant.IsEnabled = false;
            tempElemant.Visibility = Visibility.Collapsed;
            colorElement.IsEnabled = false;
            colorElement.Visibility = Visibility.Collapsed;
            cElement.IsEnabled = false;
            cElement.Visibility = Visibility.Collapsed;
        }

        private void ShowColorRect(SettingsController controller, Rectangle elem, int arrayColorIndex)
        {
            elem.Fill = new SolidColorBrush(Color.FromRgb(
                Convert.ToByte(Convert.ToInt32(controller.Settings.ColorList[arrayColorIndex].Substring(0, 2), 16)),
                Convert.ToByte(Convert.ToInt32(controller.Settings.ColorList[arrayColorIndex].Substring(2, 2), 16)),
                Convert.ToByte(Convert.ToInt32(controller.Settings.ColorList[arrayColorIndex].Substring(4, 2), 16))));
        }

#endregion 

        private Array GetSelectedAreaImage(BitmapSource bitmap, Point a, Point b)
        {
            CheckSizeOfArea(bitmap, a, b, out a, out b);
            if (a.X == 0 && a.Y == 0 && b.X == 0 && b.Y == 0)
                return null;
            //from StackOverflow
            // stride must be a number of bits? for buffer to copy the selected area
            //var stride = bitmap.PixelWidth * (bitmap.Format.BitsPerPixel / 8);
            var stride = (int) (Math.Abs((b.X - a.X))*(bitmap.Format.BitsPerPixel/8));

            //var length = bitmap.PixelHeight * stride;
            var length = (int) (Math.Abs((b.Y - a.Y)) * stride);

            //int length = (int)(((b.X - a.X) * (b.Y - a.Y) + 1) * bitmap.Format.BitsPerPixel);// + 100000);
            //int stride = (bitmap.PixelWidth * 
            //              bitmap.Format.BitsPerPixel + 7) / 8;
            //Array newbitmap = new byte[length];
            Array newbitmap = new byte[length];
            var rect = new Int32Rect((int) a.X, (int) a.Y, (int)(b.X - a.X), (int)(b.Y - a.Y));
            try
            {
                bitmap.CopyPixels(rect, newbitmap, stride, 0);
            }
            catch (Exception e)
            {
                var alertDlg = new AlertDialog("Произошла ошибка " + e.ToString());
                alertDlg.Show();
                return null;
            }
            return newbitmap;
        }

        private void OnAreaListUpdate()
        {

            AreaDataGrid.ItemsSource = GetData();

            if (_areaList.Count > 0)
            {
                if (AreaDataGrid.Visibility != Visibility.Visible) 
                    AreaDataGrid.Visibility = Visibility.Visible;
                if (EmptyAreaDataGridNotification.Visibility != Visibility.Collapsed)
                    EmptyAreaDataGridNotification.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (AreaDataGrid.Visibility != Visibility.Collapsed)
                    AreaDataGrid.Visibility = Visibility.Collapsed;
                if (EmptyAreaDataGridNotification.Visibility != Visibility.Visible)
                    EmptyAreaDataGridNotification.Visibility = Visibility.Visible;
            }
        }

        private ObservableCollection<Area> GetData()
        {
            return _areaList;
        }

        private List<double> GetTempList()
        {
            var tempList = new List<double>();
            double temp;
            Double.TryParse(_1Color.Text, out temp);
            tempList.Add(temp);
            Double.TryParse(_2Color.Text, out temp);
            tempList.Add(temp);
            Double.TryParse(_3Color.Text, out temp);
            tempList.Add(temp);
            Double.TryParse(_4Color.Text, out temp);
            tempList.Add(temp);
            Double.TryParse(_5Color.Text, out temp);
            tempList.Add(temp);
            Double.TryParse(_6Color.Text, out temp);
            tempList.Add(temp);
            Double.TryParse(_7Color.Text, out temp);
            tempList.Add(temp);
            Double.TryParse(_8Color.Text, out temp);
            tempList.Add(temp);

            return tempList;
        }

        private void CheckSizeOfArea(BitmapSource bm, Point a, Point b, out Point c, out Point d)
        {
            c = a;
            d = b;
            if (c.Y > bm.Height)
                c.Y = bm.Height;
            if (d.Y > bm.Height)
                d.Y = bm.Height;
            if (c.Y < 0)
                c.Y = 0;
            if (d.Y < 0)
                d.Y = 0;
            if (c.X > bm.Width)
                c.X = bm.Width;
            if (d.X > bm.Width)
                d.X = bm.Width;
            if (c.X < 0)
                c.X = 0;
            if (d.X < 0)
                d.X = 0;

            if (a != c || b != d)
            {
                ChangeSelection();
            }
        }

        private void ChangeSelection()
        {
            const string str = "Выбранная область превышает размер изображения или выходит за его границы."+
                               " Область будет уменьшена, области выделения выходящие за рамки изображения будут обрезаны.";
            var warningDlg = new WarningDialog(str);
            warningDlg.Show();
        }

        private void ClearWithoutPic()
        {
            _areaList = new ObservableCollection<Area>();
            OnAreaListUpdate();
            _index = 0;
        }

        #endregion

    }
}
