using FWS.Generic.Framework.Interfaces.Entity;

namespace FWS.Generic.Framework.Interfaces
{
    public interface IGenericDto<E> : IGuidNullableId, ITracksTimeNullable where E : IGuidId, ITracksTime
    {
        E ToEntity();
        void UpdateEntity(E entity);
    }
}