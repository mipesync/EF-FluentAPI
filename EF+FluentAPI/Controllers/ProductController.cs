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
        private readonly DBContext _dbContext;

        public ProductController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("products_get")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            return Json(products);
        }
    }
}