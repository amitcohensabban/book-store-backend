using book_store.Models;
using book_store.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace book_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartRepository _cartRepository;


        public CartController(IBooksRepository booksRepository, UserManager<AppUser> userManager, ICartRepository cartRepository)
        {
            _booksRepository = booksRepository;
            _userManager = userManager;
            _cartRepository = cartRepository;
        }

        [HttpPost("add")]
        public async Task<ActionResult<CartModel>> AddBookToCart(string userId,string bookId)
        {
            var book = await _booksRepository.GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }

            var cart = await _cartRepository.AddBookToCartAsync(user, book);
            return cart;
        }
        [HttpDelete("remove")]
        public async Task<ActionResult<CartModel>> RemoveBookFromCart(string userId, string bookId)
        {
            var book = await _booksRepository.GetBookById(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }

            var cart = await _cartRepository.RemoveBookFromCartAsync(user, book);
            return cart;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(user.Id);
            if (cart == null || cart.Books.Count == 0)
            {
                return BadRequest("User cart is empty.");
            }

            await _cartRepository.CheckoutAsync(user, cart);

            return Ok(user.Books);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<CartModel>> GetCartByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized();
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(user.Id);
            return cart;
        }

    }
}
