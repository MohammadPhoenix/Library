using FluentAssertions;
using Library.Persistance.EF;
using Library.Services.LendingManagments;
using Library.Services.LendingManagments.Contracts;
using Library.Services.LendingManagments.Exceptions;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.BookCategories;
using Library.Test.Tools.Books;
using Library.Test.Tools.LendingManagments;
using Library.Test.Tools.MemberShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Lendingmanagments.Update
{
    [Scenario("ثبت تاریخ تحویل یک کتاب امانت داده شده به یکی از اعضای کتابخانه بعد از تاریخ برگشت")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private readonly LendingManagmentService _sut;
        private int _memberShipId;
        private short _bookCategoryId;
        private int _bookId;
        private int _lendingManagmentId;
        private Func<Task> _expected;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            _sut = LendingManagmentFactory.CreateService(_context);
        }
        [Given(" تنها یک امانت داده شده مربوط به کتابی با عنوان سفر به اعماق زمین در دسته بندی علمی تخیلی " +
            "به یک عضو کتابخانه با نام محمد غلامی با تاریخ برگشت 2021,07,04 وجود دارد ")]
        private void Given()
        {
            _memberShipId = new MemberShipBuilder(_context)
                   .WithFullName("Mohammad Gholami")
                   .Build();
            _bookCategoryId = BookCategoryFactory.AddBookCategoryWithTitle(_context, "ScienceFiction");
            _bookId = new BookBuilder(_context)
                .WithTitleAndBookCategoryId("TravelToDeepEarth", _bookCategoryId)
                .Build();
            _lendingManagmentId = LendingManagmentFactory.AddLendingManagment(_memberShipId , _bookId ,new DateTime(2021,07,02) );
        }
        [When("زمانی که کتاب با عنوان سفر به اعماق زمینتوسط عضوی با نام محمد غلامی در تاریخ 2021,07,04 برگشت داده شود")]
        private void When()
        {
             _expected = ()=>  _sut.Update(_lendingManagmentId);
            
        }
        [Then("تنها یک امانت داده شده مربوط به کتاب با عنوان سفر به اعماق زمین به یک عضو" +
            " کتابخانه با نام محمد غلامی با تاریخ برگشت 2021,07,04 و تاریخ تحویل 2021,07,05  وجود داشته باشد")]
        private void Then()
        {
            _expected.Should().ThrowExactly<AuthorizedDeliveryDateIsOverException>();
            var expectedAfterUpdate = _context.LendingManagments.Single(_=>_.Id == _lendingManagmentId);
            expectedAfterUpdate.MemberShipId.Should().Be(_memberShipId);
            expectedAfterUpdate.BookId.Should().Be(_bookId);
            expectedAfterUpdate.AuthorizedDeliveryDate = new DateTime(2021, 07, 02);
            expectedAfterUpdate.DeliveryDate.Should().NotBeNull();
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
