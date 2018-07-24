using System;
using FWS.Generic.Framework.Interfaces;

namespace FWS.Generic.Framework.Helpers.TimePeriod
{
    public class TimePeriodWeek : ITimePeriodWeek
    {
        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }
        public bool IsCurrent { get; set; }
        public int WeekNumber { get; set; }

        public override string ToString()
        {
            return this.WeekNumber + ". " + this.FirstDay.Date + " - " + this.LastDay;
        }
    }
}