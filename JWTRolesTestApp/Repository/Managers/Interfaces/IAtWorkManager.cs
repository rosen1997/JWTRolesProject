using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Managers.Interfaces
{
    public interface IAtWorkManager : IRepositoryBase<AtWork>
    {
        public IEnumerable<AtWork> GetAllWithEmployees();
        public AtWork GetByEmployeeId(int employeeId);
    }
}
