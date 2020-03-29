using AutoMapper;
using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.Services.Interfaces;
using JWTRolesTestApp.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Services
{
    public class RoleService : IRoleRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void AddRole(string RoleDescription)
        {
            Role role = new Role() { RoleDescription = RoleDescription };
            unitOfWork.RoleManager.Create(role);
        }

        public IEnumerable<RoleModel> GetAllRoles()
        {
            return mapper.Map<IEnumerable<RoleModel>>(unitOfWork.RoleManager.GetAll());
        }
    }
}
