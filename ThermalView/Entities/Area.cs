using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using ThermalView.Controllers;
using ThermalView.SetUp;

namespace ThermalView.Entities
{
    /// <summary>
    /// Area entity for list
    /// </summary>
    public class Area : INotifyPropertyChanged
    {
        private int _id;
        private ObservableCollection<byte> _points;
        private string _name;
        private bool _isMain;
        
        public int AreaID
        {
            get { return _id; }
            set { _id = value; }
        }

        public ObservableCollection<byte> Points { 
            get { return _points; }  
            set {
                _points = value;
                FirePropertyChanged("Points");
            }  
        }

        public string Name { 
            get { return _name; }
            set { 
                _name = value;
                FirePropertyChanged("Name");
            }
        }

        public bool Main { 
            get { return _isMain; }
            set
            {
                _isMain = value;
                FirePropertyChanged("Main");
            } 
        }

        public Area()
        {
            Points = new ObservableCollection<byte>();
            Name = "Area";
            Main = false;
        }

        void FirePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
