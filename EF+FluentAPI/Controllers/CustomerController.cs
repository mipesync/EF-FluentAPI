using EF_FluentAPI.DbContexts;
using EF_FluentAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_FluentAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly DBContext _dbContext;

        public CustomerController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            var customer = _dbContext.Customers.Include(u => u.Orders);
            return Json(customer);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _dbContext.Customers.Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == id);
            return Json(customer);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Некорректные данные!" });

            var credential = await _dbContext.Credentials.FirstOrDefaultAsync(u => u.Username == User.Identity!.Name);

            if (credential is null) return BadRequest();

            customer.Credential = credential;

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return Json(customer);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Customer customerData)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Некорректные данные!" });

            var customer = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (customer is null) return NotFound(new { message = "Пользователь не найден!" });

            customer = customerData;
            customer.Id = id;

            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();

            return Json(customer);
        }
    }
}
