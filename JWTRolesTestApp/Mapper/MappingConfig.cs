using AutoMapper;
using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;


namespace JWTRolesTestApp.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            EmployeeMap();
            RoleMap();
            LoginInfoMap();
        }

        private void EmployeeMap()
        {
            CreateMap<Employee, EmployeeModel>()
                ;
        }

        private void RoleMap()
        {
            CreateMap<Role, RoleModel>()
                ;
        }

        private void LoginInfoMap()
        {
            CreateMap<LoginInfo, LoginModel>()
                ;
        }
    }
}
