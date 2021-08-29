using StoreManagement.Models;
using System.Threading.Tasks;

namespace StoreManagemnt.DataAccess
{
    public interface IUserRepository
    {
        Task<bool> Add(User user);
        Task<User> Login(string email, string password);
    }
}
