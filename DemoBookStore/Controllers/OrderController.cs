using DemoBookStore.Data;
using DemoBookStore.Helpers;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoBookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly DemoBookStoreContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly string SessionKey = "OrderSession";

        public OrderController(DemoBookStoreContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PlaceOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var orderBooks = HttpContext.Session.GetObject<List<BookModel>>(SessionKey);
            if (!orderBooks.Any()) return BadRequest("Your order is empty");

            OrderModel newOrder = new OrderModel
            {
                User = user,
                Books = orderBooks.Select(b => _context.Books.Find(b.Id)).ToList(),
                Date = DateTime.Now
            };

            _context.OrderModel.Add(newOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewOrders()
        {
            return View(await _context.OrderModel.ToListAsync());
        }
    }
}
