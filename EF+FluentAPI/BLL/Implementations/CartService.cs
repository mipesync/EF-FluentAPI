using EF_FluentAPI.BLL.Interfaces;
using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Models;
using EF_FluentAPI.Models.Intermediate_Entities;
using Microsoft.EntityFrameworkCore;

namespace EF_FluentAPI.BLL.Implementations
{
    public class CartService : ICartService
    {
        private readonly DBContext _dbContext;

        public CartService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cart?> AddToCart(Customer customer, List<Product> products, Cart cart)
        {
            if (cart is null)
            {
                cart = new Cart { Customer = customer, Products = products! };

                cart.Count++;
                cart.TotalPrice += products[0]!.Price;
                await _dbContext.Carts.AddAsync(cart);
            }
            else
            {
                _dbContext.ProductCarts.Add(new ProductCart { Cart = cart, Product = products[0] });
                cart.Count++;
                cart.TotalPrice += products[0]!.Price;
                _dbContext.Carts.Update(cart);
            }

            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart?> GetById(string id)
        {
            return await _dbContext.Carts.Include(u => u.Products).FirstOrDefaultAsync(u => u.Id.ToString() == id);
        }

        public async Task<Cart?> GetCartByCustomerId(string customerId)
        {
            return await _dbContext.Carts.Include(u => u.Customer).Include(u => u.Products).FirstOrDefaultAsync(u => u.CustomerId == customerId);
        }

        public async Task<Cart?> RemoveFromCart(Cart cart, string id)
        {
            var product = cart.Products!.FirstOrDefault(u => u.Id.ToString() == id);
            cart.Products!.Remove(product!);
            cart.Count--;
            cart.TotalPrice -= product!.Price;

            _dbContext.Carts.Update(cart);
            await _dbContext.SaveChangesAsync();

            return cart;
        }
    }
}
