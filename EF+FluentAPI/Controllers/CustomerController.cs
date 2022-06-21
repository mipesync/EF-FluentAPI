using EF_FluentAPI.BLL;
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
        private readonly ServiceManager _serviceManager;

        public CustomerController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("getAll")] //GET: /getAll
        public IActionResult GetAll()
        {
            return Json(_serviceManager.CustomerService.GetAll());
        }
        
        [AllowAnonymous]
        [HttpGet("getById")] //GET: /getById?id=
        public async Task<IActionResult> GetById(string id)
        {
            return Json(await _serviceManager.CustomerService.GetById(id));
        }

        [HttpPost("create")] //POST: /create
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Некорректные данные!" });

            var credential = await _serviceManager.CredentialService.GetByUsername(User.Identity!.Name!);

            if (credential is null) return BadRequest();

            return Json(await _serviceManager.CustomerService.Create(customer, credential));
        }

        [HttpPost("edit/{id}")] //GET: /edit/id
        public async Task<IActionResult> Edit(string id, [FromBody] Customer customerData)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = "Некорректные данные!" });

            var customer = await _serviceManager.CustomerService.GetById(id);

            if (customer is null) return NotFound(new { message = "Пользователь не найден!" });

            return Json(await _serviceManager.CustomerService.Edit(customerData, customer));
        }
    }
}
