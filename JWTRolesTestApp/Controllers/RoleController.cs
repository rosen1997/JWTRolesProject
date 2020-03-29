using JWTRolesTestApp.Models;
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
    public class RoleController : Controller
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            IEnumerable<RoleModel> roleModels = roleRepository.GetAllRoles();

            if (!roleModels.Any())
                return Problem();

            return Ok(roleModels);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddRole")]
        public IActionResult AddRole(string RoleDescription)
        {
            try
            {
                roleRepository.AddRole(RoleDescription);
                return Ok();
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

    }
}
