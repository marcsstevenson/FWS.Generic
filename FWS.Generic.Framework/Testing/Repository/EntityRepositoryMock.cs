using System;
using System.Collections.Generic;
using FWS.Generic.Framework.Enumerations;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Interfaces.Entity;

namespace FWS.Generic.Framework.Testing.Repository
{
    public class EntityRepositoryMock : RepositoryMock, IEntityRepository //IDisposable
    {
        public EntityRepositoryMock()
            : base()
        {
        }

        /// <summary>
        /// Saves the the state of the entity to be modified or created
        /// </summary>
        /// <param name="entity"></param>
        public virtual CommitAction Save<T>(T entity) where T : class, IEntity
        {
            if (entity.Id != Guid.Empty && this.Exists(entity))
            {
                this.Update(entity);
                return CommitAction.Update;
            }
            else
            {
                if (entity.Id == Guid.Empty)
                    entity.Id = GuidComb.Generate();

                this.Add(entity);
                return CommitAction.Add;
            }
        }

        public void Delete<T>(Guid id) where T : class, IEntity
        {
            this.Delete<T>(i => i.Id == id);
        }

        public void Delete<T>(IEntity guidEntity) where T : class, IEntity
        {
            this.Delete<T>(i => i.Id == guidEntity.Id);
        }

        public virtual bool Exists<T>(T entity) where T : class, IEntity
        {
            if (entity == null)
                return false;

            return this.FirstOrDefault<T>(i => i.Id == entity.Id) != null;
        }
    }
}
