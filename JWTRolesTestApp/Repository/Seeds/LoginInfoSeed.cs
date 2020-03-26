using JWTRolesTestApp.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Seeds
{
    public class LoginInfoSeed
    {
        public static LoginInfo[] Seed()
        {
            return new LoginInfo[]
            {
                new LoginInfo{Id=1, Username="rosen", Password="rosen", EmployeeId=1 },
                new LoginInfo{Id=2, Username="neli", Password="neli", EmployeeId=2 },
            };
        }
    }
}
