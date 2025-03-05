namespace DemoBookStore.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public DateTime Date { get; set; }
        public List<BookModel> Books { get; set; } = new List<BookModel>();
    }
}