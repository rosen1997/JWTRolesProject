using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.Managers.Interfaces;
using JWTRolesTestApp.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Managers
{
    public class RoleManager : RepositoryBase<Role> , IRoleManager
    {
        public RoleManager(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
