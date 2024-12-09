using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Utlis;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.View.MediaPageView
{
    public class FollowButtonVisiblityConverter : IValueConverter
    {
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetRequiredService<ISessionManager>();
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is long userId)
            {
                if (userId == _sessionManager.RetriveUserFromStorage().UserId)
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
