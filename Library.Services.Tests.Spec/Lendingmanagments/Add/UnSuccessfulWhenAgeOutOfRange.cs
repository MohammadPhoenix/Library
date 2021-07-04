using FluentAssertions;
using Library.Persistance.EF;
using Library.Services.LendingManagments.Contracts;
using Library.Services.LendingManagments.Exceptions;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.BookCategories;
using Library.Test.Tools.Books;
using Library.Test.Tools.LendingManagments;
using Library.Test.Tools.MemberShips;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Lendingmanagments.Add
{
    [Scenario(" ثبت امانت دادن یک کتاب به یکی از اعضای کتابخانه با سن خارج از رده سنی کتاب ")]
    public class UnSuccessfulWhenAgeOutOfRange : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private LendingManagmentService _sut;
        private int _memberShipId;
        private short _bookCategoryId;
        private int _bookId;
        private AddLendingManagmentDto _dto;
        public UnSuccessfulWhenAgeOutOfRange(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _sut = LendingManagmentFactory.CreateService(_context);
        }
        [Given("یک عضو در کتابخانه با نام محمد زارع و سن 15 سال (2006,01,01)وجود دارد")]
        [And("و یک کتاب با عنوان سفر به اعماق زمین و رده سنی 16 تا 80 سال در دسته بندی علمی تخیلی وجود دارد")]
        private void Given()
        {
            _memberShipId = new MemberShipBuilder(_context)
                .WithFullName("Mohammad Zare")
                .WithBirthDate(new DateTime(2006, 01, 01))
                .Build();
            _bookCategoryId = BookCategoryFactory
                .AddBookCategoryWithTitle(_context, "ScienceFiction");
            _bookId = new BookBuilder(_context)
                .WithTitleAndBookCategoryId("TravelToDeepEarth", _bookCategoryId).Build();
        }
        [When("زمانی که یک کتاب با نام سفر به اعماق زمین با رده سنی 16 تا 80 سال" +
            " را به عضوی از کتابخانه با نام محمد زارع با سن 15 سال با تاریخ برگشت 2021,07,04 به امانت میسپاریم")]
        private void When()
        {
            _dto = LendingManagmentFactory
                .Generate_AddLendingManagmentDto(_memberShipId, _bookId, new DateTime(2021, 07, 04));

        }
        [Then("نباید هیچ امانتی به فهرست امانت ها اضافه گردد")]
        [And("خطای سن اعضا خارج از محدوده ی سنی کتاب است نمایش داده شود")]
        private void Then()
        {
            Func<Task> expedtedException = () => _sut.Add(_dto);
            expedtedException.Should().ThrowExactly<AgeOutOfRangeException>();

        }
        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When(),
                _ => Then());
        }
    }
}
