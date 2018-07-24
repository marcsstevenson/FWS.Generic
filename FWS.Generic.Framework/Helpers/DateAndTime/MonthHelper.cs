﻿using System.Collections.Generic;
using System.Globalization;

namespace FWS.Generic.Framework.Helpers.DateAndTime
{
    public static class MonthHelper
    {
        public static List<string> GetMonthsForCurrentCulture()
        {
            return GetMonthsForCulture(CultureInfo.CurrentCulture);
        }

        public static List<string> GetMonthsForCulture(CultureInfo cultureInfo)
        {
            var monthNames = new List<string>();

            for (var i = 1; i <= 12; i++)
                monthNames.Add(GetMonthNameForCulture(cultureInfo, i));

            return monthNames;
        }

        public static string GetMonthNameForCurrentCulture(int monthNumber)
        {
            return GetMonthNameForCulture(CultureInfo.CurrentCulture, monthNumber);
        }

        /// <summary>
        /// Returns the month name for a given culture and month number
        /// </summary>
        /// <param name="cultureInfo">The culture to translate</param>
        /// <param name="monthNumber">The number of the month. Eg January = 1</param>
        public static string GetMonthNameForCulture(CultureInfo cultureInfo, int monthNumber)
        {
            return cultureInfo.DateTimeFormat.GetMonthName(monthNumber);
        }
    }
}