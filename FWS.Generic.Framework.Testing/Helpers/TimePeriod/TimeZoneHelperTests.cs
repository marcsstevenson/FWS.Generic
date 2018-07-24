using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Helpers.DateAndTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FWS.Generic.Framework.Testing.Helpers.TimePeriod
{
    [TestClass]
    public class TimeZoneHelperTests
    {
        [TestMethod]
        public void TimeZoneShallConvertToStringAndBack()
        {
            //Setup
            var timeZone = TimeZoneInfo.Local;

            TimeZoneInfo.ConvertTime(Clock.Now(), timeZone);

            //Exercise
            var timeZoneInfoFromString = TimeZoneHelper.TimeZoneInfoFromId(timeZone.Id);

            //Verify
            Assert.AreEqual(timeZone.Id, timeZoneInfoFromString.Id);
        }

        [TestMethod]
        public void LocalTimeForNZTShallNotBeTheSameAsUTC()
        {
            //Setup
            var time = Clock.Now();
            var timeZone = TimeZoneHelper.TimeZoneInfoFromId(TimeZoneHelper.NewZealandTimeZone);
            
            //Exercise
            var timeInNewZealand = timeZone.GetLocalTime(time);

            //Verify
            Assert.AreNotEqual(timeInNewZealand, time);
        }
    }
}