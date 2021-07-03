using Library.Services.LendingManagments.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [Route("api/[controller]/LendingManagment")]
    public class LendingManagmentController : Controller
    {
        private readonly LendingManagmentService _service;

        public LendingManagmentController(LendingManagmentService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Post(AddLendingManagmentDto dto)
        {
            await _service.Add(dto);
        }
        [HttpPut("{lendingManagmentId}")]
        public async Task Put(int lendingManagmentId)
        {
            await _service.Update(lendingManagmentId);
        }
    }
}
