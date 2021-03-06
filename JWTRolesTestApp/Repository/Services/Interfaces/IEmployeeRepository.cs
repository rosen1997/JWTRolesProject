﻿using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Services.Interfaces
{
    public interface IEmployeeRepository
    {
        public EmployeeModel Authenticate(string username, string password);

        public IEnumerable<EmployeeModel> GetAllEmployees();

        public EmployeeModel GetById(int id);

        public EmployeeModel CreateEmployee(CreateEmployeeModel createEmployeeModel, ref string message);

        public void DeleteEmployee(int id);

        public string UpdateEmployee(UpdateEmployeeModel employeeModel);
    }
}
