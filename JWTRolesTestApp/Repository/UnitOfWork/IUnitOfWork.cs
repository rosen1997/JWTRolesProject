using JWTRolesTestApp.Repository.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IEmployeeManager EmployeeManager { get; }
        ILoginInfoManager LoginInfoManager { get; }
        IRoleManager RoleManager { get; }
        IAtWorkManager AtWorkManager { get; }

        ILoginHistoryManager LoginHistoryManager { get; }

        void SaveChanges();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
