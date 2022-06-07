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

        [HttpPost("cartOperate")] // POST: /cartOperate?id=&isRemoved=
        public async Task<IActionResult> CartOperate(string id, bool isRemoved)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(u => u.Id.ToString() == id);
            if (product is null) return BadRequest(new { message = "Продукт не найден!" });

            var requestCookies = Request.Headers["psid"].FirstOrDefault();
            if (requestCookies == null) return Ok();

            var psid = requestCookies!.Split("&").Where(u => u != "").ToList();
            if (isRemoved) psid.Remove(id);
            else psid.Add(id);
            var response = string.Join("&", psid);

            Response.Headers.Append("count", $"{psid.Count}");
            Response.Headers.Append("psid", response);

            return Ok();
        }

        [HttpGet("getFromCookie")] // GET: /getFromCookie
        public async Task<IActionResult> GetFromCookie()
        {
            List<Product> products = new List<Product>();

            var requestCookies = Request.Headers["psid"].FirstOrDefault();
            var psid = requestCookies!.Split("&").Where(u => u != "");
            decimal totalPrice = 0;

            foreach (var id in psid)
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(u => u.Id.ToString() == id);
                totalPrice += product!.Price;
                products.Add(product!);
            }

            var order = new OrderDto
            {
                Name = new OrderNameGenerator().Generate(),
                Products = products,
                TotalPrice = totalPrice
            };

            return Json(order);
        }
    }
}
