using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Models.Constants;
using LoginCoralisAPI.Models.Entities;
using LoginCoralisAPI.Models.Entities.DTOs;
using LoginCoralisAPI.Repositories.AccountRepository;
using LoginCoralisAPI.Repositories.SessionTokenRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _repository;
        private readonly ISessionTokenRepository _STrepository;
        public UserService(UserManager<User> usermanager,IUserRepository repository, ISessionTokenRepository strepository)
        {
            _userManager = usermanager;
            _repository = repository;
            _STrepository = strepository;
        }
        public async Task<bool> RegisterUserAsAdminAsync(RegisterUserDTO dto)
        {
            var registerUser = new User();
            registerUser.Email = dto.email;
            registerUser.name = dto.name;
            registerUser.UserName = dto.email;
            //registerUser.password = dto.password;

            var result = await _userManager.CreateAsync(registerUser, dto.password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(registerUser, UserRoleType.Admin);
                return true;
            }
            return false;
        }
        public async Task<bool> RegisterUserAsync(RegisterUserDTO dto)
        {
            var registerUser = new User();
            registerUser.Email = dto.email;
            registerUser.name = dto.name;
            registerUser.UserName = dto.email;
            //registerUser.password = dto.password;

            var result = await _userManager.CreateAsync(registerUser, dto.password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(registerUser, UserRoleType.User);
                return true;
            }
            return false;
        }
        public async Task<string>LoginUser(LoginUserDTO dto)
        {
            User user = await _userManager.FindByEmailAsync(dto.email);
            //User password = await _userManager.CheckPasswordAsync(user);

            if(user !=null)
            {
                var passwordOk = await _userManager.CheckPasswordAsync(user, dto.password);
                if (passwordOk)
                {
                    user = await _repository.GetByIdWithRoles(user.Id);
                    List<string> roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
                    var newJti = Guid.NewGuid().ToString();
                    //JWT TOKEN ID      GENERALLY UNIQUE IDENTIFIER
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom secret key for auth"));

                    var token = GenerateJwtToken(signinkey, user, roles, tokenHandler, newJti);
                    _STrepository.create(new SessionToken(newJti, user.Id, token.ValidTo));
                    await _STrepository.saveAsync();
                    return tokenHandler.WriteToken(token);
                }
            }
            return "";
        }
        private SecurityToken GenerateJwtToken(SymmetricSecurityKey signinkey,User user,List<string> roles, JwtSecurityTokenHandler tokenHandler,string jti)
        {
            var subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Email,user.Email),
                                                           new Claim(ClaimTypes.Name,user.name),
                                                           new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                                                           new Claim(JwtRegisteredClaimNames.Jti, jti)
            });

            foreach(var role in roles)
            {
                subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(signinkey, SecurityAlgorithms.HmacSha256)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
