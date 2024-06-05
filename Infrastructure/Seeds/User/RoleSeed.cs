using Domain.Constants;
using Domain.Entities;

namespace Infrastructure.Seeds
{
    public static class RoleSeed
    {
        public static IList<Role> GetRoles()
        {
            return
            [
                new (RolesNames.Admin.Id,RolesNames.Admin.Name),
                new (RolesNames.SuperAdmin.Id, RolesNames.SuperAdmin.Name),
            ];
        }
    }
}
