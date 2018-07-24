using System;

namespace FWS.Generic.Framework.Interfaces
{
    public interface ITracksTimeNullable
    {
        DateTime? Created { get; set; }

        DateTime? Modified { get; set; }
    }
}