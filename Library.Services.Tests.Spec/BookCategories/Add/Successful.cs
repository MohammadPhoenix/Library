using FluentAssertions;
using Library.Entites;
using Library.Persistance.EF;
using Library.Services.BookCategories.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.BookCategories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.BookCategories.Add
{
    [Scenario("ثبت یک دسته بندی")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private BookCategoryService sut;
        private AddBookCategoryDto bookCategory;
        private short bookCategoryId;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            sut = BookCategoryFactory.CreateService(_context);
        }
        [Given("هیچ دسته بندی ای وجود ندارد")]
        private void Given()
        {
        }
        [When("یک دسته بندی با عنوان علمی تخیلی اضافه می نمایم")]
        private async Task When()
        {
            bookCategory = BookCategoryFactory.GenerateBookCategoryWithTitleDto(_context, "ScienceFiction");
            bookCategoryId = await sut.Add(bookCategory);
        }
        [Then("")]
        private void Then()
        {
            var expected = _context.BookCategories.Single(_ => _.Id == bookCategoryId);
            expected.Title.Should().Be("ScienceFiction");
        }
        [Fact]
        private void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then()
                );
        }
    }
}
