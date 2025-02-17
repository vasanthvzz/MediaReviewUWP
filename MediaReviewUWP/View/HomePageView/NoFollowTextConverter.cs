using MediaReviewClassLibrary.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.View.HomePageView
{
    public class NoFollowTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var loader = new ResourceLoader();
            if(value is FollowType followType)
            {
                if(followType == FollowType.FOLLOWER)
                {
                    return loader.GetString("NoFollower");
                }
                return loader.GetString("NoFollowee");
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
