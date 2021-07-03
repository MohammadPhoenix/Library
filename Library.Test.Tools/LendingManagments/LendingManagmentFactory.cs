using Library.Entites;
using Library.Persistance.EF;
using Library.Persistance.EF.Books;
using Library.Persistance.EF.LendingManagments;
using Library.Persistance.EF.MemberShips;
using Library.Services.LendingManagments;
using Library.Services.LendingManagments.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Test.Tools.LendingManagments
{
    public class LendingManagmentFactory
    {
        private static EFDataContext _context;

        public static LendingManagmentAppService CreateService(EFDataContext context)
        {
            _context = context;
            var unitOfWork = new EFUnitOfWorkRepository(context);
            var repository = new EFLendingManagmentRepository(context);
            var memberRepisitory = new EFMemberShipRepository(context);
            var bookRepository = new EFBookRepository(context);

            return new LendingManagmentAppService(repository, unitOfWork, memberRepisitory, bookRepository);
        }
        public static AddLendingManagmentDto Generate_AddLendingManagmentDto(int memberId , int bookId , DateTime authorizedDeliveryDate)
        {
            return new AddLendingManagmentDto
            {
                MemberId = memberId,
                BookId = bookId,
                AuthorizedDeliveryDate = authorizedDeliveryDate
            };
        }

        public   static int AddLendingManagment(int memberShipId, int bookId, DateTime dateTime)
        {
           var lendingManagment  =  new LendingManagment
            {
                MemberShipId = memberShipId,
                BookId = bookId,
                AuthorizedDeliveryDate = dateTime,
                
            };
            _context.LendingManagments.Add(lendingManagment);
            _context.SaveChanges();
            return lendingManagment.Id;
        }
    }
}
