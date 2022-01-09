using LoginCoralisAPI.Repositories.SessionTokenRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Helpers
{
    public class SessionTokenValidator
    {
        public static async Task ValidateSessioToken(TokenValidatedContext context)
        {
            var repository = context.HttpContext.RequestServices.GetRequiredService<ISessionTokenRepository>();
            if (context.Principal.HasClaim(c => c.Type.Equals(JwtRegisteredClaimNames.Jti)))
            {
                var jti = context.Principal.Claims.FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Jti)).Value;
                var tokenInDb = await repository.GetByJti(jti);
                if(tokenInDb!=null && tokenInDb.ExpirationDate>DateTime.Now)
                {
                    return;
                }
            }
            context.Fail("");
        }
    }
}
