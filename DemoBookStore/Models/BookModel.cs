namespace DemoBookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AuthorModel> Authors { get; set; } = new List<AuthorModel>();
        public string Genre { get; set; }
        public List<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
        public decimal Price { get; set; }
        public bool IsElectronic { get; set; }
        public bool IsAvailable { get; set; }
        public int? AgeRestriction { get; set; }
        public List<OrderModel> Orders { get; set; } = new List<OrderModel>();
    }
}
