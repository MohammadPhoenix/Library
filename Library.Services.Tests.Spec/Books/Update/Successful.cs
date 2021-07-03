using FluentAssertions;
using Library.Persistance.EF;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.BookCategories;
using Library.Test.Tools.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Books.Update
{
    [Scenario("ویرایش مشخصات یک کتاب")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly BookService sut;
        private short _categoryId;
        private int _bookId;
        private UpdateBookDto updateBookDto;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            sut = BookFactory.CreateService(_context);
        }
        [Given("تنها یک کتاب با عنوان سفر به اعماق زمین و نویسنده ژول ورن " +
            "در دسته بندی علمی تخیلی و با رده سنی16 تا 80 سال در فهرست کتابها وجود دارد ")]
        private void Given()
        {
            _categoryId = BookCategoryFactory.AddBookCategoryWithTitle(_context, "ScienceFiction");
            _bookId = new BookBuilder(_context)
                .WithTitleAndBookCategoryId("TravelToDeppEarth", _categoryId)
                .WithWriter("ZholVern")
                .WithMinAndMaxAge(16, 80).Build();
        }
        [When("عنوان کتاب را به شهر شناور ویرایش می کنم")]
        private async Task  When()
        {
            updateBookDto = new UpdateBookDto()
            {
                Title = "FloatingCity",
                Writer = "ZholVern",
                BookCategoryId = _categoryId,
                MinimumAge = 16,
                MaximumAge = 80
            
            };
            await sut.Update(_bookId, updateBookDto);
        }
        [Then("باید در فهرست کتاب ها تنها یک کتاب با عنوان شهر شناور و نویسنده ژول ورن " +
            "در دسته بندی علمی تخیلی و با رده سنی 16 تا 80 سال وجود داشته باشد")]
        private void Then()
        {
            var expected = _context.Books.Single(_ => _.Id == _bookId);
            expected.Title.Should().Be(updateBookDto.Title);
            expected.Writer.Should().Be(updateBookDto.Writer);
            expected.MaximumAge.Should().Be(updateBookDto.MaximumAge);
            expected.MinimumAge.Should().Be(updateBookDto.MinimumAge);
            expected.BookCategoryId.Should().Be(updateBookDto.BookCategoryId);
        }
        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _=>Given(),
                _=>When().Wait(),
                _=>Then());
        }

    }
}
