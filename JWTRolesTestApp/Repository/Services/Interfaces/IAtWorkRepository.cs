using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Services.Interfaces
{
    public interface IAtWorkRepository
    {
        public AtWorkModel GetByEmployeeId(int employeeId);

        public AtWork GetEntityByEmployeeId(int employeeId);

        public IEnumerable<AtWorkModel> GetAllRows();

        public void LoginAtWork(int employeeId);
    }
}
