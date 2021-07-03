using Library.Entites;
using Library.Services.LendingManagments.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Persistance.EF.LendingManagments
{
    public class EFLendingManagmentRepository : LendingManagmentRepository
    {
        private readonly EFDataContext _context;

        public EFLendingManagmentRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(LendingManagment lendingManagmen)
        {
            _context.LendingManagments.Add(lendingManagmen);
        }

        public LendingManagment Find(int lendingManagmentId)
        {
            return _context.LendingManagments.FirstOrDefault(_ => _.Id == lendingManagmentId);
        }
    }
}
