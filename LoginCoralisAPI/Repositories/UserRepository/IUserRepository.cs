using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.AccountRepository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<List<User>> GetAllUsersWithAddress();
        Task<User> GetByIdWithRoles(int id);
    }
}
