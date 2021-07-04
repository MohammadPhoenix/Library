using Library.Services.MemberShips.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.RestApi.Controllers
{

    [Route("api/[controller]")]
    public class MemberShipController : Controller
    {
        private readonly MemberShipService _service;

        public MemberShipController(MemberShipService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Post(AddMemberShipDto dto)
        {
            await _service.Add(dto);
        }
    }
}
