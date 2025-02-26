using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoBookStore.Data;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace DemoBookStore.Controllers
{
    public class UserController : Controller
    {
        private readonly DemoBookStoreContext _context;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly UserManager<UserModel> _userManager;

        public UserController(DemoBookStoreContext context, SignInManager<UserModel> signInManager, 
            UserManager<UserModel> userManager)
        {
            _context = context;
            _signInManager = signInManager; 
            _userManager = userManager;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View();
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Age,Address,Id,Firstname,Lastname,Email,Password")] UserModel userModel, string password)
        {
            userModel.LockoutEnabled = false;
            userModel.NormalizedEmail = _userManager.NormalizeEmail(userModel.Email);
            userModel.NormalizedUserName = userModel.Email; // Firstname + Lastname
            userModel.PasswordHash = password;

            ModelState.Remove("Reviews"); // optional
            ModelState.Remove("Orders");

            if (ModelState.IsValid)
            {
                var user = new UserModel
                {
                    UserName = userModel.Email,
                    Email = userModel.Email,
                    Firstname = userModel.Firstname,
                    Lastname = userModel.Lastname,
                    Age = userModel.Age,
                    Address = userModel.Address,
                    NormalizedEmail = userModel.NormalizedEmail,
                    PasswordHash = userModel.PasswordHash
                };

                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Age,Address,Id,Firstname,Lastname,Email,Password")] UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userModel = await _context.Users.FindAsync(id);
            if (userModel != null)
            {
                _context.Users.Remove(userModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET
        public IActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or Password.");
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found.");
            }

            return View();
        }

        private bool UserModelExists(string id)
        {
            return _context.Users.FirstOrDefaultAsync(user => user.Id == id) != null;
        }
    }
}
