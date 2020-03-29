using JWTRolesTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Services.Interfaces
{
    public interface IRoleRepository
    {
        public IEnumerable<RoleModel> GetAllRoles();

        public void AddRole(string RoleDescription);
    }
}
