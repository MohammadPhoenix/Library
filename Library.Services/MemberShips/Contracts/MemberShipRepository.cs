using Library.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.MemberShips.Contracts
{
    public interface MemberShipRepository
    {
        void Add(MemberShip memberShip);
        DateTime GetBirthDateById(int Id);
        bool IsExistThisMemberShipById(int memberId);
    }
}
