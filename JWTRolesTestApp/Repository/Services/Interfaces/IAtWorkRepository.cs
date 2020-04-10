using JWTRolesTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Services.Interfaces
{
    public interface IAtWorkRepository
    {
        public AtWorkModel GetByEmployeeId(int employeeId);

        public IEnumerable<AtWorkModel> GetAllRows();

        public void LoginAtWork(int employeeId);

        public void LeaveWork();
    }
}
