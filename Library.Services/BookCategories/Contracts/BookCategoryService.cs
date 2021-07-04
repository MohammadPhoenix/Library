using System.Threading.Tasks;

namespace Library.Services.BookCategories.Contracts
{
    public interface BookCategoryService
    {
        Task<short> Add(AddBookCategoryDto dto);
    }
}
