using book_store.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace book_store.Repositories
{
    public interface IBooksRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookById(string id);
        Task<string> AddBookAsync(NewBookModel newBookModel);
        Task<List<BookModel>> GetBooksForUserAsync(string userId);
    }
}
