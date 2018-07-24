using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FWS.Generic.Framework.Interfaces.Entity
{
    public interface IRepository
    {
        /// <summary>
        /// Returns true if the repository contains any for the predicate
        /// </summary>
        bool Any<T>(Func<T, bool> predicate) where T : class;

        /// <summary>
        /// Gets a IIQueryable list of entities for a certain type
        /// </summary>
        /// <returns></returns>
        IQueryable<T> AllQueryable<T>() where T : class;

        /// <summary>
        /// Gets a IIQueryable list of entities for a certain type
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> AllEnumerable<T>() where T : class;

        /// <summary>
        /// Gets a IIQueryable list of entities for a certain type
        /// </summary>
        /// <returns></returns>
        IList<T> AllList<T>() where T : class;

        /// <summary>
        /// Returns the count for a given predicate
        /// </summary>
        /// <param name="predicate">A function predicate</param>
        /// <returns></returns>
        int Count<T>(Func<T, bool> predicate) where T : class;

        /// <summary>
        /// Gets a list of entities for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        
        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Single<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T SingleOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T First<T>(Expression<Func<T, bool>> predicate) where T : class;

        T FirstOrDefault<T>() where T : class;

        T First<T>() where T : class;

        T Max<T>() where T : class;

        /// <summary>
        /// Adds a new entity to the database
        /// </summary>
        /// <param name="entity"></param>
        void Add<T>(T entity) where T : class;

        /// <summary>
        /// Adds a list of new entity to the database
        /// </summary>
        /// <param name="entities"></param>
        void AddRange<T>(IEnumerable<T> entities) where T : class;

        /// <summary>
        /// sets the state of the entity to be modified
        /// </summary>
        /// <param name="entity"></param>
        void Update<T>(T entity) where T : class;

        /// <summary>
        /// deletes the entity from the database
        /// </summary>
        /// <param name="entity"></param>
        void Delete<T>(T entity) where T : class;

        /// <summary>
        /// deletes the entity from the database
        /// </summary>
        /// <param name="entities"></param>
        void DeleteRange<T>(IEnumerable<T> entities) where T : class;

        /// <summary>
        /// deletes entities from the database based on a filter
        /// </summary>
        void Delete<T>(Expression<Func<T, Boolean>> predicate) where T : class;

        /// <summary>
        /// Returns true if the repository contains any for the predicate
        /// </summary>
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets a IIQueryable list of entities for a certain type
        /// </summary>
        /// <returns></returns>
        Task<IList<T>> AllListAsync<T>() where T : class;

        /// <summary>
        /// Returns the count
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync<T>() where T : class;

        /// <summary>
        /// Returns the count for a given predicate
        /// </summary>
        /// <param name="predicate">A function predicate</param>
        /// <returns></returns>
        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// Gets a single entity for a certain type based on a filter
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<T> FirstOrDefaultAsync<T>() where T : class;

        Task<T> FirstAsync<T>() where T : class;

        Task<T> MaxAsync<T>() where T : class;

    }
}
