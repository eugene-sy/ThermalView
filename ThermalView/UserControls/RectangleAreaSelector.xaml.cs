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
using ThermalView.Entities;

namespace ThermalView.UserControls
{
    /// <summary>
    /// Interaction logic for RectangleAreaSelector.xaml
    /// </summary>
    public partial class RectangleAreaSelector : UserControl
    {
        #region // properties
        public Point StartPos;
        public Point EndPos;
        private bool _isMouseCaptured;
        private FrameworkElement _cursor;
        public bool LeaveSelectionVisible { get; set; }
        public FrameworkElement DragCursor
        {
            get
            {
                return _cursor;
            }
            set
            {
                _cursor = value;
                SetDragCursor();
            }
        }
        #endregion

        public RectangleAreaSelector()
        {
            InitializeComponent();
            Init();
            SelectionHolder.MouseLeftButtonDown += OnRectangleAreaSelectorMouseLeftButtonDown;
            SelectionHolder.MouseMove += OnRectangleAreaSelectorMouseMove;
            SelectionHolder.MouseLeave += OnRectangleAreaSelectorMouseLeave;
            SelectionHolder.MouseLeftButtonUp += OnRectangleAreaSelectorMouseLeftButtonUp;
        }

        private void Init()
        {
            StartPos = new Point(0, 0);
            _isMouseCaptured = false;
        }

        #region //event handlers
        
        private void OnRectangleAreaSelectorMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            var item = (FrameworkElement) sender;
            StartPos = args.GetPosition(sender as IInputElement);
            _isMouseCaptured = true;
            item.CaptureMouse();

            SelectedArea.Visibility = Visibility.Visible;
        }

        private void OnRectangleAreaSelectorMouseMove(object sender, MouseEventArgs args)
        {
            var item = (FrameworkElement)sender;
            // position the cursor if there is one.
            if (_cursor != null)
            {
                CursorHolderPopup.IsOpen = true;
                Point pt = args.GetPosition(item);
                CursorHolderPopup.VerticalOffset = pt.Y;
                CursorHolderPopup.HorizontalOffset = pt.X;
            }

            if (_isMouseCaptured)
            {
                var endPoint = args.GetPosition(item);

                var actualEndPoint = new Point();
                var actualStartPoint = new Point();

                actualEndPoint.X = endPoint.X - StartPos.X;
                actualEndPoint.Y = endPoint.Y - StartPos.Y;

                // if we are dragging left we need to invert the values.
                if (actualEndPoint.X < 1)
                {
                    actualEndPoint.X = StartPos.X - endPoint.X;
                    actualStartPoint.X = endPoint.X;
                }
                else
                {
                    actualStartPoint.X = StartPos.X;
                }

                // if we are dragging up we need to invert the values.
                if (actualEndPoint.Y < 1)
                {
                    actualEndPoint.Y = StartPos.Y - endPoint.Y;
                    actualStartPoint.Y = endPoint.Y;
                }
                else
                {
                    actualStartPoint.Y = StartPos.Y;
                }

                // make sure we stay in bounds and not drawing too small or too large.
                if (actualStartPoint.X < 0 || endPoint.X > item.ActualWidth ||
                    actualStartPoint.Y < 0 || endPoint.Y > item.ActualHeight) return;

                // set the position and dimension.                
                SelectedArea.Margin = new Thickness(actualStartPoint.X, actualStartPoint.Y, 0, 0);
                SelectedArea.Width = actualEndPoint.X;
                SelectedArea.Height = actualEndPoint.Y;

                var p = new Point() { X = actualEndPoint.X + actualStartPoint.X, Y = actualEndPoint.Y + actualStartPoint.Y };
                OnSelectionInProgress(actualStartPoint, p);
            }
        }

        private void OnRectangleAreaSelectorMouseLeave(object sender, MouseEventArgs args)
        {
            if (_cursor != null)
            {
                CursorHolderPopup.IsOpen = false;
            }
        }

        private void OnRectangleAreaSelectorMouseLeftButtonUp(object sender, MouseButtonEventArgs args)
        {
            var item = (FrameworkElement)sender;
            Point pt = args.GetPosition(item);
            EndPos = pt;

            // hide the custom cursor if it is out of bounds
            if (_cursor != null)
            {
                if (pt.X > this.ActualWidth || pt.Y > this.ActualHeight
                    || pt.X < 0 || pt.Y < 0)
                {
                    CursorHolderPopup.IsOpen = false;
                }
            }

            _isMouseCaptured = false;
            item.ReleaseMouseCapture();

            var actualStartPoint = new Point() { X = SelectedArea.Margin.Left, Y = SelectedArea.Margin.Top };
            var actualEndPoint = new Point() { X = actualStartPoint.X + SelectedArea.Width, Y = actualStartPoint.Y + SelectedArea.Height };
            OnSelectionEnd(actualStartPoint, actualEndPoint);

            StartTextPopup.IsOpen = false;
            EndTextPopup.IsOpen = false;

            ResetSelectedArea();
        }

#endregion

        #region //methods

        private void SetDragCursor()
        {
            if (_cursor != null)
            {
                _cursor.IsHitTestVisible = false;
                CursorHolderPopup.Child = _cursor;
                SelectionHolder.Cursor = Cursors.None;
            }
            else
            {
                CursorHolderPopup.Child = null;
                SelectionHolder.Cursor = null;
            }
        }

        private void ResetSelectedArea()
        {
            if (LeaveSelectionVisible == false)
            {
                SelectedArea.Visibility = Visibility.Collapsed;
                SelectedArea.Width = 0;
                SelectedArea.Margin = new Thickness(0.0);
                StartTextPopup.IsOpen = false;
                EndTextPopup.IsOpen = false;
            }
        }


        #endregion

        #region //Class Events

        public delegate void SelectionEndEventHandler(RectangleAreaSelectionEventArgs args);
        public event SelectionEndEventHandler SelectionEnd;

        private void OnSelectionEnd(Point startPoint, Point endPoint)
        {
            if (SelectionEnd != null)
            {
                var eventArgs = new RectangleAreaSelectionEventArgs(startPoint, endPoint, RectangleAreaSelectionEventArgs.DragSelectEventTypes.SelectionEnd);
                SelectionEnd(eventArgs);
            }
        }

        public delegate void SelectionInProgressEventHandler(RectangleAreaSelectionEventArgs e);
        public event SelectionInProgressEventHandler SelectionInProgress;

        private void OnSelectionInProgress(Point startPoint, Point endPoint)
        {
            if (SelectionInProgress != null)
            {
                var eventArgs = new RectangleAreaSelectionEventArgs(startPoint, endPoint, RectangleAreaSelectionEventArgs.DragSelectEventTypes.SelectionInProgress);
                SelectionInProgress(eventArgs);
            }
        }

        #endregion Class Events
    }
}
