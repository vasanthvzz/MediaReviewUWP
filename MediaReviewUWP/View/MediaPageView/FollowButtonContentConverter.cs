using System;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.View.MediaPageView
{
    public class FollowButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isFollowing)
            {
                return isFollowing ? "Following" : "Follow";
            }
            return "Follow"; // Default value
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string stringValue)
            {
                return stringValue.Equals("Following", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
