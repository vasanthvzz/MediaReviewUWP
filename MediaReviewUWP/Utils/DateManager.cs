using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewUWP.Utils
{
    public static class DateManager
    {
        public static string RelativeToCurrent(DateTime date)
        {
            var now = DateTime.Now;
            var timespan = now - date;

            if (timespan.TotalSeconds < 60)
                return $"{(int)timespan.TotalSeconds} second{(timespan.TotalSeconds < 2 ? "" : "s")} ago";
            else if (timespan.TotalMinutes < 60)
                return $"{(int)timespan.TotalMinutes} minute{(timespan.TotalMinutes < 2 ? "" : "s")} ago";
            else if (timespan.TotalHours < 24)
                return $"{(int)timespan.TotalHours} hour{(timespan.TotalHours < 2 ? "" : "s")} ago";
            else if (timespan.TotalDays < 7)
                return $"{(int)timespan.TotalDays} day{(timespan.TotalDays < 2 ? "" : "s")} ago";
            else if (timespan.TotalDays < 30)
                return $"{(int)(timespan.TotalDays / 7)} week{((timespan.TotalDays / 7) < 2 ? "" : "s")} ago";
            else if (timespan.TotalDays < 365)
                return $"{(int)(timespan.TotalDays / 30)} month{((timespan.TotalDays / 30) < 2 ? "" : "s")} ago";
            else
                return $"{(int)(timespan.TotalDays / 365)} year{((timespan.TotalDays / 365) < 2 ? "" : "s")} ago";
        }
    }
}
