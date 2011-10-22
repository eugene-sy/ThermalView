using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ThermalView.Helpers;

//////////////////////////////////////////////////////////////////////////
/// Storage class for storing Bitmap
//////////////////////////////////////////////////////////////////////////
namespace ThermalView.Entities
{
    public class BitMapSource
    {
        public BitmapSource BmSource { get; set; }
        public int BitsPerPix { get; set; }

        public BitMapSource(BitmapSource bms)
        {
            BmSource = bms;
            BitsPerPix = bms.Format.BitsPerPixel;
        }

        public BitMapSource(string fileRoute)
        {
            string fileExt = ImageFileHelper.GetFileExtension(fileRoute);

            switch(fileExt)
            {
                case ".jpeg":
                case ".jpg":
                    GetBitmapSourceFromJpeg(fileRoute);
                    break;
                case ".png":
                    GetBitmapSourceFromPng(fileRoute);
                    break;
                case ".gif":
                    GetBitmapSourceFromGif(fileRoute);
                    break;
                case ".tiff":
                    GetBitmapSourceFromTiff(fileRoute);
                    break;
                case ".wmp":
                    GetBitmapSourceFromWmp(fileRoute);
                    break;
                case ".bmp":
                    GetBitmapSourceFromBmp(fileRoute);
                    break;
                default:
                    throw new Exception();
                    // should be changed and caught
            }
        }


        // file encoders 
        private void GetBitmapSourceFromJpeg(string fileRoute)
        {
            Stream imageStream = new FileStream(fileRoute, FileMode.Open, FileAccess.Read, FileShare.Read);
            var decoder = new JpegBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BmSource = decoder.Frames[0];
            BitsPerPix = BmSource.Format.BitsPerPixel;
        }

        private void GetBitmapSourceFromPng(string fileRoute)
        {
            Stream imageStream = new FileStream(fileRoute, FileMode.Open, FileAccess.Read, FileShare.Read);
            var decoder = new PngBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BmSource = decoder.Frames[0];
            BitsPerPix = BmSource.Format.BitsPerPixel;
        }

        private void GetBitmapSourceFromTiff(string fileRoute)
        {
            Stream imageStream = new FileStream(fileRoute, FileMode.Open, FileAccess.Read, FileShare.Read);
            var decoder = new TiffBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BmSource = decoder.Frames[0];
            BitsPerPix = BmSource.Format.BitsPerPixel;
        }

        private void GetBitmapSourceFromGif(string fileRoute)
        {
            Stream imageStream = new FileStream(fileRoute, FileMode.Open, FileAccess.Read, FileShare.Read);
            var decoder = new GifBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BmSource = decoder.Frames[0];
            BitsPerPix = BmSource.Format.BitsPerPixel;
        }

        private void GetBitmapSourceFromWmp(string fileRoute)
        {
            Stream imageStream = new FileStream(fileRoute, FileMode.Open, FileAccess.Read, FileShare.Read);
            var decoder = new WmpBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BmSource = decoder.Frames[0];
            BitsPerPix = BmSource.Format.BitsPerPixel;
        }

        private void GetBitmapSourceFromBmp(string fileRoute)
        {
            Stream imageStream = new FileStream(fileRoute, FileMode.Open, FileAccess.Read, FileShare.Read);
            var decoder = new BmpBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BmSource = decoder.Frames[0];
            BitsPerPix = BmSource.Format.BitsPerPixel;
        }
        
        // no saving for now!
       
    }
}
