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
    public class EmployeeManager : RepositoryBase<Employee>, IEmployeeManager
    {
        public EmployeeManager(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Employee> GetAllWithRoles()
        {
            return RepositoryContext.Employees
                .Include(x => x.Role)
                .ToList();
        }
    }
}
