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
        private readonly ILoginInfoRepository loginInfoRepository;
        private readonly AppSettings appSettings;

        public EmployeeService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings, IMapper mapper, ILoginInfoRepository loginInfoRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.loginInfoRepository = loginInfoRepository;
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

            return employee;
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            IEnumerable<Employee> employees = unitOfWork.EmployeeManager.GetAllWithRoles();

            IEnumerable<EmployeeModel> employeeModels = mapper.Map<IEnumerable<EmployeeModel>>(employees);

            return employeeModels;
        }

        public EmployeeModel GetById(int id)
        {
            EmployeeModel employee = mapper.Map<EmployeeModel>(unitOfWork.EmployeeManager.GetByIdWithRole(id));

            return employee;
        }

        private Employee GetEntityById(int id)
        {
            return unitOfWork.EmployeeManager.FindById(id);
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

        //public EmployeeModel CreateEmployee(CreateEmployeeModel createEmployeeModel)
        //{
        //    if (unitOfWork.LoginInfoManager.GetAllWIthEmployee().SingleOrDefault(x => x.Username == createEmployeeModel.Username) != null)
        //        return null;

        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(createEmployeeModel.Password, out passwordHash, out passwordSalt);

        //    Employee employee = mapper.Map<Employee>(createEmployeeModel);
        //    int roleId = unitOfWork.RoleManager.GetAll().Where(x => x.RoleDescription == createEmployeeModel.RoleDescription).FirstOrDefault().Id;
        //    employee.RoleId = roleId;
        //    LoginInfo loginInfo = mapper.Map<LoginInfo>(createEmployeeModel);
        //    loginInfo.PasswordSalt = passwordSalt;
        //    loginInfo.PasswordHash = passwordHash;

        //    if (!unitOfWork.EmployeeManager.CreateUser(employee, loginInfo))
        //    {
        //        return null;
        //    }

        //    EmployeeModel employeeModel = mapper.Map<EmployeeModel>(employee);

        //    return employeeModel;

        //}

        public EmployeeModel CreateEmployee(CreateEmployeeModel createEmployeeModel, ref string message)
        {
            if (unitOfWork.LoginInfoManager.GetAllWIthEmployee().SingleOrDefault(x => x.Username == createEmployeeModel.Username) != null)
            {
                message = "Username already exists!";
                return null;
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(createEmployeeModel.Password, out passwordHash, out passwordSalt);

            Employee employee = mapper.Map<Employee>(createEmployeeModel);
            int roleId = unitOfWork.RoleManager.GetAll().Where(x => x.RoleDescription == createEmployeeModel.RoleDescription).FirstOrDefault().Id;
            employee.RoleId = roleId;
            LoginInfo loginInfo = mapper.Map<LoginInfo>(createEmployeeModel);
            loginInfo.PasswordSalt = passwordSalt;
            loginInfo.PasswordHash = passwordHash;

            try
            {
                unitOfWork.BeginTransaction();

                unitOfWork.EmployeeManager.Create(employee);
                unitOfWork.SaveChanges();

                loginInfo.EmployeeId = employee.Id;
                unitOfWork.LoginInfoManager.Create(loginInfo);
                unitOfWork.SaveChanges();

                unitOfWork.CommitTransaction();
            }
            catch
            {
                unitOfWork.RollbackTransaction();
                return null;
            }

            EmployeeModel employeeModel = mapper.Map<EmployeeModel>(employee);

            return employeeModel;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
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


        public void DeleteEmployee(int id)
        {
            Employee employee = GetEntityById(id);
            if (employee != null)
            {
                unitOfWork.EmployeeManager.Delete(employee);
                unitOfWork.SaveChanges();
            }

        }

        //public void UpdateEmployee(UpdateEmployeeModel employeeModel)
        //{
        //    bool updateUser = false;
        //    bool updateLoginInfo = false;

        //    Employee employeeDB = GetEntityById(employeeModel.EmployeeId);
        //    LoginInfo loginDB = loginInfoRepository.GetEntityByUserId(employeeModel.EmployeeId);

        //    if (employeeDB == null || loginDB == null)
        //    {
        //        return;
        //    }

        //    int roleId = -1;
        //    if (employeeModel.RoleDescription != null)
        //    {
        //        roleId = unitOfWork.RoleManager.GetAll().Where(x => x.RoleDescription == employeeModel.RoleDescription).FirstOrDefault().Id;
        //    }

        //    if ((!string.IsNullOrWhiteSpace(employeeModel.FirstName) && employeeModel.FirstName != employeeDB.FirstName)
        //        || (!string.IsNullOrWhiteSpace(employeeModel.MiddleName) && employeeModel.MiddleName != employeeDB.MiddleName)
        //        || (!string.IsNullOrWhiteSpace(employeeModel.LastName) && employeeModel.LastName != employeeDB.LastName)
        //        || (roleId != -1 && employeeDB.RoleId != roleId))
        //    {
        //        employeeDB.FirstName = employeeModel.FirstName;
        //        employeeDB.MiddleName = employeeModel.MiddleName;
        //        employeeDB.LastName = employeeModel.LastName;

        //        employeeDB.RoleId = roleId;
        //        updateUser = true;
        //    }
        //    if (!string.IsNullOrWhiteSpace(employeeModel.Password) && !VerifyPasswordHash(employeeModel.Password, loginDB.PasswordHash, loginDB.PasswordSalt))
        //    {
        //        byte[] passwordHash, passwordSalt;
        //        CreatePasswordHash(employeeModel.Password, out passwordHash, out passwordSalt);

        //        loginDB.PasswordSalt = passwordSalt;
        //        loginDB.PasswordHash = passwordHash;

        //        updateLoginInfo = true;
        //    }

        //    if (updateUser && updateLoginInfo)
        //        unitOfWork.EmployeeManager.UpdateUser(employeeDB, loginDB);
        //    else if (updateUser)
        //        unitOfWork.EmployeeManager.UpdateUser(employeeDB, null);
        //    else if (updateLoginInfo)
        //        unitOfWork.EmployeeManager.UpdateUser(null, loginDB);
        //}

        public string UpdateEmployee(UpdateEmployeeModel employeeModel)
        {
            bool updateUser = false;
            bool updateLoginInfo = false;

            Employee employeeDB = GetEntityById(employeeModel.EmployeeId);
            LoginInfo loginDB = loginInfoRepository.GetEntityByUserId(employeeModel.EmployeeId);

            if (employeeDB == null || loginDB == null)
            {
                return "Update failed! Could not find employee.";
            }

            int roleId = -1;
            if (employeeModel.RoleDescription != null)
            {
                roleId = unitOfWork.RoleManager.GetAll().Where(x => x.RoleDescription == employeeModel.RoleDescription).FirstOrDefault().Id;
            }

            if ((!string.IsNullOrWhiteSpace(employeeModel.FirstName) && employeeModel.FirstName != employeeDB.FirstName)
                || (!string.IsNullOrWhiteSpace(employeeModel.MiddleName) && employeeModel.MiddleName != employeeDB.MiddleName)
                || (!string.IsNullOrWhiteSpace(employeeModel.LastName) && employeeModel.LastName != employeeDB.LastName)
                || (roleId != -1 && employeeDB.RoleId != roleId))
            {
                employeeDB.FirstName = employeeModel.FirstName;
                employeeDB.MiddleName = employeeModel.MiddleName;
                employeeDB.LastName = employeeModel.LastName;

                employeeDB.RoleId = roleId;
                updateUser = true;
            }
            if (!string.IsNullOrWhiteSpace(employeeModel.Password) && !VerifyPasswordHash(employeeModel.Password, loginDB.PasswordHash, loginDB.PasswordSalt))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(employeeModel.Password, out passwordHash, out passwordSalt);

                loginDB.PasswordSalt = passwordSalt;
                loginDB.PasswordHash = passwordHash;

                updateLoginInfo = true;
            }

            if (updateUser && updateLoginInfo)
            {
                try
                {
                    unitOfWork.BeginTransaction();

                    unitOfWork.EmployeeManager.Update(employeeDB);
                    unitOfWork.SaveChanges();

                    unitOfWork.LoginInfoManager.Update(loginDB);
                    unitOfWork.SaveChanges();

                    unitOfWork.CommitTransaction();
                }
                catch(Exception e)
                {
                    unitOfWork.RollbackTransaction();
                    return e.Message;
                }
            }
            else if (updateUser)
            {
                try
                {
                    unitOfWork.EmployeeManager.Update(employeeDB);
                    unitOfWork.SaveChanges();
                }
                catch(Exception e)
                {
                    return e.Message;
                }
            }
            else if (updateLoginInfo)
            {
                try
                {
                    unitOfWork.LoginInfoManager.Update(loginDB);
                    unitOfWork.SaveChanges();
                }
                catch(Exception e)
                {
                    return e.Message;
                }
            }

            return "Update sucsessfull.";
        }
    }
}
