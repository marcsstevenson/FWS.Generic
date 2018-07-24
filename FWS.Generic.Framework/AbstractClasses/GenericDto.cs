using AutoMapper;
using FWS.Generic.Framework.Interfaces;
using FWS.Generic.Framework.Interfaces.Entity;
using System;

namespace FWS.Generic.Framework.AbstractClasses
{
    public abstract class GenericDto<TEntity> : IGenericDto<TEntity> where TEntity : IEntity
    {
        public Guid? Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        protected GenericDto()
        { }

        protected GenericDto(TEntity entity)
        {
            Mapper.Map(entity, this);
        }

        public TEntity ToEntity()
        {
            return Mapper.Map<TEntity>(this);
        }

        public void UpdateEntity(TEntity entity)
        {
            Mapper.Map(this, entity);
        }
    }
}
