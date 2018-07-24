using System;

namespace FWS.Generic.Framework.Interfaces
{
    public interface ITracksTime
    {
        DateTime Created { get; set; }

        DateTime Modified { get; set; }
    }
}