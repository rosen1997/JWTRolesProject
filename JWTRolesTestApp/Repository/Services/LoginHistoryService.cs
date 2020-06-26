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
    public class LoginHistoryService : ILoginHistoryRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoginHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<LoginHistoryModel> GetAllRows()
        {
            return mapper.Map<IEnumerable<LoginHistoryModel>>(unitOfWork.LoginHistoryManager.GetAllWithEmployees());
        }

        public IEnumerable<LoginHistoryModel> GetAllRowsByEmployeeId(int employeeId)
        {
            return mapper.Map<IEnumerable<LoginHistoryModel>>(unitOfWork.LoginHistoryManager.GetAllByEmployeeId(employeeId));
        }

        public string LogoutFromWork(AtWork atWork)
        {
            LoginHistory loginHistory = new LoginHistory() { LoginTime = atWork.LoginTime, LogoutTime = DateTime.Now, EmployeeId = atWork.EmployeeId };

            try
            {
                unitOfWork.BeginTransaction();

                unitOfWork.AtWorkManager.Delete(atWork);
                unitOfWork.SaveChanges();

                unitOfWork.LoginHistoryManager.Create(loginHistory);
                unitOfWork.SaveChanges();

                unitOfWork.CommitTransaction();

                return "Logout successful.";
            }
            catch(Exception e)
            {
                unitOfWork.RollbackTransaction();
                return e.Message;
            }
        }
    }
}
