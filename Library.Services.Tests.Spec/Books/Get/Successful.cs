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

namespace Library.Services.Tests.Spec.Books.Get
{
    [Scenario("نمایش فهرست های کتاب های یک دسته بندی خاص")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private BookService sut;
        private short bookCategoryId;
        private int bookId;
        private List<GetBookDto> booklistinthiscategory;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            sut = BookFactory.CreateService(_context);
        }
        [Given("در دسته بندی علمی تخیلی تنها یک کتاب با عنوان سفر اعماق زمین وجود دارد")]
        private void Given()
        {
             bookCategoryId = BookCategoryFactory.AddBookCategoryWithTitle(_context , "ScienceFiction");
             bookId = new BookBuilder(_context)
                .WithTitleAndBookCategoryId("TravelToDeepEarth",bookCategoryId)
                .Build();
        }
        [When("فهرست کتاب های با دسته بندی علمی را مشاهده می کنم")]
        private async Task When()
        {
          booklistinthiscategory = await sut.GetAllBooksInThisCategory(bookCategoryId);
        }
        [Then("تنها یک کتاب با عنوان سفر به اعماق زمین در فهرست کتاب های دسته بندی علمی تخیلی باید وجود داشت باشد")]
        private void Then()
        {
            var expected = _context.Books.Single(_=>_.BookCategoryId == bookCategoryId);
            expected.Id.Should().Be(bookId);
            expected.Title.Should().Be(booklistinthiscategory[0].Title);
            expected.Writer.Should().Be(booklistinthiscategory[0].Writer);
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
