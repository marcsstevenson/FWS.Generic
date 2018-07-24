using System;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Helpers.DateAndTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers
{
    [TestClass]
    public class DateTimeHelperTets
    {

        [TestMethod]
        public void ToStartOfDayAndToEndOfDayShallBeAMillisecondAppartForSequentialDays()
        {
            //Setup
            var testDate1 = new DateTime(2014, 10, 20);
            var testDate2 = new DateTime(2014, 10, 21);

            //Exercise
            var endOfTestDate1 = testDate1.ToEndOfDay();
            var startOfTestDate2 = testDate2.ToStartOfDay();

            //Verify
            Assert.IsTrue(endOfTestDate1.AddMilliseconds(1).Equals(startOfTestDate2));
        }

        [TestMethod]
        public void GetTheNext5thMinuteShallReturn1435For1433()
        {
            //Setup
            var dateTime = new DateTime(2015, 1, 29, 14, 33, 52);

            //Exercise
            var next5th = dateTime.GetTheNext5thMinute();

            //Verify
            Assert.AreEqual(new DateTime(2015, 1, 29, 14, 35, 0), next5th);
        }

        [TestMethod]
        public void GetTheNext5thMinuteShallReturn1500For1457()
        {
            //Setup
            var dateTime = new DateTime(2015, 1, 29, 14, 57, 52);

            //Exercise
            var next5th = dateTime.GetTheNext5thMinute();

            //Verify
            Assert.AreEqual(new DateTime(2015, 1, 29, 15, 00, 0), next5th);
        }

        [TestMethod]
        public void GetTheNext5thMinuteShallReturnForNextDayFor2358()
        {
            //Setup
            var dateTime = new DateTime(2015, 1, 29, 23, 58, 12);

            //Exercise
            var next5th = dateTime.GetTheNext5thMinute();

            //Verify
            Assert.AreEqual(new DateTime(2015, 1, 30, 00, 00, 0), next5th);
        }
    }
}
