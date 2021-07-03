using Library.Persistance.EF;
using Library.Persistance.EF.BookCategories;
using Library.Persistance.EF.Books;
using Library.Services.Books;
using Library.Services.Books.Contracts;

namespace Library.Test.Tools.Books
{
    public class BookFactory
    {
        public static BookAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWorkRepository(context);
            var repository = new EFBookRepository(context);
            var categoryRepository = new EFBookCategoryRepository(context);
            return new BookAppService(repository, unitOfWork, categoryRepository);
        }

        public static AddBookDto GenerateBookDtoForAdding(string title = "TravelToDeppEarth", string writer = "ZholVern", short bookCategoryId = 0, byte maxAge = 80, byte minAge = 16)
        {
            return new AddBookDto
            {
                Title = title,
                BookCategoryId = bookCategoryId,
                Writer = writer,
                MaximumAge = maxAge,
                MinimumAge = minAge
            };
        }
    }
}
