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
            int? userId = GetLoginInfoUserId(username, password);
            if (userId == null)
                return null;

            EmployeeModel employee = GetById(userId.GetValueOrDefault());

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, employee.Id.ToString()),
                    new Claim(ClaimTypes.Role, employee.Role.RoleDescription)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
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

        public int? GetLoginInfoUserId(string username, string password)
        {
            LoginInfo login = unitOfWork.LoginInfoManager.GetAllWIthEmployee().SingleOrDefault(x => x.Username == username);
            if (login == null)
                return null;

            if (!VerifyPasswordHash(password, login.PasswordHash, login.PasswordSalt))
                return null;

            return login.EmployeeId;
        }

        public EmployeeModel CreateEmployee(CreateEmployeeModel createEmployeeModel)
        {
            if (unitOfWork.LoginInfoManager.GetAllWIthEmployee().SingleOrDefault(x => x.Username == createEmployeeModel.Username) != null)
                return null;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(createEmployeeModel.Password, out passwordHash, out passwordSalt);

            Employee employee = mapper.Map<Employee>(createEmployeeModel);
            int roleId = unitOfWork.RoleManager.GetAll().Where(x => x.RoleDescription == createEmployeeModel.RoleDescription).FirstOrDefault().Id;
            employee.RoleId = roleId;
            LoginInfo loginInfo = mapper.Map<LoginInfo>(createEmployeeModel);
            loginInfo.PasswordSalt = passwordSalt;
            loginInfo.PasswordHash = passwordHash;

            if (!unitOfWork.EmployeeManager.CreateUser(employee, loginInfo))
            {
                return null;
            }

            EmployeeModel employeeModel = mapper.Map<EmployeeModel>(employee);

            return employeeModel;


        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
