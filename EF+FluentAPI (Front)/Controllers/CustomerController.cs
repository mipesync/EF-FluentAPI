using EF_FluentAPI__Front_.JsonDeserializer;
using EF_FluentAPI__Front_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

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
                    requestBody = JsonSerializer.Serialize(new { customer = customerData, username = HttpContext.Session.GetString("uname") });

                var httpResponse = await httpClient.PostAsync(uri,
                    new StringContent(requestBody, Encoding.UTF8, "application/json"));

                if (((int)httpResponse.StatusCode) != 200)
                {
                    ViewData["ErrorMessage"] = new Deserializer().Deserialize(httpResponse.Content.ReadAsStringAsync().Result);
                    return View();
                }

                return Redirect("~/login");
            }
        }
    }
}
