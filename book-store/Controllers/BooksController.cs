﻿using book_store.Models;
using book_store.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace book_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }
        [HttpGet("")]
        //[Authorize]
        public async Task<IActionResult> GetAllBooks()
        {
            var res = await _booksRepository.GetAllBooksAsync();
            if (res?.Count > 0)
            {
                return Ok(res);
            }
            return NotFound();
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] NewBookModel newBookModel)
        {
            var createdBookId = await _booksRepository.AddBookAsync(newBookModel);
            return Ok("added book");
        }
    }
}