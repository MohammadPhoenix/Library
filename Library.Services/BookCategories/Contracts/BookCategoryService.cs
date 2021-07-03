using Library.Entites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.BookCategories.Contracts
{
    public interface BookCategoryService
    {
        Task<short> Add(AddBookCategoryDto dto);
    }
}
