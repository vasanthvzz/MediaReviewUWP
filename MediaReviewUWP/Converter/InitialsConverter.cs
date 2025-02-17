using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.Converter
{
    public class InitialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string userName)
            {
                return userName.ToUpper().First();
            }
            return "T";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}