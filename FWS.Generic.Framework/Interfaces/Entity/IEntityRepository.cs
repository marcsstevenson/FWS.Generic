using System;
using FWS.Generic.Framework.Enumerations;

namespace FWS.Generic.Framework.Interfaces.Entity
{
    public interface IEntityRepository : IRepository
    {
        bool Exists<T>(T entity) where T : class, IEntity;
        CommitAction Save<T>(T entity) where T : class, IEntity;
        void Delete<T>(Guid id) where T : class, IEntity;
        void Delete<T>(IEntity guidEntity) where T : class, IEntity;
    }
}
