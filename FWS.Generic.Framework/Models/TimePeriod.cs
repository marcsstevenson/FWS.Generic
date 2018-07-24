using System;
using FWS.Generic.Framework.Interfaces;

namespace FWS.Generic.Framework.Models
{
    public class TimePeriod : ITimePeriod
    {
        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }
    }
}