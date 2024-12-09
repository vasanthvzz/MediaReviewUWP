using System;

namespace MediaReviewUWP.Utils
{
    public static class DateManager
    {
        public static string RelativeToCurrent(DateTime date)
        {
            var now = DateTime.Now;
            var timespan = now - date;

            if (timespan.TotalSeconds < 60)
                return "Just now";
            else if (timespan.TotalMinutes < 60)
                return $"{(int)timespan.TotalMinutes} minute{(timespan.TotalMinutes < 2 ? "" : "s")} ago";
            else if (timespan.TotalHours < 24)
                return "Today";
            else if (timespan.TotalHours < 48)
                return "Yesderday";
            else
                return $"{date.ToString("dd MMM yyyy")}";
        }
    }
}
