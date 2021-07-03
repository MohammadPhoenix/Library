using FluentAssertions;
using Library.Infrastructure.Test;
using Library.Persistance.EF;
using Library.Services.BookCategories;
using Library.Services.BookCategories.Exceptions;
using Library.Test.Tools.BookCategories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.BookCategories
{
    public class BookCategoryServiceTests
    {
        private readonly EFDataContext _context;
        private BookCategoryAppService _sut;

        public BookCategoryServiceTests()
        {

            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _sut = BookCategoryFactory.CreateService(_context);
        }
        [Fact]
        public void Throw_exceptin_when_thisBookCategory_is_exist()
        {
            BookCategoryFactory.AddBookCategoryWithTitle(_context, "ScienceFiction");
            var bookCategory = BookCategoryFactory.GenerateBookCategoryWithTitleDto(_context, "ScienceFiction");

            Func<Task> expected = () => _sut.Add(bookCategory);

            expected.Should().ThrowExactly<ThisBookCategoryIsExistException>();
        }
    }
}
