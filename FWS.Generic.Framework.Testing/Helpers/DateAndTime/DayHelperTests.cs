using System;
using FWS.Generic.Framework.Helpers.DateAndTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.DateAndTime
{
    [TestClass]
    public class DayHelperTests
    {
        [TestMethod]
        public void NextDayOfWeekForMondayShallReturnTuesday()
        {
            //Setup
            const DayOfWeek monday = DayOfWeek.Monday;

            //Exercise
            var nextDayOfWeek = monday.NextDayOfWeek();

            //Verify
            Assert.AreEqual(DayOfWeek.Tuesday, nextDayOfWeek);
        }

        [TestMethod]
        public void PreviousDayOfWeekForThursdayShallReturnWednesday()
        {
            //Setup
            const DayOfWeek thursday = DayOfWeek.Thursday;

            //Exercise
            var previousDayOfWeek = thursday.PreviousDayOfWeek();

            //Verify
            Assert.AreEqual(DayOfWeek.Wednesday, previousDayOfWeek);
        }

        [TestMethod]
        public void DayBetweenForMondayAndSundayShallReturn6()
        {
            //Exercise
            var differential = DayHelper.DaysBetween(DayOfWeek.Monday, DayOfWeek.Sunday);

            //Verify
            Assert.AreEqual(6, differential);
        }

        [TestMethod]
        public void DayBetweenForMondayAndTuesdayShallReturn1()
        {
            //Exercise
            var differential = DayHelper.DaysBetween(DayOfWeek.Monday, DayOfWeek.Tuesday);

            //Verify
            Assert.AreEqual(1, differential);
        }

        [TestMethod]
        public void DayBetweenForMondayAndMondayShallReturn0()
        {
            //Exercise
            var differential = DayHelper.DaysBetween(DayOfWeek.Monday, DayOfWeek.Monday);

            //Verify
            Assert.AreEqual(0, differential);
        }

        [TestMethod]
        public void GetLastDayOfWeekForATuesdayShouldReturnTheNextSundayForFirstDayOfWeekBeingMonday()
        {
            //Setup
            var workingDate = new DateTime(2014, 7, 8); //A Tuesday

            //Exercise
            var lastDayOfWeek = DayHelper.GetLastDayOfWeek(workingDate, DayOfWeek.Monday);

            //Verify
            Assert.AreEqual(DayOfWeek.Tuesday, workingDate.DayOfWeek);
            Assert.AreEqual(new DateTime(2014, 7, 13), lastDayOfWeek);
        }

        [TestMethod]
        public void DayIsBetweenShallReturnTrueForTuesdayIsBetweenMondayAndWednesday()
        {
            //Exercise
            var isBetween = DayOfWeek.Tuesday.IsBetween(DayOfWeek.Monday, DayOfWeek.Wednesday);

            //Verify
            Assert.IsTrue(isBetween);
        }

        [TestMethod]
        public void DayIsBetweenShallReturnFalseForMondayIsBetweenTuesdayAndWednesday()
        {
            //Exercise
            var isBetween = DayOfWeek.Monday.IsBetween(DayOfWeek.Tuesday, DayOfWeek.Wednesday);

            //Verify
            Assert.IsFalse(isBetween);
        }

        [TestMethod]
        public void DayIsBetweenShallReturnTrueForTuesdayIsBetweenFridayAndWednesday()
        {
            //Exercise
            var isBetween = DayOfWeek.Tuesday.IsBetween(DayOfWeek.Friday, DayOfWeek.Wednesday);

            //Verify
            Assert.IsTrue(isBetween);
        }

        [TestMethod]
        public void DayIsBetweenShallReturnFalseForTuesdayIsBetweenFridayAndMonday()
        {
            //Exercise
            var isBetween = DayOfWeek.Tuesday.IsBetween(DayOfWeek.Friday, DayOfWeek.Monday);

            //Verify
            Assert.IsFalse(isBetween);
        }

        [TestMethod]
        public void DayIsBetweenShallReturnFalseForTuesdayIsBetweenFridayAndTuesday()
        {
            //Exercise
            var isBetween = DayOfWeek.Tuesday.IsBetween(DayOfWeek.Friday, DayOfWeek.Tuesday);

            //Verify
            Assert.IsFalse(isBetween);
        }

        [TestMethod]
        public void DayIsBetweenShallReturnTrueForTuesdayIsBetweenTuesdayAndWednesday()
        {
            //Exercise
            var isBetween = DayOfWeek.Tuesday.IsBetween(DayOfWeek.Tuesday, DayOfWeek.Wednesday);

            //Verify
            Assert.IsTrue(isBetween);
        }

        [TestMethod]
        public void DayIsBetweenShallReturnTrueForTuesdayIsBetweenTuesdayAndMonday()
        {
            //Exercise
            var isBetween = DayOfWeek.Tuesday.IsBetween(DayOfWeek.Tuesday, DayOfWeek.Monday);

            //Verify
            Assert.IsTrue(isBetween);
        }
    }
}
