using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Managers.Interfaces
{
    public interface ILoginHistoryManager : IRepositoryBase<LoginHistory>
    {
        public IEnumerable<LoginHistory> GetAllWithEmployees();

        public IEnumerable<LoginHistory> GetAllByEmployeeId(int employeeId);
    }
}
