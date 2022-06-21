using EF_FluentAPI.BLL;
using EF_FluentAPI.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EF_FluentAPI.Controllers
{
    [ApiController]
    [Route("api/product")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ServiceManager _serviceManager;

        public ProductController(ServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("products_get")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var products = _serviceManager.ProductService.GetAll();
            return Json(products);
        }
    }
}