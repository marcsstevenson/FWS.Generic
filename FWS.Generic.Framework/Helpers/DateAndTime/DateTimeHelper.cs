using System;

namespace FWS.Generic.Framework.Helpers.DateAndTime
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Returns a new instance without time components (hours, minutes etc)
        /// </summary>
        public static DateTime ToDateOnly(this DateTime dateTime)
        {
            return new DateTime(year: dateTime.Year, month: dateTime.Month, day: dateTime.Day);
        }

        /// <summary>
        /// Returns the start of this day
        /// </summary>
        public static DateTime ToStartOfDay(this DateTime dateTime)
        {
            return dateTime.ToDateOnly(); //This is the same thing
        }

        /// <summary>
        /// Returns the end this day
        /// </summary>
        public static DateTime ToEndOfDay(this DateTime dateTime)
        {
            return new DateTime(year: dateTime.Year, month: dateTime.Month, day: dateTime.Day, hour: 23, minute: 59, second: 59, millisecond: 999);
        }

        /// <summary>
        /// Returns the start of month (first day of the month)
        /// </summary>
        public static DateTime ToStartOfMonth(this DateTime dateTime)
        {
            return new DateTime(year: dateTime.Year, month: dateTime.Month, day: 1);
        }


        /// <summary>
        /// Returns the end of month (end day of the month)
        /// </summary>
        public static DateTime ToEndOfMonth(this DateTime dateTime)
        {
            return new DateTime(year: dateTime.Year, month: dateTime.Month, day: DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        /// <summary>
        /// Returns true if the date components are the dates are the same
        /// </summary>
        public static bool IsSameDate(this DateTime dateTime, DateTime toCompare)
        {
            return dateTime.Year == toCompare.Year &&
                   dateTime.Month == toCompare.Month &&
                   dateTime.Date == toCompare.Date;
        }

        /// <summary>
        /// Returns true if the date components are the dates are the same
        /// </summary>
        public static bool IsSameDateAndTime(this DateTime dateTime, DateTime toCompare)
        {
            return dateTime.Year == toCompare.Year &&
                   dateTime.Month == toCompare.Month &&
                   dateTime.Date == toCompare.Date &&
                   dateTime.Hour == toCompare.Hour &&
                   dateTime.Minute == toCompare.Minute &&
                   dateTime.Second == toCompare.Second;
        }

        public static bool IsLastDayOfMonth(this DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }

        public static DayOfWeek? GetDayOfWeekFromString(string dayOfWeekString )
        {

            switch (dayOfWeekString.Trim().ToLower())
            {
                case "monday":
                    return DayOfWeek.Monday;
                    
                case "tuesday":
                    return DayOfWeek.Tuesday;
                    
                case "wednesday":
                    return DayOfWeek.Wednesday;
                    
                case "thursday":
                    return DayOfWeek.Thursday;
                    
                case "friday":
                    return DayOfWeek.Friday;
                    
                case "saturday":
                    return DayOfWeek.Saturday;
                    
                case "sunday":
                    return DayOfWeek.Sunday;
                    
                default:
                    return null;
            }
        }

        public static DateTime GetTheNext5thMinute()
        {
            return GetTheNext5thMinute(Clock.Now());
        }

        public static DateTime GetTheNext5thMinute(this DateTime dateTime)
        {
            var minute = dateTime.Minute;
            var nextMinute = 0;
            var addHour = false;
            //var addDay = false;

            if (minute%5 == 0)
                nextMinute += 5; //Just add 5
            else
            {
                int fifths = Convert.ToInt32(System.Math.Ceiling(Convert.ToDouble(minute) / 5));
                nextMinute = fifths * 5;
            }

            if (nextMinute >= 60)
            {
                nextMinute = 0;
                addHour = true;
            }

            var next5th = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, nextMinute, 0);

            if (addHour) next5th = next5th.AddHours(1);

            return next5th;
        }
        
        public static DateTime GetMidPoint(DateTime firstDay, DateTime lastDay)
        {
            var timeSpan = lastDay.Subtract(firstDay);
            return firstDay.Add(new TimeSpan(timeSpan.Ticks / 2));
        }

        public static string GetReadable(this DateTime dateTime)
        {
            return dateTime.ToString("dd MMM yyyy");
        }

        /// <summary>
        /// Returns a date from a yyyy-mm-dd format of string
        /// </summary>
        public static DateTime? FromYyyyMmDd(string yyyyMmDd)
        {
            if (string.IsNullOrEmpty(yyyyMmDd) || yyyyMmDd.Length != 10 || !yyyyMmDd.Contains("-")) return null;

            var splits = yyyyMmDd.Split('-');

            if (splits.Length != 3) return null;

            var yyyy = int.Parse(splits[0]);
            var mm = int.Parse(splits[1]);
            var dd = int.Parse(splits[2]);

            return new DateTime(yyyy, mm, dd);
        }

        /// <summary>
        /// Returns the date in a year, month, day format
        /// </summary>
        /// <param name="dateTime">The date to convert</param>
        public static string ToYyyyMmDd(DateTime dateTime)
        {
            return ToYyyyMmDd(dateTime, "-");
        }

        /// <summary>
        /// Returns the date in a year, month, day format
        /// </summary>
        /// <param name="dateTime">The date to convert</param>
        /// <param name="delimiter">The delimiter to use. Default: "-". Eg 2016-03-18 vs 20160318</param>
        public static string ToYyyyMmDd(DateTime dateTime, string delimiter)
        {
            var yyyy = dateTime.Year.ToString();
            var mm = dateTime.Month.ToString().PadLeft(2, '0');
            var dd = dateTime.Day.ToString().PadLeft(2, '0');
            
            return yyyy + delimiter + mm + delimiter + dd;
        }

        /// <summary>
        /// Returns the date in a year, month, day format
        /// </summary>
        /// <param name="dateTime">The date to convert</param>
        /// <param name="delimiter">The delimiter to use. Default: "-". Eg 2016-03-18 vs 20160318</param>
        public static string ToYyMmDd(DateTime dateTime, string delimiter)
        {
            var yy = dateTime.ToString("yy");
            var mm = dateTime.Month.ToString().PadLeft(2, '0');
            var dd = dateTime.Day.ToString().PadLeft(2, '0');

            return yy + delimiter + mm + delimiter + dd;
        }

        /// <summary>
        /// Returns the time in a hour, minute format
        /// </summary>
        /// <param name="dateTime">The time to convert</param>
        /// <param name="delimiter">The delimiter to use. Eg 14:02 vs 1401</param>
        public static string ToHhMm(DateTime dateTime, string delimiter = ":")
        {
            var hh = dateTime.Hour.ToString().PadLeft(2, '0');
            var mm = dateTime.Minute.ToString().PadLeft(2, '0');
            
            return hh + delimiter + mm;
        }

        /// <summary>
        /// Returns the time in a hour, minute, second format
        /// </summary>
        /// <param name="dateTime">The time to convert</param>
        /// <param name="delimiter">The delimiter to use. Eg 14:02 vs 1401</param>
        public static string ToHhMmSs(DateTime dateTime, string delimiter = ":")
        {
            var ss = dateTime.Second.ToString().PadLeft(2, '0');
            
            return ToHhMm(dateTime, delimiter) + delimiter + ss;
        }

        public static DateTime GetNextMidnight(DateTime dateTime)
        {
            return dateTime.Date.AddDays(1);
        }

        public static int GetMinutesUntilMidnight(DateTime dateTime)
        {
            var nextMidnight = GetNextMidnight(dateTime);
            return Convert.ToInt32(System.Math.Floor(nextMidnight.Subtract(dateTime).TotalMinutes));
        }
        
        public static DateTime Next(this DateTime date, DayOfWeek dayOfWeek)
        {
            return date.Date.AddDays(7 - (date.DayOfWeek - dayOfWeek + 7) % 7);
        }

        public static DateTime Previous(this DateTime date, DayOfWeek day)
        {
            return date.AddDays(-((7 - (day - date.DayOfWeek)) % 7));
        }

        /// <summary>
        /// Converts date string to DateTime object
        /// </summary>
        public static DateTime ToDateTime(this string dateTimeString, IFormatProvider formatProvider = null)
        {
            return formatProvider == null ? Convert.ToDateTime(dateTimeString) : Convert.ToDateTime(dateTimeString, formatProvider);
        }
    }
}