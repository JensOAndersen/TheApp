using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheApp.Api.Entities;
using TheApp.Api.Services;

namespace TheApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            User user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
            {
                return BadRequest(new { Message = "Username or Password is incorrect" });
            }

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }
    }
}