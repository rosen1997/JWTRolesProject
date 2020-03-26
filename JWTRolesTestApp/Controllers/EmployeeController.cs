using JWTRolesTestApp.Models;
using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
