using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ThermalView.SetUp
{
    class BitmapProxy
    {
        /// <summary>
        /// counts how many pixels are in the area
        /// this is damn slow method
        /// </summary>
        /// <param name="bm">source bitmap</param>
        /// <returns>square of bitmap</returns>
        public static int GetBitmapSquareInPixels(Bitmap bm)
        {
            int count = 0;

            for (int x = 0; x <= bm.Width; x++)
            {
                for (int y = 0; y <= bm.Height; y++)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
