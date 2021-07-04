using Library.Entites;

namespace Library.Services.BookCategories.Contracts
{
    public interface BookCategoryRepository
    {
        void Add(BookCategory bookCategory);
        bool IsExistBookCategoryWithThisTitle(string title);
        bool IsThisBookCategoryExist(short bookCategoryId);
    }
}
