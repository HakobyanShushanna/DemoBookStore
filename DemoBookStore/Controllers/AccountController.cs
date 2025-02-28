using DemoBookStore.Data;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoBookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserModel> _signInManagerUser;
        // private readonly UserManager<AuthorModel> _authorManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly DemoBookStoreContext _context;

        public AccountController(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager, 
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
                UserModel? userToRemove = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
                if (userToRemove != null)
                {
                    List<OrderModel> orderModels = await _context.OrderModel.ToListAsync();
                    foreach (OrderModel orderModel in orderModels)
                    {
                        if(orderModel.User.Id == userToRemove.Id)
                        {
                            _context.OrderModel.Remove(orderModel);
                        }
                    }
                    _context.Users.Remove(userToRemove);
                    await _context.SaveChangesAsync();
                    LogoutConfirmed();
                }
            }

            return BadRequest("Something went wrong");
        }
    }
}
