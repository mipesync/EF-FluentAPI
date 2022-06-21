using EF_FluentAPI.Models;

namespace EF_FluentAPI.BLL.Interfaces
{
    public interface ICartService
    {
        Task<Cart?> GetCartByCustomerId(string customerId);
        Task<Cart?> GetById(string id);
        Task<Cart?> AddToCart(Customer customer, List<Product> products, Cart cart);
        Task<Cart?> RemoveFromCart(Cart cart, string id);
    }
}
