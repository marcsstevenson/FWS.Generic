using System;
using FWS.Generic.Framework.Helpers.DateAndTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.DateAndTime
{
    [TestClass]
    public class DateAndTimeTests
    {
        [TestMethod]
        public void WeeksBetweenAn7DaySpanShallReturn2InclusiveRoundedUp()
        {
            //Setup
            var date1 = new DateTime(2000, 1, 1);
            var date2 = date1.AddDays(7);

            //Exercise
            int weeksBetween = date2.WeeksBetween(date1);

            //Verify
            Assert.AreEqual(2, weeksBetween);
        }

        [TestMethod]
        public void WeeksBetweenAn7DaySpanShallReturn1NonInclusiveRoundedUp()
        {
            //Setup
            var date1 = new DateTime(2000, 1, 1);
            var date2 = date1.AddDays(7);

            //Exercise
            int weeksBetween = date2.WeeksBetween(date1, false);

            //Verify
            Assert.AreEqual(1, weeksBetween);
        }

        [TestMethod]
        public void GetNextMidnightShallReturnAsExpected()
        {
            //Setup
            var testFixture = new DateTime(2000, 1, 2, 3, 56, 6);
            var expectedResult = new DateTime(2000, 1, 3);

            //Exercise
            var actualResult = DateTimeHelper.GetNextMidnight(testFixture);

            //Verify
            Assert.IsTrue(expectedResult.IsSameDateAndTime(actualResult));
        }

        [TestMethod]
        public void GetMinutesUntilMidnightShallREturn6For235405()
        {
            //Setup
            var testFixture = new DateTime(2000, 1, 2, 23, 54, 05);
            var expectedResult = 5;

            //Exercise
            var actualResult = DateTimeHelper.GetMinutesUntilMidnight(testFixture);

            //Verify
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
