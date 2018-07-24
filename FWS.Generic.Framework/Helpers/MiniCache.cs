using System;
using System.Collections.Generic;
using System.Linq;
using FWS.Generic.Framework.Interfaces.Entity;

namespace FWS.Generic.Framework.Helpers
{
    public class MiniCache<T> : List<T> where T : class, IEntity
    {
        private readonly IEntityRepository _repository;

        public MiniCache(IEntityRepository repository)
        {
            _repository = repository;
        }

        public MiniCache(IEntityRepository repository, IEnumerable<T> existingList) : this(repository)
        {
            this.AddRange(existingList);
        }

        public T Get(Guid id)
        {
            //Do we have this id already?
            var match = this.FirstOrDefault(i => i.Id == id);

            //Return the match if already cached
            if (match != null) return match;

            //Get from the repository
            match = this._repository.FirstOrDefault<T>(i => i.Id == id);

            //Return the bad news if needed
            if (match == null) return null;

            //Least we forget
            this.Add(match);

            return match;
        }
    }
}