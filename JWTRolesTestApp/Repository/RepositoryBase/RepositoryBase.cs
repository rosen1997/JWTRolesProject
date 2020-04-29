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

        public virtual T FindById(int id)
        {
            return RepositoryContext.Set<T>().Find(id);
        }

        /// <summary>
        /// Add one entry to table
        /// </summary>
        /// <param name="entity">Entry to add</param>
        public virtual void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
            RepositoryContext.SaveChanges();
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
                catch (Exception)
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
            RepositoryContext.SaveChanges();
        }

        public virtual void UpdateUser(Employee employee, LoginInfo loginInfo)
        {
            if (employee != null && loginInfo != null)
            {
                using (var transaction = RepositoryContext.Database.BeginTransaction())
                {
                    try
                    {
                        RepositoryContext.Employees.Update(employee);
                        RepositoryContext.SaveChanges();

                        RepositoryContext.LoginInfos.Update(loginInfo);
                        RepositoryContext.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            else if (employee != null)
            {
                RepositoryContext.Employees.Update(employee);
                RepositoryContext.SaveChanges();
            }
            else if (loginInfo != null)
            {
                RepositoryContext.LoginInfos.Update(loginInfo);
                RepositoryContext.SaveChanges();
            }
        }

        /// <summary>
        /// Delete specific entry based on PK
        /// </summary>
        /// <param name="entity">Entry to delete</param>
        public virtual void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
            RepositoryContext.SaveChanges();
        }

        /// <summary>
        /// Count all entires in table
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return RepositoryContext.Set<T>().Count();
        }

        public virtual void LogoutUser(AtWork atWork, LoginHistory loginHistory)
        {
            using (var transaction = RepositoryContext.Database.BeginTransaction())
            {
                try
                {
                    RepositoryContext.AtWork.Remove(atWork);
                    RepositoryContext.SaveChanges();

                    RepositoryContext.LoginHistory.Add(loginHistory);
                    RepositoryContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }

            }
        }
    }
}
