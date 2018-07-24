using System;

namespace FWS.Generic.Framework.Interfaces
{
    public interface IDateRangeNullable
    {
        DateTime? StartDate { get; set; }
        
        DateTime? EndDate { get; set; }
    }
}
