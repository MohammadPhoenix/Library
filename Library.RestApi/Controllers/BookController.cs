using Library.Services.Books.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.RestApi.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly BookService _service;

        public BookController(BookService service)
        {
            _service = service;
        }
        [HttpGet()]
        public async Task<List<GetBookDto>> Get(short bookCategoryId)
        {
            return await _service.GetAllBooksInThisCategory(bookCategoryId);
        }
        [HttpPost()]
        public async Task Post(AddBookDto dto)
        {
            await _service.Add(dto);
        }
        [HttpPut("{bookId}")]
        public async Task Put(int bookId, UpdateBookDto dto)
        {
            await _service.Update(bookId, dto);
        }
    }
}
