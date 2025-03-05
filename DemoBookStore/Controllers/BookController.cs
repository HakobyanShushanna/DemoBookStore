using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoBookStore.Data;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly DemoBookStoreContext _context;
        private readonly UserManager<UserModel> _userManager;
        public BookController(DemoBookStoreContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }
    }
}
