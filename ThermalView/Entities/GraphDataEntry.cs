using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalView.Entities
{
    public class GraphDataEntry
    {
        public int ColorIndex { get; set; }
        public String ColorString { get; set; }
        public int NumberOfPoints { get; set; }

        public GraphDataEntry()
        {
            ColorIndex = 0;
            ColorString = "00000000";
            NumberOfPoints = 0;
        }

        public GraphDataEntry(int index, String colStr)
        {
            ColorIndex = index;
            ColorString = colStr;
            NumberOfPoints = 0;
        }
    }
}
