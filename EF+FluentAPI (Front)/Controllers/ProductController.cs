using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_FluentAPI__Front_.Controllers
{
    public class ProductController : Controller
    {
        private readonly string _url;

        public ProductController(IConfiguration configuration)
        {
            _url = configuration.GetValue<string>("ApiConnectionString:ApiUrl");
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Choose(string id)
        {
            return View();
        }
    }
}
