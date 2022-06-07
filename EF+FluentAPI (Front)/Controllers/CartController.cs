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
    
    [HttpPost("cartOperate")]
    public async Task<IActionResult> CartOperate(string id, bool isRemoved)
    {            
        using (HttpClient httpClient = new())
        {
            string uri = $"{_url}api/cart/cartOperate?id={id}&isRemoved={isRemoved}";
            
            SetDefaultHeaders(httpClient);
            var httpResponse = await httpClient.PostAsync(uri, null);

            var psid = httpResponse.Headers.FirstOrDefault(u => u.Key == "psid").Value?.FirstOrDefault();
            var count = httpResponse.Headers.FirstOrDefault(u => u.Key == "count").Value?.FirstOrDefault();
            Response.Cookies.Append("psid", psid == null ? "" : psid);
            Response.Cookies.Append("count", count == null ? "" : count);

            ViewData["CartLenght"] = count;

            return Ok();
        }
    }
    
    [HttpGet("cart")]
    public async Task<IActionResult> GetFromCookies()
    {
        using (HttpClient httpClient = new())
        {
            string uri = $"{_url}api/cart/getFromCookie";

            SetDefaultHeaders(httpClient);
            var httpResponse = await httpClient.GetStringAsync(uri);

            var order = JsonSerializer.Deserialize<OrderDto>(httpResponse,
                new JsonSerializerOptions(JsonSerializerDefaults.Web))!;

            return View(order);
        }
    }

    private HttpClient SetDefaultHeaders(HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Add("psid", Request.Cookies["psid"]);
        httpClient.DefaultRequestHeaders.Add("count", Request.Cookies["count"]);
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");

        return httpClient;
    } 
}