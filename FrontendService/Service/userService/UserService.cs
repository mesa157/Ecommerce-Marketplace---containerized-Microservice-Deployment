using System.Collections.Generic;
using System.Threading.Tasks;
using FrontendService.Models.User;

namespace FrontendService.Service.userService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["UserServiceApi:BaseUrl"];
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"{_apiBaseUrl}/api/User/{id}");
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _httpClient.GetFromJsonAsync<User>($"{_apiBaseUrl}/api/User/username/{username}");
        }

        public async Task AddUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/User", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>($"{_apiBaseUrl}/api/User");
        }
    }
}
