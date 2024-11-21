using System.Threading.Tasks;
using UserService.Model;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}
