using System.Text;
using System.Text.Json;
using EF_FluentAPI__Front_.JsonDeserializer;
using EF_FluentAPI__Front_.Models;
using Microsoft.AspNetCore.Mvc;

namespace EF_FluentAPI__Front_.Controllers;

public class OrderController : Controller
{
    private readonly string _url;

    public OrderController(IConfiguration configuration)
    {
        _url = configuration.GetValue<string>("ApiConnectionString:ApiUrl");
    }
    
    [HttpPost("confirmed")]
    public async Task<IActionResult> Confirmed()
    {
        using (HttpClient httpClient = new())
        {
            string uri = $"{_url}api/cart/getCart?customerId={Request.Cookies["cid"]}";
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");
            var httpResponse = await httpClient.GetStringAsync(uri);

            var cart = JsonSerializer.Deserialize<Cart>(httpResponse,
                new JsonSerializerOptions(JsonSerializerDefaults.Web))!;

            var orderDto = new OrderDto { Products = cart.Products!, TotalPrice = cart.TotalPrice};

            uri = $"{_url}api/order/placeAnOrder?customerId={Request.Cookies["cid"]}";
            var requestBody = JsonSerializer.Serialize(orderDto);
            var asd = await httpClient.PostAsync(uri,
                new StringContent(requestBody, Encoding.UTF8, "application/json"));

            return View();
        }
    }
    
    [HttpGet("details")]
    public async Task<IActionResult> Details(string name)
    {
        using (HttpClient httpClient = new())
        {
            string uri = $"{_url}api/order/getByName?name={name}";
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");
            var httpResponse = await httpClient.GetStringAsync(uri);

            var order = JsonSerializer.Deserialize<Order>(httpResponse,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
            
            return View(order);
        }
    }
}