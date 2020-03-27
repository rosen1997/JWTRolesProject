using JWTRolesTestApp.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.RepositoryBase
{
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Read whole table content
        /// </summary>
        /// <returns>All entries in table</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Parametrized query
        /// </summary>
        /// <param name="predicate">Condition for searching</param>
        /// <returns>All entries matching search criteria</returns>
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Parametrized query with No Tracking
        /// </summary>
        /// <param name="predicate">Condition for searching</param>
        /// <param name="track"></param>
        /// <returns>All entries matching search criteria</returns>
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool track);

        /// <summary>
        /// Add one entry to table
        /// </summary>
        /// <param name="entity">Entry to add</param>
        void Create(T entity);

        bool CreateUser(Employee employee, LoginInfo loginInfo);

        /// <summary>
        /// Update specific entry based on PK
        /// </summary>
        /// <param name="entity">Entry to update</param>
        void Update(T entity);

        /// <summary>
        /// Delete specific entry based on PK
        /// </summary>
        /// <param name="entity">Entry to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Find if specific entry exist
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <returns>Entry if exists</returns>
        T Exist(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find if last specific entry exist
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <param name="keySelector"></param>
        /// <returns>Last entry if exists</returns>
        T LastExist<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector);


        /// <summary>
        /// Find if last specific entry exist
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <param name="keySelector"></param>
        /// <param name="track"></param>
        /// <returns>Last entry if exists</returns>
        T LastExist<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool track);

        /// <summary>
        /// Count all entries
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Count all entries by condition
        /// </summary>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);
    }
}
