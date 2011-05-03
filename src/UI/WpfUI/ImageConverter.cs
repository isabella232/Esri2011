using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EsriDE.Samples.ContentFinder.WpfUI
{
	internal sealed class ImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType,
							  object parameter, CultureInfo culture)
		{
			try
			{
				var result = GetImageSourceFromBitmap((Bitmap) value);
				return result;
			}
			catch
			{
				return new BitmapImage();
			}
		}

		public object ConvertBack(object value, Type targetType,
								  object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		private static BitmapImage GetImageSourceFromBitmap(Bitmap bitmap)
		{
			var memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Jpeg);

			var bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
			bitmapImage.EndInit();

			return bitmapImage;
		}
	}
}