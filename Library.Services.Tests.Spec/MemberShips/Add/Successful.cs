using FluentAssertions;
using Library.Persistance.EF;
using Library.Persistance.EF.MemberShips;
using Library.Services.MemberShips;
using Library.Services.MemberShips.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.MemberShips.Add
{
    [Scenario("ثبت یک عضو جدید به کتابخانه")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private MemberShipService _sut;
        private AddMemberShipDto _memberShipDto;
        private int _memberShipId;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            var unitOfWork = new EFUnitOfWorkRepository(_context);
            var repository = new EFMemberShipRepository(_context);
            _sut = new MemberShipAppService(repository, unitOfWork);

        }
        [Given("هیچ عضوی در کتابخانه وجود ندارد")]
        private void Given()
        {
        }
        [When("یک شخص با نام محمد غلامی و سن 22 سال (1999,01,01)و آدرس شیراز-اطلسی " +
            "به فهرست اعضای کتابخانه اضافه می کنم")]
        private async Task When()
        {
            _memberShipDto = new AddMemberShipDto
            {
                FullName = "Mohammad Gholami",
                BirthDate = new DateTime(1999, 01, 01),
                Address = "Shiraz-Atlasi"
            };
            _memberShipId = await _sut.Add(_memberShipDto);
        }
        [Then("باید تنها یک شخص با نام محمد غلامی و سن 22 سال (1999,01,01) و " +
            "آدرس شیراز-اطلسی به فهرست اعضای کتابخانه اضافه شده باشد")]
        private void Then()
        {
            var expected = _context.MemberShips.Single(_ => _.Id == _memberShipId);
            expected.FullName.Should().Be(_memberShipDto.FullName);
            expected.BirthDate.Should().Be(_memberShipDto.BirthDate);
            expected.Address.Should().Be(_memberShipDto.Address);
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
