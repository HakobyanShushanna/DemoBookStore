using Microsoft.AspNetCore.Identity;

namespace DemoBookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<BookModel> Items { get; set; }
    }
}
