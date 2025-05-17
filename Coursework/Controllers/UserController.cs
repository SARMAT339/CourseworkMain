using Microsoft.AspNetCore.Mvc;

namespace Coursework.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
