using FluentAssertions;
using Library.Infrastructure.Test;
using Library.Persistance.EF;
using Library.Services.Books;
using Library.Services.Books.Exceptions;
using Library.Test.Tools.Books;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.Books
{
    public class BookServiceTests
    {
        private readonly EFDataContext _context;
        private readonly BookAppService _sut;
        public BookServiceTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _sut = BookFactory.CreateService(_context);
        }
        [Fact]
        public void Throw_exception_when_bookCategory_notExist()
        {
            var dto = BookFactory.GenerateBookDtoForAdding(bookCategoryId: -1);

            Func<Task> expected = () => _sut.Add(dto);

            expected.Should().ThrowExactly<ThisBookCategoryNotFoundException>();
        }
    }
}
