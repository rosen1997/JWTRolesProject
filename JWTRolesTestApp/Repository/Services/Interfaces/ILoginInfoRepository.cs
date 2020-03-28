using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Services.Interfaces
{
    public interface ILoginInfoRepository
    {
        LoginModel GetByUserId(int id);

        LoginInfo GetEntityByUserId(int id);
    }
}
