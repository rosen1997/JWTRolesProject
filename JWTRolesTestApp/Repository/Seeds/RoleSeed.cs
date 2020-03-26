using JWTRolesTestApp.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Seeds
{
    public class RoleSeed
    {
        public static Role[] Seed()
        {
            return new Role[]
            {
                new Role{Id=1, RoleDescription="Admin"},
                new Role{Id=2, RoleDescription="User"},
            };
        }
    }
}
