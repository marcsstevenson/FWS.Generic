using FWS.Generic.Framework.Interfaces.Entity;

namespace FWS.Generic.Framework.AbstractClasses
{
    public abstract class RailedName : Entity, IRailedName
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        protected RailedName() : base()
        {
            
        }
    }
}
