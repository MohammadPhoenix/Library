using System.Threading.Tasks;

namespace Library.Services.LendingManagments.Contracts
{
    public interface LendingManagmentService
    {
        Task<int> Add(AddLendingManagmentDto dto);
        Task Update(int lendingManagmentId);
    }
}
