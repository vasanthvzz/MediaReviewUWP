using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.View.HomePageView
{

    public class ImageToBrushConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                if (value is string imagePath)
                {
                    // Create an ImageBrush from the image path
                    return new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(imagePath))
                    };
                }
                else if (value is BitmapImage bitmapImage)
                {
                    // Create an ImageBrush from a BitmapImage
                    return new ImageBrush
                    {
                        ImageSource = bitmapImage
                    };
                }

                return null; // Return null if value is not a valid image
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                throw new NotImplementedException("ConvertBack is not implemented.");
            }
        }

}
