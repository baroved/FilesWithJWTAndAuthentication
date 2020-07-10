using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServerCore.Infra;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore.Services
{
    public class JWTAuthenticationService : IJWTAuthenticationService
    {
        private readonly IConfiguration _config;
        public JWTAuthenticationService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> GenerateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["secret"]);
            var tokenInfo = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenData = tokenHandler.CreateToken(tokenInfo);
            var token = tokenHandler.WriteToken(tokenData);
            return token;
        }
    }
}
