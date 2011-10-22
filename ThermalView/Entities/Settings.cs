using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ThermalView.Entities
{
    /// <summary>
    /// entity class for storing and saving user data about pictures                                                                            
    /// </summary>
    
    public class Settings
    {
        public int DPI { get; set; }
        public int ColorPallet { get; set; }
        public ObservableCollection<string> ColorList { get; set; }

        public Settings() 
        {
            DPI = 96;
            ColorPallet = 8;
            ColorList = new ObservableCollection<String>();
        }

        public Settings(int dpi, int cpNumber, ObservableCollection<string> colorList)
        {
            DPI = dpi;
            ColorPallet = cpNumber;
            ColorList = colorList;
        }
    }
}
