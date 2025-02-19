using Microsoft.AspNetCore.Mvc;

namespace DemoBookStore.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
