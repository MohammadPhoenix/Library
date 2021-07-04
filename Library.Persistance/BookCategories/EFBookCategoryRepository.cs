using Library.Entites;
using Library.Services.BookCategories.Contracts;
using System.Linq;

namespace Library.Persistance.EF.BookCategories
{
    public class EFBookCategoryRepository : BookCategoryRepository
    {
        private readonly EFDataContext _context;

        public EFBookCategoryRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(BookCategory bookCategory)
        {
            _context.BookCategories.Add(bookCategory);
        }

        public bool IsExistBookCategoryWithThisTitle(string title)
        {
            return _context.BookCategories.FirstOrDefault(_ => _.Title == title) != null;
        }
        public bool IsThisBookCategoryExist(short bookCategoryId)
        {
            return _context.BookCategories.FirstOrDefault(_ => _.Id == bookCategoryId) != null;
        }
    }
}
