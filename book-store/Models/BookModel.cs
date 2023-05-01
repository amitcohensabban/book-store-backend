namespace book_store.Models
{
    public class BookModel
    {
        public  string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public AuthorModel? Author { get; set; }
        public double? Price { get; set; }

    }
}
