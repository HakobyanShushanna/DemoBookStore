using DemoBookStore.Data;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoBookStore.Helpers;

namespace DemoBookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly DemoBookStoreContext _context;
        private readonly UserManager<UserModel> _userManager;

        public OrderController(DemoBookStoreContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null) return Unauthorized();

            var orders = await _context.Orders
                .Where(o => o.User == user)
                .Include(o=>o.Books)
                .OrderByDescending(o=>o.Date)
                .ToListAsync();

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var user = await _userManager.GetUserAsync (User);
            if (user == null) return Unauthorized();

            var orderBooks = HttpContext.Session.GetObject<List<BookModel>>("OrderSession");
            if(!orderBooks.Any()) return BadRequest("Your order is empty.");

            var newOrder = new OrderModel
            {
                User = user,
                Books = orderBooks
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("OrderSession");

            return RedirectToAction(nameof(MyOrders));
        }
    }
}
