using AutoMapper;
using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.RepositoryBase;
using JWTRolesTestApp.Repository.Services.Interfaces;
using JWTRolesTestApp.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Services
{
    public class LoginInfoService : ILoginInfoRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoginInfoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public LoginModel GetByUserId(int id)
        {
            LoginInfo loginInfo = unitOfWork.LoginInfoManager.GetAll().SingleOrDefault(x => x.EmployeeId == id);

            LoginModel loginModel = mapper.Map<LoginModel>(loginInfo);

            return loginModel;
        }

        public LoginInfo GetEntityByUserId(int id)
        {
            return unitOfWork.LoginInfoManager.GetAll().FirstOrDefault(x => x.EmployeeId == id);
        }
    }
}
