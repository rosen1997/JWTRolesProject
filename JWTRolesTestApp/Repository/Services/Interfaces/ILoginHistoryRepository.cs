using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Services.Interfaces
{
    public interface ILoginHistoryRepository
    {
        public IEnumerable<LoginHistoryModel> GetAllRows();

        public IEnumerable<LoginHistoryModel> GetAllRowsByEmployeeId(int employeeId);

        public string LogoutFromWork(AtWork atWorkModel);
    }
}
