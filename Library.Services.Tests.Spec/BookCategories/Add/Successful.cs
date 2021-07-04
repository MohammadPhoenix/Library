using FluentAssertions;
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
        private BookCategoryService _sut;
        private AddBookCategoryDto _bookCategory;
        private short _bookCategoryId;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _sut = BookCategoryFactory.CreateService(_context);
        }
        [Given("هیچ دسته بندی ای وجود ندارد")]
        private void Given()
        {
        }
        [When("یک دسته بندی با عنوان علمی تخیلی اضافه می نمایم")]
        private async Task When()
        {
            _bookCategory = BookCategoryFactory.GenerateBookCategoryWithTitleDto(_context, "ScienceFiction");
            _bookCategoryId = await _sut.Add(_bookCategory);
        }
        [Then("")]
        private void Then()
        {
            var expected = _context.BookCategories.Single(_ => _.Id == _bookCategoryId);
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
