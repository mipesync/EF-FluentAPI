using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Generators;
using EF_FluentAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EF_FluentAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly DBContext _dbContext;

        public CartController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getCart")] // GET: /getCart?customerId=
        public async Task<IActionResult> GetCart(string customerId)
        {
            var cart = await _dbContext.Carts.Include(u => u.Customer).Include(u => u.Products).FirstOrDefaultAsync(u => u.CustomerId == customerId);
            if (cart is null) return Ok();

            return Json(cart);
        }

        [HttpPost("addToCart")] // POST: /addToCart?id=?customerId=
        public async Task<IActionResult> AddToCart(string id, string customerId)
        {
            List<Product?> products = new List<Product?> { await _dbContext.Products.FirstOrDefaultAsync(u => u.Id.ToString() == id) };

            var customer = await _dbContext.Customers.FirstOrDefaultAsync(u => u.Id == customerId);
            if (customer is null) return BadRequest(new { message = "Пользователь не найден!" });

            var cart = await _dbContext.Carts.Include(u => u.Customer).Include(u => u.Products).FirstOrDefaultAsync(u => u.CustomerId == customerId);
            if (cart is null)
            {
                cart = new Cart { Customer = customer, Products = products! };
                await _dbContext.Carts.AddAsync(cart);
            }
            else
            {
                products.AddRange(cart.Products!);
                cart.Products = products!;
                _dbContext.Carts.Update(cart);
            }

            await _dbContext.SaveChangesAsync();

            return Json(cart);
        }

        [HttpPost("removeFromCart")] // POST: /removeFromCart?id=?customerId=
        public async Task<IActionResult> RemoveFromCart(string id, string customerId)
        {
            List<Product?> products = new List<Product?>();

            var cart = await _dbContext.Carts.Include(u => u.Customer).Include(u => u.Products).FirstOrDefaultAsync(u => u.CustomerId == customerId);
            if (cart is null) return Ok();

            var product = cart.Products!.FirstOrDefault(u => u.Id.ToString() == id);
            products.AddRange(cart.Products!);
            products.Remove(product);
            cart.Products = products!;

            _dbContext.Carts.Update(cart);
            await _dbContext.SaveChangesAsync();

            return Json(cart);
        }
    }
}
