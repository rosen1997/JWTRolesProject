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

        T FindById(int id);
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

        void UpdateUser(Employee employee, LoginInfo loginInfo);

        /// <summary>
        /// Delete specific entry based on PK
        /// </summary>
        /// <param name="entity">Entry to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Count all entries
        /// </summary>
        /// <returns></returns>
        int Count();
    }
}
