using Library.Entites;
using Library.Persistance.EF;
using Library.Persistance.EF.BookCategories;
using Library.Services.BookCategories;
using Library.Services.BookCategories.Contracts;

namespace Library.Test.Tools.BookCategories
{
    public static class BookCategoryFactory
    {
        public static BookCategoryAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWorkRepository(context);
            var repository = new EFBookCategoryRepository(context);
            return new BookCategoryAppService(repository, unitOfWork);
        }
        public static AddBookCategoryDto GenerateBookCategoryWithTitleDto(EFDataContext context, string Title = "ScienceFiction")
        {
            var bookCategory = new AddBookCategoryDto
            {
                Title = Title
            };
            return bookCategory;
        }
        public static short AddBookCategoryWithTitle(EFDataContext context, string Title = "ScienceFiction")
        {
            var bookCategory = new BookCategory
            {
                Title = Title
            };
            context.BookCategories.Add(bookCategory);
            context.SaveChanges();
            return bookCategory.Id;
        }
    }
}
