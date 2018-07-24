using System;
using System.Linq;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Helpers.DateAndTime;
using FWS.Generic.Framework.Helpers.TimePeriod;
using FWS.Generic.Framework.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.TimePeriod
{
    [TestClass]
    public class TimePeriodHelperTests
    {
        [TestMethod]
        public void GetCurrentShallReturnTheExpectedValue()
        {
            //Setup
            var testTimePeriods = new List<TestClass>
            {
                new TestClass
                {
                    FirstDay = new DateTime(2000, 1, 1),
                    LastDay = new DateTime(2000, 1, 7)
                },
                new TestClass
                {
                    FirstDay = new DateTime(2000, 1, 8),
                    LastDay = new DateTime(2000, 1, 14)
                },
                new TestClass
                {
                    FirstDay = new DateTime(2000, 1, 15),
                    LastDay = new DateTime(2000, 1, 21)
                }
            };

            //Exercise
            var current = testTimePeriods.GetCurrent(new DateTime(2000, 1, 9));

            //Verify
            Assert.IsNotNull(current, "GetCurrent did not return a value");
            Assert.AreEqual(testTimePeriods[1].FirstDay, current.FirstDay, "Get current did not return the expected time period");
        }

        [TestMethod]
        public void GetCurrentShallReturnNullForOutOfRangeValue()
        {
            //Setup
            var testTimePeriods = new List<TestClass>
            {
                new TestClass
                {
                    FirstDay = new DateTime(2000, 1, 1),
                    LastDay = new DateTime(2000, 1, 7)
                },
                new TestClass
                {
                    FirstDay = new DateTime(2000, 1, 8),
                    LastDay = new DateTime(2000, 1, 14)
                },
                new TestClass
                {
                    FirstDay = new DateTime(2000, 1, 15),
                    LastDay = new DateTime(2000, 1, 21)
                }
            };

            //Exercise
            var current = testTimePeriods.GetCurrent(new DateTime(2001, 2, 9));

            //Verify
            Assert.IsNull(current, "GetCurrent returned a value");
        }

        [TestMethod]
        public void BuildTimePeriodWeeksWithNoFirstDayOfWeekShallReturnAnExpectedResult()
        {
            //Setup
            var testTimePeriod = new TestClass
            {
                FirstDay = new DateTime(2014, 6, 2),
                LastDay = new DateTime(2014, 6, 27)
            };

            //Exercise
            var timePeriodWeeks = testTimePeriod.BuildTimePeriodWeeks<TimePeriodWeek>(new DateTime(2014, 6, 10), null);

            //Verify
            Assert.IsNotNull(timePeriodWeeks, "BuildTimePeriodWeeks did not return a value");
            Assert.AreEqual(4, timePeriodWeeks.Count, "We expected 4 weeks");
            Assert.AreEqual(1, timePeriodWeeks.Count(i => i.IsCurrent), "We expected only 1 current week");

            for (var i = 0; i < timePeriodWeeks.Count - 1; i++)
                Assert.AreEqual(i + 1, timePeriodWeeks[i].WeekNumber, "The week numbers were not correctly set");

            Assert.IsTrue(testTimePeriod.LastDay.IsSameDate(timePeriodWeeks.Last().LastDay));
            Assert.IsTrue(testTimePeriod.FirstDay.IsSameDate(timePeriodWeeks.First().FirstDay));
        }

        [TestMethod]
        public void BuildTimePeriodWeeksWithOverAFirstDayOfMondayShallReturnAnTwoTimePeriods()
        {
            //Setup
            var testTimePeriod = new TestClass
            {
                FirstDay = new DateTime(2014, 6, 6), //Friday
                LastDay = new DateTime(2014, 6, 10) //Tuesday
            };

            //Exercise
            var timePeriodWeeks = testTimePeriod.BuildTimePeriodWeeks<TimePeriodWeek>(new DateTime(2014, 6, 9), DayOfWeek.Monday);

            //Verify
            Assert.IsTrue(testTimePeriod.FirstDay.DayOfWeek == DayOfWeek.Friday);
            Assert.IsTrue(testTimePeriod.LastDay.DayOfWeek == DayOfWeek.Tuesday);

            Assert.IsNotNull(timePeriodWeeks, "BuildTimePeriodWeeks did not return a value");
            Assert.AreEqual(2, timePeriodWeeks.Count, "We expected 2 weeks");

            Assert.IsTrue(new DateTime(2014, 6, 8).IsSameDate(timePeriodWeeks.First().LastDay));
            Assert.IsTrue(new DateTime(2014, 6, 9).IsSameDate(timePeriodWeeks.Last().FirstDay));

            Assert.AreEqual(1, timePeriodWeeks.Count(i => i.IsCurrent), "We expected only 1 current week");

            for (var i = 0; i < timePeriodWeeks.Count - 1; i++)
                Assert.AreEqual(i + 1, timePeriodWeeks[i].WeekNumber, "The week numbers were not correctly set");

            Assert.IsTrue(testTimePeriod.LastDay.IsSameDate(timePeriodWeeks.Last().LastDay));
            Assert.IsTrue(testTimePeriod.FirstDay.IsSameDate(timePeriodWeeks.First().FirstDay));
        }

        [TestMethod]
        public void BuildTimePeriodWeeksShallIgnoreTimes()
        {
            //Setup
            var testTimePeriod = new TestClass
            {
                FirstDay = new DateTime(2014, 10, 13, 10, 0, 0),
                LastDay = new DateTime(2014, 10, 21, 10, 0, 0)
            };

            Clock.Now = () => new DateTime(2014, 10, 18);

            //Exercise
            var timePeriodWeeks = testTimePeriod.BuildTimePeriodWeeks<TimePeriodWeek>(null, null);

            //Verify
            Assert.IsTrue(timePeriodWeeks.Count(i => i.IsCurrent) == 1, "There should be 1 current time period");

            Assert.IsTrue(timePeriodWeeks.All(i => i.FirstDay.Hour == 0), "None of the first days should have an Hour value");
            Assert.IsTrue(timePeriodWeeks.All(i => i.FirstDay.Minute == 0), "None of the first days should have a Minute value");
            Assert.IsTrue(timePeriodWeeks.All(i => i.FirstDay.Second == 0), "None of the first days should have a Second value");
            
            Assert.IsTrue(timePeriodWeeks[0].LastDay.AddDays(1).Equals(timePeriodWeeks[1].FirstDay));

            //Clean up
            Clock.Reset();
        }

        [TestMethod]
        public void GetDaysForA1DayTimePeriodShallReturn1Day()
        {
            //Setup
            var testTimePeriod = new TestClass
            {
                FirstDay = new DateTime(2000, 1, 1),
                LastDay = new DateTime(2000, 1, 1)
            };

            //Exercise
            var days = testTimePeriod.GetDates();

            //Verify
            Assert.IsNotNull(days, "BuildTimePeriodWeeks did not return a value");
            Assert.AreEqual(1, days.Count);
        }

        [TestMethod]
        public void GetDaysForA3DayTimePeriodShallReturn3Day()
        {
            //Setup
            var testTimePeriod = new TestClass
            {
                FirstDay = new DateTime(2000, 1, 1),
                LastDay = new DateTime(2000, 1, 3)
            };

            //Exercise
            var days = testTimePeriod.GetDates();

            //Verify
            Assert.IsNotNull(days, "BuildTimePeriodWeeks did not return a value");
            Assert.AreEqual(3, days.Count);
        }

        [TestMethod]
        public void WeeksInTimePeriodShallReturn1ForAMonToFriWeekWhenMonIsStart()
        {
            //Setup
            var test = new TestClass()
            {
                FirstDay = new DateTime(2014, 07, 07), //Monday
                LastDay = new DateTime(2014, 07, 11) //Friday
            };

            //Exercise
            var weeksInTimePeriod = test.WeeksInTimePeriod(DayOfWeek.Monday, true, true);

            //Verify
            Assert.AreEqual(1 , weeksInTimePeriod);
        }

        [TestMethod]
        public void WeeksInTimePeriodShallReturn2ForATwoWeekSplitWhenMonIsStart()
        {
            //Setup
            var test = new TestClass()
            {
                FirstDay = new DateTime(2014, 07, 02), //Wednesday
                LastDay = new DateTime(2014, 07, 08) //Tuesday
            };

            //Exercise
            var weeksInTimePeriod = test.WeeksInTimePeriod(DayOfWeek.Monday, true, true);

            //Verify
            Assert.AreEqual(2, weeksInTimePeriod);
        }

        [TestMethod]
        public void GetDatesForAThreeDayPeriodShallReturnThreeDates()
        {
            //Setup
            var testClass = new TestClass
            {
                FirstDay = new DateTime(2014, 07, 18),
                LastDay = new DateTime(2014, 07, 20)
            };

            //Exercise
            var dates = testClass.GetDates();

            //Verify
            Assert.AreEqual(3, dates.Count);
        }

        [TestMethod]
        public void FilterForOpenDaysShallRemoveFridayWhenFridayIsNotAnOpenDay()
        {
            //Setup
            var testClass = new TestClass
            {
                FirstDay = new DateTime(2014, 07, 1),
                LastDay = new DateTime(2014, 07, 28) 
            };
            var dates = testClass.GetDates();
            const DayOfWeek closedDay = DayOfWeek.Friday;
            var openDays = EnumHelper.EnumToList<DayOfWeek>();
            openDays.Remove(closedDay); //Remove our closed day

            //Exercise
            dates.FilterForOpenDays(openDays);

            //Verify
            Assert.IsTrue(dates.Select(i => i.DayOfWeek).All(i => i != closedDay)); //No Fridays
        }

        [TestMethod]
        public void FilterForExclusionDatesShallRemoveAsExpected()
        {
            //Setup
            var testClass = new TestClass
            {
                FirstDay = new DateTime(2014, 07, 18),
                LastDay = new DateTime(2014, 07, 20)
            };
            var dates = testClass.GetDates();

            //Exercise
            //Exercise
            dates.FilterForExclusionDates(new List<DateTime>{ new DateTime(2014, 7, 19) }); //Filter out the middle day

            //Verify
            Assert.AreEqual(2, dates.Count);
        }

        [TestMethod]
        public void FridayTimePeriodWithFridayNotAnOpenDayShallBeDeterminedToBeEmpty()
        {
            //Setup
            var testClass = new TestClass
            {
                FirstDay = new DateTime(2014, 07, 18), //Friday
                LastDay = new DateTime(2014, 07, 18) //Friday
            };

            const DayOfWeek closedDay = DayOfWeek.Friday;
            var openDays = EnumHelper.EnumToList<DayOfWeek>();
            openDays.Remove(closedDay); //Remove our closed day

            //Exercise
            var isEmpty = testClass.IsEmpty(openDays);

            //Verify
            Assert.IsTrue(isEmpty); //No Fridays
        }

        [TestMethod]
        public void SingleDateWithDateIsExclusionDateShallBeDeterminedToBeEmpty()
        {
            //Setup
            var testClass = new TestClass
            {
                FirstDay = new DateTime(2014, 07, 18), //Friday
                LastDay = new DateTime(2014, 07, 18) //Friday
            };
            
            //Exercise
            var isEmpty = testClass.IsEmpty(exclusionDates: new List<DateTime> { new DateTime(2014, 7, 18) });

            //Verify
            Assert.IsTrue(isEmpty); //No 2014, 7, 18
        }

        [TestMethod]
        public void GetFirstDayMatchFridayShallReturnFridayForATimePeriodOfOnlyOneFriday()
        {
            //Setup
            var happyFriday = new DateTime(2014, 07, 18); //Friday
            var testClass = new TestClass
            {
                FirstDay = happyFriday,
                LastDay = happyFriday
            };

            //Exercise
            var dateMatch = testClass.GetFirstDayMatch(DayOfWeek.Friday);

            //Verify
            Assert.IsNotNull(dateMatch);
            Assert.AreEqual(happyFriday, dateMatch);
        }

        [TestMethod]
        public void GetFirstDayMatchFridayShallReturnNullForATimePeriodOfOnlyOneMonday()
        {
            //Setup
            var monday = new DateTime(2014, 07, 13); //Mondays
            var testClass = new TestClass
            {
                FirstDay = monday,
                LastDay = monday
            };

            //Exercise
            var dateMatch = testClass.GetFirstDayMatch(DayOfWeek.Friday);

            //Verify
            Assert.IsNull(dateMatch);
        }

        [TestMethod]
        public void GetFirstDayMatchFridayShallReturnFirstFridayForATimePeriodOfOnlyTwoFridays()
        {
            //Setup
            var happyFriday = new DateTime(2014, 07, 18); //Friday
            var testClass = new TestClass
            {
                FirstDay = happyFriday,
                LastDay = happyFriday.AddDays(7)
            };

            //Exercise
            var dateMatch = testClass.GetFirstDayMatch(DayOfWeek.Friday);

            //Verify
            Assert.IsNotNull(dateMatch);
            Assert.AreEqual(happyFriday, dateMatch);
        }

        [TestMethod]
        public void ContainsDayOfWeekShallReturnMondayPositiveWithinSingleDay()
        {
            //Setup
            var test = new TestClass()
            {
                FirstDay = new DateTime(2015, 1, 19), //Monday
                LastDay = new DateTime(2015, 1, 19) //Monday
            };

            //Exercise
            var containsDayOfWeek = test.ContainsDayOfWeek(DayOfWeek.Monday);

            //Verify
            Assert.IsTrue(containsDayOfWeek);
        }

        [TestMethod]
        public void ContainsDayOfWeekShallReturnMondayNegativeWithinSingleDay()
        {
            //Setup
            var test = new TestClass()
            {
                FirstDay = new DateTime(2015, 1, 20), //Tuesday
                LastDay = new DateTime(2015, 1, 20) //Tuesday
            };
            
            //Exercise
            var containsDayOfWeek = test.ContainsDayOfWeek(DayOfWeek.Monday);

            //Verify
            Assert.IsFalse(containsDayOfWeek);
        }

        [TestMethod]
        public void ContainsDayOfWeekShallReturnMondayPositiveWithinRange()
        {
            //Setup
            var test = new TestClass()
            {
                FirstDay = new DateTime(2015, 1, 16), //Friday
                LastDay = new DateTime(2015, 1, 21) //Wednesday
            };

            //Exercise
            var containsDayOfWeek = test.ContainsDayOfWeek(DayOfWeek.Monday);

            //Verify
            Assert.IsTrue(containsDayOfWeek);
        }

        [TestMethod]
        public void ContainsDayOfWeekShallReturnMondayNegativeWithinRange()
        {
            //Setup
            var test = new TestClass()
            {
                FirstDay = new DateTime(2015, 1, 16), //Friday
                LastDay = new DateTime(2015, 1, 18) //Sunday
            };

            //Exercise
            var containsDayOfWeek = test.ContainsDayOfWeek(DayOfWeek.Monday);

            //Verify
            Assert.IsFalse(containsDayOfWeek);
        }

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
        public void GetClosestToTimeShallReturnTheFirstOptionWhenBeforeAllOptions()
        {
            //Setup
            var testClasses = new List<TestClass>()
            {
                new TestClass(new DateTime(2015, 3, 1), new DateTime(2015, 3, 3)),
                new TestClass(new DateTime(2015, 3, 4), new DateTime(2015, 3, 6))
            };

            //Exercise
            var closestToTime = testClasses.GetClosestToTime(new DateTime(2015, 2, 28));

            //Verify
            Assert.AreEqual(new DateTime(2015, 3, 1), closestToTime.FirstDay);
        }

        [TestMethod]
        public void GetClosestToTimeShallReturnTheLastOptionWhenAfterAllOptions()
        {
            //Setup
            var testClasses = new List<TestClass>()
            {
                new TestClass(new DateTime(2015, 3, 1), new DateTime(2015, 3, 3)),
                new TestClass(new DateTime(2015, 3, 4), new DateTime(2015, 3, 6))
            };

            //Exercise
            var closestToTime = testClasses.GetClosestToTime(new DateTime(2015, 3, 7));

            //Verify
            Assert.AreEqual(new DateTime(2015, 3, 4), closestToTime.FirstDay);
        }
        
        [TestMethod]
        public void GetClosestToTimeShallReturnTheCurrentOptionWhenDuringAnOption()
        {
            //Setup
            var testClasses = new List<TestClass>()
            {
                new TestClass(new DateTime(2015, 3, 1), new DateTime(2015, 3, 3)),
                new TestClass(new DateTime(2015, 3, 4), new DateTime(2015, 3, 6))
            };

            //Exercise
            var closestToTime = testClasses.GetClosestToTime(new DateTime(2015, 3, 2));

            //Verify
            Assert.AreEqual(new DateTime(2015, 3, 1), closestToTime.FirstDay);
        }

        [TestMethod]
        public void IsLastDayOfMonthShallReturnTrueForLeapDay()
        {
            //Verify
            Assert.IsTrue(new DateTime(2016, 2, 29).IsLastDayOfMonth());
        }

        [TestMethod]
        public void IsLastDayOfMonthShallReturnFalseForSecondOfMonth()
        {
            //Verify
            Assert.IsFalse(new DateTime(2016, 2, 2).IsLastDayOfMonth());
        }

        private class TestClass : ITimePeriod
        {
            public TestClass()
            {
            }

            public TestClass(DateTime firstDay, DateTime lastDay)
            {
                FirstDay = firstDay;
                LastDay = lastDay;
            }

            public DateTime FirstDay { get; set; }
            public DateTime LastDay { get; set; }
        }
    }
}