using Library.Entites;
using Library.Persistance.EF;
using System;

namespace Library.Test.Tools.MemberShips
{
    public class MemberShipBuilder
    {
        private readonly EFDataContext _context;

        public MemberShipBuilder(EFDataContext context)
        {
            _context = context;
        }
        private MemberShip _memberShip = new MemberShip
        {
            FullName = "Mohammad Gholami",
            BirthDate = new DateTime(1999, 01, 01),
            Address = "Shiraz-Atlasi"
        };
        public MemberShipBuilder WithFullName(string fullName)
        {
            _memberShip.FullName = fullName;
            return this;
        }
        public MemberShipBuilder WithBirthDate(DateTime birthDate)
        {
            _memberShip.BirthDate = birthDate;
            return this;
        }
        public MemberShipBuilder WithAddress(string address)
        {
            _memberShip.Address = address;
            return this;
        }
        public int Build()
        {
            _context.MemberShips.Add(_memberShip);
            _context.SaveChanges();
            return _memberShip.Id;
        }
    }
}
