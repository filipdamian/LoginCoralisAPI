using LoginCoralisAPI.Data;
using LoginCoralisAPI.Models.Entities;
using LoginCoralisAPI.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.SessionTokenRepository
{
    public class SessionTokenRepository:GenericRepository<SessionToken>,ISessionTokenRepository
    {
        public SessionTokenRepository(LoginContext context) : base(context) { }

        public async Task<SessionToken> GetByJti(string Jti)
        {
            return await _context.SessionTokens.FirstOrDefaultAsync(t => t.Jti.Equals(Jti));
        }
    }
}
