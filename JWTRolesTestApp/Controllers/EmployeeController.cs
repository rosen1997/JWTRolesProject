using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace JWTRolesTestApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromBody]LoginModel loginModel)
        {
            if (loginModel.Username == null && loginModel.Password == null)
                return NotFound();

            EmployeeModel employee = employeeRepository.Authenticate(loginModel.Username, loginModel.Password);

            if (employee == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            IEnumerable<EmployeeModel> employees = employeeRepository.GetAllEmployees();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EmployeeModel employee = employeeRepository.GetById(id);

            if (employee == null)
                return NotFound();

            int currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee([FromBody] CreateEmployeeModel createEmployeeModel)
        {
            EmployeeModel employee = employeeRepository.CreateEmployee(createEmployeeModel);

            if (employee == null)
            {
                return Problem();
            }

            return Ok(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                employeeRepository.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] UpdateEmployeeModel employeeModel)
        {
            try
            {
                employeeRepository.UpdateEmployee(employeeModel);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("UpdateEmployeePassword")]
        public IActionResult UpdateEmployeePassword([FromBody] UpdateEmployeeModel employeeModel)
        {
            int currentUserId = int.Parse(User.Identity.Name);

            if (currentUserId != employeeModel.EmployeeId)
            {
                return Forbid();
            }

            try
            {
                employeeRepository.UpdateEmployee(employeeModel);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
