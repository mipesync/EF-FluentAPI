using EF_FluentAPI__Front_.JsonDeserializer;
using EF_FluentAPI__Front_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace EF_FluentAPI__Front_.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _url;

        public AuthController(IConfiguration configuration)
        {
            _url = configuration.GetValue<string>("ApiConnectionString:ApiUrl");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] Credential credentialData)
        {
            using (HttpClient httpClient = new())
            {
                string uri = $"{_url}api/auth/login",
                    requestBody = JsonSerializer.Serialize(credentialData);

                var httpResponse = await httpClient.PostAsync(uri,
                    new StringContent(requestBody, Encoding.UTF8, "application/json"));

                if (((int)httpResponse.StatusCode) != 200)
                {
                    ViewData["ErrorMessage"] = new Deserializer().Deserialize(httpResponse.Content.ReadAsStringAsync().Result);
                    return View();
                }

                var token = httpResponse.Headers.FirstOrDefault(u => u.Key == "access_token").Value.FirstOrDefault();
                Response.Cookies.Append("access_token", token!);

                return Redirect("~/");
            }
        }

        [HttpGet("sign-up")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp([FromForm] Credential credentialData)
        {
            using (HttpClient httpClient = new())
            {
                string uri = $"{_url}api/auth/sign-up",
                    requestBody = JsonSerializer.Serialize(credentialData);

                var httpResponse = await httpClient.PostAsync(uri,
                    new StringContent(requestBody, Encoding.UTF8, "application/json"));

                if (((int)httpResponse.StatusCode) != 200) 
                {
                    ViewData["ErrorMessage"] = new Deserializer().Deserialize(httpResponse.Content.ReadAsStringAsync().Result);
                    return View();
                }

                var token = httpResponse.Headers.FirstOrDefault(u => u.Key == "access_token").Value.FirstOrDefault();
                Response.Cookies.Append("access_token", token!);

                return Redirect($"~/create");
            }
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            return View();
        }

        [HttpPost("logout")]
        public ActionResult LogOutConfirmed()
        {
            Response.Cookies.Delete("access_token");
            return Redirect("~/login");
        }
    }
}
