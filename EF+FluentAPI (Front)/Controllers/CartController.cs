using EF_FluentAPI__Front_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using EF_FluentAPI__Front_.JsonDeserializer;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EF_FluentAPI__Front_.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly string _url;

    public CartController(IConfiguration configuration)
    {
        _url = configuration.GetValue<string>("ApiConnectionString:ApiUrl");
    }
    
    public async Task<IActionResult> AddToCart(string id)
    {            
        using (HttpClient httpClient = new())
        {
            string uri = $"{_url}api/cart/addToCart?id={id}&customerId={Request.Cookies["cid"]}";

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");
            await httpClient.PostAsync(uri, null);

            return Redirect("~/");
        }
    }
    
    public async Task<IActionResult> RemoveFromCart(string id)
    {            
        using (HttpClient httpClient = new())
        {
            string uri = $"{_url}api/cart/removeFromCart?id={id}&customerId={Request.Cookies["cid"]}";

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");
            await httpClient.PostAsync(uri, null);

            return Redirect("~/cart");
        }
    }
    
    [HttpGet("cart")]
    public async Task<IActionResult> GetCart()
    {
        using (HttpClient httpClient = new())
        {
            string uri = $"{_url}api/cart/getCart?customerId={Request.Cookies["cid"]}";

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");

            var httpResponse = await httpClient.GetStringAsync(uri);

            var cart = JsonSerializer.Deserialize<Cart>(httpResponse,
                new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
            
            return View(cart);
        }
    }
}