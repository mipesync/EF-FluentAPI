using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Generators;
using EF_FluentAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            return Json(orders);
        }

        [HttpGet("getByCustomerId")] // GET: /getByCustomerId?customerId=
        public IActionResult GetOrdersByCustomerId(string customerId)
        {
            var orders = _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).Where(u => u.CustomerId == customerId).ToList();

            return Json(orders);
        }
        
        [AllowAnonymous]
        [HttpGet("getByCustomerPhone")] // GET: /getByCustomerPhone?customerPhone=
        public IActionResult GetOrdersByCustomerPhone(string customerPhone)
        {
            var orders = _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).Where(u => u.Customer.Phone == customerPhone).ToList();

            return Json(orders);
        }

        [AllowAnonymous]
        [HttpGet("getByDate")] // GET: /getByDate?date=
        public IActionResult GetOrdersByDate(DateTime date)
        {
            var orders = _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).Where(u => u.OrderDate.Date == date).ToList();

            return Json(orders);
        }

        [AllowAnonymous]
        [HttpGet("getByName")] // GET: /getByName?name=
        public IActionResult GetOrdersByNumber(string name)
        {
            var orders = _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).Where(u => u.Name == name).ToList();

            return Json(orders);
        }

        [HttpGet("getById")] // GET: /getById?id=
        public async Task<IActionResult> GetOrderById(int id)
        {
            var orders = await _dbContext.Orders.Include(u => u.Products).Include(u => u.Customer).FirstOrDefaultAsync(u => u.Id == id);

            return Json(orders);
        }

        [HttpPost("complete")] // GET: /complete?customerId=
        public async Task<IActionResult> Complete([FromBody] OrderDto orderDto, string customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(u => u.Id == customerId);

            var order = new Order { Name = orderDto.Name, Products = orderDto.Products, TotalPrice = orderDto.TotalPrice, IsCompleted = true, Customer = customer! };
            
            await _dbContext.Orders.AddAsync(order);
            _dbContext.Customers.Attach(customer!);
            foreach (var elem in order.Products)
            {
                _dbContext.Products.Attach(elem);
            }
            await _dbContext.SaveChangesAsync();

            return Json(order);
        }
    }
}
