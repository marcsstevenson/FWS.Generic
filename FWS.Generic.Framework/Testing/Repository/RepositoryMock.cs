using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Interfaces.Entity;
using FWS.Generic.Framework.UnitOfWork;

namespace FWS.Generic.Framework.Testing.Repository
{
    public class RepositoryMock : IRepositoryMock, IRepository
    {
        public bool Any()
        {
            return DbSet.Any();
        }

        public bool Any<T>(Func<T, bool> predicate) where T : class
        {
            var typeName = typeof(T).FullName;
            return DbSet.ContainsKey(typeName) && (DbSet[typeName] as IList<T>).Any(predicate);
        }

        public Dictionary<string, object> DbSet { get; private set; } = new Dictionary<string, object>();

        public Dictionary<string, object> DeletionTrail { get; private set; } = new Dictionary<string, object>();

        public RepositoryMock()
        { }

        public void SetData<T>(IList<T> dbSet) where T : class
        {
            DbSet[typeof(T).FullName] = dbSet;
        }

        public IList<T> GetData<T>() where T : class
        {
            return this.DbSet[typeof(T).FullName] as IList<T>;
        }

        public void Initalise(IUnitOfWork unitOfWork) { }

        public IList<T> GetList<T>() where T : class
        {
            if (DbSet.TryGetValue(typeof(T).FullName, out object o))
                return o as IList<T>;

            var list = new List<T>();
            DbSet[typeof(T).FullName] = list;

            return list;
        }

        public IList<T> GetDeleteList<T>() where T : class
        {
            if (DeletionTrail.TryGetValue(typeof(T).FullName, out object o))
                return o as IList<T>;

            var list = new List<T>();
            DeletionTrail[typeof(T).FullName] = list;

            return list;
        }

        /// <summary>
        /// Adds a new entity to the database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add<T>(T entity) where T : class
        {
            GetList<T>().Add(entity);

            var newEntity = entity as IEntity;
            if (newEntity == null)
                return;

            // ensure the dates are set correctly
            newEntity.Created = Clock.Now();
            newEntity.Modified = Clock.Now();

            newEntity.SeedId();
            

        }


        /// <summary>
        /// sets the state of the entity to be modified
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update<T>(T entity) where T : class
        {
            var newEntity = entity as IEntity;
            if (newEntity != null)
                newEntity.Modified = Clock.Now();
        }

        /// <summary>
        /// deletes the entity from the database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete<T>(T entity) where T : class
        {
            GetList<T>().Remove(entity);

            GetDeleteList<T>().Add(entity);
        }
        
        public virtual void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
                this.Add(entity);
        }

        public virtual void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
                this.Delete(entity);
        }

        /// <summary>
        /// deletes entities from the database based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete<T>(Expression<Func<T, Boolean>> predicate) where T : class
        {
            IEnumerable<T> objects = GetList<T>().AsQueryable().Where<T>(predicate).ToList();

            foreach (T obj in objects)
                GetList<T>().Remove(obj);
        }
        
        /// <summary>
        /// Gets a list of all entities for a certain type
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> AllQueryable<T>() where T : class
        {
            return GetList<T>().AsQueryable();
        }

        public IEnumerable<T> AllEnumerable<T>() where T : class
        {
            return GetList<T>().AsQueryable();
        }

        public IList<T> AllList<T>() where T : class
        {
            return GetList<T>().AsQueryable().ToList();
        }

        public int Count<T>() where T : class
        {
            return GetList<T>().Count();
        }

        public int Count<T>(Func<T, bool> predicate) where T : class
        {
            return GetList<T>().Count(predicate);
        }

        public virtual T Max<T>() where T : class
        {
            return GetList<T>().Max<T>();
        }
        
        /// <summary>
        /// Gets a list of entities for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return GetList<T>().AsQueryable().Where(predicate);
        }

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return GetList<T>().AsQueryable().Where(predicate).FirstOrDefault<T>();
        }

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T First<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return GetList<T>().AsQueryable().Where(predicate).First<T>();
        }


        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <returns></returns>
        public virtual T FirstOrDefault<T>() where T : class
        {
            return GetList<T>().AsQueryable().FirstOrDefault<T>();
        }

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <returns></returns>
        public virtual T First<T>() where T : class
        {
            return GetList<T>().AsQueryable().First<T>();
        }

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T Single<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return GetList<T>().AsQueryable().Where(predicate).Single<T>();
        }

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T SingleOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return GetList<T>().AsQueryable().Where(predicate).SingleOrDefault<T>();
        }

        public void SaveChanges()
        {
            //Twiddle thumbs because the in memory list will have already been updated (Me: we could batch the modifications up until this point...)

            return;
        }


        public Task<T> MaxAsync<T>() where T : class
        {
            return Task.Run(() => GetList<T>().Max<T>());
        }

        public Task<bool> AnyAsync<T>() where T : class
        {
            return Task.Run(() => GetList<T>().Any());
        }

        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Task.Run(() => GetList<T>().AsQueryable().Where(predicate).First<T>() != null);
        }

        public Task<IList<T>> AllListAsync<T>() where T : class
        {
            return Task.Run(() => GetList<T>().AsQueryable().ToList() as IList<T>);
        }

        public Task<int> CountAsync<T>() where T : class
        {
            return Task.Run(() => GetList<T>().AsQueryable().Count());
        }

        public Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Task.Run(() => GetList<T>().AsQueryable().Count(predicate));
        }

        public Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Task.Run(() => GetList<T>().AsQueryable().Where(predicate).Single<T>());
        }

        public Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Task.Run(() => GetList<T>().AsQueryable().Where(predicate).SingleOrDefault<T>());
        }

        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Task.Run(() => GetList<T>().AsQueryable().Where(predicate).FirstOrDefault<T>());
        }

        public Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Task.Run(() => GetList<T>().AsQueryable().Where(predicate).First<T>());
        }

        public Task<T> FirstOrDefaultAsync<T>() where T : class
        {
            return Task.Run(() => GetList<T>().AsQueryable().FirstOrDefault());
        }

        public Task<T> FirstAsync<T>() where T : class
        {
            return Task.Run(() => GetData<T>().AsQueryable().First());
        }
    }
}