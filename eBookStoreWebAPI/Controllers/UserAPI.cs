using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace eBookStoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [AllowAnonymous]
    public class UserAPI : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserAPI(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                _userService.AddUser(user);
                return Ok("User added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}