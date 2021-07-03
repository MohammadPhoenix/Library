using Library.Entites;
using Library.Services.BookCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistance.EF.BookCategories
{
   public class EFBookCategoryRepository : BookCategoryRepository
    {
        private readonly EFDataContext _context;

        public EFBookCategoryRepository(EFDataContext context)
        {
           _context = context;
        }

        public void Add(BookCategory bookCategory)
        {
            _context.BookCategories.Add(bookCategory);
        }

        public bool IsExistBookCategoryWithThisTitle(string title)
        {
            if (_context.BookCategories.FirstOrDefault(_ => _.Title == title) == null)
                return false;
            else
                return true;
        }
        public bool IsThisBookCategoryExist(short bookCategoryId)
        {
            return _context.BookCategories.FirstOrDefault(_ => _.Id == bookCategoryId) != null;
                

        }
    }
}
