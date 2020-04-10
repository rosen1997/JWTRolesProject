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
    public class LoginController : Controller
    {
        private readonly IAtWorkRepository atWorkRepository;

        public LoginController(IAtWorkRepository atWorkRepository)
        {
            this.atWorkRepository = atWorkRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllAtWork")]
        public IActionResult GetAllRowsAtWork()
        {
            IEnumerable<AtWorkModel> atWorkModels = atWorkRepository.GetAllRows();

            return Ok(atWorkModels);
        }

        [HttpGet]
        [Route("GetAtWorkByEmployeeId")]
        public IActionResult GetAtWorkByEmployeeId()
        {
            int currentUserId = int.Parse(User.Identity.Name);

            AtWorkModel atWorkModel = atWorkRepository.GetByEmployeeId(currentUserId);

            if (atWorkModel != null)
            {
                return Ok(atWorkModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("LoginAtWork")]
        public IActionResult LoginAtWork()
        {
            IActionResult result = GetAtWorkByEmployeeId();
            if (result.ToString() != new NotFoundResult().ToString())
                return BadRequest();

            int currentUserId = int.Parse(User.Identity.Name);

            atWorkRepository.LoginAtWork(currentUserId);

            return Ok();
        }
    }
}
