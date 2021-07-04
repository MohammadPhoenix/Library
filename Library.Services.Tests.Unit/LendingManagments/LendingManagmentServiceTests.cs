using FluentAssertions;
using Library.Infrastructure.Test;
using Library.Persistance.EF;
using Library.Services.LendingManagments;
using Library.Services.LendingManagments.Contracts;
using Library.Services.LendingManagments.Exceptions;
using Library.Test.Tools.BookCategories;
using Library.Test.Tools.Books;
using Library.Test.Tools.LendingManagments;
using Library.Test.Tools.MemberShips;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.LendingManagments
{
    public class LendingManagmentServiceTests
    {
        private readonly EFDataContext _context;
        private readonly LendingManagmentService _sut;
        public LendingManagmentServiceTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _sut = LendingManagmentFactory.CreateService(_context);
        }
        [Fact]
        public void Update_throw_exception_when_lendingManagment_not_found()
        {
            var bookCategoryId = BookCategoryFactory.AddBookCategoryWithTitle(_context);
            var bookId = new BookBuilder(_context)
                .WithTitleAndBookCategoryId(categoryId: bookCategoryId)
                .Build();
            var memberShipId = new MemberShipBuilder(_context).Build();
            var lendingId = LendingManagmentFactory.AddLendingManagment(memberShipId, bookId, new DateTime(2021, 01, 01));

            Func<Task> expected = () => _sut.Update(-1);

            expected.Should().ThrowExactly<ThisLendingManagmentNotFoundException>();
        }
        [Fact]
        public void Add_throw_exception_when_memberShip_not_found()
        {
            var bookCategoryId = BookCategoryFactory.AddBookCategoryWithTitle(_context);
            var bookId = new BookBuilder(_context)
                .WithTitleAndBookCategoryId(categoryId: bookCategoryId)
                .Build();

            var lendingdto = LendingManagmentFactory.Generate_AddLendingManagmentDto(-1,bookId,new DateTime(1999,01,01));
            Func<Task> expected = () => _sut.Add(lendingdto);

            expected.Should().ThrowExactly<ThisMemberShipNotFoundException>();
        }
        [Fact]
        public void Add_throw_exception_when_book_not_found () {

            var memberShipId = new MemberShipBuilder(_context).Build();
            var lendingdto = LendingManagmentFactory.Generate_AddLendingManagmentDto(memberShipId, -1 , new DateTime(1999, 01, 01));

            Func<Task> expected = () => _sut.Add(lendingdto);

            expected.Should().ThrowExactly<ThisBookNotFoundException>();
        }
    }
}
