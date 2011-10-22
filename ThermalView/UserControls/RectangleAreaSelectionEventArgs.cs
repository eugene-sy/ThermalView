using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ThermalView.UserControls
{
    public class RectangleAreaSelectionEventArgs : EventArgs
    {
        #region Constructors

        public RectangleAreaSelectionEventArgs()
        {

        }

        public RectangleAreaSelectionEventArgs(Point startPoint, Point endPoint, DragSelectEventTypes type)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.EventType = type;
        }

        #endregion  Constructors

        #region Properties

        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public DragSelectEventTypes EventType { get; set; }

        #endregion Properties

        public enum DragSelectEventTypes
        {
            SelectionInProgress,
            SelectionEnd
        }
    }
}
