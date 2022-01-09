using LoginCoralisAPI.Models.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsAdminAsync(RegisterUserDTO dto);
        Task<bool> RegisterUserAsync(RegisterUserDTO dto);
        Task<string> LoginUser(LoginUserDTO dto);
    }
}
