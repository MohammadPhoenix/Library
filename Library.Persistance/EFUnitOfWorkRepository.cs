using Library.Services;
using System;
using System.Threading.Tasks;

namespace Library.Persistance.EF
{
    public class EFUnitOfWorkRepository : UnitOfWork
    {
        private readonly EFDataContext _context;

        public EFUnitOfWorkRepository(EFDataContext context)
        {
            _context = context;
        }
        public async Task SaveComplete()
        {
           await _context.SaveChangesAsync();
        }
    }
}
