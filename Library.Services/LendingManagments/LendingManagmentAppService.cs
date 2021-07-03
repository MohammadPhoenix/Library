using Library.Entites;
using Library.Services.Books.Contracts;
using Library.Services.LendingManagments.Contracts;
using Library.Services.LendingManagments.Exceptions;
using Library.Services.MemberShips.Contracts;
using System;
using System.Threading.Tasks;

namespace Library.Services.LendingManagments
{
    public class LendingManagmentAppService : LendingManagmentService
    {
        private readonly LendingManagmentRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly MemberShipRepository _memberRepository;
        private readonly BookRepository _bookRepository;

        public LendingManagmentAppService(LendingManagmentRepository repository, UnitOfWork unitOfWork, MemberShipRepository memberShipRepository, BookRepository bookRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _memberRepository = memberShipRepository;
            _bookRepository = bookRepository;
        }

        public async Task<int> Add(AddLendingManagmentDto dto)
        {
            GuardAgainstMemberShipNotFound(dto);
            GuardAgainstThisBookNotFound(dto);
            GuardAgainstAgeOutOfRange(dto);
            var lendingManagmen = new LendingManagment
            {
                MemberShipId = dto.MemberId,
                BookId = dto.BookId,
                AuthorizedDeliveryDate = dto.AuthorizedDeliveryDate
            };
            _repository.Add(lendingManagmen);
            await _unitOfWork.SaveComplete();
            return lendingManagmen.Id;
        }





        public async Task Update(int lendingManagmentId)
        {
            var lendingManagment = _repository.Find(lendingManagmentId);
            GuardAgainstLendingManagmentNotFound(lendingManagment);
            lendingManagment.DeliveryDate = DateTime.Now;
            await _unitOfWork.SaveComplete();
            GuardAgainstAuthorizedDeliveryIsOver(lendingManagment);
        }

        private void GuardAgainstLendingManagmentNotFound(LendingManagment lendingManagment)
        {
            if (lendingManagment == null)
                throw new ThisLendingManagmentNotFoundException();
        }
        private void GuardAgainstAgeOutOfRange(AddLendingManagmentDto dto)
        {
            var memberBirthDate = _memberRepository.GetBirthDateById(dto.MemberId);
            var book = _bookRepository.Find(dto.BookId);
            var minimumLegalAgeForUsingBook = book.MinimumAge;
            var maximumLegalAgeForUsingBook = book.MaximumAge;
            var memberAge = ((DateTime.UtcNow - memberBirthDate).TotalDays) / 365;
            if (memberAge < minimumLegalAgeForUsingBook || memberAge > maximumLegalAgeForUsingBook)
                throw new AgeOutOfRangeException();
        }
        private void GuardAgainstAuthorizedDeliveryIsOver(LendingManagment lendingManagment)
        {
            if (lendingManagment.AuthorizedDeliveryDate < DateTime.Now)
                throw new AuthorizedDeliveryDateIsOverException();
        }
        private void GuardAgainstMemberShipNotFound(AddLendingManagmentDto dto)
        {
            if (!_memberRepository.IsExistThisMemberShipById(dto.MemberId))
                throw new ThisMemberShipNotFoundException();
        }
        private void GuardAgainstThisBookNotFound(AddLendingManagmentDto dto)
        {
            if (_bookRepository.IsExistThisBookById(dto.BookId))
                throw new ThisBookNotFoundException();
        }
    }
}
