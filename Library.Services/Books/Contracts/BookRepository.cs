using Library.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Books.Contracts
{
    public interface BookRepository
    {
        void Add(Book dto);
        Book Find(int bookId);
        Task<List<GetBookDto>> GetAllBooksWithThisCategory(short bookCategoryId);
        bool IsExistThisBookById(int bookId);
    }
}
