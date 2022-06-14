using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Generators;
using EF_FluentAPI.Models;
using EF_FluentAPI.Models.Intermediate_Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_FluentAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DBContext _dbContext;

        public OrderController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("get")] // GET: /get
        public IActionResult GetOrders()
        {
            var orders = _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).ToList();
            if (orders is null) return NotFound(new {message = "Заказы не найдены"});
            return Json(orders);
        }

        [HttpGet("getByCustomerId")] // GET: /getByCustomerId?customerId=
        public IActionResult GetOrdersByCustomerId(string customerId)
        {
            var orders = _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).Where(u => u.CustomerId == customerId).ToList();
            if (orders is null) return NotFound(new {message = "Заказы не найдены"});
            return Json(orders);
        }
        
        [AllowAnonymous]
        [HttpGet("getByCustomerPhone")] // GET: /getByCustomerPhone?customerPhone=
        public IActionResult GetOrdersByCustomerPhone(string customerPhone)
        {
            var orders = _dbContext.Orders.Include(u => u.Products).Where(u => u.Customer.Phone.Contains(customerPhone.Trim())).ToList();
            if (orders is null) return NotFound(new {message = "Заказы не найдены"});
            return Json(orders);
        }

        [AllowAnonymous]
        [HttpGet("getByDate")] // GET: /getByDate?date=
        public IActionResult GetOrdersByDate(DateTime date)
        {
            var orders = _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).Where(u => u.OrderDate.Date == date).ToList();
            if (orders is null) return NotFound(new {message = "Заказы не найдены"});
            return Json(orders);
        }

        [AllowAnonymous]
        [HttpGet("getByName")] // GET: /getByName?name=
        public async Task<IActionResult> GetOrdersByNumber(string name)
        {
            var order = await _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).FirstOrDefaultAsync(u => u.Name == name);
            if (order is null) return NotFound(new {message = "Заказ не найден"});
            return Json(order);
        }

        [HttpGet("getById")] // GET: /getById?id=
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).FirstOrDefaultAsync(u => u.Id == id);
            if (order is null) return NotFound(new {message = "Заказ не найден"});
            return Json(order);
        }

        [HttpPost("placeAnOrder")] // Post: /placeAnOrder?customerId=
        public async Task<IActionResult> PlaceAnOrder([FromBody] OrderDto orderDto, string customerId)
        {
            var customer = await _dbContext.Customers.Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == customerId);
            var order = new Order { Name = new OrderNameGenerator().Generate(_dbContext), Customer = customer!, 
                IsCompleted = true, TotalPrice = orderDto.TotalPrice };
            
            foreach (var product in orderDto.Products)
            {
                _dbContext.Products.Attach(product);
            }
            await _dbContext.Orders.AddAsync(order);
            foreach (var product in orderDto.Products)
            {
                for (int i = 0; i < product.ProductCarts!.Count; i++)
                {
                    await _dbContext.ProductOrders.AddAsync(new ProductOrder { Order = order, Product = product });
                }
            }
            
            var cart = await _dbContext.Carts.Include(u => u.Products).FirstOrDefaultAsync(u => u.Id == customer!.Cart!.Id);

            cart!.ProductCarts!.Clear();
            cart.Products!.Clear();
            cart.TotalPrice = 0;
            cart.Count = 0;
            _dbContext.Carts.Update(cart);
            
            await _dbContext.SaveChangesAsync();

            return Json(order);
        }
    }
}
