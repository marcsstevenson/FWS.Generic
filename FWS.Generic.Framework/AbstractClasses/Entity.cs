using System;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Interfaces;
using FWS.Generic.Framework.Interfaces.Entity;

namespace FWS.Generic.Framework.AbstractClasses
{
    public abstract class Entity : IEntity
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Guid Id { get; set; }

        public bool Equals(IGuidId id)
        {
            return this.Id == id.Id;
        }

        protected Entity()
        {
            this.SetForCreated();
        }
    }
}
