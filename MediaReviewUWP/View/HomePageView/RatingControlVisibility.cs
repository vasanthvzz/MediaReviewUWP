using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.View.HomePageView
{
    public class RatingControlVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string str)
            {
                return string.IsNullOrEmpty(str) ? Visibility.Collapsed : Visibility.Visible;
            }
            if (value is short rating)
            {
                return rating <= 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            if (value is float score)
            {
                return score <= 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}