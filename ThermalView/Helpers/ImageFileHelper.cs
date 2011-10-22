//////////////////////////////////////////////////////////////////////////
// helper class
// providing helper methods for image controls in main window
// also implements exception throwing for it
//
// Developer: Eugene Sypachev
// 2010
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThermalView.Helpers
{
    public class ImageFileHelper
    {

        /// <summary>
        /// parser method for file route
        /// </summary>
        /// <param name="fileRoute">files route string</param>
        /// <returns>file name with extension</returns>
        public static string GetFileName(string fileRoute)
        {
            return System.IO.Path.GetFileName(fileRoute);
        }

        /// <summary>
        /// parser method for file extension
        /// </summary>
        /// <param name="routeToFile">files route string</param>
        /// <returns>file extension</returns>
        public static string GetFileExtension(string routeToFile)
        {
            string extension;
            try
            {
                // ReSharper disable PossibleNullReferenceException
                extension = System.IO.Path.GetExtension(routeToFile).ToLower();
                // ReSharper restore PossibleNullReferenceException
            }
            catch (NullReferenceException)
            {
                extension = "";
            }
            return extension;
        }

    }
}
