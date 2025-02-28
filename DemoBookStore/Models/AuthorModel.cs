namespace DemoBookStore.Models
{
    public class AuthorModel:Person
    {
        public List<BookModel> Books { get; set; } = new List<BookModel>();
        public List<AwardModel> Awards { get; set; } = new List<AwardModel>();
        public double AverageScore { get; set; } = 0;
    }
}