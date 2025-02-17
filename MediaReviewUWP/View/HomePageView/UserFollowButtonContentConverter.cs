using MediaReviewClassLibrary.Models.Constants;
using MediaReviewUWP.ViewObject;
using System;
using System.Windows;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.View.HomePageView
{
    public class UserFollowButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is bool follow)
            {
                var loader = new ResourceLoader();
                if (follow)
                {
                    return loader.GetString("RemoveText");
                }
                else
                {
                    return loader.GetString("Undo");
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
