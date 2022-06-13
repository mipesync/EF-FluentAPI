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
        private readonly string _url;

        public HomeController(IConfiguration configuration)
        {
            _url = configuration.GetValue<string>("ApiConnectionString:ApiUrl");
        }

        public async Task<IActionResult> Index()
        {
            using (HttpClient httpClient = new())
            {
                var uri = $"{_url}api/product/products_get";
                var httpResponse = await httpClient.GetStringAsync(uri);
                var products = JsonSerializer.Deserialize<List<Product>>(httpResponse,
                    new JsonSerializerOptions(JsonSerializerDefaults.Web))!;

                if (Request.Cookies["access_token"] is not null)
                {
                    uri = $"{_url}api/cart/getCart?customerId={Request.Cookies["cid"]}";
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");
                    httpResponse = await httpClient.GetStringAsync(uri);
                    try
                    {
                        var cart = JsonSerializer.Deserialize<Cart>(httpResponse,
                            new JsonSerializerOptions(JsonSerializerDefaults.Web))!;

                        ViewData["CartLenght"] = cart.Count;  
                    }
                    catch { /* ignored */ }
                } else ViewData["CartLenght"] = 0;

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