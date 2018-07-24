using System;
using FWS.Generic.Framework.Caching.Providers;
using FWS.Generic.Framework.Interfaces;
using FWS.Generic.Framework.Interfaces.Entity;
using FWS.Generic.Framework.UnitOfWork;

namespace FWS.Generic.Framework.Testing.Repository
{
    public class PersistanceFactoryMock : IPersistanceFactory
    {
        public IRepository BuildRepository()
        {
            return new RepositoryMock();
        }

        public IEntityRepository BuildEntityRepository()
        {
            return new EntityRepositoryMock();
        }
        
        public IUnitOfWork BuildUnitOfWork()
        {
            return new UnitOfWorkMock();
        }

        /// <summary>
        /// This allows test classes to customise what cache provider to use
        /// </summary>
        public Func<ICacheProvider> CacheProviderSeeder { get; set; }

        public ICacheProvider GetCacheProvider()
        {
            return this.CacheProviderSeeder == null ? new NoCacheProvider() : this.CacheProviderSeeder();
        }

        public void Dispose()
        {
            //Twiddle thumbs
        }
    }
}
