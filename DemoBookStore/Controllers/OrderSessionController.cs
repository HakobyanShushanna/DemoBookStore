using DemoBookStore.Data;
using DemoBookStore.Helpers;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookStore.Controllers
{
    public class OrderSessionController : Controller
    {
        private readonly DemoBookStoreContext _context;
        private const string SessionKey = "OrderSession";

        public OrderSessionController(DemoBookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var order = HttpContext.Session.GetObject<List<BookModel>>(SessionKey) ?? new List<BookModel>();
            return View(order);
        }

        [HttpPost]
        public IActionResult AddToOrder(int id)
        {
            var book = _context.Books.Find(id);
            if(book == null) return NotFound();

            var order = HttpContext.Session.GetObject<List<BookModel>>("SessionKey") ?? new List<BookModel>();
            order.Add(book);

            HttpContext.Session.SetObject(SessionKey, order);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromOrder(int id)
        {
            var order = HttpContext.Session.GetObject<List<BookModel>>(SessionKey) ?? new List<BookModel> { new BookModel() };
            var bookToRemove = order.FirstOrDefault(b => b.Id == id);

            if(bookToRemove != null)
            {
                order.Remove(bookToRemove);
                HttpContext.Session.SetObject(SessionKey, order);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearOrder()
        {
            HttpContext.Session.Remove(SessionKey);
            return RedirectToAction("Index");
        }
    }
}
