using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.View.MediaPageView
{
    public class FollowButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var loader = new ResourceLoader();
            if (value is bool isFollowing)
            {
                return isFollowing ? loader.GetString("Following") : loader.GetString("Follow");
            }
            return loader.GetString("Follow");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string stringValue)
            {
                return stringValue.Equals("IsFollow", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}