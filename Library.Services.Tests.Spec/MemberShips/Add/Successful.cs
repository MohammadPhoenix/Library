using FluentAssertions;
using Library.Persistance.EF;
using Library.Services.MemberShips;
using Library.Services.MemberShips.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.MemberShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.MemberShips.Add
{
    [Scenario("ثبت یک عضو جدید به کتابخانه")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext _context;
        private MemberShipService sut;
        private AddMemberShipDto _memberShipdto;
        private int _memberShipId;

        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            _context = CreateDataContext();
            sut = MemberShipFactory.CreateService(_context);
        }
        [Given("هیچ عضوی در کتابخانه وجود ندارد")]
        private void Given()
        {
        }
        [When("یک شخص با نام محمد غلامی و سن 22 سال (1999,01,01)و آدرس شیراز-اطلسی " +
            "به فهرست اعضای کتابخانه اضافه می کنم")]
        private async Task When()
        {
            _memberShipdto = new AddMemberShipDto
            {
                FullName = "Mohammad Gholami",
                BirthDate = new DateTime(1999, 01, 01),
                Address = "Shiraz-Atlasi"
            };
            _memberShipId = await sut.Add(_memberShipdto);
        }
        [Then("باید تنها یک شخص با نام محمد غلامی و سن 22 سال (1999,01,01) و " +
            "آدرس شیراز-اطلسی به فهرست اعضای کتابخانه اضافه شده باشد")]
        private void Then()
        {
            var expected = _context.MemberShips.Single(_=>_.Id==_memberShipId);
            expected.FullName.Should().Be(_memberShipdto.FullName);
            expected.BirthDate.Should().Be(_memberShipdto.BirthDate);
            expected.Address.Should().Be(_memberShipdto.Address);
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
