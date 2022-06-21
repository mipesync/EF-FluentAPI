using EF_FluentAPI.BLL.Interfaces;
using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_FluentAPI.BLL.Implementations
{
    public class ProductService : IProductService
    {
        private readonly DBContext _dbContext;

        public ProductService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }

        public async Task<Product?> GetById(string id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(u => u.Id.ToString() == id);
        }
    }
}
