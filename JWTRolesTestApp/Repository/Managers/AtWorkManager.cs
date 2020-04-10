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
    public class AtWorkManager : RepositoryBase<AtWork>, IAtWorkManager
    {
        public AtWorkManager(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<AtWork> GetAllWithEmployees()
        {
            return RepositoryContext.AtWork
                .Include(x => x.Employee)
                .Include(x=>x.Employee.Role)
                .ToList();
        }

        public AtWork GetByEmployeeId(int employeeId)
        {
            return GetAllWithEmployees().Where(x => x.EmployeeId == employeeId).FirstOrDefault();
        }
    }
}
