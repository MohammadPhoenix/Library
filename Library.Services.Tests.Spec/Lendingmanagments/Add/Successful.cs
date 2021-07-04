using FluentAssertions;
using Library.Persistance.EF;
using Library.Services.LendingManagments.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.BookCategories;
using Library.Test.Tools.Books;
using Library.Test.Tools.LendingManagments;
using Library.Test.Tools.MemberShips;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Lendingmanagments.Add
{
    [Scenario(" ثبت امانت دادن یک کتاب به یکی از اعضای کتابخانه ")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly LendingManagmentService _sut;
        private int _memberShipId;
        private short _bookCategoryId;
        private int _bookId;
        private int _lendingManagmentId;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _sut = LendingManagmentFactory.CreateService(_context);
        }
        [Given(" یک عضو کتابخانه با نام محمد غلامی و سن 22 سال (1999,01,01)و آدرس شیراز-اطلسی وجود دارد")]
        [And("یک کتاب با عنوان سفر به اعماق زمین ورده ی سنی 16 تا 88 سال و دسته بندی علمی تخیلی وجود دارد")]
        private void Given()
        {
            _memberShipId = new MemberShipBuilder(_context)
                .WithFullName("Mohammad Gholami")
                .WithBirthDate(new DateTime(1999, 01, 01))
                .WithAddress("Shiraz-Atlasi")
                .Build();

            _bookCategoryId = BookCategoryFactory.AddBookCategoryWithTitle(_context, "ScienceFiction");

            _bookId = new BookBuilder(_context)
                .WithTitleAndBookCategoryId("TravelToDeepEarth", _bookCategoryId)
                .Build();

        }
        [When("یک کتاب با عنوان سفر به اعماق زمین و رده سنی 16 تا 88 سال به عضوی" +
            " از کتابخانه با نام محمد غلامی و سن 22 سال و آدرس شیراز-اطلسی با تاریخ برگشت 2021,07,04 به امانت سپرده می شود")]
        private async Task When()
        {
            var dto = LendingManagmentFactory
                .Generate_AddLendingManagmentDto(_memberShipId, _bookId, new DateTime(2021, 07, 04));

            _lendingManagmentId = await _sut.Add(dto);
        }
        [Then("در فهرست امانت ها باید تنها یک کتاب با عنوان سفر به اعماق زمین و رده سنی 16 تا 88 سال" +
            " به عضوی از کتاب خانه با نام محمد غلامی و 22 سال با تاریخ برگشت 2021,07,04 به امانت سپرده شده باشد")]
        private void Then()
        {
            var expected = _context.LendingManagments.Single(_ => _.Id == _lendingManagmentId);
            expected.MemberShipId.Should().Be(_memberShipId);
            expected.BookId.Should().Be(_bookId);
            expected.AuthorizedDeliveryDate.Should().Be(new DateTime(2021, 07, 04));
            expected.DeliveryDate.Should().BeNull();
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
