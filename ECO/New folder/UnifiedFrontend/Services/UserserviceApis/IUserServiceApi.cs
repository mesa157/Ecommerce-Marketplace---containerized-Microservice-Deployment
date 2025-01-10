using UnifiedFrontend.Models.UserModel;

namespace UnifiedFrontend.Services.UserserviceApis
{
    public interface IUserServiceApi
    {
        Task<bool> RegisterUserAsync(object user);
        Task<string> LoginUserAsync(object credentials);
        Task<UserProfileDto> GetUserProfileAsync(Guid userId, string token);
        Task<bool> UpdateUserProfileAsync(Guid userId, UserProfileDto profile, string token);
        Task<bool> DeleteUserAccountAsync(Guid userId, string token);
    }
}
