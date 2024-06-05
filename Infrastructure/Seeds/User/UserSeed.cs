using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds
{
    public static class UserSeed
    {
        public static readonly User Admin = new( UsersNames.Admin.UserName, UsersNames.Admin.FirstName, UsersNames.Admin.LastName, UsersNames.Admin.Email);
        public static readonly User SuperAdmin = new( UsersNames.SuperAdmin.UserName, UsersNames.SuperAdmin.FirstName, UsersNames.SuperAdmin.LastName, UsersNames.SuperAdmin.Email);
        public static readonly User User = new(UsersNames.User.UserName, UsersNames.User.FirstName, UsersNames.User.LastName, UsersNames.User.Email);
       
    }
}
