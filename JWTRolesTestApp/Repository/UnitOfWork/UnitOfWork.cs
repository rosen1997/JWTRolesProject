using JWTRolesTestApp.Repository.Managers;
using JWTRolesTestApp.Repository.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }

        public IEmployeeManager EmployeeManager { get; }

        public ILoginInfoManager LoginInfoManager { get; }

        public IRoleManager RoleManager { get; }

        public IAtWorkManager AtWorkManager { get; }
    }
}
