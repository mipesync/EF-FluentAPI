using System.Globalization;
using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using EF_FluentAPI.Models.Intermediate_Entities;
using EF_FluentAPI.BLL;

namespace EF_FluentAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ServiceManager _serviceManager;

        public CartController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("getCart")] // GET: /getCart?customerId=
        public async Task<IActionResult> GetCart(string customerId)
        {
            var cart = await _serviceManager.CartService.GetCartByCustomerId(customerId);
            if (cart is null) return NotFound();

            return Json(cart);
        }

        [HttpPost("addToCart")] // POST: /addToCart?id=?customerId=
        public async Task<IActionResult> AddToCart(string id, string customerId)
        {
            List<Product?> products = new List<Product?> { await _serviceManager.ProductService.GetById(id) };

            var customer = await _serviceManager.CustomerService.GetById(customerId);
            if (customer is null) return BadRequest(new { message = "Пользователь не найден!" });

            var cart = await _serviceManager.CartService.GetCartByCustomerId(customerId);

            return Json(await _serviceManager.CartService.AddToCart(customer, products!, cart!));
        }

        [HttpPost("removeFromCart")] // POST: /removeFromCart?id=?customerId=
        public async Task<IActionResult> RemoveFromCart(string id, string customerId)
        {
            var cart = await _serviceManager.CartService.GetCartByCustomerId(customerId);
            if (cart is null) return Ok();

            return Json(await _serviceManager.CartService.RemoveFromCart(cart, id));
        }
    }
}
