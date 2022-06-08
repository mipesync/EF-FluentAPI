using EF_FluentAPI__Front_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace EF_FluentAPI__Front_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _url;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _url = configuration.GetValue<string>("ApiConnectionString:ApiUrl");
        }

        public async Task<IActionResult> Index()
        {
            using (HttpClient httpClient = new())
            {
                var httpResponseProducts = await httpClient.GetStringAsync($"{_url}api/product/products_get");
                var products = JsonSerializer.Deserialize<List<Product>>(httpResponseProducts,
                    new JsonSerializerOptions(JsonSerializerDefaults.Web))!;

                if (Request.Cookies["access_token"] is not null)
                {
                    string uri = $"{_url}api/cart/getCart?customerId={Request.Cookies["cid"]}";
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");
                    var httpResponseCart = await httpClient.GetStringAsync(uri);

                    var cart = JsonSerializer.Deserialize<Cart>(httpResponseCart,
                        new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
                    
                    ViewData.Add("CartLenght", cart.Products!.Count);                    
                } else ViewData.Add("CartLenght", 0);  

                return View(products);
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}