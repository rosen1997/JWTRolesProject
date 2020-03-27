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
            CreateEmployeeMap();
            CreateEmployeeLoginMap();
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

        private void CreateEmployeeMap()
        {
            CreateMap<CreateEmployeeModel, Employee>()
                ;
        }

        private void CreateEmployeeLoginMap()
        {
            CreateMap<CreateEmployeeModel, LoginInfo>()
                ;
        }
    }
}
