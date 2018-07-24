using System;
using System.Collections.Generic;

namespace FWS.Generic.Framework.Interfaces.GuidIdTree
{
    public interface IGuidTreeItem : IGuidId
    {
        IGuidTreeItem Parent { get; set; }
        ICollection<IGuidTreeItem> Children { get; set; }
        string Ancestry { get; set; }
        //bool PendingDeletion { get; set; }
    }
}