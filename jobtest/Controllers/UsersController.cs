using Microsoft.AspNetCore.Mvc;

namespace jobtest.Controllers;

public class UsersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}