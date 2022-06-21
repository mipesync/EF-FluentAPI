using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EF_FluentAPI.BLL;
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
    private readonly ServiceManager _serviceManager;

    public AuthController(ServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] Credential credentialData)
    {
        if (!ModelState.IsValid) return BadRequest(new {message = "Некорректные данные!"});

        var credential = await _serviceManager.CredentialService.GetByUsername(credentialData.Username);

        if (credential is not null) return BadRequest(new { message = "Пользователь с таким именем уже существует!" });

        credentialData.Passhash = BCrypt.Net.BCrypt.EnhancedHashPassword(credentialData.Passhash);

        var token = _serviceManager.AuthService.JwtIssue(credential!);
        Response.Headers.Append("access_token", token);

        return Json(await _serviceManager.AuthService.SignUp(credentialData)!);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Credential credentialData)
    {
        if (!ModelState.IsValid) return BadRequest(new {message = "Некорректные данные!"});

        var credential = await _serviceManager.CredentialService.GetByUsername(credentialData.Username);

        if (credential is null) return NotFound(new { message = "Пользователь не найден" });
        if (!BCrypt.Net.BCrypt.EnhancedVerify(credentialData.Passhash, credential.Passhash)) return BadRequest(new { message = "Неверный пароль" });

        var token = _serviceManager.AuthService.JwtIssue(credential);
        Response.Headers.Append("access_token", token);
                
        return Json(credential);
    }
}