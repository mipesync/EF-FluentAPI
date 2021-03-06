using EF_FluentAPI__Front_.JsonDeserializer;
using EF_FluentAPI__Front_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace EF_FluentAPI__Front_.Controllers
{
    public class CustomerController : Controller
    {
        private readonly string _url;

        public CustomerController(IConfiguration configuration)
        {
            _url = configuration.GetValue<string>("ApiConnectionString:ApiUrl");
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }        
        
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] Customer customerData)
        {
            using (HttpClient httpClient = new())
            {
                string uri = $"{_url}api/customer/create",
                    requestBody = JsonSerializer.Serialize(customerData);
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");
                var httpResponse = await httpClient.PostAsync(uri,
                    new StringContent(requestBody, Encoding.UTF8, "application/json"));

                if ((int)httpResponse.StatusCode != 200)
                {
                    ViewData["ErrorMessage"] = new Deserializer().Deserialize(httpResponse.Content.ReadAsStringAsync().Result);
                    return View();
                }
                Response.Cookies.Append("cid", customerData.Id, new CookieOptions { Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)) });

                return Redirect("~/");
            }
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            using (HttpClient httpClient = new())
            {
                string uri = $"{_url}api/customer/getById?id={Request.Cookies["cid"]}";
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["access_token"]}");
                var httpResponse = await httpClient.GetStringAsync(uri);
                
                var customer = JsonSerializer.Deserialize<Customer>(httpResponse,
                    new JsonSerializerOptions(JsonSerializerDefaults.Web))!;

                return View(customer);
            }
        }
    }
}
