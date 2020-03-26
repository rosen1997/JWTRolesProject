using AutoMapper;
using JWTRolesTestApp.Helpers;
using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.Services.Interfaces;
using JWTRolesTestApp.Repository.UnitOfWork;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Services
{
    public class EmployeeService : IEmployeeRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppSettings appSettings;

        public EmployeeService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        public EmployeeModel Authenticate(string username, string password)
        {
            LoginModel login = GetLoginInfo(username, password);
            if (login == null)
                return null;

            EmployeeModel employee = GetById(login.Employee.Id);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, employee.Id.ToString()),
                    new Claim(ClaimTypes.Role, employee.Role.RoleDescription)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            employee.Token = tokenHandler.WriteToken(token);

            EmployeeModel employeeModel = mapper.Map<EmployeeModel>(employee);

            return employeeModel;
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            IEnumerable<Employee> employees = unitOfWork.EmployeeManager.GetAllWithRoles();

            IEnumerable<EmployeeModel> employeeModels = mapper.Map<IEnumerable<EmployeeModel>>(employees);

            return employeeModels;
        }

        public EmployeeModel GetById(int id)
        {
            IEnumerable<EmployeeModel> employees = GetAllEmployees();

            EmployeeModel employee = employees.FirstOrDefault(x => x.Id == id);

            return employee;
        }

        public LoginModel GetLoginInfo(string username, string password)
        {
            LoginInfo login = unitOfWork.LoginInfoManager.GetAllWIthEmployee().SingleOrDefault(x => x.Username == username && x.Password == password);
            if (login == null)
                return null;

            LoginModel loginModel = mapper.Map<LoginModel>(login);

            return loginModel;
        }
    }
}
