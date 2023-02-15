using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace OutlookCalendar.Domain.Core.Helpers
{
    public static class DateTimeHelpers
    {
        /// <summary>
        /// Returns TimeZone adjusted time for a given from a Utc or local time.
        /// Date is first converted to UTC then adjusted.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        public static DateTime ToTimeZoneTime(this DateTime time, TimeZoneInfo timeZoneInfo)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(time, timeZoneInfo);
        }

        public static DateTime ToUTC(this DateTime time, string timeZoneId)
        {
            time = DateTime.SpecifyKind(time, DateTimeKind.Unspecified);
            var result = TimeZoneInfo.ConvertTimeToUtc(time, GetTimesZone(timeZoneId));
            return result;
        }

        public static TimeZoneInfo GetTimesZone(string Id)
        {
            return GetTimesZones().Where(c => c.Id.Equals(Id)).FirstOrDefault();
        }

        public static ReadOnlyCollection<TimeZoneInfo> GetTimesZones()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }


        public static DateTime ToStartOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime ToEndOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, DateTimeKind.Utc);
        }

        public static DateTime? ToStartOfDay(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;
            return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, 0, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime? ToEndOfDay(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;
            return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, 23, 59, 59,
                DateTimeKind.Utc);
        }

        /// <summary>
        /// Returns TimeZone adjusted time for a given from a Utc or local time.
        /// Date is first converted to UTC then adjusted.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeZoneId"></param>
        /// <returns></returns>
        public static DateTime ToTimeZoneTime(this DateTime time, string timeZoneId = "SA Pacific Standard Time")
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return time.ToTimeZoneTime(tzi);
        }

        /// <summary>
        /// Returns TimeZone adjusted time for a given from a Utc or local time.
        /// Date is first converted to UTC then adjusted.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeZoneId"></param>
        /// <returns></returns>
        public static DateTime? ToTimeZoneTime(this DateTime? time, string timeZoneId = "SA Pacific Standard Time")
        {
            if (time == null)
                return null;
            return time.Value.ToTimeZoneTime(timeZoneId);
        }

        public static DateTime? ToUTC(this DateTime? time, string timeZoneId)
        {
            if (time == null)
                return null;
            return time.Value.ToUTC(timeZoneId);
        }

        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue) return dateTime; // do not modify "guard" values
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }

        public static string DateToTimestamp(DateTime date)
        {
            return new DateTime(date.Year,
                                date.Month,
                                date.Day).Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
        }
    }
}
