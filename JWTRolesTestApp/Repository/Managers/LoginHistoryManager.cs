using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.Managers.Interfaces;
using JWTRolesTestApp.Repository.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Managers
{
    public class LoginHistoryManager : RepositoryBase<LoginHistory>, ILoginHistoryManager
    {
        public LoginHistoryManager(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<LoginHistory> GetAllByEmployeeId(int employeeId)
        {
            return RepositoryContext.LoginHistory
                .Include(x => x.Employee)
                .Include(x => x.Employee.Role)
                .Where(x => x.EmployeeId == employeeId)
                .ToList();
        }

        public IEnumerable<LoginHistory> GetAllWithEmployees()
        {
            return RepositoryContext.LoginHistory
                .Include(x => x.Employee)
                .Include(x => x.Employee.Role)
                .ToList();
        }
    }
}
