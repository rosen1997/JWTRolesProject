using JWTRolesTestApp.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.RepositoryBase
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Context class
        /// </summary>
        protected RepositoryContext RepositoryContext { get; set; }

        /// <summary>
        /// Constructor for 
        /// </summary>
        /// <param name="repositoryContext">Context object</param>
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        /// <summary>
        /// Read whole table content
        /// </summary>
        /// <returns>All entries in table</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return RepositoryContext.Set<T>();
        }

        /// <summary>
        /// Parametrized query
        /// </summary>
        /// <param name="predicate">Condition for searching</param>
        /// <returns>All entries matching search criteria</returns>
        public virtual IEnumerable<T> FindByCondition(Expression<Func<T, bool>> predicate)
        {
            return RepositoryContext.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Parametrized query with No Tracking
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="track"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool track)
        {
            if (track)
            {
                return RepositoryContext.Set<T>().Where(predicate);
            }
            else
            {
                return RepositoryContext.Set<T>().Where(predicate).AsNoTracking();
            }
        }

        /// <summary>
        /// Add one entry to table
        /// </summary>
        /// <param name="entity">Entry to add</param>
        public virtual void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public virtual bool CreateUser(Employee employee, LoginInfo loginInfo)
        {
            using (var transaction = RepositoryContext.Database.BeginTransaction())
            {
                try
                {
                    RepositoryContext.Employees.Add(employee);
                    RepositoryContext.SaveChanges();

                    loginInfo.EmployeeId = employee.Id;
                    RepositoryContext.LoginInfos.Add(loginInfo);
                    RepositoryContext.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch(Exception)
                {
                    transaction.Rollback();
                    return false;
                }

            }
        }

        /// <summary>
        /// Update specific entry based on PK
        /// </summary>
        /// <param name="entity">Entry to update</param>
        public virtual void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        /// <summary>
        /// Delete specific entry based on PK
        /// </summary>
        /// <param name="entity">Entry to delete</param>
        public virtual void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Find if specific entry exist
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <returns>Entry if exists</returns>
        public virtual T Exist(Expression<Func<T, bool>> predicate)
        {
            return RepositoryContext.Set<T>().Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// Find if last specific entry exist
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <param name="keySelector"></param>
        /// <returns>Last entry if exists</returns>
        public virtual T LastExist<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector)
        {
            return RepositoryContext.Set<T>().Where(predicate).OrderByDescending(keySelector).FirstOrDefault();
        }

        /// <summary>
        /// Find if last specific entry exist
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <param name="keySelector"></param>
        /// <param name="track"></param>
        /// <returns>Last entry if exists</returns>
        public virtual T LastExist<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> keySelector, bool track)
        {
            IQueryable<T> query =
                RepositoryContext
                .Set<T>()
                .Where(predicate)
                .OrderByDescending(keySelector);
            if (track)
            {
                query.AsNoTracking();
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Count all entires in table
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return RepositoryContext.Set<T>().Count();
        }

        /// <summary>
        /// Count all entries in table by condition
        /// </summary>
        /// <param name="predicate">Criteria for searching</param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return RepositoryContext.Set<T>().Where(predicate).Count();
        }
    }
}
