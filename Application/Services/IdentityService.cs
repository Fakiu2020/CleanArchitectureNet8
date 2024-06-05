using Application.Common;
using Application.Common.Exceptions;
using Application.Manangers;
using CleanArchitecture.Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims; 


namespace Application.Services
{

    public class IdentityService : IIdentityService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly UserManager<User> _userManager;

        public IdentityService(
             UserManager<User> userManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<string?> GetUserNameById(int userId)
        {
            User? user = await _userManager.FindByIdAsync(userId.ToString());
            return user?.UserName;
        }

        public async Task<int?> GetUserIdByUserName(string userName)
        {
            User? user = await _userManager.FindByNameAsync(userName);
            return user?.Id;
        }

        public async Task<(Result Result, int UserId)> CreateUserAsync(string userName, string password, string firstName, string lastName, string email)
        {
            User user = new(userName,firstName,lastName,email);

            IdentityResult result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> ChangeRolesAsync(int userId, IEnumerable<string> roles)
        {
            User user = await GetUserByIdAsync(userId);
            IList<string> userRoles = await GetUserRolesAsync(userId);
            IdentityResult removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!removeResult.Succeeded)
                return removeResult.ToApplicationResult();

            IdentityResult addResult = await _userManager.AddToRolesAsync(user, roles);
            return addResult.ToApplicationResult();
        }

        public async Task<bool> CheckPasswordAsync(int userId, string password)
        {
            User user = await GetUserByIdAsync(userId);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<Result> ChangePasswordAsync(int userId, string password)
        {
            User user = await GetUserByIdAsync(userId);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, token, password);
            return result.ToApplicationResult();
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userName)
        {
            var user = await GetUserByEmailAsync(userName);
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<Result> ResetPasswordAsync(string userName, string token, string password)
        {
            var user = await GetUserByEmailAsync(userName);
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            return result.ToApplicationResult();
        }

        public async Task<Result> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            User user = await GetUserByIdAsync(userId);
            IdentityResult result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.ToApplicationResult();
        }

        public async Task<bool> IsInRoleAsync(int userId, string role)
        {
            User? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(int userId, string policyName)
        {
            User? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                return false;

            ClaimsPrincipal principal = await _userClaimsPrincipalFactory.CreateAsync(user);
            AuthorizationResult result = await _authorizationService.AuthorizeAsync(principal, policyName);
            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(int userId)
        {
            User user = await GetUserByIdAsync(userId);
            return await DeleteUserAsync(user);
        }

        public async Task<IList<string>> GetUserRolesAsync(int userId)
        {
            User user = await GetUserByIdAsync(userId);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<Result> DeleteUserAsync(User user)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            return result.ToApplicationResult();
        }

        private async Task<User> GetUserByIdAsync(int userId)
        {
            User? user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                throw new NotFoundException(ServiceError.UserNotFound.Message);

            return user;
        }

        private async Task<User> GetUserByEmailAsync(string userName)
        {
            User? user = await _userManager.FindByEmailAsync(userName);

            if (user is null)
                throw new NotFoundException(ServiceError.UserNotFound.Message);

            return user;
        }


       
    }

    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
