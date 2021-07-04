using Library.Entites;

namespace Library.Services.LendingManagments.Contracts
{
    public interface LendingManagmentRepository
    {
        void Add(LendingManagment lendingManagmen);
        LendingManagment Find(int lendingManagmentId);
    }
}
