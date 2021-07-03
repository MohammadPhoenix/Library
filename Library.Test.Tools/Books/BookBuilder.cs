using Library.Entites;
using Library.Persistance.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Test.Tools.Books
{
    public class BookBuilder
    {
        private readonly EFDataContext _context;
        public BookBuilder(EFDataContext context)
        {
            _context = context;
        }
        private Book book = new Book
        {
            Title = "TravelToDeppEarth",
            BookCategoryId = 0,
            Writer = "ZholVern",
            MaximumAge = 80,
            MinimumAge = 16,
           // BookCategory = new BookCategory() { Title = "ScienceFiction" }
        };
        public BookBuilder WithTitleAndBookCategoryId(string title = "TravelToDeppEarth", short categoryId=0)
        {
            book.Title = title;
            book.BookCategoryId = categoryId;
            return this;
        }
        public BookBuilder WithWriter(string writer)
        {
            book.Writer = writer;
            return this;
        }
        public BookBuilder WithMinAndMaxAge(byte minAge, byte maxAge)
        {
            book.MinimumAge = minAge;
            book.MaximumAge = maxAge;
            return this;
        }
        public int Build()
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book.Id;
        }
    }
}
