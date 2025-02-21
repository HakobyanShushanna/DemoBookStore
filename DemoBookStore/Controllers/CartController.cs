using DemoBookStore.Helpers;
using DemoBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DemoBookStore.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "ShoppingCart";

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
            return View(cart);
        }

        public IActionResult AddToCart(int id, string name, decimal price)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(i => i.Book.Id == id);
            
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem { Book = new BookModel(), Quantity = 1 });
            }

            HttpContext.Session.SetObject(CartSessionKey, cart);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(CartSessionKey);

            if (cart != null)
            {
                cart.RemoveAll(i => i.Id == id);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction("Index");
        }
    }
}
