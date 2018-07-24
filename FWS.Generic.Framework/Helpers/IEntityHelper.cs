using System;
using FWS.Generic.Framework.Interfaces.Entity;

namespace FWS.Generic.Framework.Helpers
{
    public static class IEntityHelper
    {
        public static void SeedId(this IEntity entity)
        {
            entity.Id = GuidComb.Generate();
        }

        public static bool IsSameId(this IEntity entity, Guid? id)
        {
            if (entity == null)
            {
                return !id.HasValue;
            }
            else
            {
                return entity.Id == id;
            }
        }
    }
}
