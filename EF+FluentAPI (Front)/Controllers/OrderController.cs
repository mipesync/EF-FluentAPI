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
    
    public async Task<IActionResult> OrderConfirmed()
    {
        using (HttpClient httpClient = new())
        {
            string uri = $"{_url}api/cart/getCart?customerId={Request.Cookies["cid"]}";
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");
            var httpResponse = await httpClient.GetStringAsync(uri);

            var cart = JsonSerializer.Deserialize<Cart>(httpResponse,
                new JsonSerializerOptions(JsonSerializerDefaults.Web))!;

            decimal totalPrice = 0;
            foreach (var product in cart.Products!)
            {
                totalPrice += product.Price;
            }
            var order = new Order { Customer = cart.Customer!, Products = cart.Products!, IsCompleted = true, TotalPrice = totalPrice};

            uri = $"{_url}api/order/placeAnOrder?customerId={Request.Cookies["cid"]}";
            var requestBody = JsonSerializer.Serialize(order);
            await httpClient.PostAsync(uri,
                new StringContent(requestBody, Encoding.UTF8, "application/json"));

            return View(order);
        }
    }
}