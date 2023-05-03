using System.ComponentModel.DataAnnotations;

namespace book_store.Models
{
    public class NewBookModel
    {
        [Required(ErrorMessage = "Please add a title")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Please add the author id")]
        public string AuthorId { get; set; }
        public double Price { get; set; }
    }
}
