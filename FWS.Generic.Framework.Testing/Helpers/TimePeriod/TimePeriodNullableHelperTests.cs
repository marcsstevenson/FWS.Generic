using FWS.Generic.Framework.Helpers.TimePeriod;
using FWS.Generic.Framework.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FWS.Generic.Framework.Testing.Helpers.TimePeriod
{
    [TestClass]
    public class TimePeriodNullableHelperTests
    {
        [TestMethod]
        public void GetMidPointShallReturnTheMiddleDayOfThreeDays()
        {
            //Setup
            var testClass = new TestClass()
            {
                FirstDay = new DateTime(2015, 3, 1),
                LastDay = new DateTime(2015, 3, 3)
            };

            //Exercise
            var midPoint = testClass.GetMidPoint();

            //Verify
            Assert.AreEqual(new DateTime(2015, 3, 2), midPoint);
        }

        [TestMethod]
        public void FilterForDateTimeIsInRangeShallIncludeFirstAndLastDayNullValues()
        {
            //Setup
            var testList = new List<TestClass>()
            {
                new TestClass(null, null)
            };

            //Exercise
            var filteredValues = testList.FilterForDateTimeIsInRange(new DateTime(2015, 3, 1));

            //Verify
            Assert.AreEqual(1, filteredValues.Count);
        }

        [TestMethod]
        public void FilterForDateTimeIsInRangeShallIncludeNullLastDayWhenTimeIsAfterFirstDay()
        {
            //Setup
            var testList = new List<TestClass>()
            {
                new TestClass(new DateTime(2015, 3, 1), null)
            };

            //Exercise
            var filteredValues = testList.FilterForDateTimeIsInRange(new DateTime(2015, 3, 2));

            //Verify
            Assert.AreEqual(1, filteredValues.Count);
        }

        [TestMethod]
        public void FilterForDateTimeIsInRangeShallNotIncludeNullLastDayWhenTimeIsBeforeFirstDay()
        {
            //Setup
            var testList = new List<TestClass>()
            {
                new TestClass(new DateTime(2015, 3, 1), null)
            };

            //Exercise
            var filteredValues = testList.FilterForDateTimeIsInRange(new DateTime(2015, 2, 28));

            //Verify
            Assert.AreEqual(0, filteredValues.Count);
        }

        [TestMethod]
        public void FilterForDateTimeIsInRangeShallIncludeNullFirstDayWhenTimeIsBeforeLastDay()
        {
            //Setup
            var testList = new List<TestClass>()
            {
                new TestClass(null, new DateTime(2015, 3, 3))
            };

            //Exercise
            var filteredValues = testList.FilterForDateTimeIsInRange(new DateTime(2015, 3, 2));

            //Verify
            Assert.AreEqual(1, filteredValues.Count);
        }

        [TestMethod]
        public void FilterForDateTimeIsInRangeShallNotIncludeNullFirstDayWhenTimeIsAfterLastDay()
        {
            //Setup
            var testList = new List<TestClass>()
            {
                new TestClass(null, new DateTime(2015, 3, 3))
            };

            //Exercise
            var filteredValues = testList.FilterForDateTimeIsInRange(new DateTime(2015, 3, 4));

            //Verify
            Assert.AreEqual(0, filteredValues.Count);
        }

        private class TestClass : ITimePeriodNullable
        {
            public TestClass()
            {
            }

            public TestClass(DateTime? firstDay, DateTime? lastDay)
            {
                FirstDay = firstDay;
                LastDay = lastDay;
            }

            public DateTime? FirstDay { get; set; }
            public DateTime? LastDay { get; set; }
        }
    }
}