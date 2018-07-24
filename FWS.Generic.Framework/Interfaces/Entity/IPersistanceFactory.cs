using FWS.Generic.Framework.UnitOfWork;

namespace FWS.Generic.Framework.Interfaces.Entity
{
    public interface IPersistanceFactory// : IDisposable
    {
        IEntityRepository BuildEntityRepository();
        IUnitOfWork BuildUnitOfWork();
        ICacheProvider GetCacheProvider();
    }
}
