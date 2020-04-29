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
    public class LoginController : Controller
    {
        private readonly IAtWorkRepository atWorkRepository;
        private readonly ILoginHistoryRepository loginHistoryRepository;

        public LoginController(IAtWorkRepository atWorkRepository, ILoginHistoryRepository loginHistoryRepository)
        {
            this.atWorkRepository = atWorkRepository;
            this.loginHistoryRepository = loginHistoryRepository;
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

        [HttpPut]
        [Route("LogoutFromWork")]
        public IActionResult LogoutFromWork()
        {
            int currentUserId = int.Parse(User.Identity.Name);

            AtWork atWork = atWorkRepository.GetEntityByEmployeeId(currentUserId);

            if (atWork == null)
            {
                return BadRequest();
            }

            loginHistoryRepository.LogoutFromWork(atWork);

            return Ok();
        }

        [HttpGet]
        [Route("GetLoginHistory")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetLoginHistory()
        {
            return Ok(loginHistoryRepository.GetAllRows());
        }

        [HttpGet]
        [Route("GetLoginHistoryByEmployeeId")]
        public IActionResult GetLoginHistoryByEmployeeId(int id)
        {
            int currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            IEnumerable<LoginHistoryModel> loginHistoryModel = loginHistoryRepository.GetAllRowsByEmployeeId(id);

            return Ok(loginHistoryModel);
        }
    }
}
