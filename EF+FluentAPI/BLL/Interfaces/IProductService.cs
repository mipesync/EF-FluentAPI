using EF_FluentAPI.Models;

namespace EF_FluentAPI.BLL.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAll();
        Task<Product?> GetById(string id);
    }
}
