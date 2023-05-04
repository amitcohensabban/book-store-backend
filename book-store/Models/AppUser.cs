using Microsoft.AspNetCore.Identity;

namespace book_store.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IList<BookModel>? Books { get; set; }
        public CartModel? Cart { get; set; }
    }
}
