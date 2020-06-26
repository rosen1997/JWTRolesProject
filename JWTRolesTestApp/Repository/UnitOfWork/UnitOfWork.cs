using JWTRolesTestApp.Repository.Managers;
using JWTRolesTestApp.Repository.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace JWTRolesTestApp.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext repositoryContext;

        public UnitOfWork(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            EmployeeManager = new EmployeeManager(repositoryContext);
            LoginInfoManager = new LoginInfoManager(repositoryContext);
            RoleManager = new RoleManager(repositoryContext);
            AtWorkManager = new AtWorkManager(repositoryContext);
            LoginHistoryManager = new LoginHistoryManager(repositoryContext);
        }

        public IEmployeeManager EmployeeManager { get; }

        public ILoginInfoManager LoginInfoManager { get; }

        public IRoleManager RoleManager { get; }

        public IAtWorkManager AtWorkManager { get; }

        public ILoginHistoryManager LoginHistoryManager { get; }

        public void SaveChanges()
        {
            repositoryContext.SaveChanges();
        }

        public void BeginTransaction()
        {
            repositoryContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            repositoryContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            repositoryContext.Database.RollbackTransaction();
        }
    }
}
