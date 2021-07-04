using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Books.Contracts
{
    public interface BookService
    {
        Task<int> Add(AddBookDto createBookDto);
        Task Update(int bookId, UpdateBookDto updateBookDto);
        Task<List<GetBookDto>> GetAllBooksInThisCategory(short bookCategoryId);
    }
}
