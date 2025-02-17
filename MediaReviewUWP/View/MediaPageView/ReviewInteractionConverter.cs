using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Utility;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.View.MediaPageView
{
    public class ReviewInteractionConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is long userId)
            {
                if (userId == SessionManager.User.UserId)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}