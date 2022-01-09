using LoginCoralisAPI.Data;
using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.AccountRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LoginContext context) : base(context) { }
        public async Task<List<User>> GetAllUsersWithAddress()
        {
            return await _context.Users.Include(usr => usr.Address).ToListAsync();
        }


        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.Include(usr=>usr.Address).Where(usr => usr.Email.Equals(email)).FirstOrDefaultAsync();  
        }

        public async Task<User> GetByIdWithRoles(int id)
        {
            return await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u=>u.Id.Equals(id));
        }
    }
}
