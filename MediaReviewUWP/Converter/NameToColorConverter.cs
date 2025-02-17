using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.Converter
{
    internal class NameToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string userName && !string.IsNullOrEmpty(userName))
            {
                if (userName.Length >= 3)
                {
                    byte redValue = (byte)(userName[0] % 255);
                    byte greenValue = (byte)(userName[1] % 255);
                    byte blueValue = (byte)(userName[2] % 255);
                    return Color.FromArgb(255, redValue, greenValue, blueValue);
                }
                else
                {
                    return new SolidColorBrush(Colors.LightGray);
                }
            }
            return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}