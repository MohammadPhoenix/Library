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

namespace Library.Services.Tests.Spec.Books.Add
{
    [Scenario("ثبت یک کتاب جدید")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private BookService sut;
        private short bookCategoryId;
        private AddBookDto bookDto;
        private int bookId;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            sut = BookFactory.CreateService(_context);
        }
        [Given("یک دسته بندی با عنوان علمی تخیلی وجود دارد")]
        private void Given()
        {
          bookCategoryId = BookCategoryFactory.AddBookCategoryWithTitle(_context , "ScienceFiction");
        }
        [When(" کتابی با عنوان سفر به اعماق زمین و نویسنده ژول ورن " +
            "در دسته بندی علمی تخیلی و با رده سنی 16 تا 80 سال اضافه می نمایم")]
        private async Task When()
        {
            bookDto = new AddBookDto {
                Title = "TravelToDeppEarth",
                BookCategoryId = bookCategoryId,
                Writer = "ZholVern",
                MaximumAge = 80,
                MinimumAge = 16
            };
            bookId = await sut.Add(bookDto);
        }
        [Then("در فهرست کتاب ها باید تنها یک کتاب با عنوان سفر به اعماق زمین و" +
            " نویسنده ژول ورن در دسته بندی علمی تخیلی و با رده سنی 16 تا 80 سال وجود داشت باشد  ")]
        private void Then()
        {
            var expected = _context.Books.Single(c => c.Id == bookId);
            expected.BookCategoryId.Should().Be(bookDto.BookCategoryId);
            expected.Title.Should().Be(bookDto.Title);
            expected.Writer.Should().Be(bookDto.Writer);
            expected.MaximumAge.Should().Be(bookDto.MaximumAge);
            expected.MinimumAge.Should().Be(bookDto.MinimumAge);
        }
        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then());
        }
    }
}
