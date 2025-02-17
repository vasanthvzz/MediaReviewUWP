using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Utility;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.View.MediaPageView
{
    public class FollowButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is long userId)
            {
                if (userId == SessionManager.User.UserId)
                {
                    return Visibility.Collapsed;
                }
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}