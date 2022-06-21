using EF_FluentAPI.BLL.Interfaces;
using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_FluentAPI.BLL.Implementations
{
    public class CredentialService : ICredentialService
    {
        private readonly DBContext _dbContext;

        public CredentialService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Credential?> GetByUsername(string username)
        {
            return await _dbContext.Credentials.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
