using EF_FluentAPI.BLL;
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
        private readonly ServiceManager _serviceManager;

        public OrderController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("get")] // GET: /get
        public IActionResult GetAll()
        {
            var orders = _serviceManager.OrderService.GetAll();
            if (orders is null) return NotFound(new {message = "Заказы не найдены"});
            return Json(orders);
        }

        [HttpGet("getByCustomerId")] // GET: /getByCustomerId?customerId=
        public IActionResult GetOrdersByCustomerId(string customerId)
        {
            var orders = _serviceManager.OrderService.GetOrdersByCustomerId(customerId);
            if (orders is null) return NotFound(new {message = "Заказы не найдены"});
            return Json(orders);
        }
        
        [AllowAnonymous]
        [HttpGet("getByCustomerPhone")] // GET: /getByCustomerPhone?customerPhone=
        public IActionResult GetOrdersByCustomerPhone(string customerPhone)
        {
            var orders = _serviceManager.OrderService.GetOrdersByCustomerPhone(customerPhone);
            if (orders is null) return NotFound(new {message = "Заказы не найдены"});
            return Json(orders);
        }

        [AllowAnonymous]
        [HttpGet("getByDate")] // GET: /getByDate?date=
        public IActionResult GetOrdersByDate(DateTime date)
        {
            var orders = _serviceManager.OrderService.GetOrdersByDate(date);
            if (orders is null) return NotFound(new {message = "Заказы не найдены"});
            return Json(orders);
        }

        [AllowAnonymous]
        [HttpGet("getByName")] // GET: /getByName?name=
        public async Task<IActionResult> GetByName(string name)
        {
            var order = await _serviceManager.OrderService.GetByName(name);
            if (order is null) return NotFound(new {message = "Заказ не найден"});
            return Json(order);
        }

        [HttpGet("getById")] // GET: /getById?id=
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _serviceManager.OrderService.GetById(id);
            if (order is null) return NotFound(new {message = "Заказ не найден"});
            return Json(order);
        }

        [HttpPost("placeAnOrder")] // Post: /placeAnOrder?customerId=
        public async Task<IActionResult> PlaceAnOrder([FromBody] OrderDto orderDto, string customerId)
        {
            var customer = await _serviceManager.CustomerService.GetById(customerId);

            var cart = await _serviceManager.CartService.GetById(customer!.Cart!.Id.ToString());

            return Json(await _serviceManager.OrderService.PlaceAnOrder(orderDto, customerId, customer, cart!));
        }
    }
}
