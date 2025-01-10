using System.Collections.Generic;
using System.Threading.Tasks;
using FrontendService.Service.userService.Model;

namespace FrontendService.Service.userService
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
