using FluentAssertions;
using Library.Persistance.EF;
using Library.Services.Books.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.BookCategories;
using Library.Test.Tools.Books;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Books.Add
{
    [Scenario("ثبت یک کتاب جدید")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private BookService _sut;
        private short _bookCategoryId;
        private AddBookDto _bookDto;
        private int _bookId;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _sut = BookFactory.CreateService(_context);
        }
        [Given("یک دسته بندی با عنوان علمی تخیلی وجود دارد")]
        private void Given()
        {
            _bookCategoryId = BookCategoryFactory.AddBookCategoryWithTitle(_context, "ScienceFiction");
        }
        [When(" کتابی با عنوان سفر به اعماق زمین و نویسنده ژول ورن " +
            "در دسته بندی علمی تخیلی و با رده سنی 16 تا 80 سال اضافه می نمایم")]
        private async Task When()
        {
            _bookDto = new AddBookDto
            {
                Title = "TravelToDeppEarth",
                BookCategoryId = _bookCategoryId,
                Writer = "ZholVern",
                MaximumAge = 80,
                MinimumAge = 16
            };

            _bookId = await _sut.Add(_bookDto);
        }
        [Then("در فهرست کتاب ها باید تنها یک کتاب با عنوان سفر به اعماق زمین و" +
            " نویسنده ژول ورن در دسته بندی علمی تخیلی و با رده سنی 16 تا 80 سال وجود داشت باشد  ")]
        private void Then()
        {
            var expected = _context.Books.Single(c => c.Id == _bookId);
            expected.BookCategoryId.Should().Be(_bookDto.BookCategoryId);
            expected.Title.Should().Be(_bookDto.Title);
            expected.Writer.Should().Be(_bookDto.Writer);
            expected.MaximumAge.Should().Be(_bookDto.MaximumAge);
            expected.MinimumAge.Should().Be(_bookDto.MinimumAge);
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
