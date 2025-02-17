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
    public class StringToImageBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value is string path)
                {
                    return new BitmapImage(new Uri(path, UriKind.Absolute));
                }
            }
            catch { }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
