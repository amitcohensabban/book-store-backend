using book_store.Models;

namespace book_store.Repositories
{
    public interface ICartRepository
    {
        Task<CartModel> AddBookToCartAsync(AppUser user, BookModel book);
        Task<CartModel> RemoveBookFromCartAsync(AppUser user, BookModel book);
    }
}
