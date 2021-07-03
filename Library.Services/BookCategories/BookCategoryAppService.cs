using Library.Entites;
using Library.Services.BookCategories.Contracts;
using Library.Services.BookCategories.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.BookCategories
{
    public class BookCategoryAppService : BookCategoryService
    {
        private readonly BookCategoryRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public BookCategoryAppService(BookCategoryRepository repository ,UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<short> Add(AddBookCategoryDto dto)
        {
            GuardAgainstBookCategoryIsExist(dto);
            var bookCategory = new BookCategory { Title = dto.Title };
            _repository.Add(bookCategory);
            await _unitOfWork.SaveComplete();
            return bookCategory.Id;
        }

        private void GuardAgainstBookCategoryIsExist(AddBookCategoryDto dto)
        {
            if (_repository.IsExistBookCategoryWithThisTitle(dto.Title))
            {
                throw new ThisBookCategoryIsExistException();
            }
        }

    }
}
