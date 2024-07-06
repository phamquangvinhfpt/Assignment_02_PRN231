using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using eBookStoreWebAPI.Models;
using eBookStoreWebAPI.Ultils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace eBookStoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthAPI : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public AuthAPI(IUserService userService, IConfiguration configuration, IJwtService jwtService)
        {
            _userService = userService;
            _configuration = configuration;
            _jwtService = jwtService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = _userService.GetUserByEmailAndPassword(loginModel.Email, loginModel.Password);
            var token = null as string;
            var data = new { token, user};
            if (user == null)
            {
                if (loginModel.Email == _configuration.GetSection("DefaultAccount")["Email"] && loginModel.Password == _configuration.GetSection("DefaultAccount")["Password"])
                {
                    user = new User
                    {
                        EmailAddress = loginModel.Email,
                        Password = loginModel.Password,
                        RoleId = 1
                    };
                    token = _jwtService.GenerateToken(user);
                    return Ok(new {
                        StatusCode = 200,
                        Status = true,
                        Message = "Login successfully",
                        Data = new { token, user }
                    });
                }
                return NotFound("Invalid email or password");
            }

            token = _jwtService.GenerateToken(user);

            return Ok(new 
            {
                StatusCode = 200,
                Status = true,
                Message = "Login successfully",
                Data = new { token, user }
            });
        }
    }
}