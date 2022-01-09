using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Entities.DTOs;
using LoginCoralisAPI.Repositories.AccountRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

       
        [Authorize(AuthenticationSchemes = "Bearer")]
        
        [HttpGet]
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repository.GetAllUsersWithAddress();
            var usersToReturn = new List<UserDTO>();
            foreach (var user in users)
            {
                usersToReturn.Add(new UserDTO(user));
            }
            return Ok(usersToReturn);
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _repository.GetByEmail(email);
            return Ok(new UserDTO(user));
        }
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _repository.GetByEmail(email);
            if (user == null)
                return NotFound("Account doesn't exist");
            _repository.delete(user);
            await _repository.saveAsync();
           return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO dto)
        {
            User newuser = new User();
           // newuser.id = dto.id;
            newuser.name = dto.name;
            newuser.phone = dto.phone;
            newuser.password = dto.password;
            newuser.Address = dto.Address;
            _repository.create(newuser);
            await _repository.saveAsync();
            return Ok(new UserDTO(newuser));
        }
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateAccountsName(string email,CreateUserDTO dto)
        {

            var user = await _repository.GetByEmail(email);
            if (user == null)
                return NotFound("Account doesn't exist");

           user.name = dto.name;
           await _repository.saveAsync();
           return Ok(new UserDTO(user));
        }
    }
}
