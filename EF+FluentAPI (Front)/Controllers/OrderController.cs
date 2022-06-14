using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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
            await httpClient.PostAsync(uri,
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
            
            try
            {
                var httpResponse = await httpClient.GetStringAsync(uri);
                var order = JsonSerializer.Deserialize<Order>(httpResponse,
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return View(order);
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Заказ не найден!";
                return View("~/Views/Order/FindOrder.cshtml");
            }
        }
    }

    [HttpGet("find-order")]
    public ActionResult FindOrder()
    {
        return View();
    }

    [HttpPost("finder")]
    public async Task<IActionResult> Finder([FromForm] string number)
    {
        if (Regex.IsMatch(number, @"^\d{6}$"))
            return RedirectToAction("Details", routeValues: new {name = number});
        else if (Regex.IsMatch(number, @"^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$"))
        {
            using (HttpClient httpClient = new())
            {
                if (Regex.IsMatch(number, @"^\d{11}$"))
                    number = number.Substring(1).Insert(0, "+7");
                string uri = $"{_url}api/order/getByCustomerPhone?customerPhone={number}";
                var httpResponse = await httpClient.GetStringAsync(uri);

                var orders = JsonSerializer.Deserialize<ICollection<Order>>(httpResponse,
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));
                if (orders!.Count == 0)
                {
                    ViewData["ErrorMessage"] = "На этот номер не совершались заказы!";
                    return View("~/Views/Order/FindOrder.cshtml");
                }
                
                return View("~/Views/Order/DetailsByPhone.cshtml", orders);
            }
        }
        else
        {
            ViewData["ErrorMessage"] = "Неверный формат данных!";
            return View("~/Views/Order/FindOrder.cshtml");
        }
    }
}