using Library.Entites;
using Library.Services.MemberShips.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistance.EF.MemberShips
{
   public  class EFMemberShipRepository : MemberShipRepository
    {
        private readonly EFDataContext _context;

        public EFMemberShipRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(MemberShip memberShip)
        {
            _context.MemberShips.Add(memberShip);
        }

        public DateTime GetBirthDateById(int Id)
        {
            return _context.MemberShips.Where(_ => _.Id == Id).Select(_ => _.BirthDate).FirstOrDefault();
        }

        public bool IsExistThisMemberShipById(int memberId)
        {
            return _context.MemberShips.FirstOrDefault(_ => _.Id == memberId) != null;
        }
    }
}
