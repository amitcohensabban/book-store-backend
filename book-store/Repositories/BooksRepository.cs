using book_store.Data;
using book_store.Models;
using Microsoft.EntityFrameworkCore;

namespace book_store.Repositories
{
    public class BooksRepository:IBooksRepository
    {
        private readonly BookStoreContext _context;
        public BooksRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books.Distinct().ToList();
        }
        public async Task<BookModel> GetBookById(string id)
        {
            /*var book = await _context.Books.FindAsync(id);*/
            var book = await _context.Books.Include(b => b.Author).Where(b => b.Id == id).FirstOrDefaultAsync();
            return book;
        }
        public async Task<string> AddBookAsync(NewBookModel newBookModel)
        {
            var existingAuthor = await _context.Authors.FindAsync(newBookModel.AuthorId);

            if (existingAuthor == null)
            {
                // If the author doesn't exist, you may choose to handle this situation as per your requirement.
                return "-1";
            }

            var newBook = new BookModel
            {
                Id = Guid.NewGuid().ToString(),
                Title = newBookModel.Title,
                Description = newBookModel.Description,
                Price = newBookModel.Price,
                Author = existingAuthor
            };

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }
        public async Task<List<BookModel>> GetBooksForUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var user = await _context.Users
                .Include(u => u.Books)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new ArgumentException($"User with Id {userId} does not exist");

            return user.Books.ToList();
        }

    }

}

