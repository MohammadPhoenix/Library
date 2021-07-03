using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.BookCategories.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace Library.RestApi.Controllers
{
    [Route("api/[controller]/BookCategory")]
    public class BookCategoryController : Controller
    {
        private readonly BookCategoryService _service;

        public BookCategoryController(BookCategoryService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Post(AddBookCategoryDto dto)
        {
           await _service.Add(dto);
        }
    }
}
