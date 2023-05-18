using book_store.Data;
using book_store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace book_store.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly BookStoreContext _context;
        public CartRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<CartModel> AddBookToCartAsync(AppUser user, BookModel book)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var cart = await _context.Carts.Include(c => c.Books)
                                             .FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (cart == null)
            {
                cart = new CartModel
                {
                    UserId = user.Id,
                    Books = new List<BookModel>(),
                    TotalBooks = 0,
                    TotalPrice = 0
                };
                book.Quantity++;
                cart.Books.Add(book);
                cart.TotalBooks++;
                cart.TotalPrice += book.Price;
                _context.Carts.Add(cart);
            }
            else
            {
                var existingBook = cart.Books.FirstOrDefault(b => b.Id == book.Id);
                if (existingBook != null)
                {
                    existingBook.Quantity++;
                    cart.TotalBooks++;
                    cart.TotalPrice += book.Price;
                }
                else
                {
                    cart.TotalBooks++;
                    cart.TotalPrice += book.Price;
                    book.Quantity++;
                    cart.Books.Add(book);
                }
            }
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<CartModel> RemoveBookFromCartAsync(AppUser user, BookModel book)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (book == null)
                throw new ArgumentNullException(nameof(book));

            var cart = await _context.Carts.Include(c => c.Books)
                                             .FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (cart == null)
            {
                throw new InvalidOperationException("User cart not found.");
            }

            var existingBook = cart.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook == null)
            {
                throw new InvalidOperationException("Book not found in cart.");
            }

            if (existingBook.Quantity > 1)
            {
                existingBook.Quantity--;
                cart.TotalBooks--;
                cart.TotalPrice -= book.Price;
            }
            else
            {
                cart.TotalBooks--;
                cart.TotalPrice -= book.Price;
                cart.Books.Remove(existingBook);
            }

            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<CartModel> GetCartByUserIdAsync(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var cart = await _context.Carts
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return cart;
        }

        public async Task<IList<BookModel>> CheckoutAsync(AppUser user, CartModel cart)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            foreach (var book in cart.Books)
            {
                if (user.Books == null)
                {
                    user.Books = new List<BookModel>();
                }

                user.Books.Add(book);
                book.Quantity--;
            }

            cart.TotalBooks = 0;
            cart.TotalPrice = 0;
            cart.Books.Clear();

            await _context.SaveChangesAsync();

            return user.Books;


        }


    }
}
