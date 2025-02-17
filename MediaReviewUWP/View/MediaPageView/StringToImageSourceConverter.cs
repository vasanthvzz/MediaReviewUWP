using System;
using System.Diagnostics;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.MediaPageView
{
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
            {
                try
                {
                    return new BitmapImage(new Uri(imagePath));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}