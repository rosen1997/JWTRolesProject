using JWTRolesTestApp.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Seeds
{
    public class EmployeeSeed
    {
        public static Employee[] Seed()
        {
            return new Employee[]
            {
                new Employee{Id=1, FirstName="Rosen",MiddleName="Yavorov", LastName="Lechev", RoleId=1},
                new Employee { Id = 2, FirstName = "Neli", MiddleName = "Lychezarova", LastName = "Zarkova", RoleId=2},
            };
        }
    }
}
