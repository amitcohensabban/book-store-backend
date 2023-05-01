﻿namespace book_store.Models
{
    public class AuthorModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public IList<BookModel>? Books { get; set; }
    }
}
