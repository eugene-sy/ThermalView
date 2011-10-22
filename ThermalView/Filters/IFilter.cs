using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using ThermalView.Entities;

namespace ThermalView.Filters
{
    interface IFilter
    {
        ColorConvertedBitmap FilterImage(ref BitMapSource source);
    }
}
