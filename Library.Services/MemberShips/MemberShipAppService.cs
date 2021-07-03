using Library.Entites;
using Library.Services.MemberShips.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.MemberShips
{
    public class MemberShipAppService : MemberShipService
    {
        private readonly MemberShipRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public MemberShipAppService(MemberShipRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(AddMemberShipDto dto)
        {
            var memberShip = new MemberShip
            {
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                Address = dto.Address
            };
            _repository.Add(memberShip);
            await _unitOfWork.SaveComplete();
            return memberShip.Id;
        }
    }
}
