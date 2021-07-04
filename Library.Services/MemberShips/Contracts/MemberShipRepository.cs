using Library.Entites;
using System;

namespace Library.Services.MemberShips.Contracts
{
    public interface MemberShipRepository
    {
        void Add(MemberShip memberShip);
        DateTime GetBirthDateById(int Id);
        bool IsExistThisMemberShipById(int memberId);
    }
}
