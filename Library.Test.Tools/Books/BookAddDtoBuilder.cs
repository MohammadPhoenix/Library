using Library.Entites;
using Library.Persistance.EF;
using Library.Services.Books.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Test.Tools.Books
{
    public class BookAddDtoBuilder
    {
        private AddBookDto book = new AddBookDto
        {
            Title = "TravelToDeppEarth",
            BookCategoryId = 0,
            Writer = "ZholVern",
            MaximumAge = 80,
            MinimumAge = 16
        };

        public  BookAddDtoBuilder WithTitle(string title)
        {
            book.Title = title;
            return this;
        }

        public BookAddDtoBuilder WithTiltle(string writer)
        {
            book.Writer = writer;
            return this;
        }

        public BookAddDtoBuilder WithBookCategoryId(short bookCategoryId)
        {
            book.BookCategoryId = bookCategoryId;
            return this;
        }
        public BookAddDtoBuilder WithMinAge(byte minAge)
        {
            book.MinimumAge = minAge;
            return this;
        }
        public BookAddDtoBuilder WithMaxAge(byte maxAge)
        {
            book.MaximumAge = maxAge;
            return this;
        }
        public AddBookDto Build() {
            return book;
        }
        
    }
}
