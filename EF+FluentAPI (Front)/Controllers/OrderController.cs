using Microsoft.AspNetCore.Mvc;

namespace EF_FluentAPI__Front_.Controllers;

public class OrderController : Controller
{
    private readonly string _url;

    public OrderController(IConfiguration configuration)
    {
        _url = configuration.GetValue<string>("ApiConnectionString:ApiUrl");
    }
    
    public IActionResult Index()
    {
        return View();
    }
}