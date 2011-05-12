using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using stdole;

namespace EsriDE.Trials.ContentLoader.ContentFinder
{
    public class BitmapUtil
    {
        public static Bitmap GetBitmap(IPicture picture)
        {
            try
            {
                Image image = IPictureToImage(picture);
                return (Bitmap) image;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Image IPictureToImage(object pict)
        {
            IntPtr ptr2;
            if (pict == null)
            {
                return null;
            }
            IntPtr zero = IntPtr.Zero;
            var picture = (IPicture) pict;
            int type = picture.Type;
            if (type == 1)
            {
                try
                {
                    ptr2 = new IntPtr(picture.hPal);
                    zero = ptr2;
                }
                catch (COMException comException)
                {
                    Debug.WriteLine(comException);
                }
            }
            ptr2 = new IntPtr(picture.Handle);
            return GetImageFromParams(picture, ptr2, type, zero, picture.Width, picture.Height);
        }

        private static Image GetImageFromParams(object pict, IntPtr handle, int pictype, IntPtr paletteHandle, int width,
                                                int height)
        {
            switch (pictype)
            {
                case -1:
                    return null;

                case 0:
                    return null;

                case 1:
                    return Image.FromHbitmap(handle, paletteHandle);

                case 2:
                    {
                        var wmfHeader = new WmfPlaceableFileHeader();
                        wmfHeader.BboxRight = (short) width;
                        wmfHeader.BboxBottom = (short) height;
                        var metafile = new Metafile(handle, wmfHeader, false);
                        return (Image) RuntimeHelpers.GetObjectValue(metafile.Clone());
                    }
                case 4:
                    {
                        var metafile2 = new Metafile(handle, false);
                        return (Image) RuntimeHelpers.GetObjectValue(metafile2.Clone());
                    }
            }
            throw new Exception("AXUnknownImage");
        }
    }
}