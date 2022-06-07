using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EF_FluentAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly DBContext _dbContext;

    public AuthController(DBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] Credential credentialData)
    {
        if (!ModelState.IsValid) return BadRequest(new {message = "Некорректные данные!"});

        var credential = _dbContext.Credentials.FirstOrDefault(u => u.Username == credentialData.Username);

        if (credential is not null) return BadRequest(new { message = "Пользователь с таким именем уже существует!" });

        credentialData.Passhash = BCrypt.Net.BCrypt.EnhancedHashPassword(credentialData.Passhash);

        await _dbContext.Credentials.AddAsync(credentialData);
        await _dbContext.SaveChangesAsync();

        var token = JwtIssue(credentialData);
        Response.Headers.Append("access_token", token);

        return Json(credentialData);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] Credential credentialData)
    {
        if (!ModelState.IsValid) return BadRequest(new {message = "Некорректные данные!"});

        var credential = _dbContext.Credentials.Include(u => u.Customer).FirstOrDefault(u => u.Username == credentialData.Username);

        if (credential is null) return NotFound(new { message = "Пользователь не найден" });
        if (!BCrypt.Net.BCrypt.EnhancedVerify(credentialData.Passhash, credential.Passhash)) return BadRequest(new { message = "Неверный пароль" });

        var token = JwtIssue(credentialData);
        Response.Headers.Append("access_token", token);
                
        return Json(credential);
    }

    private string JwtIssue(Credential credential)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, credential.Username)};

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}