using JWTRolesTestApp.Models;
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
    public class LoginInfoManager : RepositoryBase<LoginInfo>, ILoginInfoManager
    {
        public LoginInfoManager(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<LoginInfo> GetAllWIthEmployee()
        {
            return RepositoryContext.LoginInfos
                .Include(x => x.Employee)
                .ToList();
        }
    }
}
