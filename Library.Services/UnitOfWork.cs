using System.Threading.Tasks;

namespace Library.Services
{
    public interface UnitOfWork
    {
        Task SaveComplete();
    }
}
