using Application.Common;
using Application.Common.Exceptions;

namespace Application.Services
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameById(int userId);
        Task<int?> GetUserIdByUserName(string userName);

        Task<bool> IsInRoleAsync(int userId, string role);

        Task<bool> AuthorizeAsync(int userId, string policyName);

        Task<(Result Result, int UserId)> CreateUserAsync(string userName, string password, string firstName, string lastName, string email);

        Task<Result> ChangeRolesAsync(int userId, IEnumerable<string> roles);

        Task<bool> CheckPasswordAsync(int userId, string password);

        Task<Result> ChangePasswordAsync(int userId, string password);

        Task<string> GeneratePasswordResetTokenAsync(string userName);

        Task<Result> ResetPasswordAsync(string userName, string token, string password);

        Task<Result> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

        Task<IList<string>> GetUserRolesAsync(int userId);

        Task<Result> DeleteUserAsync(int userId);
    }
}
