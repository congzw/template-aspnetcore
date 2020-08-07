using System;

namespace Common
{
    public class MyDateHelper
    {
        public Func<DateTime> GetDateDefault = () => new DateTime(2000, 1, 1);
        public Func<DateTime> GetDateNow = () => DateTime.Now;
        public static MyDateHelper Instance = new MyDateHelper();
    }

    public static class MyDatetimeExtensions
    {
        public static string AsFormat(this DateTime datetime, string format = "yyyyMMdd-HH:mm:ss")
        {
            return datetime.ToString(format);
        }

        public static string AsFormat(this DateTime? datetime, string format = "yyyyMMdd-HH:mm:ss")
        {
            return datetime?.ToString(format);
        }

        public static string AsFormat2(this DateTime datetime, string format = "yyyyMMdd-HH:mm:ss.fff")
        {
            return datetime.ToString(format);
        }

        public static string AsFormat2(this DateTime? datetime, string format = "yyyyMMdd-HH:mm:ss.fff")
        {
            return datetime?.ToString(format);
        }
    }
}
