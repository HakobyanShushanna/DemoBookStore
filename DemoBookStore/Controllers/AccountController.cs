using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;

        public AccountController(SignInManager<UserModel> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Home", new { area = "Common" });
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }

        public async Task<IActionResult> LogoutConfirmed()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
