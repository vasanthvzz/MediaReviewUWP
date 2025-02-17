using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.HomePageView
{
    public class ImageToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string imagePath)
            {
                return new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(imagePath))
                };
            }
            else if (value is BitmapImage bitmapImage)
            {
                return new ImageBrush
                {
                    ImageSource = bitmapImage
                };
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException("ConvertBack is not implemented.");
        }
    }
}