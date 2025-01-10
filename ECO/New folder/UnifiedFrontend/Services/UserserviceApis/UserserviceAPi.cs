using Newtonsoft.Json;
using System.Text;
using UnifiedFrontend.Models.UserModel;

namespace UnifiedFrontend.Services.UserserviceApis
{

    public class UserServiceApi : IUserServiceApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly BackendServiceOptions _options;

        public UserServiceApi(IHttpClientFactory httpClientFactory, BackendServiceOptions options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
        }

        public async Task<bool> RegisterUserAsync(object user)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_options.UserServiceUrl}/api/users/register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> LoginUserAsync(object credentials)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_options.UserServiceUrl}/api/users/login", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<UserProfileDto> GetUserProfileAsync(Guid userId, string token)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync($"{_options.UserServiceUrl}/api/users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserProfileDto>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<bool> UpdateUserProfileAsync(Guid userId, UserProfileDto profile, string token)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(profile), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_options.UserServiceUrl}/api/users/{userId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAccountAsync(Guid userId, string token)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"{_options.UserServiceUrl}/api/users/{userId}");
            return response.IsSuccessStatusCode;
        }
    }

    // DTO for User Profile
    
    }
