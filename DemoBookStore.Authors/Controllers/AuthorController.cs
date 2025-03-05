using Microsoft.AspNetCore.Mvc;
using DemoBookStore.Data;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace DemoBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly DemoBookStoreContext _context;
        private readonly UserManager<AuthorModel> _userManager;
        private readonly SignInManager<AuthorModel> _signInManager;

        public AuthorController(DemoBookStoreContext context, UserManager<AuthorModel> userManager,
            SignInManager<AuthorModel> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            return View();
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AverageScore,Id,Firstname,Lastname,Email,Password")] AuthorModel authorModel, string password)
        {
            authorModel.LockoutEnabled = false;
            authorModel.NormalizedEmail = _userManager.NormalizeEmail(authorModel.Email);
            authorModel.NormalizedUserName = authorModel.Email; // Firstname + Lastname
            authorModel.PasswordHash = password;

            ModelState.Remove("Reviews"); // optional
            ModelState.Remove("Orders");

            if (ModelState.IsValid)
            {
                var user = new AuthorModel
                {
                    UserName = authorModel.Email,
                    Email = authorModel.Email,
                    Firstname = authorModel.Firstname,
                    Lastname = authorModel.Lastname,
                    NormalizedEmail = authorModel.NormalizedEmail,
                    PasswordHash = authorModel.PasswordHash
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
            return View(authorModel);
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorModel = await _context.Authors.FindAsync(id);
            if (authorModel == null)
            {
                return NotFound();
            }
            return View(authorModel);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("AverageScore,Id,Firstname,Lastname,Email,Password")] AuthorModel authorModel)
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        private bool AuthorModelExists(string id)
        {
            return false;
        }
    }
}
