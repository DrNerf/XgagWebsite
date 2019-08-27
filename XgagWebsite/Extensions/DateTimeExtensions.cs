using System;

namespace XgagWebsite
{
    public static class DateTimeExtensions
    {
        public static string GetDateString(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        public static string GetTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }
    }
}