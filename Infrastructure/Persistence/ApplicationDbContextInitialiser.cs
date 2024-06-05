using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Configuration;
using Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly DataContext _context;
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<User> _userManager;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
            DataContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private void AddUserInfo(User userInfo)
        {
            _context.Users.Add(userInfo);
        }

        private async Task AddIdentityUserAsync(User userAccount, string password, string role)
        {
            await _userManager.CreateAsync(userAccount, password);
            await _userManager.AddToRolesAsync(userAccount, new[] { role });
        }

        private async Task AddRoleAsync(IdentityRole<int> role)
        {
            if (!_roleManager.Roles.Any(r => r.Name == role.Name))
            {
                await _roleManager.CreateAsync(role);
            }
        }

        private async Task TrySeedAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await AddRoleAsync(new IdentityRole<int>(RolesNames.Admin.Name));
                await AddRoleAsync(new IdentityRole<int>(RolesNames.SuperAdmin.Name));
                await AddRoleAsync(new IdentityRole<int>(RolesNames.User.Name));
            }

            if (!_userManager.Users.Any())
            {
                const string sharedPassword = "Test123.";
                await AddIdentityUserAsync(UserSeed.Admin, sharedPassword, Roles.Admin.ToString());
                await AddIdentityUserAsync(UserSeed.SuperAdmin, sharedPassword, Roles.SuperAdmin.ToString());
                await AddIdentityUserAsync(UserSeed.User, sharedPassword, Roles.User.ToString());
            }

            if (!_context.Users.Any())
            {
                AddUserInfo(UserSeed.Admin);
                AddUserInfo(UserSeed.User);
                AddUserInfo(UserSeed.SuperAdmin);
            }

            await _context.SaveChangesAsync();
        }
    }
}
