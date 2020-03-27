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
                new LoginInfo{Id=1, Username="rosen", PasswordHash=new byte[]{ }, PasswordSalt= new byte[]{ },EmployeeId=1 },
                new LoginInfo{Id=2, Username="neli", PasswordHash=new byte[]{ }, PasswordSalt= new byte[]{ }, EmployeeId=2 },
            };
        }
    }
}
