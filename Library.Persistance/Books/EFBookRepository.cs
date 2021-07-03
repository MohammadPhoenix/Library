using Library.Entites;
using Library.Services.Books.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistance.EF.Books
{
    public class EFBookRepository : BookRepository
    {
        private readonly EFDataContext _context;

        public EFBookRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public Book Find(int bookId)
        {
            return _context.Books.FirstOrDefault(_ => _.Id == bookId);
        }

        public async Task<List<GetBookDto>> GetAllBooksWithThisCategory(short bookCategoryId)
        {
            return await (from book in _context.Books
                          where book.BookCategoryId == bookCategoryId
                          select new GetBookDto
                          {
                              Title = book.Title,
                              Writer = book.Writer
                          }).ToListAsync();
        }

        public bool IsExistThisBookById(int bookId)
        {
            return _context.Books.FirstOrDefault(_ => _.Id == bookId) == null;
        }

       
    }
}
