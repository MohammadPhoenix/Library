using Library.Entites;
using Library.Services.BookCategories.Contracts;
using Library.Services.Books.Contracts;
using Library.Services.Books.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Books
{
    public class BookAppService : BookService
    {
        private readonly BookRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly BookCategoryRepository _categoryRepository;

        public BookAppService(BookRepository repository, UnitOfWork unitOfWork , BookCategoryRepository categoryRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<int> Add(AddBookDto dto)
        {
            GuardAgainstBookCategoryNotFound(dto);
            var book = new Book
            {
                Title = dto.Title,
                BookCategoryId = dto.BookCategoryId,
                Writer = dto.Writer,
                MaximumAge = dto.MaximumAge,
                MinimumAge = dto.MinimumAge
            };
            _repository.Add(book);
            await _unitOfWork.SaveComplete();
            return book.Id;

        }

        private void GuardAgainstBookCategoryNotFound(AddBookDto dto)
        {
            if (!_categoryRepository.IsThisBookCategoryExist(dto.BookCategoryId))
                throw new ThisBookCategoryNotFoundException();
        }

        public async Task<List<GetBookDto>> GetAllBooksInThisCategory(short bookCategoryId)
        {
            return await _repository.GetAllBooksWithThisCategory(bookCategoryId);
        }

        //public async Task<List<GetBookDto>> GetAllBooksInThisCategory(short bookCategoryId)
        //{
        //    var booksInThisCategory = await _repository.GetAllBooksWithThisCategory(bookCategoryId);
        //    List<GetBookDto> list = new List<GetBookDto>();
        //    foreach (var item in booksInThisCategory)
        //    {
        //        list.Add(new GetBookDto
        //        {
        //            Title = item.Title,
        //            Writer = item.Writer
        //        });
        //    }
        //    return list;

        //}

        public async Task Update(int bookId, UpdateBookDto dto)
        {
            var book = _repository.Find(bookId);
            book.Title = dto.Title;
            book.Writer = dto.Writer;
            book.BookCategoryId = dto.BookCategoryId;
            book.MaximumAge = dto.MaximumAge;
            book.MinimumAge = dto.MinimumAge;
            await _unitOfWork.SaveComplete();

        }


    }
}
