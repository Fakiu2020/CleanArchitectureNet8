using Domain.Constants;
using Domain.Entities;

namespace Infrastructure.Seeds
{
    public static class UserRoleSeed
    {
        public static IList<UserRole> GetUserRoles()
        {
            return
            [
                new (UsersNames.Admin.Id,RolesNames.Admin.Id),
                new (UsersNames.SuperAdmin.Id,RolesNames.SuperAdmin.Id),
            ];
        }
    }
}
