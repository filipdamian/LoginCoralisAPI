using LoginCoralisAPI.Models.Entities;
using LoginCoralisAPI.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.SessionTokenRepository
{
    public interface ISessionTokenRepository: IGenericRepository<SessionToken>
    {
        Task<SessionToken> GetByJti(string Jti);
    }
}
