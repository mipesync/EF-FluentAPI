using EF_FluentAPI.BLL.Interfaces;
using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EF_FluentAPI.BLL.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly DBContext _dbContext;

        public AuthService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string JwtIssue(Credential credential)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, credential.Username) };

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public async Task<Credential?> SignUp(Credential credentialData)
        {
            credentialData.Passhash = BCrypt.Net.BCrypt.EnhancedHashPassword(credentialData.Passhash);

            await _dbContext.Credentials.AddAsync(credentialData);
            await _dbContext.SaveChangesAsync();

            return credentialData;
        }
    }
}
