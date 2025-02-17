using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.ResourceDictionary
{
    public class LighterColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is SolidColorBrush brush)
            {
                return new SolidColorBrush(ColorHelper.GetLighterShade(brush.Color));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DarkerColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is SolidColorBrush brush)
            {
                return new SolidColorBrush(ColorHelper.GetDarkerShade(brush.Color));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}