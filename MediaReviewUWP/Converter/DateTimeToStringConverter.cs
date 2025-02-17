using System;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.Converter
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is DateTime dateTime)
            {
                return dateTime.Date.ToString("dd MMM yyyy");
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
