namespace DemoBookStore.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Stars { get; set; }
        public UserModel User { get; set; } = new UserModel();
        public BookModel Book { get; set; } = new BookModel();
        public string Description { get; set; }
    }
}