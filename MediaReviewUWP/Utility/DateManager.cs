using System;
using Windows.ApplicationModel.Resources;

namespace MediaReviewUWP.Utility
{
    public static class DateManager
    {
        public static string GetDateRelativeToCurrent(DateTime date)
        {
            ResourceLoader loader = new ResourceLoader();
            var now = DateTime.Now;
            var timespan = now - date;

            if (timespan.TotalSeconds < 60)
                return loader.GetString("JustNow");
            else if (timespan.TotalMinutes < 60)
                return $"{(int)timespan.TotalMinutes} {(timespan.TotalMinutes < 2 ? loader.GetString("minute") : loader.GetString("minutes"))}  {loader.GetString("ago")}";
            else if (timespan.TotalHours < 24)
                return loader.GetString("Today");
            else if (timespan.TotalHours < 48)
                return loader.GetString("Yesterday");
            else
                return date.ToString("dd MMM yyyy");
        }
    }
}