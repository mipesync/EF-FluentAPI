using EF_FluentAPI.Models;

namespace EF_FluentAPI.BLL.Interfaces
{
    public interface ICredentialService
    {
        Task<Credential?> GetByUsername(string username);
    }
}
