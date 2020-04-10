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
    public class AtWorkService : IAtWorkRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AtWorkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<AtWorkModel> GetAllRows()
        {
            IEnumerable<AtWorkModel> atWorkModels = mapper.Map<IEnumerable<AtWorkModel>>(unitOfWork.AtWorkManager.GetAllWithEmployees());

            return atWorkModels;
        }

        public AtWorkModel GetByEmployeeId(int employeeId)
        {
            AtWorkModel atWorkModel = mapper.Map<AtWorkModel>(unitOfWork.AtWorkManager.GetByEmployeeId(employeeId));

            return atWorkModel;
        }

        public void LeaveWork()
        {
            throw new NotImplementedException();
        }

        public void LoginAtWork(int employeeId)
        {
            AtWork atWork = new AtWork() { LoginTime = DateTime.Now, EmployeeId = employeeId };
            unitOfWork.AtWorkManager.Create(atWork);
        }
    }
}
