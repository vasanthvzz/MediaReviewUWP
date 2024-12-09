using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Utlis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.View.MediaPageView
{
    public class ReviewInteractionConverter : IValueConverter
    {
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetRequiredService<ISessionManager>();
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is long userId)
            {
                if(userId == _sessionManager.RetriveUserFromStorage().UserId)
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
