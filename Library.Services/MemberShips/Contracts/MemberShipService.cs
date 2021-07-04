using System.Threading.Tasks;

namespace Library.Services.MemberShips.Contracts
{
    public interface MemberShipService
    {
        Task<int> Add(AddMemberShipDto memberShip);
    }
}
