using System;

namespace HuyenThoaiNSO.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetTimeAgo(this DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalDays > 365)
            {
                var years = (int)(timeSpan.TotalDays / 365);
                return $"{years} {(years == 1 ? "year" : "years")} ago";
            }
            else if (timeSpan.TotalDays > 30)
            {
                var months = (int)(timeSpan.TotalDays / 30);
                return $"{months} {(months == 1 ? "month" : "months")} ago";
            }
            else if (timeSpan.TotalDays >= 1)
            {
                var days = (int)timeSpan.TotalDays;
                return $"{days} {(days == 1 ? "day" : "days")} ago";
            }
            else if (timeSpan.TotalHours >= 1)
            {
                var hours = (int)timeSpan.TotalHours;
                return $"{hours} {(hours == 1 ? "hour" : "hours")} ago";
            }
            else if (timeSpan.TotalMinutes >= 1)
            {
                var minutes = (int)timeSpan.TotalMinutes;
                return $"{minutes} {(minutes == 1 ? "minute" : "minutes")} ago";
            }
            else
            {
                var seconds = (int)timeSpan.TotalSeconds;
                return $"{seconds} {(seconds == 1 ? "second" : "seconds")} ago";
            }
        }
    }
}