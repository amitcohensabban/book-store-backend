namespace book_store.Models
{
    public class CartModel
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public IList<BookModel>? Books { get; set; }
        public int? TotalBooks { get; set; }
        public double? TotalPrice { get; set; }
         
    }
}
