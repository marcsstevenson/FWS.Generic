using System;
using System.Collections.Generic;
using FWS.Generic.Framework.Enumerations;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimePeriodHelper = FWS.Generic.Framework.Helpers.TimePeriod.TimePeriodHelper;

namespace FWS.Generic.Framework.Testing.Helpers
{
    [TestClass]
    public class TimePeriodTests
    {
        [TestMethod]
        public void TimePeriodsWithFirstAfterFinishShallFailValidation()
        {
            //Setup
            var timePeriods = new List<ITimePeriod>
            {
                new TestSubject{ FirstDay = new DateTime(2000, 2, 1), LastDay = new DateTime(2000, 1, 1)}
            };

            //Exercise
            var validation = TimePeriodHelper.ValidateTimePeriods(timePeriods, false);

            //Assert
            Assert.IsNotNull(validation, "Validation should have failed");
            Assert.AreEqual(validation.Message, Framework.Helpers.TimePeriod.TimePeriodHelper.AllTimePeriodsShallHaveFirstDaysBeforeLastDaysErrorMessage, "The incorrect error message was returned from validation");
        }

        [TestMethod]
        public void OverlappingTimePeriodsShallFailValidation()
        {
            //Setup
            var timePeriods = new List<ITimePeriod>
            {
                new TestSubject{ FirstDay = new DateTime(2000, 1, 1), LastDay = new DateTime(2000, 3, 1)},
                new TestSubject{ FirstDay = new DateTime(2000, 2, 1), LastDay = new DateTime(2000, 4, 1)},
            };
            
            //Exercise
            var validation = Framework.Helpers.TimePeriod.TimePeriodHelper.ValidateTimePeriods(timePeriods, false);

            //Assert
            Assert.IsNotNull(validation, "Validation should have failed");
            Assert.AreEqual(validation.Message, Framework.Helpers.TimePeriod.TimePeriodHelper.OverlapErrorMessage, "The incorrect error message was returned from validation");
        }

        [TestMethod]
        public void NonOverlappingTimePeriodsShallPassValidation()
        {
            //Setup
            var timePeriods = new List<ITimePeriod>
            {
                new TestSubject{ FirstDay = new DateTime(2000, 1, 1), LastDay = new DateTime(2000, 2, 1)},
                new TestSubject{ FirstDay = new DateTime(2000, 3, 1), LastDay = new DateTime(2000, 4, 1)},
            };

            //Exercise
            var validation = Framework.Helpers.TimePeriod.TimePeriodHelper.ValidateTimePeriods(timePeriods, false);

            //Assert
            Assert.IsNull(validation, "Validation should have passed");
        }

        [TestMethod]
        public void ExceedingYearDurationShallFailValidation()
        {
            //Setup
            var timePeriods = new List<ITimePeriod>
            {
                new TestSubject{ FirstDay = new DateTime(2000, 1, 1), LastDay = new DateTime(2000, 3, 1)},
                new TestSubject{ FirstDay = new DateTime(2001, 2, 1), LastDay = new DateTime(2001, 4, 1)},
            };

            //Exercise
            var validation = Framework.Helpers.TimePeriod.TimePeriodHelper.ValidateTimePeriods(timePeriods, true);

            //Assert
            Assert.IsNotNull(validation, "Validation should have failed");
            Assert.AreEqual(validation.Message, Framework.Helpers.TimePeriod.TimePeriodHelper.YearExceededErrorMessage, "The incorrect error message was returned from validation");
        }

        [TestMethod]
        public void StartingBeforeYearStartShallFailValidation()
        {
            //Setup
            var timePeriods = new List<ITimePeriod>
            {
                new TestSubject{ FirstDay = new DateTime(2000, 1, 1), LastDay = new DateTime(2000, 3, 1)},
                new TestSubject{ FirstDay = new DateTime(2000, 5, 1), LastDay = new DateTime(2000, 6, 1)},
            };

            //Exercise
            var validation = Framework.Helpers.TimePeriod.TimePeriodHelper.ValidateTimePeriods(timePeriods, true, new DateTime(2000,2,1));

            //Assert
            Assert.IsNotNull(validation, "Validation should have failed");
            Assert.AreEqual(validation.Message, Framework.Helpers.TimePeriod.TimePeriodHelper.FirstDayBeforeStartOfYearErrorMessage, "The incorrect error message was returned from validation");
        }

        private class TestSubject : ITimePeriod
        {
            public DateTime FirstDay { get; set; }
            public DateTime LastDay { get; set; }
        }
    }
}
