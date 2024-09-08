using Microsoft.AspNetCore.Mvc;

namespace MessageExchange.API.Controllers;

public class MessageViewController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}