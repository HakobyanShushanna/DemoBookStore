using DemoBookStore.Data;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoBookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AuthorModel> _signInManagerUser;
        private readonly UserManager<AuthorModel> _userManager;
        private readonly DemoBookStoreContext _context;

        public AccountController(SignInManager<AuthorModel> signInManager, UserManager<AuthorModel> userManager, 
            DemoBookStoreContext context)
        {
            _signInManagerUser = signInManager;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
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
            await _signInManagerUser.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                AuthorModel? userToRemove = await _context.Authors.FirstOrDefaultAsync(u => u.Id == user.Id);
                if (userToRemove != null)
                {
                    _context.Authors.Remove(userToRemove);
                    await _context.SaveChangesAsync();
                    LogoutConfirmed();
                }
            }

            return BadRequest("Something went wrong");
        }
    }
}
