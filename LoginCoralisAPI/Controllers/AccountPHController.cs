using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Models.Constants;
using LoginCoralisAPI.Models.Entities.DTOs;
using LoginCoralisAPI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//PH= Place Holder Name
namespace LoginCoralisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountPHController : ControllerBase
    {   //dependecy injection
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        public  AccountPHController(UserManager<User> userManager,IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }
        [HttpPost("Registerasadmin")]
        
        public async Task<IActionResult> RegisterAsAdmin([FromBody]RegisterUserDTO dto)
        {
            var exists = await _userManager.FindByEmailAsync(dto.email);
            if (exists!=null)
                return BadRequest("User already registered");
            var result = await _userService.RegisterUserAsAdminAsync(dto);
            if(result)
                return Ok(result);
            return BadRequest();
        }
        [HttpPost("Registerasuser")]

        public async Task<IActionResult> RegisterAsUser([FromBody] RegisterUserDTO dto)
        {
            var exists = await _userManager.FindByEmailAsync(dto.email);
            if (exists != null)
                return BadRequest("User already registered");
            var result = await _userService.RegisterUserAsync(dto);
            if (result)
                return Ok(result);
            return BadRequest();
        }
        [HttpPost("login")]
       
        public async Task<IActionResult> Login([FromBody]LoginUserDTO dto)
        {
            var token = await _userService.LoginUser(dto);
            if (token == "")
                return Unauthorized();
            return Ok(new { token });
        }
    }
}
