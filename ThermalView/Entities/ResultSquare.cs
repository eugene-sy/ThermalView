using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalView.Entities
{
    public class ResultSquare
    {
        public string AreaName { get; set; }
        public double squarePerSent { get; set; }

        public ResultSquare()
        {
            AreaName = "area name";
            squarePerSent = 0;
        }
    }
}
