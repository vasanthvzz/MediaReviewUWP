using MediaReviewUWP.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.Converter
{
    public class TileImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var placeholderImage = new BitmapImage(new Uri(ImageManager.GetDefaultTileImagePath(), UriKind.Absolute));
            string imagePath = "";
            if (value is BitmapImage b && b.UriSource is Uri uri)
            {
                imagePath = uri.AbsoluteUri;
            }
            else if (value is String s)
            {
                imagePath = s;
            }
            var bitmapImage = new BitmapImage();
            _ = FileManager.LoadImageAsync(imagePath, bitmapImage, placeholderImage);
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
