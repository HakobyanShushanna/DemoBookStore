using DemoBookStore.Data;
using DemoBookStore.Helpers;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookStore.Controllers
{
    public class OrderSessionController : Controller
    {
        private readonly DemoBookStoreContext _context;
        private readonly string SessionKey = "OrderSession";

        public OrderSessionController(DemoBookStoreContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var order = HttpContext.Session.GetObject<List<BookModel>>(SessionKey) ?? new List<BookModel>();
            
            Dictionary<int, int> counts = new Dictionary<int, int>();
            Dictionary<int, decimal> sums = new Dictionary<int, decimal>();
            List<BookModel> uniqueBooks = new List<BookModel>();

            foreach (var o in order)
            {
                if (counts.ContainsKey(o.Id))
                {
                    counts[o.Id] += 1;
                    sums[o.Id] += o.Price;
                }
                else
                {
                    counts.Add(o.Id, 1);
                    sums.Add(o.Id, o.Price);
                    uniqueBooks.Add(o);
                }
            }

            ViewBag.Counts = counts;
            ViewBag.Sums = sums;
            ViewBag.Sum = sums.Values.Sum();
            return View(uniqueBooks);
        }

        [Authorize]
        public IActionResult AddToOrder(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            var order = HttpContext.Session.GetObject<List<BookModel>>(SessionKey) ?? new List<BookModel>();
            order.Add(book);

            HttpContext.Session.SetObject(SessionKey, order);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoveFromOrder(int id)
        {
            var order = HttpContext.Session.GetObject<List<BookModel>>(SessionKey) ?? new List<BookModel>();
            var bookToRemove = order.FirstOrDefault(b => b.Id == id);

            if(bookToRemove != null)
            {
                order.Remove(bookToRemove);
                HttpContext.Session.SetObject(SessionKey, order);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult ClearOrder()
        {
            HttpContext.Session.Remove(SessionKey);
            return RedirectToAction("Index");
        }
    }
}
