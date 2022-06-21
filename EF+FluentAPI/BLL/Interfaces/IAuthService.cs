using EF_FluentAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EF_FluentAPI.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<Credential?> SignUp(Credential credentialData);
        string JwtIssue(Credential credential);
    }
}
